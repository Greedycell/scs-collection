using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CRS.Core;
using CRS.Files.Logic;
using CRS.Logic;

namespace CRS.Helpers
{
	// Token: 0x02000006 RID: 6
	internal static class Helpers
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000212C File Offset: 0x0000032C
		public static void AddDataSlots(this List<byte> list, List<DataSlot> data)
		{
			list.AddInt32(data.Count);
			foreach (DataSlot dataSlot in data)
			{
				list.AddRange(dataSlot.Encode());
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000218C File Offset: 0x0000038C
		public static void AddInt32(this List<byte> list, int data)
		{
			list.AddRange(BitConverter.GetBytes(data).Reverse<byte>());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000219F File Offset: 0x0000039F
		public static void AddInt64(this List<byte> list, long data)
		{
			list.AddRange(BitConverter.GetBytes(data).Reverse<byte>());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B4 File Offset: 0x000003B4
		public static void AddString(this List<byte> list, string data)
		{
			if (data == null)
			{
				list.AddRange(BitConverter.GetBytes(-1).Reverse<byte>());
				return;
			}
			list.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(data)).Reverse<byte>());
			list.AddRange(Encoding.UTF8.GetBytes(data));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
		public static byte[] HexaToBytes(string hex)
		{
			return (from x in Enumerable.Range(0, hex.Length)
				where x % 2 == 0
				select Convert.ToByte(hex.Substring(x, 2), 16)).ToArray<byte>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000226C File Offset: 0x0000046C
		public static byte[] ReadAllBytes(this BinaryReader br)
		{
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[4096];
				int num;
				while ((num = br.Read(array, 0, array.Length)) != 0)
				{
					memoryStream.Write(array, 0, num);
				}
				array2 = memoryStream.ToArray();
			}
			return array2;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C8 File Offset: 0x000004C8
		public static Data ReadDataReference(this BinaryReader br)
		{
			int num = br.ReadInt32WithEndian();
			return ObjectManager.DataTables.GetDataById(num);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E8 File Offset: 0x000004E8
		public static int ReadInt32WithEndian(this BinaryReader br)
		{
			byte[] array = br.ReadBytes(4);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002314 File Offset: 0x00000514
		public static long ReadInt64WithEndian(this BinaryReader br)
		{
			byte[] array = br.ReadBytes(8);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002340 File Offset: 0x00000540
		public static string ReadScString(this BinaryReader br)
		{
			int num = br.ReadInt32WithEndian();
			string text;
			if (num > -1)
			{
				if (num > 0)
				{
					byte[] array = br.ReadBytes(num);
					text = Encoding.UTF8.GetString(array);
				}
				else
				{
					text = string.Empty;
				}
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002380 File Offset: 0x00000580
		public static ushort ReadUInt16WithEndian(this BinaryReader br)
		{
			byte[] array = br.ReadBytes(2);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToUInt16(array, 0);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023AC File Offset: 0x000005AC
		public static uint ReadUInt32WithEndian(this BinaryReader br)
		{
			byte[] array = br.ReadBytes(4);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToUInt32(array, 0);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D5 File Offset: 0x000005D5
		public static string Bytes2Hexa(byte[] p)
		{
			return BitConverter.ToString(p).Replace("-", string.Empty);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023EC File Offset: 0x000005EC
		public static string String2Hexa(string str)
		{
			return (from t in str.ToCharArray()
				select Convert.ToInt32(t)).Aggregate(string.Empty, (string current, int value) => current + string.Format("{0:X}", value));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000244C File Offset: 0x0000064C
		public static bool TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key)
		{
			TValue tvalue;
			return self.TryRemove(key, out tvalue);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002464 File Offset: 0x00000664
		public static void AddInt64LE(this List<byte> p, long i)
		{
			p.AddRange(new byte[]
			{
				(byte)i,
				(byte)(((uint)i >> 8) & 255U),
				(byte)(((uint)i >> 16) & 255U),
				(byte)(((uint)i >> 24) & 255U)
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024B0 File Offset: 0x000006B0
		public static int ReadVInt(this BinaryReader br)
		{
			byte b = br.ReadByte();
			int num = (int)(b & 128);
			int num2 = (int)(b & 63);
			if ((b & 64) != 0)
			{
				if (num != 0)
				{
					b = br.ReadByte();
					num = (((int)b << 6) & 8128) | num2;
					if ((b & 128) != 0)
					{
						b = br.ReadByte();
						num |= ((int)b << 13) & 1040384;
						if ((b & 128) != 0)
						{
							b = br.ReadByte();
							num |= ((int)b << 20) & 133169152;
							if ((b & 128) != 0)
							{
								b = br.ReadByte();
								num2 = unchecked((int)((long)(num | ((int)b << 27)) | (long)(ulong)int.MinValue));
							}
							else
							{
								num2 = unchecked((int)((long)num | (long)(ulong)(-134217728)));
							}
						}
						else
						{
							num2 = unchecked((int)((long)num | (long)(ulong)(-1048576)));
						}
					}
					else
					{
						num2 = unchecked((int)((long)num | (long)(ulong)(-8192)));
					}
				}
			}
			else if (num != 0)
			{
				b = br.ReadByte();
				num2 |= ((int)b << 6) & 8128;
				if ((b & 128) != 0)
				{
					b = br.ReadByte();
					num2 |= ((int)b << 13) & 1040384;
					if ((b & 128) != 0)
					{
						b = br.ReadByte();
						num2 |= ((int)b << 20) & 133169152;
						if ((b & 128) != 0)
						{
							b = br.ReadByte();
							num2 |= (int)b << 27;
						}
					}
				}
			}
			return num2;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025E4 File Offset: 0x000007E4
		public static void Increment(this byte[] n)
		{
			for (int i = 0; i < 2; i++)
			{
				ushort num = 1;
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)n.Length))
				{
					num += (ushort)n[(int)num2];
					n[(int)num2] = (byte)num;
					num = (ushort)(num >> 8);
					num2 += 1U;
				}
			}
		}
	}
}
