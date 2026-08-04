using System;
using System.IO;
using CRS.Utilities.LZMA.Compress.LZ;
using CRS.Utilities.LZMA.Compress.RangeCoder;

namespace CRS.Utilities.LZMA.Compress.LZMA
{
	// Token: 0x0200004E RID: 78
	public class Encoder : ICoder, ISetCoderProperties, IWriteCoderProperties
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
		static Encoder()
		{
			int num = 2;
			Encoder.g_FastPos[0] = 0;
			Encoder.g_FastPos[1] = 1;
			for (byte b = 2; b < 22; b += 1)
			{
				uint num2 = 1U << (b >> 1) - 1;
				uint num3 = 0U;
				while (num3 < num2)
				{
					Encoder.g_FastPos[num] = b;
					num3 += 1U;
					num++;
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000FF5E File Offset: 0x0000E15E
		private static uint GetPosSlot(uint pos)
		{
			if (pos < 2048U)
			{
				return (uint)Encoder.g_FastPos[(int)pos];
			}
			if (pos < 2097152U)
			{
				return (uint)(Encoder.g_FastPos[(int)(pos >> 10)] + 20);
			}
			return (uint)(Encoder.g_FastPos[(int)(pos >> 20)] + 40);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000FF93 File Offset: 0x0000E193
		private static uint GetPosSlot2(uint pos)
		{
			if (pos < 131072U)
			{
				return (uint)(Encoder.g_FastPos[(int)(pos >> 6)] + 12);
			}
			if (pos < 134217728U)
			{
				return (uint)(Encoder.g_FastPos[(int)(pos >> 16)] + 32);
			}
			return (uint)(Encoder.g_FastPos[(int)(pos >> 26)] + 52);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		private void BaseInit()
		{
			this._state.Init();
			this._previoubyte = 0;
			for (uint num = 0U; num < 4U; num += 1U)
			{
				this._repDistances[(int)num] = 0U;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00010004 File Offset: 0x0000E204
		private void Create()
		{
			if (this._matchFinder == null)
			{
				BinTree binTree = new BinTree();
				int num = 4;
				if (this._matchFinderType == Encoder.EMatchFinderType.BT2)
				{
					num = 2;
				}
				binTree.SetType(num);
				this._matchFinder = binTree;
			}
			this._literalEncoder.Create(this._numLiteralPosStateBits, this._numLiteralContextBits);
			if (this._dictionarySize == this._dictionarySizePrev && this._numFastBytesPrev == this._numFastBytes)
			{
				return;
			}
			this._matchFinder.Create(this._dictionarySize, 4096U, this._numFastBytes, 274U);
			this._dictionarySizePrev = this._dictionarySize;
			this._numFastBytesPrev = this._numFastBytes;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000100A8 File Offset: 0x0000E2A8
		public Encoder()
		{
			int num = 0;
			while ((long)num < 4096L)
			{
				this._optimum[num] = new Encoder.Optimal();
				num++;
			}
			int num2 = 0;
			while ((long)num2 < 4L)
			{
				this._posSlotEncoder[num2] = new BitTreeEncoder(6);
				num2++;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00010271 File Offset: 0x0000E471
		private void SetWriteEndMarkerMode(bool writeEndMarker)
		{
			this._writeEndMark = writeEndMarker;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001027C File Offset: 0x0000E47C
		private void Init()
		{
			this.BaseInit();
			this._rangeEncoder.Init();
			for (uint num = 0U; num < 12U; num += 1U)
			{
				for (uint num2 = 0U; num2 <= this._posStateMask; num2 += 1U)
				{
					uint num3 = (num << 4) + num2;
					this._isMatch[(int)num3].Init();
					this._isRep0Long[(int)num3].Init();
				}
				this._isRep[(int)num].Init();
				this._isRepG0[(int)num].Init();
				this._isRepG1[(int)num].Init();
				this._isRepG2[(int)num].Init();
			}
			this._literalEncoder.Init();
			for (uint num = 0U; num < 4U; num += 1U)
			{
				this._posSlotEncoder[(int)num].Init();
			}
			for (uint num = 0U; num < 114U; num += 1U)
			{
				this._posEncoders[(int)num].Init();
			}
			this._lenEncoder.Init(1U << this._posStateBits);
			this._repMatchLenEncoder.Init(1U << this._posStateBits);
			this._posAlignEncoder.Init();
			this._longestMatchWasFound = false;
			this._optimumEndIndex = 0U;
			this._optimumCurrentIndex = 0U;
			this._additionalOffset = 0U;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000103C4 File Offset: 0x0000E5C4
		private void ReadMatchDistances(out uint lenRes, out uint numDistancePairs)
		{
			lenRes = 0U;
			numDistancePairs = this._matchFinder.GetMatches(this._matchDistances);
			if (numDistancePairs > 0U)
			{
				lenRes = this._matchDistances[(int)(numDistancePairs - 2U)];
				if (lenRes == this._numFastBytes)
				{
					lenRes += this._matchFinder.GetMatchLen((int)(lenRes - 1U), this._matchDistances[(int)(numDistancePairs - 1U)], 273U - lenRes);
				}
			}
			this._additionalOffset += 1U;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00010438 File Offset: 0x0000E638
		private void MovePos(uint num)
		{
			if (num > 0U)
			{
				this._matchFinder.Skip(num);
				this._additionalOffset += num;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00010458 File Offset: 0x0000E658
		private uint GetRepLen1Price(Base.State state, uint posState)
		{
			return this._isRepG0[(int)state.Index].GetPrice0() + this._isRep0Long[(int)((state.Index << 4) + posState)].GetPrice0();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0001048C File Offset: 0x0000E68C
		private uint GetPureRepPrice(uint repIndex, Base.State state, uint posState)
		{
			uint num;
			if (repIndex == 0U)
			{
				num = this._isRepG0[(int)state.Index].GetPrice0();
				num += this._isRep0Long[(int)((state.Index << 4) + posState)].GetPrice1();
			}
			else
			{
				num = this._isRepG0[(int)state.Index].GetPrice1();
				if (repIndex == 1U)
				{
					num += this._isRepG1[(int)state.Index].GetPrice0();
				}
				else
				{
					num += this._isRepG1[(int)state.Index].GetPrice1();
					num += this._isRepG2[(int)state.Index].GetPrice(repIndex - 2U);
				}
			}
			return num;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0001053E File Offset: 0x0000E73E
		private uint GetRepPrice(uint repIndex, uint len, Base.State state, uint posState)
		{
			return this._repMatchLenEncoder.GetPrice(len - 2U, posState) + this.GetPureRepPrice(repIndex, state, posState);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0001055C File Offset: 0x0000E75C
		private uint GetPosLenPrice(uint pos, uint len, uint posState)
		{
			uint lenToPosState = Base.GetLenToPosState(len);
			uint num;
			if (pos < 128U)
			{
				num = this._distancesPrices[(int)(lenToPosState * 128U + pos)];
			}
			else
			{
				num = this._posSlotPrices[(int)((lenToPosState << 6) + Encoder.GetPosSlot2(pos))] + this._alignPrices[(int)(pos & 15U)];
			}
			return num + this._lenEncoder.GetPrice(len - 2U, posState);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000105BC File Offset: 0x0000E7BC
		private uint Backward(out uint backRes, uint cur)
		{
			this._optimumEndIndex = cur;
			uint num = this._optimum[(int)cur].PosPrev;
			uint num2 = this._optimum[(int)cur].BackPrev;
			do
			{
				if (this._optimum[(int)cur].Prev1IsChar)
				{
					this._optimum[(int)num].MakeAsChar();
					this._optimum[(int)num].PosPrev = num - 1U;
					if (this._optimum[(int)cur].Prev2)
					{
						this._optimum[(int)(num - 1U)].Prev1IsChar = false;
						this._optimum[(int)(num - 1U)].PosPrev = this._optimum[(int)cur].PosPrev2;
						this._optimum[(int)(num - 1U)].BackPrev = this._optimum[(int)cur].BackPrev2;
					}
				}
				uint num3 = num;
				uint num4 = num2;
				num2 = this._optimum[(int)num3].BackPrev;
				num = this._optimum[(int)num3].PosPrev;
				this._optimum[(int)num3].BackPrev = num4;
				this._optimum[(int)num3].PosPrev = cur;
				cur = num3;
			}
			while (cur > 0U);
			backRes = this._optimum[0].BackPrev;
			this._optimumCurrentIndex = this._optimum[0].PosPrev;
			return this._optimumCurrentIndex;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000106E0 File Offset: 0x0000E8E0
		private uint GetOptimum(uint position, out uint backRes)
		{
			if (this._optimumEndIndex != this._optimumCurrentIndex)
			{
				uint num = this._optimum[(int)this._optimumCurrentIndex].PosPrev - this._optimumCurrentIndex;
				backRes = this._optimum[(int)this._optimumCurrentIndex].BackPrev;
				this._optimumCurrentIndex = this._optimum[(int)this._optimumCurrentIndex].PosPrev;
				return num;
			}
			this._optimumCurrentIndex = (this._optimumEndIndex = 0U);
			uint longestMatchLength;
			uint num2;
			if (!this._longestMatchWasFound)
			{
				this.ReadMatchDistances(out longestMatchLength, out num2);
			}
			else
			{
				longestMatchLength = this._longestMatchLength;
				num2 = this._numDistancePairs;
				this._longestMatchWasFound = false;
			}
			uint num3 = this._matchFinder.GetNumAvailableBytes() + 1U;
			if (num3 < 2U)
			{
				backRes = uint.MaxValue;
				return 1U;
			}
			if (num3 > 273U)
			{
			}
			uint num4 = 0U;
			for (uint num5 = 0U; num5 < 4U; num5 += 1U)
			{
				this.reps[(int)num5] = this._repDistances[(int)num5];
				this.repLens[(int)num5] = this._matchFinder.GetMatchLen(-1, this.reps[(int)num5], 273U);
				if (this.repLens[(int)num5] > this.repLens[(int)num4])
				{
					num4 = num5;
				}
			}
			if (this.repLens[(int)num4] >= this._numFastBytes)
			{
				backRes = num4;
				uint num6 = this.repLens[(int)num4];
				this.MovePos(num6 - 1U);
				return num6;
			}
			if (longestMatchLength >= this._numFastBytes)
			{
				backRes = this._matchDistances[(int)(num2 - 1U)] + 4U;
				this.MovePos(longestMatchLength - 1U);
				return longestMatchLength;
			}
			byte b = this._matchFinder.GetIndexByte(-1);
			byte b2 = this._matchFinder.GetIndexByte((int)(0U - this._repDistances[0] - 1U - 1U));
			if (longestMatchLength < 2U && b != b2 && this.repLens[(int)num4] < 2U)
			{
				backRes = uint.MaxValue;
				return 1U;
			}
			this._optimum[0].State = this._state;
			uint num7 = position & this._posStateMask;
			this._optimum[1].Price = this._isMatch[(int)((this._state.Index << 4) + num7)].GetPrice0() + this._literalEncoder.GetSubCoder(position, this._previoubyte).GetPrice(!this._state.IsCharState(), b2, b);
			this._optimum[1].MakeAsChar();
			uint num8 = this._isMatch[(int)((this._state.Index << 4) + num7)].GetPrice1();
			uint num9 = num8 + this._isRep[(int)this._state.Index].GetPrice1();
			if (b2 == b)
			{
				uint num10 = num9 + this.GetRepLen1Price(this._state, num7);
				if (num10 < this._optimum[1].Price)
				{
					this._optimum[1].Price = num10;
					this._optimum[1].MakeAsShortRep();
				}
			}
			uint num11 = ((longestMatchLength >= this.repLens[(int)num4]) ? longestMatchLength : this.repLens[(int)num4]);
			if (num11 < 2U)
			{
				backRes = this._optimum[1].BackPrev;
				return 1U;
			}
			this._optimum[1].PosPrev = 0U;
			this._optimum[0].Backs0 = this.reps[0];
			this._optimum[0].Backs1 = this.reps[1];
			this._optimum[0].Backs2 = this.reps[2];
			this._optimum[0].Backs3 = this.reps[3];
			uint num12 = num11;
			do
			{
				this._optimum[(int)num12--].Price = 268435455U;
			}
			while (num12 >= 2U);
			for (uint num5 = 0U; num5 < 4U; num5 += 1U)
			{
				uint num13 = this.repLens[(int)num5];
				if (num13 >= 2U)
				{
					uint num14 = num9 + this.GetPureRepPrice(num5, this._state, num7);
					do
					{
						uint num15 = num14 + this._repMatchLenEncoder.GetPrice(num13 - 2U, num7);
						Encoder.Optimal optimal = this._optimum[(int)num13];
						if (num15 < optimal.Price)
						{
							optimal.Price = num15;
							optimal.PosPrev = 0U;
							optimal.BackPrev = num5;
							optimal.Prev1IsChar = false;
						}
					}
					while ((num13 -= 1U) >= 2U);
				}
			}
			uint num16 = num8 + this._isRep[(int)this._state.Index].GetPrice0();
			num12 = ((this.repLens[0] >= 2U) ? (this.repLens[0] + 1U) : 2U);
			if (num12 <= longestMatchLength)
			{
				uint num17 = 0U;
				while (num12 > this._matchDistances[(int)num17])
				{
					num17 += 2U;
				}
				for (; ; )
				{
					uint num18 = this._matchDistances[(int)(num17 + 1U)];
					uint num19 = num16 + this.GetPosLenPrice(num18, num12, num7);
					Encoder.Optimal optimal2 = this._optimum[(int)num12];
					if (num19 < optimal2.Price)
					{
						optimal2.Price = num19;
						optimal2.PosPrev = 0U;
						optimal2.BackPrev = num18 + 4U;
						optimal2.Prev1IsChar = false;
					}
					if (num12 == this._matchDistances[(int)num17])
					{
						num17 += 2U;
						if (num17 == num2)
						{
							break;
						}
					}
					num12 += 1U;
				}
			}
			uint num20 = 0U;
			uint num21;
			for (; ; )
			{
				num20 += 1U;
				if (num20 == num11)
				{
					break;
				}
				this.ReadMatchDistances(out num21, out num2);
				if (num21 >= this._numFastBytes)
				{
					goto Block_24;
				}
				position += 1U;
				uint num22 = this._optimum[(int)num20].PosPrev;
				Base.State state;
				if (this._optimum[(int)num20].Prev1IsChar)
				{
					num22 -= 1U;
					if (this._optimum[(int)num20].Prev2)
					{
						state = this._optimum[(int)this._optimum[(int)num20].PosPrev2].State;
						if (this._optimum[(int)num20].BackPrev2 < 4U)
						{
							state.UpdateRep();
						}
						else
						{
							state.UpdateMatch();
						}
					}
					else
					{
						state = this._optimum[(int)num22].State;
					}
					state.UpdateChar();
				}
				else
				{
					state = this._optimum[(int)num22].State;
				}
				if (num22 == num20 - 1U)
				{
					if (this._optimum[(int)num20].IsShortRep())
					{
						state.UpdateShortRep();
					}
					else
					{
						state.UpdateChar();
					}
				}
				else
				{
					uint num23;
					if (this._optimum[(int)num20].Prev1IsChar && this._optimum[(int)num20].Prev2)
					{
						num22 = this._optimum[(int)num20].PosPrev2;
						num23 = this._optimum[(int)num20].BackPrev2;
						state.UpdateRep();
					}
					else
					{
						num23 = this._optimum[(int)num20].BackPrev;
						if (num23 < 4U)
						{
							state.UpdateRep();
						}
						else
						{
							state.UpdateMatch();
						}
					}
					Encoder.Optimal optimal3 = this._optimum[(int)num22];
					if (num23 < 4U)
					{
						if (num23 == 0U)
						{
							this.reps[0] = optimal3.Backs0;
							this.reps[1] = optimal3.Backs1;
							this.reps[2] = optimal3.Backs2;
							this.reps[3] = optimal3.Backs3;
						}
						else if (num23 == 1U)
						{
							this.reps[0] = optimal3.Backs1;
							this.reps[1] = optimal3.Backs0;
							this.reps[2] = optimal3.Backs2;
							this.reps[3] = optimal3.Backs3;
						}
						else if (num23 == 2U)
						{
							this.reps[0] = optimal3.Backs2;
							this.reps[1] = optimal3.Backs0;
							this.reps[2] = optimal3.Backs1;
							this.reps[3] = optimal3.Backs3;
						}
						else
						{
							this.reps[0] = optimal3.Backs3;
							this.reps[1] = optimal3.Backs0;
							this.reps[2] = optimal3.Backs1;
							this.reps[3] = optimal3.Backs2;
						}
					}
					else
					{
						this.reps[0] = num23 - 4U;
						this.reps[1] = optimal3.Backs0;
						this.reps[2] = optimal3.Backs1;
						this.reps[3] = optimal3.Backs2;
					}
				}
				this._optimum[(int)num20].State = state;
				this._optimum[(int)num20].Backs0 = this.reps[0];
				this._optimum[(int)num20].Backs1 = this.reps[1];
				this._optimum[(int)num20].Backs2 = this.reps[2];
				this._optimum[(int)num20].Backs3 = this.reps[3];
				uint price = this._optimum[(int)num20].Price;
				b = this._matchFinder.GetIndexByte(-1);
				b2 = this._matchFinder.GetIndexByte((int)(0U - this.reps[0] - 1U - 1U));
				num7 = position & this._posStateMask;
				uint num24 = price + this._isMatch[(int)((state.Index << 4) + num7)].GetPrice0() + this._literalEncoder.GetSubCoder(position, this._matchFinder.GetIndexByte(-2)).GetPrice(!state.IsCharState(), b2, b);
				Encoder.Optimal optimal4 = this._optimum[(int)(num20 + 1U)];
				bool flag = false;
				if (num24 < optimal4.Price)
				{
					optimal4.Price = num24;
					optimal4.PosPrev = num20;
					optimal4.MakeAsChar();
					flag = true;
				}
				num8 = price + this._isMatch[(int)((state.Index << 4) + num7)].GetPrice1();
				num9 = num8 + this._isRep[(int)state.Index].GetPrice1();
				if (b2 == b && (optimal4.PosPrev >= num20 || optimal4.BackPrev != 0U))
				{
					uint num25 = num9 + this.GetRepLen1Price(state, num7);
					if (num25 <= optimal4.Price)
					{
						optimal4.Price = num25;
						optimal4.PosPrev = num20;
						optimal4.MakeAsShortRep();
						flag = true;
					}
				}
				uint num26 = this._matchFinder.GetNumAvailableBytes() + 1U;
				num26 = Math.Min(4095U - num20, num26);
				num3 = num26;
				if (num3 >= 2U)
				{
					if (num3 > this._numFastBytes)
					{
						num3 = this._numFastBytes;
					}
					if (!flag && b2 != b)
					{
						uint num27 = Math.Min(num26 - 1U, this._numFastBytes);
						uint matchLen = this._matchFinder.GetMatchLen(0, this.reps[0], num27);
						if (matchLen >= 2U)
						{
							Base.State state2 = state;
							state2.UpdateChar();
							uint num28 = (position + 1U) & this._posStateMask;
							uint num29 = num24 + this._isMatch[(int)((state2.Index << 4) + num28)].GetPrice1() + this._isRep[(int)state2.Index].GetPrice1();
							uint num30 = num20 + 1U + matchLen;
							while (num11 < num30)
							{
								this._optimum[(int)(num11 += 1U)].Price = 268435455U;
							}
							uint num31 = num29 + this.GetRepPrice(0U, matchLen, state2, num28);
							Encoder.Optimal optimal5 = this._optimum[(int)num30];
							if (num31 < optimal5.Price)
							{
								optimal5.Price = num31;
								optimal5.PosPrev = num20 + 1U;
								optimal5.BackPrev = 0U;
								optimal5.Prev1IsChar = true;
								optimal5.Prev2 = false;
							}
						}
					}
					uint num32 = 2U;
					for (uint num33 = 0U; num33 < 4U; num33 += 1U)
					{
						uint num34 = this._matchFinder.GetMatchLen(-1, this.reps[(int)num33], num3);
						if (num34 >= 2U)
						{
							uint num35 = num34;
							for (; ; )
							{
								if (num11 >= num20 + num34)
								{
									uint num36 = num9 + this.GetRepPrice(num33, num34, state, num7);
									Encoder.Optimal optimal6 = this._optimum[(int)(num20 + num34)];
									if (num36 < optimal6.Price)
									{
										optimal6.Price = num36;
										optimal6.PosPrev = num20;
										optimal6.BackPrev = num33;
										optimal6.Prev1IsChar = false;
									}
									if ((num34 -= 1U) < 2U)
									{
										break;
									}
								}
								else
								{
									this._optimum[(int)(num11 += 1U)].Price = 268435455U;
								}
							}
							num34 = num35;
							if (num33 == 0U)
							{
								num32 = num34 + 1U;
							}
							if (num34 < num26)
							{
								uint num37 = Math.Min(num26 - 1U - num34, this._numFastBytes);
								uint matchLen2 = this._matchFinder.GetMatchLen((int)num34, this.reps[(int)num33], num37);
								if (matchLen2 >= 2U)
								{
									Base.State state3 = state;
									state3.UpdateRep();
									uint num38 = (position + num34) & this._posStateMask;
									uint num39 = num9 + this.GetRepPrice(num33, num34, state, num7) + this._isMatch[(int)((state3.Index << 4) + num38)].GetPrice0() + this._literalEncoder.GetSubCoder(position + num34, this._matchFinder.GetIndexByte((int)(num34 - 1U - 1U))).GetPrice(true, this._matchFinder.GetIndexByte((int)(num34 - 1U - (this.reps[(int)num33] + 1U))), this._matchFinder.GetIndexByte((int)(num34 - 1U)));
									state3.UpdateChar();
									num38 = (position + num34 + 1U) & this._posStateMask;
									uint num40 = num39 + this._isMatch[(int)((state3.Index << 4) + num38)].GetPrice1() + this._isRep[(int)state3.Index].GetPrice1();
									uint num41 = num34 + 1U + matchLen2;
									while (num11 < num20 + num41)
									{
										this._optimum[(int)(num11 += 1U)].Price = 268435455U;
									}
									uint num42 = num40 + this.GetRepPrice(0U, matchLen2, state3, num38);
									Encoder.Optimal optimal7 = this._optimum[(int)(num20 + num41)];
									if (num42 < optimal7.Price)
									{
										optimal7.Price = num42;
										optimal7.PosPrev = num20 + num34 + 1U;
										optimal7.BackPrev = 0U;
										optimal7.Prev1IsChar = true;
										optimal7.Prev2 = true;
										optimal7.PosPrev2 = num20;
										optimal7.BackPrev2 = num33;
									}
								}
							}
						}
					}
					if (num21 > num3)
					{
						num21 = num3;
						num2 = 0U;
						while (num21 > this._matchDistances[(int)num2])
						{
							num2 += 2U;
						}
						this._matchDistances[(int)num2] = num21;
						num2 += 2U;
					}
					if (num21 >= num32)
					{
						num16 = num8 + this._isRep[(int)state.Index].GetPrice0();
						while (num11 < num20 + num21)
						{
							this._optimum[(int)(num11 += 1U)].Price = 268435455U;
						}
						uint num43 = 0U;
						while (num32 > this._matchDistances[(int)num43])
						{
							num43 += 2U;
						}
						uint num44 = num32;
						for (; ; )
						{
							uint num45 = this._matchDistances[(int)(num43 + 1U)];
							uint num46 = num16 + this.GetPosLenPrice(num45, num44, num7);
							Encoder.Optimal optimal8 = this._optimum[(int)(num20 + num44)];
							if (num46 < optimal8.Price)
							{
								optimal8.Price = num46;
								optimal8.PosPrev = num20;
								optimal8.BackPrev = num45 + 4U;
								optimal8.Prev1IsChar = false;
							}
							if (num44 == this._matchDistances[(int)num43])
							{
								if (num44 < num26)
								{
									uint num47 = Math.Min(num26 - 1U - num44, this._numFastBytes);
									uint matchLen3 = this._matchFinder.GetMatchLen((int)num44, num45, num47);
									if (matchLen3 >= 2U)
									{
										Base.State state4 = state;
										state4.UpdateMatch();
										uint num48 = (position + num44) & this._posStateMask;
										uint num49 = num46 + this._isMatch[(int)((state4.Index << 4) + num48)].GetPrice0() + this._literalEncoder.GetSubCoder(position + num44, this._matchFinder.GetIndexByte((int)(num44 - 1U - 1U))).GetPrice(true, this._matchFinder.GetIndexByte((int)(num44 - (num45 + 1U) - 1U)), this._matchFinder.GetIndexByte((int)(num44 - 1U)));
										state4.UpdateChar();
										num48 = (position + num44 + 1U) & this._posStateMask;
										uint num50 = num49 + this._isMatch[(int)((state4.Index << 4) + num48)].GetPrice1() + this._isRep[(int)state4.Index].GetPrice1();
										uint num51 = num44 + 1U + matchLen3;
										while (num11 < num20 + num51)
										{
											this._optimum[(int)(num11 += 1U)].Price = 268435455U;
										}
										num46 = num50 + this.GetRepPrice(0U, matchLen3, state4, num48);
										optimal8 = this._optimum[(int)(num20 + num51)];
										if (num46 < optimal8.Price)
										{
											optimal8.Price = num46;
											optimal8.PosPrev = num20 + num44 + 1U;
											optimal8.BackPrev = 0U;
											optimal8.Prev1IsChar = true;
											optimal8.Prev2 = true;
											optimal8.PosPrev2 = num20;
											optimal8.BackPrev2 = num45 + 4U;
										}
									}
								}
								num43 += 2U;
								if (num43 == num2)
								{
									break;
								}
							}
							num44 += 1U;
						}
					}
				}
			}
			return this.Backward(out backRes, num20);
		Block_24:
			this._numDistancePairs = num2;
			this._longestMatchLength = num21;
			this._longestMatchWasFound = true;
			return this.Backward(out backRes, num20);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000116D6 File Offset: 0x0000F8D6
		private bool ChangePair(uint smallDist, uint bigDist)
		{
			return smallDist < 33554432U && bigDist >= smallDist << 7;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000116EC File Offset: 0x0000F8EC
		private void WriteEndMarker(uint posState)
		{
			if (!this._writeEndMark)
			{
				return;
			}
			this._isMatch[(int)((this._state.Index << 4) + posState)].Encode(this._rangeEncoder, 1U);
			this._isRep[(int)this._state.Index].Encode(this._rangeEncoder, 0U);
			this._state.UpdateMatch();
			uint num = 2U;
			this._lenEncoder.Encode(this._rangeEncoder, num - 2U, posState);
			uint num2 = 63U;
			uint lenToPosState = Base.GetLenToPosState(num);
			this._posSlotEncoder[(int)lenToPosState].Encode(this._rangeEncoder, num2);
			int num3 = 30;
			uint num4 = (1U << num3) - 1U;
			this._rangeEncoder.EncodeDirectBits(num4 >> 4, num3 - 4);
			this._posAlignEncoder.ReverseEncode(this._rangeEncoder, num4 & 15U);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000117C3 File Offset: 0x0000F9C3
		private void Flush(uint nowPos)
		{
			this.ReleaseMFStream();
			this.WriteEndMarker(nowPos & this._posStateMask);
			this._rangeEncoder.FlushData();
			this._rangeEncoder.FlushStream();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000117F0 File Offset: 0x0000F9F0
		public void CodeOneBlock(out long inSize, out long outSize, out bool finished)
		{
			inSize = 0L;
			outSize = 0L;
			finished = true;
			if (this._inStream != null)
			{
				this._matchFinder.SetStream(this._inStream);
				this._matchFinder.Init();
				this._needReleaseMFStream = true;
				this._inStream = null;
				if (this._trainSize > 0U)
				{
					this._matchFinder.Skip(this._trainSize);
				}
			}
			if (this._finished)
			{
				return;
			}
			this._finished = true;
			long num = this.nowPos64;
			if (this.nowPos64 == 0L)
			{
				if (this._matchFinder.GetNumAvailableBytes() == 0U)
				{
					this.Flush((uint)this.nowPos64);
					return;
				}
				uint num2;
				uint num3;
				this.ReadMatchDistances(out num2, out num3);
				uint num4 = (uint)this.nowPos64 & this._posStateMask;
				this._isMatch[(int)((this._state.Index << 4) + num4)].Encode(this._rangeEncoder, 0U);
				this._state.UpdateChar();
				byte indexByte = this._matchFinder.GetIndexByte((int)(0U - this._additionalOffset));
				this._literalEncoder.GetSubCoder((uint)this.nowPos64, this._previoubyte).Encode(this._rangeEncoder, indexByte);
				this._previoubyte = indexByte;
				this._additionalOffset -= 1U;
				this.nowPos64 += 1L;
			}
			if (this._matchFinder.GetNumAvailableBytes() == 0U)
			{
				this.Flush((uint)this.nowPos64);
				return;
			}
			for (; ; )
			{
				uint num5;
				uint optimum = this.GetOptimum((uint)this.nowPos64, out num5);
				uint num6 = (uint)this.nowPos64 & this._posStateMask;
				uint num7 = (this._state.Index << 4) + num6;
				if (optimum == 1U && num5 == 4294967295U)
				{
					this._isMatch[(int)num7].Encode(this._rangeEncoder, 0U);
					byte indexByte2 = this._matchFinder.GetIndexByte((int)(0U - this._additionalOffset));
					Encoder.LiteralEncoder.Encoder2 subCoder = this._literalEncoder.GetSubCoder((uint)this.nowPos64, this._previoubyte);
					if (!this._state.IsCharState())
					{
						byte indexByte3 = this._matchFinder.GetIndexByte((int)(0U - this._repDistances[0] - 1U - this._additionalOffset));
						subCoder.EncodeMatched(this._rangeEncoder, indexByte3, indexByte2);
					}
					else
					{
						subCoder.Encode(this._rangeEncoder, indexByte2);
					}
					this._previoubyte = indexByte2;
					this._state.UpdateChar();
				}
				else
				{
					this._isMatch[(int)num7].Encode(this._rangeEncoder, 1U);
					if (num5 < 4U)
					{
						this._isRep[(int)this._state.Index].Encode(this._rangeEncoder, 1U);
						if (num5 == 0U)
						{
							this._isRepG0[(int)this._state.Index].Encode(this._rangeEncoder, 0U);
							if (optimum == 1U)
							{
								this._isRep0Long[(int)num7].Encode(this._rangeEncoder, 0U);
							}
							else
							{
								this._isRep0Long[(int)num7].Encode(this._rangeEncoder, 1U);
							}
						}
						else
						{
							this._isRepG0[(int)this._state.Index].Encode(this._rangeEncoder, 1U);
							if (num5 == 1U)
							{
								this._isRepG1[(int)this._state.Index].Encode(this._rangeEncoder, 0U);
							}
							else
							{
								this._isRepG1[(int)this._state.Index].Encode(this._rangeEncoder, 1U);
								this._isRepG2[(int)this._state.Index].Encode(this._rangeEncoder, num5 - 2U);
							}
						}
						if (optimum == 1U)
						{
							this._state.UpdateShortRep();
						}
						else
						{
							this._repMatchLenEncoder.Encode(this._rangeEncoder, optimum - 2U, num6);
							this._state.UpdateRep();
						}
						uint num8 = this._repDistances[(int)num5];
						if (num5 != 0U)
						{
							for (uint num9 = num5; num9 >= 1U; num9 -= 1U)
							{
								this._repDistances[(int)num9] = this._repDistances[(int)(num9 - 1U)];
							}
							this._repDistances[0] = num8;
						}
					}
					else
					{
						this._isRep[(int)this._state.Index].Encode(this._rangeEncoder, 0U);
						this._state.UpdateMatch();
						this._lenEncoder.Encode(this._rangeEncoder, optimum - 2U, num6);
						num5 -= 4U;
						uint posSlot = Encoder.GetPosSlot(num5);
						uint lenToPosState = Base.GetLenToPosState(optimum);
						this._posSlotEncoder[(int)lenToPosState].Encode(this._rangeEncoder, posSlot);
						if (posSlot >= 4U)
						{
							int num10 = (int)((posSlot >> 1) - 1U);
							uint num11 = (2U | (posSlot & 1U)) << num10;
							uint num12 = num5 - num11;
							if (posSlot < 14U)
							{
								BitTreeEncoder.ReverseEncode(this._posEncoders, num11 - posSlot - 1U, this._rangeEncoder, num10, num12);
							}
							else
							{
								this._rangeEncoder.EncodeDirectBits(num12 >> 4, num10 - 4);
								this._posAlignEncoder.ReverseEncode(this._rangeEncoder, num12 & 15U);
								this._alignPriceCount += 1U;
							}
						}
						uint num13 = num5;
						for (uint num14 = 3U; num14 >= 1U; num14 -= 1U)
						{
							this._repDistances[(int)num14] = this._repDistances[(int)(num14 - 1U)];
						}
						this._repDistances[0] = num13;
						this._matchPriceCount += 1U;
					}
					this._previoubyte = this._matchFinder.GetIndexByte((int)(optimum - 1U - this._additionalOffset));
				}
				this._additionalOffset -= optimum;
				this.nowPos64 += (long)((ulong)optimum);
				if (this._additionalOffset == 0U)
				{
					if (this._matchPriceCount >= 128U)
					{
						this.FillDistancesPrices();
					}
					if (this._alignPriceCount >= 16U)
					{
						this.FillAlignPrices();
					}
					inSize = this.nowPos64;
					outSize = this._rangeEncoder.GetProcessedSizeAdd();
					if (this._matchFinder.GetNumAvailableBytes() == 0U)
					{
						break;
					}
					if (this.nowPos64 - num >= 4096L)
					{
						goto Block_24;
					}
				}
			}
			this.Flush((uint)this.nowPos64);
			return;
		Block_24:
			this._finished = false;
			finished = false;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00011DEA File Offset: 0x0000FFEA
		private void ReleaseMFStream()
		{
			if (this._matchFinder != null && this._needReleaseMFStream)
			{
				this._matchFinder.ReleaseStream();
				this._needReleaseMFStream = false;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00011E0E File Offset: 0x0001000E
		private void SetOutStream(Stream outStream)
		{
			this._rangeEncoder.SetStream(outStream);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00011E1C File Offset: 0x0001001C
		private void ReleaseOutStream()
		{
			this._rangeEncoder.ReleaseStream();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00011E29 File Offset: 0x00010029
		private void ReleaseStreams()
		{
			this.ReleaseMFStream();
			this.ReleaseOutStream();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00011E38 File Offset: 0x00010038
		private void SetStreams(Stream inStream, Stream outStream, long inSize, long outSize)
		{
			this._inStream = inStream;
			this._finished = false;
			this.Create();
			this.SetOutStream(outStream);
			this.Init();
			this.FillDistancesPrices();
			this.FillAlignPrices();
			this._lenEncoder.SetTableSize(this._numFastBytes + 1U - 2U);
			this._lenEncoder.UpdateTables(1U << this._posStateBits);
			this._repMatchLenEncoder.SetTableSize(this._numFastBytes + 1U - 2U);
			this._repMatchLenEncoder.UpdateTables(1U << this._posStateBits);
			this.nowPos64 = 0L;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00011ED0 File Offset: 0x000100D0
		public void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress)
		{
			this._needReleaseMFStream = false;
			try
			{
				this.SetStreams(inStream, outStream, inSize, outSize);
				for (; ; )
				{
					long num;
					long num2;
					bool flag;
					this.CodeOneBlock(out num, out num2, out flag);
					if (flag)
					{
						break;
					}
					if (progress != null)
					{
						progress.SetProgress(num, num2);
					}
				}
			}
			finally
			{
				this.ReleaseStreams();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00011F28 File Offset: 0x00010128
		public void WriteCoderProperties(Stream outStream)
		{
			this.properties[0] = (byte)((this._posStateBits * 5 + this._numLiteralPosStateBits) * 9 + this._numLiteralContextBits);
			for (int i = 0; i < 4; i++)
			{
				this.properties[1 + i] = (byte)((this._dictionarySize >> 8 * i) & 255U);
			}
			outStream.Write(this.properties, 0, 5);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00011F90 File Offset: 0x00010190
		private void FillDistancesPrices()
		{
			for (uint num = 4U; num < 128U; num += 1U)
			{
				uint posSlot = Encoder.GetPosSlot(num);
				int num2 = (int)((posSlot >> 1) - 1U);
				uint num3 = (2U | (posSlot & 1U)) << num2;
				this.tempPrices[(int)num] = BitTreeEncoder.ReverseGetPrice(this._posEncoders, num3 - posSlot - 1U, num2, num - num3);
			}
			for (uint num4 = 0U; num4 < 4U; num4 += 1U)
			{
				BitTreeEncoder bitTreeEncoder = this._posSlotEncoder[(int)num4];
				uint num5 = num4 << 6;
				for (uint num6 = 0U; num6 < this._distTableSize; num6 += 1U)
				{
					this._posSlotPrices[(int)(num5 + num6)] = bitTreeEncoder.GetPrice(num6);
				}
				for (uint num6 = 14U; num6 < this._distTableSize; num6 += 1U)
				{
					this._posSlotPrices[(int)(num5 + num6)] += (num6 >> 1) - 1U - 4U << 6;
				}
				uint num7 = num4 * 128U;
				uint num8;
				for (num8 = 0U; num8 < 4U; num8 += 1U)
				{
					this._distancesPrices[(int)(num7 + num8)] = this._posSlotPrices[(int)(num5 + num8)];
				}
				while (num8 < 128U)
				{
					this._distancesPrices[(int)(num7 + num8)] = this._posSlotPrices[(int)(num5 + Encoder.GetPosSlot(num8))] + this.tempPrices[(int)num8];
					num8 += 1U;
				}
			}
			this._matchPriceCount = 0U;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000120DC File Offset: 0x000102DC
		private void FillAlignPrices()
		{
			for (uint num = 0U; num < 16U; num += 1U)
			{
				this._alignPrices[(int)num] = this._posAlignEncoder.ReverseGetPrice(num);
			}
			this._alignPriceCount = 0U;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00012114 File Offset: 0x00010314
		private static int FindMatchFinder(string s)
		{
			for (int i = 0; i < Encoder.kMatchFinderIDs.Length; i++)
			{
				if (s == Encoder.kMatchFinderIDs[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00012148 File Offset: 0x00010348
		public void SetCoderProperties(CoderPropID[] propIDs, object[] properties)
		{
			uint num = 0U;
			while ((ulong)num < (ulong)((long)properties.Length))
			{
				object obj = properties[(int)num];
				switch (propIDs[(int)num])
				{
					case CoderPropID.DictionarySize:
						{
							if (!(obj is int))
							{
								throw new InvalidParamException();
							}
							int num2 = (int)obj;
							if ((long)num2 < 1L || (long)num2 > 1073741824L)
							{
								throw new InvalidParamException();
							}
							this._dictionarySize = (uint)num2;
							int num3 = 0;
							while ((long)num3 < 30L && (long)num2 > (long)(1UL << (num3 & 31)))
							{
								num3++;
							}
							this._distTableSize = (uint)(num3 * 2);
							break;
						}
					case CoderPropID.UsedMemorySize:
					case CoderPropID.Order:
					case CoderPropID.BlockSize:
					case CoderPropID.MatchFinderCycles:
					case CoderPropID.NumPasses:
					case CoderPropID.NumThreads:
						goto IL_021C;
					case CoderPropID.PosStateBits:
						{
							if (!(obj is int))
							{
								throw new InvalidParamException();
							}
							int num4 = (int)obj;
							if (num4 < 0 || (long)num4 > 4L)
							{
								throw new InvalidParamException();
							}
							this._posStateBits = num4;
							this._posStateMask = (1U << this._posStateBits) - 1U;
							break;
						}
					case CoderPropID.LitContextBits:
						{
							if (!(obj is int))
							{
								throw new InvalidParamException();
							}
							int num5 = (int)obj;
							if (num5 < 0 || (long)num5 > 8L)
							{
								throw new InvalidParamException();
							}
							this._numLiteralContextBits = num5;
							break;
						}
					case CoderPropID.LitPosBits:
						{
							if (!(obj is int))
							{
								throw new InvalidParamException();
							}
							int num6 = (int)obj;
							if (num6 < 0 || (long)num6 > 4L)
							{
								throw new InvalidParamException();
							}
							this._numLiteralPosStateBits = num6;
							break;
						}
					case CoderPropID.NumFastBytes:
						{
							if (!(obj is int))
							{
								throw new InvalidParamException();
							}
							int num7 = (int)obj;
							if (num7 < 5 || (long)num7 > 273L)
							{
								throw new InvalidParamException();
							}
							this._numFastBytes = (uint)num7;
							break;
						}
					case CoderPropID.MatchFinder:
						{
							if (!(obj is string))
							{
								throw new InvalidParamException();
							}
							Encoder.EMatchFinderType matchFinderType = this._matchFinderType;
							int num8 = Encoder.FindMatchFinder(((string)obj).ToUpper());
							if (num8 < 0)
							{
								throw new InvalidParamException();
							}
							this._matchFinderType = (Encoder.EMatchFinderType)num8;
							if (this._matchFinder != null && matchFinderType != this._matchFinderType)
							{
								this._dictionarySizePrev = uint.MaxValue;
								this._matchFinder = null;
							}
							break;
						}
					case CoderPropID.Algorithm:
						break;
					case CoderPropID.EndMarker:
						if (!(obj is bool))
						{
							throw new InvalidParamException();
						}
						this.SetWriteEndMarkerMode((bool)obj);
						break;
					default:
						goto IL_021C;
				}
				num += 1U;
				continue;
			IL_021C:
				throw new InvalidParamException();
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00012386 File Offset: 0x00010586
		public void SetTrainSize(uint trainSize)
		{
			this._trainSize = trainSize;
		}

		// Token: 0x04000210 RID: 528
		private const uint kIfinityPrice = 268435455U;

		// Token: 0x04000211 RID: 529
		private static byte[] g_FastPos = new byte[2048];

		// Token: 0x04000212 RID: 530
		private Base.State _state;

		// Token: 0x04000213 RID: 531
		private byte _previoubyte;

		// Token: 0x04000214 RID: 532
		private uint[] _repDistances = new uint[4];

		// Token: 0x04000215 RID: 533
		private const int kDefaultDictionaryLogSize = 22;

		// Token: 0x04000216 RID: 534
		private const uint kNumFastBytesDefault = 32U;

		// Token: 0x04000217 RID: 535
		private const uint kNumLenSpecSymbols = 16U;

		// Token: 0x04000218 RID: 536
		private const uint kNumOpts = 4096U;

		// Token: 0x04000219 RID: 537
		private Encoder.Optimal[] _optimum = new Encoder.Optimal[4096];

		// Token: 0x0400021A RID: 538
		private IMatchFinder _matchFinder;

		// FIX: was "private Encoder _rangeEncoder = new Encoder();" — self-referenced the
		// outer LZMA.Encoder class instead of RangeCoder.Encoder, which caused every
		// CS1503/CS1061 error in this file.
		// Token: 0x0400021B RID: 539
		private RangeCoder.Encoder _rangeEncoder = new RangeCoder.Encoder();

		// Token: 0x0400021C RID: 540
		private BitEncoder[] _isMatch = new BitEncoder[192];

		// Token: 0x0400021D RID: 541
		private BitEncoder[] _isRep = new BitEncoder[12];

		// Token: 0x0400021E RID: 542
		private BitEncoder[] _isRepG0 = new BitEncoder[12];

		// Token: 0x0400021F RID: 543
		private BitEncoder[] _isRepG1 = new BitEncoder[12];

		// Token: 0x04000220 RID: 544
		private BitEncoder[] _isRepG2 = new BitEncoder[12];

		// Token: 0x04000221 RID: 545
		private BitEncoder[] _isRep0Long = new BitEncoder[192];

		// Token: 0x04000222 RID: 546
		private BitTreeEncoder[] _posSlotEncoder = new BitTreeEncoder[4];

		// Token: 0x04000223 RID: 547
		private BitEncoder[] _posEncoders = new BitEncoder[114];

		// Token: 0x04000224 RID: 548
		private BitTreeEncoder _posAlignEncoder = new BitTreeEncoder(4);

		// Token: 0x04000225 RID: 549
		private Encoder.LenPriceTableEncoder _lenEncoder = new Encoder.LenPriceTableEncoder();

		// Token: 0x04000226 RID: 550
		private Encoder.LenPriceTableEncoder _repMatchLenEncoder = new Encoder.LenPriceTableEncoder();

		// Token: 0x04000227 RID: 551
		private Encoder.LiteralEncoder _literalEncoder = new Encoder.LiteralEncoder();

		// Token: 0x04000228 RID: 552
		private uint[] _matchDistances = new uint[548];

		// Token: 0x04000229 RID: 553
		private uint _numFastBytes = 32U;

		// Token: 0x0400022A RID: 554
		private uint _longestMatchLength;

		// Token: 0x0400022B RID: 555
		private uint _numDistancePairs;

		// Token: 0x0400022C RID: 556
		private uint _additionalOffset;

		// Token: 0x0400022D RID: 557
		private uint _optimumEndIndex;

		// Token: 0x0400022E RID: 558
		private uint _optimumCurrentIndex;

		// Token: 0x0400022F RID: 559
		private bool _longestMatchWasFound;

		// Token: 0x04000230 RID: 560
		private uint[] _posSlotPrices = new uint[256];

		// Token: 0x04000231 RID: 561
		private uint[] _distancesPrices = new uint[512];

		// Token: 0x04000232 RID: 562
		private uint[] _alignPrices = new uint[16];

		// Token: 0x04000233 RID: 563
		private uint _alignPriceCount;

		// Token: 0x04000234 RID: 564
		private uint _distTableSize = 44U;

		// Token: 0x04000235 RID: 565
		private int _posStateBits = 2;

		// Token: 0x04000236 RID: 566
		private uint _posStateMask = 3U;

		// Token: 0x04000237 RID: 567
		private int _numLiteralPosStateBits;

		// Token: 0x04000238 RID: 568
		private int _numLiteralContextBits = 3;

		// Token: 0x04000239 RID: 569
		private uint _dictionarySize = 4194304U;

		// Token: 0x0400023A RID: 570
		private uint _dictionarySizePrev = uint.MaxValue;

		// Token: 0x0400023B RID: 571
		private uint _numFastBytesPrev = uint.MaxValue;

		// Token: 0x0400023C RID: 572
		private long nowPos64;

		// Token: 0x0400023D RID: 573
		private bool _finished;

		// Token: 0x0400023E RID: 574
		private Stream _inStream;

		// Token: 0x0400023F RID: 575
		private Encoder.EMatchFinderType _matchFinderType = Encoder.EMatchFinderType.BT4;

		// Token: 0x04000240 RID: 576
		private bool _writeEndMark;

		// Token: 0x04000241 RID: 577
		private bool _needReleaseMFStream;

		// Token: 0x04000242 RID: 578
		private uint[] reps = new uint[4];

		// Token: 0x04000243 RID: 579
		private uint[] repLens = new uint[4];

		// Token: 0x04000244 RID: 580
		private const int kPropSize = 5;

		// Token: 0x04000245 RID: 581
		private byte[] properties = new byte[5];

		// Token: 0x04000246 RID: 582
		private uint[] tempPrices = new uint[128];

		// Token: 0x04000247 RID: 583
		private uint _matchPriceCount;

		// Token: 0x04000248 RID: 584
		private static string[] kMatchFinderIDs = new string[] { "BT2", "BT4" };

		// Token: 0x04000249 RID: 585
		private uint _trainSize;

		// Token: 0x020000F7 RID: 247
		private enum EMatchFinderType
		{
			// Token: 0x04000454 RID: 1108
			BT2,
			// Token: 0x04000455 RID: 1109
			BT4
		}

		// Token: 0x020000F8 RID: 248
		private class LiteralEncoder
		{
			// Token: 0x060005F7 RID: 1527 RVA: 0x0002133C File Offset: 0x0001F53C
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
				this.m_Coders = new Encoder.LiteralEncoder.Encoder2[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					this.m_Coders[(int)num2].Create();
				}
			}

			// Token: 0x060005F8 RID: 1528 RVA: 0x000213BC File Offset: 0x0001F5BC
			public void Init()
			{
				uint num = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					this.m_Coders[(int)num2].Init();
				}
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x000213F9 File Offset: 0x0001F5F9
			public Encoder.LiteralEncoder.Encoder2 GetSubCoder(uint pos, byte prevByte)
			{
				return this.m_Coders[(int)(((pos & this.m_PosMask) << this.m_NumPrevBits) + (uint)(prevByte >> 8 - this.m_NumPrevBits))];
			}

			// Token: 0x04000456 RID: 1110
			private Encoder.LiteralEncoder.Encoder2[] m_Coders;

			// Token: 0x04000457 RID: 1111
			private int m_NumPrevBits;

			// Token: 0x04000458 RID: 1112
			private int m_NumPosBits;

			// Token: 0x04000459 RID: 1113
			private uint m_PosMask;

			// Token: 0x02000134 RID: 308
			public struct Encoder2
			{
				// Token: 0x06000676 RID: 1654 RVA: 0x00021FAB File Offset: 0x000201AB
				public void Create()
				{
					this.m_Encoders = new BitEncoder[768];
				}

				// Token: 0x06000677 RID: 1655 RVA: 0x00021FC0 File Offset: 0x000201C0
				public void Init()
				{
					for (int i = 0; i < 768; i++)
					{
						this.m_Encoders[i].Init();
					}
				}

				// FIX: parameter was "Encoder rangeEncoder" (resolved to outer LZMA.Encoder); now RangeCoder.Encoder
				// Token: 0x06000678 RID: 1656 RVA: 0x00021FF0 File Offset: 0x000201F0
				public void Encode(RangeCoder.Encoder rangeEncoder, byte symbol)
				{
					uint num = 1U;
					for (int i = 7; i >= 0; i--)
					{
						uint num2 = (uint)((symbol >> i) & 1);
						this.m_Encoders[(int)num].Encode(rangeEncoder, num2);
						num = (num << 1) | num2;
					}
				}

				// FIX: parameter was "Encoder rangeEncoder"; now RangeCoder.Encoder
				// Token: 0x06000679 RID: 1657 RVA: 0x00022030 File Offset: 0x00020230
				public void EncodeMatched(RangeCoder.Encoder rangeEncoder, byte matchByte, byte symbol)
				{
					uint num = 1U;
					bool flag = true;
					for (int i = 7; i >= 0; i--)
					{
						uint num2 = (uint)((symbol >> i) & 1);
						uint num3 = num;
						if (flag)
						{
							uint num4 = (uint)((matchByte >> i) & 1);
							num3 += 1U + num4 << 8;
							flag = num4 == num2;
						}
						this.m_Encoders[(int)num3].Encode(rangeEncoder, num2);
						num = (num << 1) | num2;
					}
				}

				// Token: 0x0600067A RID: 1658 RVA: 0x00022094 File Offset: 0x00020294
				public uint GetPrice(bool matchMode, byte matchByte, byte symbol)
				{
					uint num = 0U;
					uint num2 = 1U;
					int i = 7;
					if (matchMode)
					{
						while (i >= 0)
						{
							uint num3 = (uint)((matchByte >> i) & 1);
							uint num4 = (uint)((symbol >> i) & 1);
							num += this.m_Encoders[(int)((1U + num3 << 8) + num2)].GetPrice(num4);
							num2 = (num2 << 1) | num4;
							if (num3 != num4)
							{
								i--;
								break;
							}
							i--;
						}
					}
					while (i >= 0)
					{
						uint num5 = (uint)((symbol >> i) & 1);
						num += this.m_Encoders[(int)num2].GetPrice(num5);
						num2 = (num2 << 1) | num5;
						i--;
					}
					return num;
				}

				// Token: 0x040004C5 RID: 1221
				private BitEncoder[] m_Encoders;
			}
		}

		// Token: 0x020000F9 RID: 249
		private class LenEncoder
		{
			// Token: 0x060005FB RID: 1531 RVA: 0x00021428 File Offset: 0x0001F628
			public LenEncoder()
			{
				for (uint num = 0U; num < 16U; num += 1U)
				{
					this._lowCoder[(int)num] = new BitTreeEncoder(3);
					this._midCoder[(int)num] = new BitTreeEncoder(3);
				}
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x00021494 File Offset: 0x0001F694
			public void Init(uint numPosStates)
			{
				this._choice.Init();
				this._choice2.Init();
				for (uint num = 0U; num < numPosStates; num += 1U)
				{
					this._lowCoder[(int)num].Init();
					this._midCoder[(int)num].Init();
				}
				this._highCoder.Init();
			}

			// FIX: parameter was "Encoder rangeEncoder"; now RangeCoder.Encoder
			// Token: 0x060005FD RID: 1533 RVA: 0x000214F0 File Offset: 0x0001F6F0
			public void Encode(RangeCoder.Encoder rangeEncoder, uint symbol, uint posState)
			{
				if (symbol < 8U)
				{
					this._choice.Encode(rangeEncoder, 0U);
					this._lowCoder[(int)posState].Encode(rangeEncoder, symbol);
					return;
				}
				symbol -= 8U;
				this._choice.Encode(rangeEncoder, 1U);
				if (symbol < 8U)
				{
					this._choice2.Encode(rangeEncoder, 0U);
					this._midCoder[(int)posState].Encode(rangeEncoder, symbol);
					return;
				}
				this._choice2.Encode(rangeEncoder, 1U);
				this._highCoder.Encode(rangeEncoder, symbol - 8U);
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x00021578 File Offset: 0x0001F778
			public void SetPrices(uint posState, uint numSymbols, uint[] prices, uint st)
			{
				uint price = this._choice.GetPrice0();
				uint price2 = this._choice.GetPrice1();
				uint num = price2 + this._choice2.GetPrice0();
				uint num2 = price2 + this._choice2.GetPrice1();
				uint num3;
				for (num3 = 0U; num3 < 8U; num3 += 1U)
				{
					if (num3 >= numSymbols)
					{
						return;
					}
					prices[(int)(st + num3)] = price + this._lowCoder[(int)posState].GetPrice(num3);
				}
				while (num3 < 16U)
				{
					if (num3 >= numSymbols)
					{
						return;
					}
					prices[(int)(st + num3)] = num + this._midCoder[(int)posState].GetPrice(num3 - 8U);
					num3 += 1U;
				}
				while (num3 < numSymbols)
				{
					prices[(int)(st + num3)] = num2 + this._highCoder.GetPrice(num3 - 8U - 8U);
					num3 += 1U;
				}
			}

			// Token: 0x0400045A RID: 1114
			private BitEncoder _choice;

			// Token: 0x0400045B RID: 1115
			private BitEncoder _choice2;

			// Token: 0x0400045C RID: 1116
			private BitTreeEncoder[] _lowCoder = new BitTreeEncoder[16];

			// Token: 0x0400045D RID: 1117
			private BitTreeEncoder[] _midCoder = new BitTreeEncoder[16];

			// Token: 0x0400045E RID: 1118
			private BitTreeEncoder _highCoder = new BitTreeEncoder(8);
		}

		// Token: 0x020000FA RID: 250
		private class LenPriceTableEncoder : Encoder.LenEncoder
		{
			// Token: 0x060005FF RID: 1535 RVA: 0x00021632 File Offset: 0x0001F832
			public void SetTableSize(uint tableSize)
			{
				this._tableSize = tableSize;
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0002163B File Offset: 0x0001F83B
			public uint GetPrice(uint symbol, uint posState)
			{
				return this._prices[(int)(posState * 272U + symbol)];
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x0002164D File Offset: 0x0001F84D
			private void UpdateTable(uint posState)
			{
				base.SetPrices(posState, this._tableSize, this._prices, posState * 272U);
				this._counters[(int)posState] = this._tableSize;
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x00021678 File Offset: 0x0001F878
			public void UpdateTables(uint numPosStates)
			{
				for (uint num = 0U; num < numPosStates; num += 1U)
				{
					this.UpdateTable(num);
				}
			}

			// FIX: parameter was "Encoder rangeEncoder"; now RangeCoder.Encoder
			// Token: 0x06000603 RID: 1539 RVA: 0x00021698 File Offset: 0x0001F898
			public new void Encode(RangeCoder.Encoder rangeEncoder, uint symbol, uint posState)
			{
				base.Encode(rangeEncoder, symbol, posState);
				uint[] counters = this._counters;
				uint num = counters[(int)posState] - 1U;
				counters[(int)posState] = num;
				if (num == 0U)
				{
					this.UpdateTable(posState);
				}
			}

			// Token: 0x0400045F RID: 1119
			private uint[] _prices = new uint[4352];

			// Token: 0x04000460 RID: 1120
			private uint _tableSize;

			// Token: 0x04000461 RID: 1121
			private uint[] _counters = new uint[16];
		}

		// Token: 0x020000FB RID: 251
		private class Optimal
		{
			// Token: 0x06000605 RID: 1541 RVA: 0x000216F0 File Offset: 0x0001F8F0
			public void MakeAsChar()
			{
				this.BackPrev = uint.MaxValue;
				this.Prev1IsChar = false;
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x00021700 File Offset: 0x0001F900
			public void MakeAsShortRep()
			{
				this.BackPrev = 0U;
				this.Prev1IsChar = false;
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x00021710 File Offset: 0x0001F910
			public bool IsShortRep()
			{
				return this.BackPrev == 0U;
			}

			// Token: 0x04000462 RID: 1122
			public Base.State State;

			// Token: 0x04000463 RID: 1123
			public bool Prev1IsChar;

			// Token: 0x04000464 RID: 1124
			public bool Prev2;

			// Token: 0x04000465 RID: 1125
			public uint PosPrev2;

			// Token: 0x04000466 RID: 1126
			public uint BackPrev2;

			// Token: 0x04000467 RID: 1127
			public uint Price;

			// Token: 0x04000468 RID: 1128
			public uint PosPrev;

			// Token: 0x04000469 RID: 1129
			public uint BackPrev;

			// Token: 0x0400046A RID: 1130
			public uint Backs0;

			// Token: 0x0400046B RID: 1131
			public uint Backs1;

			// Token: 0x0400046C RID: 1132
			public uint Backs2;

			// Token: 0x0400046D RID: 1133
			public uint Backs3;
		}
	}
}