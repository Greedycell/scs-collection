using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using UCS.Core;
using UCS.Files;
using UCS.Logic;

namespace UCS.Helpers
{
	// Token: 0x02000028 RID: 40
	internal static class Helpers
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000C79C File Offset: 0x0000A99C
		public static void AddDataSlots(this List<byte> list, List<DataSlot> data)
		{
			list.AddInt32(data.Count);
			foreach (DataSlot dataSlot in data)
			{
				list.AddRange(dataSlot.Encode());
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		public static void AddInt32(this List<byte> list, int data)
		{
			list.AddRange(BitConverter.GetBytes(data).Reverse<byte>());
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000C80F File Offset: 0x0000AA0F
		public static void AddInt64(this List<byte> list, long data)
		{
			list.AddRange(BitConverter.GetBytes(data).Reverse<byte>());
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000C824 File Offset: 0x0000AA24
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

		// Token: 0x06000179 RID: 377 RVA: 0x0000C874 File Offset: 0x0000AA74
		public static byte[] HexaToBytes(string hex)
		{
			return (from x in Enumerable.Range(0, hex.Length)
					where x % 2 == 0
					select Convert.ToByte(hex.Substring(x, 2), 16)).ToArray<byte>();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000C8D9 File Offset: 0x0000AAD9
		public static int ParseConfigInt(string str)
		{
			return int.Parse(ConfigurationManager.AppSettings[str]);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000C8EB File Offset: 0x0000AAEB
		public static string parseConfigString(string str)
		{
			return ConfigurationManager.AppSettings[str];
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
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

		// Token: 0x0600017D RID: 381 RVA: 0x0000C954 File Offset: 0x0000AB54
		public static Data ReadDataReference(this BinaryReader br)
		{
			int num = br.ReadInt32WithEndian();
			return ObjectManager.DataTables.GetDataById(num);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000C974 File Offset: 0x0000AB74
		public static int ReadInt32WithEndian(this BinaryReader br)
		{
			byte[] array = br.ReadBytes(4);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		public static long ReadInt64WithEndian(this BinaryReader br)
		{
			byte[] array = br.ReadBytes(8);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000C9CC File Offset: 0x0000ABCC
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

		// Token: 0x06000181 RID: 385 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		public static ushort ReadUInt16WithEndian(this BinaryReader br)
		{
			byte[] array = br.ReadBytes(2);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToUInt16(array, 0);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000CA38 File Offset: 0x0000AC38
		public static uint ReadUInt32WithEndian(this BinaryReader br)
		{
			byte[] array = br.ReadBytes(4);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array);
			}
			return BitConverter.ToUInt32(array, 0);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public static string String2Hexa(string str)
		{
			return (from t in str.ToCharArray()
					select Convert.ToInt32(t)).Aggregate(string.Empty, (string current, int value) => current + string.Format("{0:X}", value));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		public static bool TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key)
		{
			TValue tvalue;
			return self.TryRemove(key, out tvalue);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000CADC File Offset: 0x0000ACDC
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
	}
}