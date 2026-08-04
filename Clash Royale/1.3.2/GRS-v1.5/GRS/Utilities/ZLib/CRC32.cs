using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000013 RID: 19
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000C")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class CRC32
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000312D File Offset: 0x0000132D
		public CRC32()
			: this(false)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003136 File Offset: 0x00001336
		public CRC32(bool reverseBits)
			: this(-306674912, reverseBits)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003144 File Offset: 0x00001344
		public CRC32(int polynomial, bool reverseBits)
		{
			this.reverseBits = reverseBits;
			this.dwPolynomial = (uint)polynomial;
			this.GenerateLookupTable();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003167 File Offset: 0x00001367
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000316F File Offset: 0x0000136F
		public long TotalBytesRead { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003178 File Offset: 0x00001378
		public int Crc32Result
		{
			get
			{
				return (int)(~(int)this._register);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003181 File Offset: 0x00001381
		public int GetCrc32(Stream input)
		{
			return this.GetCrc32AndCopy(input, null);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000318C File Offset: 0x0000138C
		public int GetCrc32AndCopy(Stream input, Stream output)
		{
			if (input == null)
			{
				throw new Exception("The input stream must not be null.");
			}
			byte[] array = new byte[8192];
			int num = 8192;
			this.TotalBytesRead = 0L;
			int i = input.Read(array, 0, num);
			if (output != null)
			{
				output.Write(array, 0, i);
			}
			this.TotalBytesRead += (long)i;
			while (i > 0)
			{
				this.SlurpBlock(array, 0, i);
				i = input.Read(array, 0, num);
				if (output != null)
				{
					output.Write(array, 0, i);
				}
				this.TotalBytesRead += (long)i;
			}
			return (int)(~(int)this._register);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003220 File Offset: 0x00001420
		public int ComputeCrc32(int W, byte B)
		{
			return this._InternalComputeCrc32((uint)W, B);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000322A File Offset: 0x0000142A
		internal int _InternalComputeCrc32(uint W, byte B)
		{
			return (int)(this.crc32Table[(int)((W ^ (uint)B) & 255U)] ^ (W >> 8));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003240 File Offset: 0x00001440
		public void SlurpBlock(byte[] block, int offset, int count)
		{
			if (block == null)
			{
				throw new Exception("The data buffer must not be null.");
			}
			for (int i = 0; i < count; i++)
			{
				int num = offset + i;
				byte b = block[num];
				if (this.reverseBits)
				{
					uint num2 = (this._register >> 24) ^ (uint)b;
					this._register = (this._register << 8) ^ this.crc32Table[(int)num2];
				}
				else
				{
					uint num3 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8) ^ this.crc32Table[(int)num3];
				}
			}
			this.TotalBytesRead += (long)count;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032D4 File Offset: 0x000014D4
		public void UpdateCRC(byte b)
		{
			if (this.reverseBits)
			{
				uint num = (this._register >> 24) ^ (uint)b;
				this._register = (this._register << 8) ^ this.crc32Table[(int)num];
				return;
			}
			uint num2 = (this._register & 255U) ^ (uint)b;
			this._register = (this._register >> 8) ^ this.crc32Table[(int)num2];
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003334 File Offset: 0x00001534
		public void UpdateCRC(byte b, int n)
		{
			while (n-- > 0)
			{
				if (this.reverseBits)
				{
					uint num = (this._register >> 24) ^ (uint)b;
					this._register = (this._register << 8) ^ this.crc32Table[(int)((num >= 0U) ? num : (num + 256U))];
				}
				else
				{
					uint num2 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8) ^ this.crc32Table[(int)((num2 >= 0U) ? num2 : (num2 + 256U))];
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000033BC File Offset: 0x000015BC
		private static uint ReverseBits(uint data)
		{
			uint num = ((data & 1431655765U) << 1) | ((data >> 1) & 1431655765U);
			num = ((num & 858993459U) << 2) | ((num >> 2) & 858993459U);
			num = ((num & 252645135U) << 4) | ((num >> 4) & 252645135U);
			return (num << 24) | ((num & 65280U) << 8) | ((num >> 8) & 65280U) | (num >> 24);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003428 File Offset: 0x00001628
		private static byte ReverseBits(byte data)
		{
			int num = (int)data * 131586;
			uint num2 = 17055760U;
			uint num3 = (uint)(num & (int)num2);
			uint num4 = (uint)((num << 2) & (int)((int)num2 << 1));
			return (byte)(16781313U * (num3 + num4) >> 24);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000345C File Offset: 0x0000165C
		private void GenerateLookupTable()
		{
			this.crc32Table = new uint[256];
			byte b = 0;
			do
			{
				uint num = (uint)b;
				for (byte b2 = 8; b2 > 0; b2 -= 1)
				{
					if ((num & 1U) == 1U)
					{
						num = (num >> 1) ^ this.dwPolynomial;
					}
					else
					{
						num >>= 1;
					}
				}
				if (this.reverseBits)
				{
					this.crc32Table[(int)CRC32.ReverseBits(b)] = CRC32.ReverseBits(num);
				}
				else
				{
					this.crc32Table[(int)b] = num;
				}
				b += 1;
			}
			while (b != 0);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000034D0 File Offset: 0x000016D0
		private uint gf2_matrix_times(uint[] matrix, uint vec)
		{
			uint num = 0U;
			int num2 = 0;
			while (vec != 0U)
			{
				if ((vec & 1U) == 1U)
				{
					num ^= matrix[num2];
				}
				vec >>= 1;
				num2++;
			}
			return num;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000034FC File Offset: 0x000016FC
		private void gf2_matrix_square(uint[] square, uint[] mat)
		{
			for (int i = 0; i < 32; i++)
			{
				square[i] = this.gf2_matrix_times(mat, mat[i]);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003524 File Offset: 0x00001724
		public void Combine(int crc, int length)
		{
			uint[] array = new uint[32];
			uint[] array2 = new uint[32];
			if (length == 0)
			{
				return;
			}
			uint num = ~this._register;
			array2[0] = this.dwPolynomial;
			uint num2 = 1U;
			for (int i = 1; i < 32; i++)
			{
				array2[i] = num2;
				num2 <<= 1;
			}
			this.gf2_matrix_square(array, array2);
			this.gf2_matrix_square(array2, array);
			uint num3 = (uint)length;
			do
			{
				this.gf2_matrix_square(array, array2);
				if ((num3 & 1U) == 1U)
				{
					num = this.gf2_matrix_times(array, num);
				}
				num3 >>= 1;
				if (num3 == 0U)
				{
					break;
				}
				this.gf2_matrix_square(array2, array);
				if ((num3 & 1U) == 1U)
				{
					num = this.gf2_matrix_times(array2, num);
				}
				num3 >>= 1;
			}
			while (num3 != 0U);
			num ^= (uint)crc;
			this._register = ~num;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000035DB File Offset: 0x000017DB
		public void Reset()
		{
			this._register = uint.MaxValue;
		}

		// Token: 0x04000025 RID: 37
		private const int BUFFER_SIZE = 8192;

		// Token: 0x04000026 RID: 38
		private readonly uint dwPolynomial;

		// Token: 0x04000027 RID: 39
		private readonly bool reverseBits;

		// Token: 0x04000028 RID: 40
		private uint _register = uint.MaxValue;

		// Token: 0x04000029 RID: 41
		private uint[] crc32Table;
	}
}
