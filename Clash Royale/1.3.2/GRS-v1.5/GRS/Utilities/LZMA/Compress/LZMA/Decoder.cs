using System;
using System.IO;
using CRS.Utilities.LZMA.Compress.LZ;
using CRS.Utilities.LZMA.Compress.RangeCoder;

namespace CRS.Utilities.LZMA.Compress.LZMA
{
	// Token: 0x0200004D RID: 77
	public class Decoder : ICoder, ISetDecoderProperties
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000F844 File Offset: 0x0000DA44
		public Decoder()
		{
			this.m_DictionarySize = uint.MaxValue;
			int num = 0;
			while ((long)num < 4L)
			{
				this.m_PosSlotDecoder[num] = new BitTreeDecoder(6);
				num++;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000F930 File Offset: 0x0000DB30
		private void SetDictionarySize(uint dictionarySize)
		{
			if (this.m_DictionarySize != dictionarySize)
			{
				this.m_DictionarySize = dictionarySize;
				this.m_DictionarySizeCheck = Math.Max(this.m_DictionarySize, 1U);
				uint num = Math.Max(this.m_DictionarySizeCheck, 4096U);
				this.m_OutWindow.Create(num);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000F97C File Offset: 0x0000DB7C
		private void SetLiteralProperties(int lp, int lc)
		{
			if (lp > 8)
			{
				throw new InvalidParamException();
			}
			if (lc > 8)
			{
				throw new InvalidParamException();
			}
			this.m_LiteralDecoder.Create(lp, lc);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		private void SetPosBitsProperties(int pb)
		{
			if (pb > 4)
			{
				throw new InvalidParamException();
			}
			uint num = 1U << pb;
			this.m_LenDecoder.Create(num);
			this.m_RepLenDecoder.Create(num);
			this.m_PosStateMask = num - 1U;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
		private void Init(Stream inStream, Stream outStream)
		{
			this.m_RangeDecoder.Init(inStream);
			this.m_OutWindow.Init(outStream, this._solid);
			for (uint num = 0U; num < 12U; num += 1U)
			{
				for (uint num2 = 0U; num2 <= this.m_PosStateMask; num2 += 1U)
				{
					uint num3 = (num << 4) + num2;
					this.m_IsMatchDecoders[(int)num3].Init();
					this.m_IsRep0LongDecoders[(int)num3].Init();
				}
				this.m_IsRepDecoders[(int)num].Init();
				this.m_IsRepG0Decoders[(int)num].Init();
				this.m_IsRepG1Decoders[(int)num].Init();
				this.m_IsRepG2Decoders[(int)num].Init();
			}
			this.m_LiteralDecoder.Init();
			for (uint num = 0U; num < 4U; num += 1U)
			{
				this.m_PosSlotDecoder[(int)num].Init();
			}
			for (uint num = 0U; num < 114U; num += 1U)
			{
				this.m_PosDecoders[(int)num].Init();
			}
			this.m_LenDecoder.Init();
			this.m_RepLenDecoder.Init();
			this.m_PosAlignDecoder.Init();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000FB04 File Offset: 0x0000DD04
		public void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress)
		{
			this.Init(inStream, outStream);
			Base.State state = default(Base.State);
			state.Init();
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			ulong num5 = 0UL;
			if (num5 < (ulong)outSize)
			{
				if (this.m_IsMatchDecoders[(int)((int)state.Index << 4)].Decode(this.m_RangeDecoder) != 0U)
				{
					throw new DataErrorException();
				}
				state.UpdateChar();
				byte b = this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, 0U, 0);
				this.m_OutWindow.PutByte(b);
				num5 += 1UL;
			}
			while (num5 < (ulong)outSize)
			{
				uint num6 = (uint)num5 & this.m_PosStateMask;
				if (this.m_IsMatchDecoders[(int)((state.Index << 4) + num6)].Decode(this.m_RangeDecoder) == 0U)
				{
					byte @byte = this.m_OutWindow.GetByte(0U);
					byte b2;
					if (!state.IsCharState())
					{
						b2 = this.m_LiteralDecoder.DecodeWithMatchByte(this.m_RangeDecoder, (uint)num5, @byte, this.m_OutWindow.GetByte(num));
					}
					else
					{
						b2 = this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, (uint)num5, @byte);
					}
					this.m_OutWindow.PutByte(b2);
					state.UpdateChar();
					num5 += 1UL;
				}
				else
				{
					uint num8;
					if (this.m_IsRepDecoders[(int)state.Index].Decode(this.m_RangeDecoder) == 1U)
					{
						if (this.m_IsRepG0Decoders[(int)state.Index].Decode(this.m_RangeDecoder) == 0U)
						{
							if (this.m_IsRep0LongDecoders[(int)((state.Index << 4) + num6)].Decode(this.m_RangeDecoder) == 0U)
							{
								state.UpdateShortRep();
								this.m_OutWindow.PutByte(this.m_OutWindow.GetByte(num));
								num5 += 1UL;
								continue;
							}
						}
						else
						{
							uint num7;
							if (this.m_IsRepG1Decoders[(int)state.Index].Decode(this.m_RangeDecoder) == 0U)
							{
								num7 = num2;
							}
							else
							{
								if (this.m_IsRepG2Decoders[(int)state.Index].Decode(this.m_RangeDecoder) == 0U)
								{
									num7 = num3;
								}
								else
								{
									num7 = num4;
									num4 = num3;
								}
								num3 = num2;
							}
							num2 = num;
							num = num7;
						}
						num8 = this.m_RepLenDecoder.Decode(this.m_RangeDecoder, num6) + 2U;
						state.UpdateRep();
					}
					else
					{
						num4 = num3;
						num3 = num2;
						num2 = num;
						num8 = 2U + this.m_LenDecoder.Decode(this.m_RangeDecoder, num6);
						state.UpdateMatch();
						uint num9 = this.m_PosSlotDecoder[(int)Base.GetLenToPosState(num8)].Decode(this.m_RangeDecoder);
						if (num9 >= 4U)
						{
							int num10 = (int)((num9 >> 1) - 1U);
							num = (2U | (num9 & 1U)) << num10;
							if (num9 < 14U)
							{
								num += BitTreeDecoder.ReverseDecode(this.m_PosDecoders, num - num9 - 1U, this.m_RangeDecoder, num10);
							}
							else
							{
								num += this.m_RangeDecoder.DecodeDirectBits(num10 - 4) << 4;
								num += this.m_PosAlignDecoder.ReverseDecode(this.m_RangeDecoder);
							}
						}
						else
						{
							num = num9;
						}
					}
					if ((ulong)num >= (ulong)this.m_OutWindow.TrainSize + num5 || num >= this.m_DictionarySizeCheck)
					{
						if (num != 4294967295U)
						{
							throw new DataErrorException();
						}
						break;
					}
					else
					{
						this.m_OutWindow.CopyBlock(num, num8);
						num5 += (ulong)num8;
					}
				}
			}
			this.m_OutWindow.Flush();
			this.m_OutWindow.ReleaseStream();
			this.m_RangeDecoder.ReleaseStream();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000FE5C File Offset: 0x0000E05C
		public void SetDecoderProperties(byte[] properties)
		{
			if (properties.Length < 5)
			{
				throw new InvalidParamException();
			}
			int num = (int)(properties[0] % 9);
			byte b = (byte)(properties[0] / 9);
			int num2 = (int)(b % 5);
			int num3 = (int)(b / 5);
			if (num3 > 4)
			{
				throw new InvalidParamException();
			}
			uint num4 = 0U;
			for (int i = 0; i < 4; i++)
			{
				num4 += (uint)((uint)properties[1 + i] << i * 8);
			}
			this.SetDictionarySize(num4);
			this.SetLiteralProperties(num2, num);
			this.SetPosBitsProperties(num3);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000FECC File Offset: 0x0000E0CC
		public bool Train(Stream stream)
		{
			this._solid = true;
			return this.m_OutWindow.Train(stream);
		}

		// Token: 0x040001FE RID: 510
		private OutWindow m_OutWindow = new OutWindow();

		// Token: 0x040001FF RID: 511
		private RangeCoder.Decoder m_RangeDecoder = new RangeCoder.Decoder();

		// Token: 0x04000200 RID: 512
		private BitDecoder[] m_IsMatchDecoders = new BitDecoder[192];

		// Token: 0x04000201 RID: 513
		private BitDecoder[] m_IsRepDecoders = new BitDecoder[12];

		// Token: 0x04000202 RID: 514
		private BitDecoder[] m_IsRepG0Decoders = new BitDecoder[12];

		// Token: 0x04000203 RID: 515
		private BitDecoder[] m_IsRepG1Decoders = new BitDecoder[12];

		// Token: 0x04000204 RID: 516
		private BitDecoder[] m_IsRepG2Decoders = new BitDecoder[12];

		// Token: 0x04000205 RID: 517
		private BitDecoder[] m_IsRep0LongDecoders = new BitDecoder[192];

		// Token: 0x04000206 RID: 518
		private BitTreeDecoder[] m_PosSlotDecoder = new BitTreeDecoder[4];

		// Token: 0x04000207 RID: 519
		private BitDecoder[] m_PosDecoders = new BitDecoder[114];

		// Token: 0x04000208 RID: 520
		private BitTreeDecoder m_PosAlignDecoder = new BitTreeDecoder(4);

		// Token: 0x04000209 RID: 521
		private Decoder.LenDecoder m_LenDecoder = new Decoder.LenDecoder();

		// Token: 0x0400020A RID: 522
		private Decoder.LenDecoder m_RepLenDecoder = new Decoder.LenDecoder();

		// Token: 0x0400020B RID: 523
		private Decoder.LiteralDecoder m_LiteralDecoder = new Decoder.LiteralDecoder();

		// Token: 0x0400020C RID: 524
		private uint m_DictionarySize;

		// Token: 0x0400020D RID: 525
		private uint m_DictionarySizeCheck;

		// Token: 0x0400020E RID: 526
		private uint m_PosStateMask;

		// Token: 0x0400020F RID: 527
		private bool _solid;

		// Token: 0x020000F5 RID: 245
		private class LenDecoder
		{
			// Token: 0x060005ED RID: 1517 RVA: 0x000210DC File Offset: 0x0001F2DC
			public void Create(uint numPosStates)
			{
				for (uint num = this.m_NumPosStates; num < numPosStates; num += 1U)
				{
					this.m_LowCoder[(int)num] = new BitTreeDecoder(3);
					this.m_MidCoder[(int)num] = new BitTreeDecoder(3);
				}
				this.m_NumPosStates = numPosStates;
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x00021128 File Offset: 0x0001F328
			public void Init()
			{
				this.m_Choice.Init();
				for (uint num = 0U; num < this.m_NumPosStates; num += 1U)
				{
					this.m_LowCoder[(int)num].Init();
					this.m_MidCoder[(int)num].Init();
				}
				this.m_Choice2.Init();
				this.m_HighCoder.Init();
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x0002118C File Offset: 0x0001F38C
			public uint Decode(RangeCoder.Decoder rangeDecoder, uint posState)
			{
				if (this.m_Choice.Decode(rangeDecoder) == 0U)
				{
					return this.m_LowCoder[(int)posState].Decode(rangeDecoder);
				}
				uint num = 8U;
				if (this.m_Choice2.Decode(rangeDecoder) == 0U)
				{
					num += this.m_MidCoder[(int)posState].Decode(rangeDecoder);
				}
				else
				{
					num += 8U;
					num += this.m_HighCoder.Decode(rangeDecoder);
				}
				return num;
			}

			// Token: 0x04000449 RID: 1097
			private BitDecoder m_Choice;

			// Token: 0x0400044A RID: 1098
			private BitDecoder m_Choice2;

			// Token: 0x0400044B RID: 1099
			private BitTreeDecoder[] m_LowCoder = new BitTreeDecoder[16];

			// Token: 0x0400044C RID: 1100
			private BitTreeDecoder[] m_MidCoder = new BitTreeDecoder[16];

			// Token: 0x0400044D RID: 1101
			private BitTreeDecoder m_HighCoder = new BitTreeDecoder(8);

			// Token: 0x0400044E RID: 1102
			private uint m_NumPosStates;
		}

		// Token: 0x020000F6 RID: 246
		private class LiteralDecoder
		{
			// Token: 0x060005F1 RID: 1521 RVA: 0x00021224 File Offset: 0x0001F424
			public void Create(int numPosBits, int numPrevBits)
			{
				if (this.m_Coders != null && this.m_NumPrevBits == numPrevBits && this.m_NumPosBits == numPosBits)
				{
					return;
				}
				this.m_NumPosBits = numPosBits;
				this.m_PosMask = (1U << numPosBits) - 1U;
				this.m_NumPrevBits = numPrevBits;
				uint num = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				this.m_Coders = new Decoder.LiteralDecoder.Decoder2[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					this.m_Coders[(int)num2].Create();
				}
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x000212A4 File Offset: 0x0001F4A4
			public void Init()
			{
				uint num = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					this.m_Coders[(int)num2].Init();
				}
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x000212E1 File Offset: 0x0001F4E1
			private uint GetState(uint pos, byte prevByte)
			{
				return ((pos & this.m_PosMask) << this.m_NumPrevBits) + (uint)(prevByte >> 8 - this.m_NumPrevBits);
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x00021303 File Offset: 0x0001F503
			public byte DecodeNormal(RangeCoder.Decoder rangeDecoder, uint pos, byte prevByte)
			{
				return this.m_Coders[(int)this.GetState(pos, prevByte)].DecodeNormal(rangeDecoder);
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x0002131E File Offset: 0x0001F51E
			public byte DecodeWithMatchByte(RangeCoder.Decoder rangeDecoder, uint pos, byte prevByte, byte matchByte)
			{
				return this.m_Coders[(int)this.GetState(pos, prevByte)].DecodeWithMatchByte(rangeDecoder, matchByte);
			}

			// Token: 0x0400044F RID: 1103
			private Decoder.LiteralDecoder.Decoder2[] m_Coders;

			// Token: 0x04000450 RID: 1104
			private int m_NumPrevBits;

			// Token: 0x04000451 RID: 1105
			private int m_NumPosBits;

			// Token: 0x04000452 RID: 1106
			private uint m_PosMask;

			// Token: 0x02000133 RID: 307
			private struct Decoder2
			{
				// Token: 0x06000672 RID: 1650 RVA: 0x00021ECE File Offset: 0x000200CE
				public void Create()
				{
					this.m_Decoders = new BitDecoder[768];
				}

				// Token: 0x06000673 RID: 1651 RVA: 0x00021EE0 File Offset: 0x000200E0
				public void Init()
				{
					for (int i = 0; i < 768; i++)
					{
						this.m_Decoders[i].Init();
					}
				}

				// Token: 0x06000674 RID: 1652 RVA: 0x00021F10 File Offset: 0x00020110
				public byte DecodeNormal(RangeCoder.Decoder rangeDecoder)
				{
					uint num = 1U;
					do
					{
						num = (num << 1) | this.m_Decoders[(int)num].Decode(rangeDecoder);
					}
					while (num < 256U);
					return (byte)num;
				}

				// Token: 0x06000675 RID: 1653 RVA: 0x00021F40 File Offset: 0x00020140
				public byte DecodeWithMatchByte(RangeCoder.Decoder rangeDecoder, byte matchByte)
				{
					uint num = 1U;
					for (; ; )
					{
						uint num2 = (uint)((matchByte >> 7) & 1);
						matchByte = (byte)(matchByte << 1);
						uint num3 = this.m_Decoders[(int)((1U + num2 << 8) + num)].Decode(rangeDecoder);
						num = (num << 1) | num3;
						if (num2 != num3)
						{
							break;
						}
						if (num >= 256U)
						{
							goto IL_005C;
						}
					}
					while (num < 256U)
					{
						num = (num << 1) | this.m_Decoders[(int)num].Decode(rangeDecoder);
					}
				IL_005C:
					return (byte)num;
				}

				// Token: 0x040004C4 RID: 1220
				private BitDecoder[] m_Decoders;
			}
		}
	}
}