using System;
using System.IO;
using CRS.Utilities.LZMA.Common;
using CRS.Utilities.LZMA.Compress.LZMA;

namespace CRS.Utilities.LZMA.Compress.LzmaAlone
{
	// Token: 0x02000051 RID: 81
	internal abstract class LzmaBench
	{
		// Token: 0x06000286 RID: 646 RVA: 0x00012B78 File Offset: 0x00010D78
		private static uint GetLogSize(uint size)
		{
			for (int i = 8; i < 32; i++)
			{
				for (uint num = 0U; num < 256U; num += 1U)
				{
					if (size <= (1U << i) + (num << i - 8))
					{
						return (uint)((i << 8) + (int)num);
					}
				}
			}
			return 8192U;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00012BC0 File Offset: 0x00010DC0
		private static ulong MyMultDiv64(ulong value, ulong elapsedTime)
		{
			ulong num = 10000000UL;
			ulong num2 = elapsedTime;
			while (num > 1000000UL)
			{
				num >>= 1;
				num2 >>= 1;
			}
			if (num2 == 0UL)
			{
				num2 = 1UL;
			}
			return value * num / num2;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00012BF4 File Offset: 0x00010DF4
		private static ulong GetCompressRating(uint dictionarySize, ulong elapsedTime, ulong size)
		{
			ulong num = (ulong)(LzmaBench.GetLogSize(dictionarySize) - 4608U);
			ulong num2 = 1060UL + (num * num * 10UL >> 16);
			return LzmaBench.MyMultDiv64(size * num2, elapsedTime);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00012C2A File Offset: 0x00010E2A
		private static ulong GetDecompressRating(ulong elapsedTime, ulong outSize, ulong inSize)
		{
			return LzmaBench.MyMultDiv64(inSize * 220UL + outSize * 20UL, elapsedTime);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00012C40 File Offset: 0x00010E40
		private static ulong GetTotalRating(uint dictionarySize, ulong elapsedTimeEn, ulong sizeEn, ulong elapsedTimeDe, ulong inSizeDe, ulong outSizeDe)
		{
			return (LzmaBench.GetCompressRating(dictionarySize, elapsedTimeEn, sizeEn) + LzmaBench.GetDecompressRating(elapsedTimeDe, inSizeDe, outSizeDe)) / 2UL;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00012C58 File Offset: 0x00010E58
		private static void PrintValue(ulong v)
		{
			string text = v.ToString();
			int num = 0;
			while (num + text.Length < 6)
			{
				Console.Write(" ");
				num++;
			}
			Console.Write(text);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00012C90 File Offset: 0x00010E90
		private static void PrintRating(ulong rating)
		{
			LzmaBench.PrintValue(rating / 1000000UL);
			Console.Write(" MIPS");
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00012CAC File Offset: 0x00010EAC
		private static void PrintResults(uint dictionarySize, ulong elapsedTime, ulong size, bool decompressMode, ulong secondSize)
		{
			LzmaBench.PrintValue(LzmaBench.MyMultDiv64(size, elapsedTime) / 1024UL);
			Console.Write(" KB/s  ");
			ulong num;
			if (decompressMode)
			{
				num = LzmaBench.GetDecompressRating(elapsedTime, size, secondSize);
			}
			else
			{
				num = LzmaBench.GetCompressRating(dictionarySize, elapsedTime, size);
			}
			LzmaBench.PrintRating(num);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00012CF4 File Offset: 0x00010EF4
		public static int LzmaBenchmark(int numIterations, uint dictionarySize)
		{
			if (numIterations <= 0)
			{
				return 0;
			}
			if (dictionarySize < 262144U)
			{
				Console.WriteLine("\nError: dictionary size for benchmark must be >= 19 (512 KB)");
				return 1;
			}
			Console.Write("\n       Compressing                Decompressing\n\n");
			Encoder encoder = new Encoder();
			Decoder decoder = new Decoder();
			CoderPropID[] array = new CoderPropID[] { CoderPropID.DictionarySize };
			object[] array2 = new object[] { (int)dictionarySize };
			uint num = dictionarySize + 6291456U;
			int num2 = (int)(num / 2U + 1024U);
			encoder.SetCoderProperties(array, array2);
			MemoryStream memoryStream = new MemoryStream();
			encoder.WriteCoderProperties(memoryStream);
			byte[] array3 = memoryStream.ToArray();
			LzmaBench.CBenchRandomGenerator cbenchRandomGenerator = new LzmaBench.CBenchRandomGenerator();
			cbenchRandomGenerator.Set(num);
			cbenchRandomGenerator.Generate();
			CRC crc = new CRC();
			crc.Init();
			crc.Update(cbenchRandomGenerator.Buffer, 0U, cbenchRandomGenerator.BufferSize);
			LzmaBench.CProgressInfo cprogressInfo = new LzmaBench.CProgressInfo();
			cprogressInfo.ApprovedStart = (long)((ulong)dictionarySize);
			ulong num3 = 0UL;
			ulong num4 = 0UL;
			ulong num5 = 0UL;
			ulong num6 = 0UL;
			MemoryStream memoryStream2 = new MemoryStream(cbenchRandomGenerator.Buffer, 0, (int)cbenchRandomGenerator.BufferSize);
			MemoryStream memoryStream3 = new MemoryStream(num2);
			LzmaBench.CrcOutStream crcOutStream = new LzmaBench.CrcOutStream();
			for (int i = 0; i < numIterations; i++)
			{
				cprogressInfo.Init();
				memoryStream2.Seek(0L, SeekOrigin.Begin);
				memoryStream3.Seek(0L, SeekOrigin.Begin);
				encoder.Code(memoryStream2, memoryStream3, -1L, -1L, cprogressInfo);
				ulong ticks = (ulong)(DateTime.UtcNow - cprogressInfo.Time).Ticks;
				long position = memoryStream3.Position;
				if (cprogressInfo.InSize == 0L)
				{
					throw new Exception("Internal ERROR 1282");
				}
				ulong num7 = 0UL;
				for (int j = 0; j < 2; j++)
				{
					memoryStream3.Seek(0L, SeekOrigin.Begin);
					crcOutStream.Init();
					decoder.SetDecoderProperties(array3);
					ulong num8 = (ulong)num;
					DateTime utcNow = DateTime.UtcNow;
					decoder.Code(memoryStream3, crcOutStream, 0L, (long)num8, null);
					num7 = (ulong)(DateTime.UtcNow - utcNow).Ticks;
					if (crcOutStream.GetDigest() != crc.GetDigest())
					{
						throw new Exception("CRC Error");
					}
				}
				ulong num9 = (ulong)num - (ulong)cprogressInfo.InSize;
				LzmaBench.PrintResults(dictionarySize, ticks, num9, false, 0UL);
				Console.Write("     ");
				LzmaBench.PrintResults(dictionarySize, num7, (ulong)num, true, (ulong)position);
				Console.WriteLine();
				num3 += num9;
				num4 += ticks;
				num5 += num7;
				num6 += (ulong)position;
			}
			Console.WriteLine("---------------------------------------------------");
			LzmaBench.PrintResults(dictionarySize, num4, num3, false, 0UL);
			Console.Write("     ");
			LzmaBench.PrintResults(dictionarySize, num5, (ulong)num * (ulong)((long)numIterations), true, num6);
			Console.WriteLine("    Average");
			return 0;
		}

		// Token: 0x0400024E RID: 590
		private const uint kAdditionalSize = 6291456U;

		// Token: 0x0400024F RID: 591
		private const uint kCompressedAdditionalSize = 1024U;

		// Token: 0x04000250 RID: 592
		private const uint kMaxLzmaPropSize = 10U;

		// Token: 0x04000251 RID: 593
		private const int kSubBits = 8;

		// Token: 0x020000FD RID: 253
		private class CRandomGenerator
		{
			// Token: 0x06000609 RID: 1545 RVA: 0x0002171B File Offset: 0x0001F91B
			public CRandomGenerator()
			{
				this.Init();
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x00021729 File Offset: 0x0001F929
			public void Init()
			{
				this.A1 = 362436069U;
				this.A2 = 521288629U;
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x00021744 File Offset: 0x0001F944
			public uint GetRnd()
			{
				return ((this.A1 = 36969U * (this.A1 & 65535U) + (this.A1 >> 16)) << 16) ^ (this.A2 = 18000U * (this.A2 & 65535U) + (this.A2 >> 16));
			}

			// Token: 0x0400047C RID: 1148
			private uint A1;

			// Token: 0x0400047D RID: 1149
			private uint A2;
		}

		// Token: 0x020000FE RID: 254
		private class CBitRandomGenerator
		{
			// Token: 0x0600060C RID: 1548 RVA: 0x0002179F File Offset: 0x0001F99F
			public void Init()
			{
				this.Value = 0U;
				this.NumBits = 0;
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x000217B0 File Offset: 0x0001F9B0
			public uint GetRnd(int numBits)
			{
				if (this.NumBits > numBits)
				{
					uint num = this.Value & ((1U << numBits) - 1U);
					this.Value >>= numBits;
					this.NumBits -= numBits;
					return num;
				}
				numBits -= this.NumBits;
				uint num2 = this.Value << numBits;
				this.Value = this.RG.GetRnd();
				uint num3 = num2 | (this.Value & ((1U << numBits) - 1U));
				this.Value >>= numBits;
				this.NumBits = 32 - numBits;
				return num3;
			}

			// Token: 0x0400047E RID: 1150
			private LzmaBench.CRandomGenerator RG = new LzmaBench.CRandomGenerator();

			// Token: 0x0400047F RID: 1151
			private uint Value;

			// Token: 0x04000480 RID: 1152
			private int NumBits;
		}

		// Token: 0x020000FF RID: 255
		private class CBenchRandomGenerator
		{
			// Token: 0x06000610 RID: 1552 RVA: 0x0002186C File Offset: 0x0001FA6C
			public void Set(uint bufferSize)
			{
				this.Buffer = new byte[bufferSize];
				this.Pos = 0U;
				this.BufferSize = bufferSize;
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x00021888 File Offset: 0x0001FA88
			private uint GetRndBit()
			{
				return this.RG.GetRnd(1);
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x00021898 File Offset: 0x0001FA98
			private uint GetLogRandBits(int numBits)
			{
				uint rnd = this.RG.GetRnd(numBits);
				return this.RG.GetRnd((int)rnd);
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x000218BE File Offset: 0x0001FABE
			private uint GetOffset()
			{
				if (this.GetRndBit() == 0U)
				{
					return this.GetLogRandBits(4);
				}
				return (this.GetLogRandBits(4) << 10) | this.RG.GetRnd(10);
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x000218E8 File Offset: 0x0001FAE8
			private uint GetLen1()
			{
				return this.RG.GetRnd((int)(1U + this.RG.GetRnd(2)));
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x00021903 File Offset: 0x0001FB03
			private uint GetLen2()
			{
				return this.RG.GetRnd((int)(2U + this.RG.GetRnd(2)));
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x00021920 File Offset: 0x0001FB20
			public void Generate()
			{
				this.RG.Init();
				this.Rep0 = 1U;
				while (this.Pos < this.BufferSize)
				{
					if (this.GetRndBit() == 0U || this.Pos < 1U)
					{
						byte[] buffer = this.Buffer;
						uint pos = this.Pos;
						this.Pos = pos + 1U;
						buffer[(int)pos] = (byte)this.RG.GetRnd(8);
					}
					else
					{
						uint num;
						if (this.RG.GetRnd(3) == 0U)
						{
							num = 1U + this.GetLen1();
						}
						else
						{
							do
							{
								this.Rep0 = this.GetOffset();
							}
							while (this.Rep0 >= this.Pos);
							this.Rep0 += 1U;
							num = 2U + this.GetLen2();
						}
						uint num2 = 0U;
						while (num2 < num && this.Pos < this.BufferSize)
						{
							this.Buffer[(int)this.Pos] = this.Buffer[(int)(this.Pos - this.Rep0)];
							num2 += 1U;
							this.Pos += 1U;
						}
					}
				}
			}

			// Token: 0x04000481 RID: 1153
			private LzmaBench.CBitRandomGenerator RG = new LzmaBench.CBitRandomGenerator();

			// Token: 0x04000482 RID: 1154
			private uint Pos;

			// Token: 0x04000483 RID: 1155
			private uint Rep0;

			// Token: 0x04000484 RID: 1156
			public uint BufferSize;

			// Token: 0x04000485 RID: 1157
			public byte[] Buffer;
		}

		// Token: 0x02000100 RID: 256
		private class CrcOutStream : Stream
		{
			// Token: 0x06000617 RID: 1559 RVA: 0x00021A23 File Offset: 0x0001FC23
			public void Init()
			{
				this.CRC.Init();
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00021A30 File Offset: 0x0001FC30
			public uint GetDigest()
			{
				return this.CRC.GetDigest();
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000619 RID: 1561 RVA: 0x000036C8 File Offset: 0x000018C8
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x0600061A RID: 1562 RVA: 0x000036C8 File Offset: 0x000018C8
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001238F File Offset: 0x0001058F
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x0600061C RID: 1564 RVA: 0x000123B2 File Offset: 0x000105B2
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x0600061D RID: 1565 RVA: 0x000123B2 File Offset: 0x000105B2
			// (set) Token: 0x0600061E RID: 1566 RVA: 0x000123B6 File Offset: 0x000105B6
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x000123B6 File Offset: 0x000105B6
			public override void Flush()
			{
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x000123B2 File Offset: 0x000105B2
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x000123B6 File Offset: 0x000105B6
			public override void SetLength(long value)
			{
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x000036C8 File Offset: 0x000018C8
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x00021A3D File Offset: 0x0001FC3D
			public override void WriteByte(byte b)
			{
				this.CRC.UpdateByte(b);
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x00021A4B File Offset: 0x0001FC4B
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.CRC.Update(buffer, (uint)offset, (uint)count);
			}

			// Token: 0x04000486 RID: 1158
			public CRC CRC = new CRC();
		}

		// Token: 0x02000101 RID: 257
		private class CProgressInfo : ICodeProgress
		{
			// Token: 0x06000626 RID: 1574 RVA: 0x00021A6E File Offset: 0x0001FC6E
			public void Init()
			{
				this.InSize = 0L;
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x00021A78 File Offset: 0x0001FC78
			public void SetProgress(long inSize, long outSize)
			{
				if (inSize >= this.ApprovedStart && this.InSize == 0L)
				{
					this.Time = DateTime.UtcNow;
					this.InSize = inSize;
				}
			}

			// Token: 0x04000487 RID: 1159
			public long ApprovedStart;

			// Token: 0x04000488 RID: 1160
			public long InSize;

			// Token: 0x04000489 RID: 1161
			public DateTime Time;
		}
	}
}
