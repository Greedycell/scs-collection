namespace CRS.Library.LZMA.Compress.RangeCoder
{
    #region Usings

    using System;

    #endregion Usings

    internal struct BitEncoder
    {
        public const int kNumBitModelTotalBits = 11;
        public const uint kBitModelTotal = 1 << kNumBitModelTotalBits;
        private const int kNumMoveBits = 5;
        private const int kNumMoveReducingBits = 2;
        public const int kNumBitPriceShiftBits = 6;

        private uint Prob;

        public void Init()
        {
            this.Prob = kBitModelTotal >> 1;
        }

        public void UpdateModel(uint symbol)
        {
            if (symbol == 0)
            {
                this.Prob += (kBitModelTotal - this.Prob) >> kNumMoveBits;
            }
            else
            {
                this.Prob -= this.Prob >> kNumMoveBits;
            }
        }

        public void Encode(Encoder encoder, uint symbol)
        {
            // encoder.EncodeBit(Prob, kNumBitModelTotalBits, symbol); UpdateModel(symbol);
            uint newBound = (encoder.Range >> kNumBitModelTotalBits) * this.Prob;
            if (symbol == 0)
            {
                encoder.Range = newBound;
                this.Prob += (kBitModelTotal - this.Prob) >> kNumMoveBits;
            }
            else
            {
                encoder.Low += newBound;
                encoder.Range -= newBound;
                this.Prob -= this.Prob >> kNumMoveBits;
            }

            if (encoder.Range < Encoder.kTopValue)
            {
                encoder.Range <<= 8;
                encoder.ShiftLow();
            }
        }

        private static uint[] ProbPrices = new uint[kBitModelTotal >> kNumMoveReducingBits];

        static BitEncoder()
        {
            const int kNumBits = kNumBitModelTotalBits - kNumMoveReducingBits;
            for (int i = kNumBits - 1; i >= 0; i--)
            {
                uint start = (UInt32)1 << (kNumBits - i - 1);
                uint end = (UInt32)1 << (kNumBits - i);
                for (uint j = start; j < end; j++)
                {
                    ProbPrices[j] = ((UInt32)i << kNumBitPriceShiftBits) + (((end - j) << kNumBitPriceShiftBits) >> (kNumBits - i - 1));
                }
            }
        }

        public uint GetPrice(uint symbol)
        {
            return ProbPrices[(((this.Prob - symbol) ^ -(int)symbol) & (kBitModelTotal - 1)) >> kNumMoveReducingBits];
        }

        public uint GetPrice0()
        {
            return ProbPrices[this.Prob >> kNumMoveReducingBits];
        }

        public uint GetPrice1()
        {
            return ProbPrices[(kBitModelTotal - this.Prob) >> kNumMoveReducingBits];
        }
    }

    internal struct BitDecoder
    {
        public const int kNumBitModelTotalBits = 11;
        public const uint kBitModelTotal = 1 << kNumBitModelTotalBits;
        private const int kNumMoveBits = 5;

        private uint Prob;

        public void UpdateModel(int numMoveBits, uint symbol)
        {
            if (symbol == 0)
            {
                this.Prob += (kBitModelTotal - this.Prob) >> numMoveBits;
            }
            else
            {
                this.Prob -= this.Prob >> numMoveBits;
            }
        }

        public void Init()
        {
            this.Prob = kBitModelTotal >> 1;
        }

        public uint Decode(Decoder rangeDecoder)
        {
            uint newBound = (uint)(rangeDecoder.Range >> kNumBitModelTotalBits) * (uint)this.Prob;
            if (rangeDecoder.Code < newBound)
            {
                rangeDecoder.Range = newBound;
                this.Prob += (kBitModelTotal - this.Prob) >> kNumMoveBits;
                if (rangeDecoder.Range < Decoder.kTopValue)
                {
                    rangeDecoder.Code = (rangeDecoder.Code << 8) | (byte)rangeDecoder.Stream.ReadByte();
                    rangeDecoder.Range <<= 8;
                }

                return 0;
            }
            else
            {
                rangeDecoder.Range -= newBound;
                rangeDecoder.Code -= newBound;
                this.Prob -= this.Prob >> kNumMoveBits;
                if (rangeDecoder.Range < Decoder.kTopValue)
                {
                    rangeDecoder.Code = (rangeDecoder.Code << 8) | (byte)rangeDecoder.Stream.ReadByte();
                    rangeDecoder.Range <<= 8;
                }

                return 1;
            }
        }
    }
}