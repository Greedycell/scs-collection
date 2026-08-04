using System;

namespace CRS.Utilities.LZMA.Compress.LZMA
{
	// Token: 0x0200004C RID: 76
	internal abstract class Base
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000F836 File Offset: 0x0000DA36
		public static uint GetLenToPosState(uint len)
		{
			len -= 2U;
			if (len < 4U)
			{
				return len;
			}
			return 3U;
		}

		// Token: 0x040001E3 RID: 483
		public const uint kNumRepDistances = 4U;

		// Token: 0x040001E4 RID: 484
		public const uint kNumStates = 12U;

		// Token: 0x040001E5 RID: 485
		public const int kNumPosSlotBits = 6;

		// Token: 0x040001E6 RID: 486
		public const int kDicLogSizeMin = 0;

		// Token: 0x040001E7 RID: 487
		public const int kNumLenToPosStatesBits = 2;

		// Token: 0x040001E8 RID: 488
		public const uint kNumLenToPosStates = 4U;

		// Token: 0x040001E9 RID: 489
		public const uint kMatchMinLen = 2U;

		// Token: 0x040001EA RID: 490
		public const int kNumAlignBits = 4;

		// Token: 0x040001EB RID: 491
		public const uint kAlignTableSize = 16U;

		// Token: 0x040001EC RID: 492
		public const uint kAlignMask = 15U;

		// Token: 0x040001ED RID: 493
		public const uint kStartPosModelIndex = 4U;

		// Token: 0x040001EE RID: 494
		public const uint kEndPosModelIndex = 14U;

		// Token: 0x040001EF RID: 495
		public const uint kNumPosModels = 10U;

		// Token: 0x040001F0 RID: 496
		public const uint kNumFullDistances = 128U;

		// Token: 0x040001F1 RID: 497
		public const uint kNumLitPosStatesBitsEncodingMax = 4U;

		// Token: 0x040001F2 RID: 498
		public const uint kNumLitContextBitsMax = 8U;

		// Token: 0x040001F3 RID: 499
		public const int kNumPosStatesBitsMax = 4;

		// Token: 0x040001F4 RID: 500
		public const uint kNumPosStatesMax = 16U;

		// Token: 0x040001F5 RID: 501
		public const int kNumPosStatesBitsEncodingMax = 4;

		// Token: 0x040001F6 RID: 502
		public const uint kNumPosStatesEncodingMax = 16U;

		// Token: 0x040001F7 RID: 503
		public const int kNumLowLenBits = 3;

		// Token: 0x040001F8 RID: 504
		public const int kNumMidLenBits = 3;

		// Token: 0x040001F9 RID: 505
		public const int kNumHighLenBits = 8;

		// Token: 0x040001FA RID: 506
		public const uint kNumLowLenSymbols = 8U;

		// Token: 0x040001FB RID: 507
		public const uint kNumMidLenSymbols = 8U;

		// Token: 0x040001FC RID: 508
		public const uint kNumLenSymbols = 272U;

		// Token: 0x040001FD RID: 509
		public const uint kMatchMaxLen = 273U;

		// Token: 0x020000F4 RID: 244
		public struct State
		{
			// Token: 0x060005E7 RID: 1511 RVA: 0x0002104A File Offset: 0x0001F24A
			public void Init()
			{
				this.Index = 0U;
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x00021053 File Offset: 0x0001F253
			public void UpdateChar()
			{
				if (this.Index < 4U)
				{
					this.Index = 0U;
					return;
				}
				if (this.Index < 10U)
				{
					this.Index -= 3U;
					return;
				}
				this.Index -= 6U;
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x0002108D File Offset: 0x0001F28D
			public void UpdateMatch()
			{
				this.Index = ((this.Index < 7U) ? 7U : 10U);
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x000210A3 File Offset: 0x0001F2A3
			public void UpdateRep()
			{
				this.Index = ((this.Index < 7U) ? 8U : 11U);
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x000210B9 File Offset: 0x0001F2B9
			public void UpdateShortRep()
			{
				this.Index = ((this.Index < 7U) ? 9U : 11U);
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x000210D0 File Offset: 0x0001F2D0
			public bool IsCharState()
			{
				return this.Index < 7U;
			}

			// Token: 0x04000448 RID: 1096
			public uint Index;
		}
	}
}
