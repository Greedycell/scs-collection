using System;

namespace CRS.Utilities.LZMA.Common
{
	// Token: 0x02000058 RID: 88
	internal class CRC
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00013434 File Offset: 0x00011634
		static CRC()
		{
			for (uint num = 0U; num < 256U; num += 1U)
			{
				uint num2 = num;
				for (int i = 0; i < 8; i++)
				{
					if ((num2 & 1U) != 0U)
					{
						num2 = (num2 >> 1) ^ 3988292384U;
					}
					else
					{
						num2 >>= 1;
					}
				}
				CRC.Table[(int)num] = num2;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0001348B File Offset: 0x0001168B
		public void Init()
		{
			this._value = uint.MaxValue;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00013494 File Offset: 0x00011694
		public void UpdateByte(byte b)
		{
			this._value = CRC.Table[(int)((byte)this._value ^ b)] ^ (this._value >> 8);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000134B4 File Offset: 0x000116B4
		public void Update(byte[] data, uint offset, uint size)
		{
			for (uint num = 0U; num < size; num += 1U)
			{
				this._value = CRC.Table[(int)((byte)this._value ^ data[(int)(offset + num)])] ^ (this._value >> 8);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000134EF File Offset: 0x000116EF
		public uint GetDigest()
		{
			return this._value ^ uint.MaxValue;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000134F9 File Offset: 0x000116F9
		private static uint CalculateDigest(byte[] data, uint offset, uint size)
		{
			CRC crc = new CRC();
			crc.Update(data, offset, size);
			return crc.GetDigest();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001350E File Offset: 0x0001170E
		private static bool VerifyDigest(uint digest, byte[] data, uint offset, uint size)
		{
			return CRC.CalculateDigest(data, offset, size) == digest;
		}

		// Token: 0x0400026C RID: 620
		public static readonly uint[] Table = new uint[256];

		// Token: 0x0400026D RID: 621
		private uint _value = uint.MaxValue;
	}
}
