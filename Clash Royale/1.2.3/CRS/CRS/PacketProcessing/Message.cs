using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UCS.Helpers;
using UCS.Logic;
using UCS.Utilities.Sodium;

namespace UCS.PacketProcessing
{
	// Token: 0x02000090 RID: 144
	internal class Message
	{
		// Token: 0x060003CF RID: 975 RVA: 0x000073DF File Offset: 0x000055DF
		public Message()
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000127A6 File Offset: 0x000109A6
		public Message(Client c)
		{
			this.Client = c;
			this.m_vType = 0;
			this.m_vLength = -1;
			this.m_vMessageVersion = 0;
			this.m_vData = null;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000127D4 File Offset: 0x000109D4
		public Message(Client c, BinaryReader br)
		{
			this.Client = c;
			this.m_vType = br.ReadUInt16WithEndian();
			byte[] array = br.ReadBytes(3);
			this.m_vLength = 0 | ((int)array[0] << 16) | ((int)array[1] << 8) | (int)array[2];
			this.m_vMessageVersion = br.ReadUInt16WithEndian();
			this.m_vData = br.ReadBytes(this.m_vLength);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00012838 File Offset: 0x00010A38
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00012840 File Offset: 0x00010A40
		public int Broadcasting { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00012849 File Offset: 0x00010A49
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00012851 File Offset: 0x00010A51
		public Client Client { get; set; }

		// Token: 0x060003D6 RID: 982 RVA: 0x0001285C File Offset: 0x00010A5C
		public void Encrypt(byte[] plainText)
		{
			try
			{
				if (this.GetMessageType() == 20103)
				{
					byte[] array = GenericHash.Hash(this.Client.CSNonce.Concat(this.Client.CPublicKey).Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
					plainText = this.Client.CRNonce.Concat(this.Client.CSharedKey).Concat(plainText).ToArray<byte>();
					this.SetData(PublicKeyBox.Create(plainText, array, Key.Crypto.PrivateKey, this.Client.CPublicKey));
				}
				else if (this.GetMessageType() == 20104)
				{
					byte[] array2 = GenericHash.Hash(this.Client.CSNonce.Concat(this.Client.CPublicKey).Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
					plainText = this.Client.CRNonce.Concat(this.Client.CSharedKey).Concat(plainText).ToArray<byte>();
					this.SetData(PublicKeyBox.Create(plainText, array2, Key.Crypto.PrivateKey, this.Client.CPublicKey));
					this.Client.CState = 2;
				}
				else
				{
					this.Client.CRNonce = UCS.Utilities.Sodium.Utilities.Increment(UCS.Utilities.Sodium.Utilities.Increment(this.Client.CRNonce));
					this.SetData(SecretBox.Create(plainText, this.Client.CRNonce, this.Client.CSharedKey).Skip(16).ToArray<byte>());
				}
			}
			catch (Exception)
			{
				this.Client.CState = 0;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012A1C File Offset: 0x00010C1C
		public void UpdateEncrypt()
		{
			this.Client.CRNonce = UCS.Utilities.Sodium.Utilities.Increment(UCS.Utilities.Sodium.Utilities.Increment(this.Client.CRNonce));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012A3E File Offset: 0x00010C3E
		public void UpdateDecrypt()
		{
			this.Client.CSNonce = UCS.Utilities.Sodium.Utilities.Increment(UCS.Utilities.Sodium.Utilities.Increment(this.Client.CSNonce));
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012A60 File Offset: 0x00010C60
		public void Decrypt()
		{
			try
			{
				if (this.m_vType == 10101)
				{
					byte[] array = this.m_vData;
					this.Client.CPublicKey = array.Take(32).ToArray<byte>();
					this.Client.CSharedKey = this.Client.CPublicKey;
					this.Client.CRNonce = Client.GenerateSessionKey();
					byte[] array2 = GenericHash.Hash(this.Client.CPublicKey.Concat(Key.Crypto.PublicKey).ToArray<byte>(), null, 24);
					array = array.Skip(32).ToArray<byte>();
					byte[] array3 = PublicKeyBox.Open(array, array2, Key.Crypto.PrivateKey, this.Client.CPublicKey);
					this.Client.CSessionKey = array3.Take(24).ToArray<byte>();
					this.Client.CSNonce = array3.Skip(24).Take(24).ToArray<byte>();
					this.SetData(array3.Skip(24).Skip(24).ToArray<byte>());
				}
				else
				{
					this.Client.CSNonce = UCS.Utilities.Sodium.Utilities.Increment(UCS.Utilities.Sodium.Utilities.Increment(this.Client.CSNonce));
					this.SetData(SecretBox.Open(new byte[16].Concat(this.m_vData).ToArray<byte>(), this.Client.CSNonce, this.Client.CSharedKey));
				}
			}
			catch (Exception)
			{
				this.Client.CState = 0;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual void Decode()
		{
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual void Encode()
		{
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00012BF0 File Offset: 0x00010DF0
		public byte[] GetData()
		{
			return this.m_vData;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012BF8 File Offset: 0x00010DF8
		public int GetLength()
		{
			return this.m_vLength;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00012C00 File Offset: 0x00010E00
		public ushort GetMessageType()
		{
			return this.m_vType;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00012C08 File Offset: 0x00010E08
		public ushort GetMessageVersion()
		{
			return this.m_vMessageVersion;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012C10 File Offset: 0x00010E10
		public byte[] GetRawData()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.m_vType).Reverse<byte>());
			list.AddRange(BitConverter.GetBytes(this.m_vLength).Reverse<byte>().Skip(1));
			list.AddRange(BitConverter.GetBytes(this.m_vMessageVersion).Reverse<byte>());
			list.AddRange(this.m_vData);
			return list.ToArray();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public virtual void Process(Level level)
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00012C7B File Offset: 0x00010E7B
		public void SetData(byte[] data)
		{
			this.m_vData = data;
			this.m_vLength = data.Length;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012C8D File Offset: 0x00010E8D
		public void SetMessageType(ushort type)
		{
			this.m_vType = type;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00012C96 File Offset: 0x00010E96
		public void SetMessageVersion(ushort v)
		{
			this.m_vMessageVersion = v;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00012C9F File Offset: 0x00010E9F
		public string ToHexString()
		{
			return BitConverter.ToString(this.m_vData).Replace("-", " ");
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012CBB File Offset: 0x00010EBB
		public override string ToString()
		{
			return Encoding.UTF8.GetString(this.m_vData, 0, this.m_vLength);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00012CD4 File Offset: 0x00010ED4
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

		// Token: 0x0400026D RID: 621
		private byte[] m_vData;

		// Token: 0x0400026E RID: 622
		private int m_vLength;

		// Token: 0x0400026F RID: 623
		private ushort m_vMessageVersion;

		// Token: 0x04000270 RID: 624
		private ushort m_vType;
	}
}