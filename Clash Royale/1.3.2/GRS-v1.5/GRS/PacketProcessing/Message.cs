using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CRS.Helpers;
using CRS.Logic;
using CRS.Utilities.Blake2b;
using CRS.Utilities.CustomNaCl;
using CRS.PacketProcessing;

namespace CRS.PacketProcessing
{
	// Token: 0x0200006E RID: 110
	internal class Message
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000182A7 File Offset: 0x000164A7
		public Message(Device c)
		{
			this.Client = c;
			this.m_vType = 0;
			this.m_vLength = -1;
			this.m_vMessageVersion = 0;
			this.m_vData = null;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000182D4 File Offset: 0x000164D4
		public Message(Device c, BinaryReader br)
		{
			this.Client = c;
			this.m_vType = br.ReadUInt16WithEndian();
			byte[] array = br.ReadBytes(3);
			this.m_vLength = 0 | ((int)array[0] << 16) | ((int)array[1] << 8) | (int)array[2];
			this.m_vMessageVersion = br.ReadUInt16WithEndian();
			this.m_vData = br.ReadBytes(this.m_vLength);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00018338 File Offset: 0x00016538
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00018340 File Offset: 0x00016540
		public int Broadcasting { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00018349 File Offset: 0x00016549
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00018351 File Offset: 0x00016551
		public Device Client { get; set; }

		// Token: 0x0600033F RID: 831 RVA: 0x000123B6 File Offset: 0x000105B6
		public virtual void Decode()
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001835C File Offset: 0x0001655C
		public void Decrypt()
		{
			try
			{
				if (this.m_vType == 10101)
				{
					byte[] vData = this.m_vData;
					this.Client.CPublicKey = vData.Take(32).ToArray<byte>();
					Message.Blake.Init();
					Message.Blake.Update(this.Client.CPublicKey);
					Message.Blake.Update(Key.Crypto.PublicKey);
					byte[] array = Message.Blake.Finish();
					this.Client.CRNonce = array;
					byte[] array2 = CustomNaCl.OpenPublicBox(vData.Skip(32).ToArray<byte>(), array, Key.Crypto.PrivateKey, this.Client.CPublicKey);
					this.Client.CSharedKey = this.Client.CPublicKey;
					this.Client.CSessionKey = array2.Take(24).ToArray<byte>();
					this.Client.CSNonce = array2.Skip(24).Take(24).ToArray<byte>();
					this.SetData(array2.Skip(48).ToArray<byte>());
				}
				else if (this.m_vType != 10100)
				{
					this.Client.CSNonce.Increment();
					this.SetData(CustomNaCl.OpenSecretBox(new byte[16].Concat(this.m_vData).ToArray<byte>(), this.Client.CSNonce, this.Client.CSharedKey));
				}
			}
			catch (Exception)
			{
				this.Client.CState = 0;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000123B6 File Offset: 0x000105B6
		public virtual void Encode()
		{
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000184F0 File Offset: 0x000166F0
		public void Encrypt(byte[] plainText)
		{
			try
			{
				if (this.GetMessageType() == 20104 || this.GetMessageType() == 20103)
				{
					Message.Blake.Init();
					Message.Blake.Update(this.Client.CSNonce);
					Message.Blake.Update(this.Client.CPublicKey);
					Message.Blake.Update(Key.Crypto.PublicKey);
					byte[] array = Message.Blake.Finish();
					plainText = this.Client.CRNonce.Concat(this.Client.CSharedKey).Concat(plainText).ToArray<byte>();
					this.SetData(CustomNaCl.CreatePublicBox(plainText, array, Key.Crypto.PrivateKey, this.Client.CPublicKey));
					if (this.GetMessageType() == 20104)
					{
						this.Client.CState = 2;
					}
				}
				else
				{
					this.Client.CRNonce.Increment();
					this.SetData(CustomNaCl.CreateSecretBox(plainText, this.Client.CRNonce, this.Client.CSharedKey).Skip(16).ToArray<byte>());
				}
			}
			catch (Exception)
			{
				this.Client.CState = 0;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00018640 File Offset: 0x00016840
		public byte[] GetData()
		{
			return this.m_vData;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00018648 File Offset: 0x00016848
		public int GetLength()
		{
			return this.m_vLength;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00018650 File Offset: 0x00016850
		public ushort GetMessageType()
		{
			return this.m_vType;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00018658 File Offset: 0x00016858
		public ushort GetMessageVersion()
		{
			return this.m_vMessageVersion;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00018660 File Offset: 0x00016860
		public byte[] GetRawData()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.m_vType).Reverse<byte>());
			list.AddRange(BitConverter.GetBytes(this.m_vLength).Reverse<byte>().Skip(1));
			list.AddRange(BitConverter.GetBytes(this.m_vMessageVersion).Reverse<byte>());
			list.AddRange(this.m_vData);
			return list.ToArray();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000123B6 File Offset: 0x000105B6
		public virtual void Process(Level level)
		{
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000186CB File Offset: 0x000168CB
		public void SetData(byte[] data)
		{
			this.m_vData = data;
			this.m_vLength = data.Length;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000186DD File Offset: 0x000168DD
		public void SetMessageType(ushort type)
		{
			this.m_vType = type;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000186E6 File Offset: 0x000168E6
		public void SetMessageVersion(ushort v)
		{
			this.m_vMessageVersion = v;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000186F0 File Offset: 0x000168F0
		public static byte[] AddVInt(int v2)
		{
			MemoryStream memoryStream = new MemoryStream(5);
			if (v2 <= -1)
			{
				if (v2 + 63 < 0)
				{
					memoryStream.WriteByte((byte)((v2 & 63) | 64));
					return memoryStream.ToArray();
				}
				if (v2 >= -8191)
				{
					memoryStream.WriteByte((byte)(v2 | 192));
					v2 >>= 6;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 >= -1048575)
				{
					memoryStream.WriteByte((byte)(v2 | 192));
					memoryStream.WriteByte((byte)((v2 >> 6) | 128));
					v2 >>= 13;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 | 192));
				memoryStream.WriteByte((byte)((v2 >> 6) | 128));
				memoryStream.WriteByte((byte)((v2 >> 13) | 128));
				v2 >>= 20;
				if (v2 <= -134217728)
				{
					memoryStream.WriteByte((byte)(v2 | 128));
					v2 >>= 11;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 & 127));
				return memoryStream.ToArray();
			}
			else
			{
				if (v2 <= 63)
				{
					v2 &= 63;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 < 8192)
				{
					memoryStream.WriteByte((byte)((v2 & 63) | 128));
					v2 >>= 6;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 < 1048576)
				{
					memoryStream.WriteByte((byte)((v2 & 63) | 128));
					memoryStream.WriteByte((byte)((v2 >> 6) | 128));
					v2 >>= 13;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)((v2 & 63) | 128));
				memoryStream.WriteByte((byte)((v2 >> 6) | 128));
				memoryStream.WriteByte((byte)((v2 >> 13) | 128));
				v2 >>= 20;
				if (v2 >= 134217728)
				{
					memoryStream.WriteByte((byte)(v2 | 128));
					v2 >>= 11;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 & 127));
				return memoryStream.ToArray();
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000188FC File Offset: 0x00016AFC
		public static byte[] AddVInt(long v2)
		{
			MemoryStream memoryStream = new MemoryStream(5);
			if (v2 <= -1L)
			{
				if (v2 + 63L < 0L)
				{
					memoryStream.WriteByte((byte)((v2 & 63L) | 64L));
					return memoryStream.ToArray();
				}
				if (v2 >= -8191L)
				{
					memoryStream.WriteByte((byte)(v2 | 192L));
					v2 >>= 6;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 >= -1048575L)
				{
					memoryStream.WriteByte((byte)(v2 | 192L));
					memoryStream.WriteByte((byte)((v2 >> 6) | 128L));
					v2 >>= 13;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 | 192L));
				memoryStream.WriteByte((byte)((v2 >> 6) | 128L));
				memoryStream.WriteByte((byte)((v2 >> 13) | 128L));
				v2 >>= 20;
				if (v2 <= -134217728L)
				{
					memoryStream.WriteByte((byte)(v2 | 128L));
					v2 >>= 11;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 & 127L));
				return memoryStream.ToArray();
			}
			else
			{
				if (v2 <= 63L)
				{
					v2 &= 63L;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 < 8192L)
				{
					memoryStream.WriteByte((byte)((v2 & 63L) | 128L));
					v2 >>= 6;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				if (v2 < 1048576L)
				{
					memoryStream.WriteByte((byte)((v2 & 63L) | 128L));
					memoryStream.WriteByte((byte)((v2 >> 6) | 128L));
					v2 >>= 13;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)((v2 & 63L) | 128L));
				memoryStream.WriteByte((byte)((v2 >> 6) | 128L));
				memoryStream.WriteByte((byte)((v2 >> 13) | 128L));
				v2 >>= 20;
				if (v2 >= 134217728L)
				{
					memoryStream.WriteByte((byte)(v2 | 128L));
					v2 >>= 11;
					memoryStream.WriteByte((byte)v2);
					return memoryStream.ToArray();
				}
				memoryStream.WriteByte((byte)(v2 & 127L));
				return memoryStream.ToArray();
			}
		}

		// Token: 0x040002B9 RID: 697
		private byte[] m_vData;

		// Token: 0x040002BA RID: 698
		private int m_vLength;

		// Token: 0x040002BB RID: 699
		private ushort m_vMessageVersion;

		// Token: 0x040002BC RID: 700
		private ushort m_vType;

		// Token: 0x040002BD RID: 701
		private static readonly Hasher Blake = Blake2B.Create(new Blake2BConfig
		{
			OutputSizeInBytes = 24
		});
	}
}
