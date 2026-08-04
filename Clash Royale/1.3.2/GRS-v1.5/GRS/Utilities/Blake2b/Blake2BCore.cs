using System;

namespace CRS.Utilities.Blake2b
{
	// Token: 0x02000062 RID: 98
	public sealed class Blake2BCore
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00013B58 File Offset: 0x00011D58
		internal static ulong BytesToUInt64(byte[] buf, int offset)
		{
			return ((ulong)buf[offset + 7] << 56) | ((ulong)buf[offset + 6] << 48) | ((ulong)buf[offset + 5] << 40) | ((ulong)buf[offset + 4] << 32) | ((ulong)buf[offset + 3] << 24) | ((ulong)buf[offset + 2] << 16) | ((ulong)buf[offset + 1] << 8) | (ulong)buf[offset];
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00013BB0 File Offset: 0x00011DB0
		private static void UInt64ToBytes(ulong value, byte[] buf, int offset)
		{
			buf[offset + 7] = (byte)(value >> 56);
			buf[offset + 6] = (byte)(value >> 48);
			buf[offset + 5] = (byte)(value >> 40);
			buf[offset + 4] = (byte)(value >> 32);
			buf[offset + 3] = (byte)(value >> 24);
			buf[offset + 2] = (byte)(value >> 16);
			buf[offset + 1] = (byte)(value >> 8);
			buf[offset] = (byte)value;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00013C08 File Offset: 0x00011E08
		private void Compress(byte[] block, int start)
		{
			ulong[] h = this._h;
			ulong[] m = this._m;
			if (BitConverter.IsLittleEndian)
			{
				Buffer.BlockCopy(block, start, m, 0, 128);
			}
			else
			{
				for (int i = 0; i < 16; i++)
				{
					m[i] = Blake2BCore.BytesToUInt64(block, start + (i << 3));
				}
			}
			ulong num = h[0];
			ulong num2 = h[1];
			ulong num3 = h[2];
			ulong num4 = h[3];
			ulong num5 = h[4];
			ulong num6 = h[5];
			ulong num7 = h[6];
			ulong num8 = h[7];
			ulong num9 = 7640891576956012808UL;
			ulong num10 = 13503953896175478587UL;
			ulong num11 = 4354685564936845355UL;
			ulong num12 = 11912009170470909681UL;
			ulong num13 = 5840696475078001361UL ^ this._counter0;
			ulong num14 = 11170449401992604703UL ^ this._counter1;
			ulong num15 = 2270897969802886507UL ^ this._finalizationFlag0;
			ulong num16 = 6620516959819538809UL ^ this._finalizationFlag1;
			num = num + num5 + m[0];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[1];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[2];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[3];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[4];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[5];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[6];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[7];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[8];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[9];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[10];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[11];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[12];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[13];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[14];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[15];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[14];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[10];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[4];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[8];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[9];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[15];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[13];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[6];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[1];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[12];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[0];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[2];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[11];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[7];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[5];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[3];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[11];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[8];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[12];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[0];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[5];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[2];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[15];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[13];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[10];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[14];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[3];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[6];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[7];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[1];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[9];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[4];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[7];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[9];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[3];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[1];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[13];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[12];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[11];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[14];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[2];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[6];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[5];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[10];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[4];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[0];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[15];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[8];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[9];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[0];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[5];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[7];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[2];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[4];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[10];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[15];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[14];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[1];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[11];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[12];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[6];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[8];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[3];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[13];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[2];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[12];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[6];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[10];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[0];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[11];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[8];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[3];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[4];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[13];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[7];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[5];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[15];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[14];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[1];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[9];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[12];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[5];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[1];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[15];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[14];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[13];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[4];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[10];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[0];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[7];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[6];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[3];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[9];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[2];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[8];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[11];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[13];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[11];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[7];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[14];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[12];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[1];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[3];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[9];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[5];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[0];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[15];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[4];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[8];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[6];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[2];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[10];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[6];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[15];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[14];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[9];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[11];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[3];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[0];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[8];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[12];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[2];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[13];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[7];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[1];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[4];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[10];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[5];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[10];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[2];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[8];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[4];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[7];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[6];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[1];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[5];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[15];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[11];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[9];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[14];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[3];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[12];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[13];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[0];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[0];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[1];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[2];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[3];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[4];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[5];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[6];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[7];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[8];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[9];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[10];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[11];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[12];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[13];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[14];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[15];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			num = num + num5 + m[14];
			num13 ^= num;
			num13 = (num13 >> 32) | (num13 << 32);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 24) | (num5 << 40);
			num = num + num5 + m[10];
			num13 ^= num;
			num13 = (num13 >> 16) | (num13 << 48);
			num9 += num13;
			num5 ^= num9;
			num5 = (num5 >> 63) | (num5 << 1);
			num2 = num2 + num6 + m[4];
			num14 ^= num2;
			num14 = (num14 >> 32) | (num14 << 32);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 24) | (num6 << 40);
			num2 = num2 + num6 + m[8];
			num14 ^= num2;
			num14 = (num14 >> 16) | (num14 << 48);
			num10 += num14;
			num6 ^= num10;
			num6 = (num6 >> 63) | (num6 << 1);
			num3 = num3 + num7 + m[9];
			num15 ^= num3;
			num15 = (num15 >> 32) | (num15 << 32);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 24) | (num7 << 40);
			num3 = num3 + num7 + m[15];
			num15 ^= num3;
			num15 = (num15 >> 16) | (num15 << 48);
			num11 += num15;
			num7 ^= num11;
			num7 = (num7 >> 63) | (num7 << 1);
			num4 = num4 + num8 + m[13];
			num16 ^= num4;
			num16 = (num16 >> 32) | (num16 << 32);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 24) | (num8 << 40);
			num4 = num4 + num8 + m[6];
			num16 ^= num4;
			num16 = (num16 >> 16) | (num16 << 48);
			num12 += num16;
			num8 ^= num12;
			num8 = (num8 >> 63) | (num8 << 1);
			num = num + num6 + m[1];
			num16 ^= num;
			num16 = (num16 >> 32) | (num16 << 32);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 24) | (num6 << 40);
			num = num + num6 + m[12];
			num16 ^= num;
			num16 = (num16 >> 16) | (num16 << 48);
			num11 += num16;
			num6 ^= num11;
			num6 = (num6 >> 63) | (num6 << 1);
			num2 = num2 + num7 + m[0];
			num13 ^= num2;
			num13 = (num13 >> 32) | (num13 << 32);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 24) | (num7 << 40);
			num2 = num2 + num7 + m[2];
			num13 ^= num2;
			num13 = (num13 >> 16) | (num13 << 48);
			num12 += num13;
			num7 ^= num12;
			num7 = (num7 >> 63) | (num7 << 1);
			num3 = num3 + num8 + m[11];
			num14 ^= num3;
			num14 = (num14 >> 32) | (num14 << 32);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 24) | (num8 << 40);
			num3 = num3 + num8 + m[7];
			num14 ^= num3;
			num14 = (num14 >> 16) | (num14 << 48);
			num9 += num14;
			num8 ^= num9;
			num8 = (num8 >> 63) | (num8 << 1);
			num4 = num4 + num5 + m[5];
			num15 ^= num4;
			num15 = (num15 >> 32) | (num15 << 32);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 24) | (num5 << 40);
			num4 = num4 + num5 + m[3];
			num15 ^= num4;
			num15 = (num15 >> 16) | (num15 << 48);
			num10 += num15;
			num5 ^= num10;
			num5 = (num5 >> 63) | (num5 << 1);
			h[0] ^= num ^ num9;
			h[1] ^= num2 ^ num10;
			h[2] ^= num3 ^ num11;
			h[3] ^= num4 ^ num12;
			h[4] ^= num5 ^ num13;
			h[5] ^= num6 ^ num14;
			h[6] ^= num7 ^ num15;
			h[7] ^= num8 ^ num16;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000167C8 File Offset: 0x000149C8
		public void Initialize(ulong[] config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (config.Length != 8)
			{
				throw new ArgumentException("config length must be 8 words");
			}
			this._isInitialized = true;
			this._h[0] = 7640891576956012808UL;
			this._h[1] = 13503953896175478587UL;
			this._h[2] = 4354685564936845355UL;
			this._h[3] = 11912009170470909681UL;
			this._h[4] = 5840696475078001361UL;
			this._h[5] = 11170449401992604703UL;
			this._h[6] = 2270897969802886507UL;
			this._h[7] = 6620516959819538809UL;
			this._counter0 = 0UL;
			this._counter1 = 0UL;
			this._finalizationFlag0 = 0UL;
			this._finalizationFlag1 = 0UL;
			this._bufferFilled = 0;
			Array.Clear(this._buf, 0, this._buf.Length);
			for (int i = 0; i < 8; i++)
			{
				this._h[i] ^= config[i];
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000168E0 File Offset: 0x00014AE0
		public void HashCore(byte[] array, int start, int count)
		{
			if (!this._isInitialized)
			{
				throw new InvalidOperationException("Not initialized");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((long)start + (long)count > (long)array.Length)
			{
				throw new ArgumentOutOfRangeException("start+count");
			}
			int num = start;
			int num2 = 128 - this._bufferFilled;
			if (this._bufferFilled > 0 && count > num2)
			{
				Array.Copy(array, num, this._buf, this._bufferFilled, num2);
				this._counter0 += 128UL;
				if (this._counter0 == 0UL)
				{
					this._counter1 += 1UL;
				}
				this.Compress(this._buf, 0);
				num += num2;
				count -= num2;
				this._bufferFilled = 0;
			}
			while (count > 128)
			{
				this._counter0 += 128UL;
				if (this._counter0 == 0UL)
				{
					this._counter1 += 1UL;
				}
				this.Compress(array, num);
				num += 128;
				count -= 128;
			}
			if (count > 0)
			{
				Array.Copy(array, num, this._buf, this._bufferFilled, count);
				this._bufferFilled += count;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00016A32 File Offset: 0x00014C32
		public byte[] HashFinal()
		{
			return this.HashFinal(false);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00016A3C File Offset: 0x00014C3C
		public byte[] HashFinal(bool isEndOfLayer)
		{
			if (!this._isInitialized)
			{
				throw new InvalidOperationException("Not initialized");
			}
			this._isInitialized = false;
			this._counter0 += (ulong)this._bufferFilled;
			this._finalizationFlag0 = ulong.MaxValue;
			if (isEndOfLayer)
			{
				this._finalizationFlag1 = ulong.MaxValue;
			}
			for (int i = this._bufferFilled; i < this._buf.Length; i++)
			{
				this._buf[i] = 0;
			}
			this.Compress(this._buf, 0);
			byte[] array = new byte[64];
			for (int j = 0; j < 8; j++)
			{
				Blake2BCore.UInt64ToBytes(this._h[j], array, j << 3);
			}
			return array;
		}

		// Token: 0x0400028A RID: 650
		private const int NumberOfRounds = 12;

		// Token: 0x0400028B RID: 651
		private const int BlockSizeInBytes = 128;

		// Token: 0x0400028C RID: 652
		private const ulong IV0 = 7640891576956012808UL;

		// Token: 0x0400028D RID: 653
		private const ulong IV1 = 13503953896175478587UL;

		// Token: 0x0400028E RID: 654
		private const ulong IV2 = 4354685564936845355UL;

		// Token: 0x0400028F RID: 655
		private const ulong IV3 = 11912009170470909681UL;

		// Token: 0x04000290 RID: 656
		private const ulong IV4 = 5840696475078001361UL;

		// Token: 0x04000291 RID: 657
		private const ulong IV5 = 11170449401992604703UL;

		// Token: 0x04000292 RID: 658
		private const ulong IV6 = 2270897969802886507UL;

		// Token: 0x04000293 RID: 659
		private const ulong IV7 = 6620516959819538809UL;

		// Token: 0x04000294 RID: 660
		private static readonly int[] Sigma = new int[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15, 14, 10, 4, 8,
			9, 15, 13, 6, 1, 12, 0, 2, 11, 7,
			5, 3, 11, 8, 12, 0, 5, 2, 15, 13,
			10, 14, 3, 6, 7, 1, 9, 4, 7, 9,
			3, 1, 13, 12, 11, 14, 2, 6, 5, 10,
			4, 0, 15, 8, 9, 0, 5, 7, 2, 4,
			10, 15, 14, 1, 11, 12, 6, 8, 3, 13,
			2, 12, 6, 10, 0, 11, 8, 3, 4, 13,
			7, 5, 15, 14, 1, 9, 12, 5, 1, 15,
			14, 13, 4, 10, 0, 7, 6, 3, 9, 2,
			8, 11, 13, 11, 7, 14, 12, 1, 3, 9,
			5, 0, 15, 4, 8, 6, 2, 10, 6, 15,
			14, 9, 11, 3, 0, 8, 12, 2, 13, 7,
			1, 4, 10, 5, 10, 2, 8, 4, 7, 6,
			1, 5, 15, 11, 9, 14, 3, 12, 13, 0,
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15, 14, 10, 4, 8,
			9, 15, 13, 6, 1, 12, 0, 2, 11, 7,
			5, 3
		};

		// Token: 0x04000295 RID: 661
		private readonly byte[] _buf = new byte[128];

		// Token: 0x04000296 RID: 662
		private int _bufferFilled;

		// Token: 0x04000297 RID: 663
		private ulong _counter0;

		// Token: 0x04000298 RID: 664
		private ulong _counter1;

		// Token: 0x04000299 RID: 665
		private ulong _finalizationFlag0;

		// Token: 0x0400029A RID: 666
		private ulong _finalizationFlag1;

		// Token: 0x0400029B RID: 667
		private readonly ulong[] _h = new ulong[8];

		// Token: 0x0400029C RID: 668
		private bool _isInitialized;

		// Token: 0x0400029D RID: 669
		private readonly ulong[] _m = new ulong[16];
	}
}
