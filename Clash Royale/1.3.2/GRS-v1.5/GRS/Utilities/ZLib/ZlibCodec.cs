using System;
using System.Runtime.InteropServices;

namespace CRS.Utilities.ZLib
{
	// Token: 0x0200002E RID: 46
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public sealed class ZlibCodec
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000BC48 File Offset: 0x00009E48
		public ZlibCodec()
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000BC60 File Offset: 0x00009E60
		public ZlibCodec(CompressionMode mode)
		{
			if (mode == CompressionMode.Compress)
			{
				if (this.InitializeDeflate() != 0)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
			}
			else
			{
				if (mode != CompressionMode.Decompress)
				{
					throw new ZlibException("Invalid ZlibStreamFlavor.");
				}
				if (this.InitializeInflate() != 0)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000BCBA File Offset: 0x00009EBA
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BCC2 File Offset: 0x00009EC2
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BCDF File Offset: 0x00009EDF
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			if (this.dstate != null)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BD25 File Offset: 0x00009F25
		public int Inflate(FlushType flush)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BD46 File Offset: 0x00009F46
		public int EndInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int num = this.istate.End();
			this.istate = null;
			return num;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BD6D File Offset: 0x00009F6D
		public int SyncInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BD8D File Offset: 0x00009F8D
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000BD96 File Offset: 0x00009F96
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BDA6 File Offset: 0x00009FA6
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000BDB6 File Offset: 0x00009FB6
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000BDCD File Offset: 0x00009FCD
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			if (this.istate != null)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			this.dstate = new DeflateManager();
			this.dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return this.dstate.Initialize(this, this.CompressLevel, this.WindowBits, this.Strategy);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000BE39 File Offset: 0x0000A039
		public int Deflate(FlushType flush)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000BE5A File Offset: 0x0000A05A
		public int EndDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000BE77 File Offset: 0x0000A077
		public void ResetDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000BE97 File Offset: 0x0000A097
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000BEB9 File Offset: 0x0000A0B9
		public int SetDictionary(byte[] dictionary)
		{
			if (this.istate != null)
			{
				return this.istate.SetDictionary(dictionary);
			}
			if (this.dstate != null)
			{
				return this.dstate.SetDictionary(dictionary);
			}
			throw new ZlibException("No Inflate or Deflate state!");
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		internal void flush_pending()
		{
			int num = this.dstate.pendingCount;
			if (num > this.AvailableBytesOut)
			{
				num = this.AvailableBytesOut;
			}
			if (num == 0)
			{
				return;
			}
			if (this.dstate.pending.Length <= this.dstate.nextPending || this.OutputBuffer.Length <= this.NextOut || this.dstate.pending.Length < this.dstate.nextPending + num || this.OutputBuffer.Length < this.NextOut + num)
			{
				throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", this.dstate.pending.Length, this.dstate.pendingCount));
			}
			Array.Copy(this.dstate.pending, this.dstate.nextPending, this.OutputBuffer, this.NextOut, num);
			this.NextOut += num;
			this.dstate.nextPending += num;
			this.TotalBytesOut += (long)num;
			this.AvailableBytesOut -= num;
			this.dstate.pendingCount -= num;
			if (this.dstate.pendingCount == 0)
			{
				this.dstate.nextPending = 0;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C03C File Offset: 0x0000A23C
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.AvailableBytesIn;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.AvailableBytesIn -= num;
			if (this.dstate.WantRfc1950HeaderBytes)
			{
				this._Adler32 = Adler.Adler32(this._Adler32, this.InputBuffer, this.NextIn, num);
			}
			Array.Copy(this.InputBuffer, this.NextIn, buf, start, num);
			this.NextIn += num;
			this.TotalBytesIn += (long)num;
			return num;
		}

		// Token: 0x0400015D RID: 349
		internal uint _Adler32;

		// Token: 0x0400015E RID: 350
		public int AvailableBytesIn;

		// Token: 0x0400015F RID: 351
		public int AvailableBytesOut;

		// Token: 0x04000160 RID: 352
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x04000161 RID: 353
		internal DeflateManager dstate;

		// Token: 0x04000162 RID: 354
		public byte[] InputBuffer;

		// Token: 0x04000163 RID: 355
		internal InflateManager istate;

		// Token: 0x04000164 RID: 356
		public string Message;

		// Token: 0x04000165 RID: 357
		public int NextIn;

		// Token: 0x04000166 RID: 358
		public int NextOut;

		// Token: 0x04000167 RID: 359
		public byte[] OutputBuffer;

		// Token: 0x04000168 RID: 360
		public CompressionStrategy Strategy;

		// Token: 0x04000169 RID: 361
		public long TotalBytesIn;

		// Token: 0x0400016A RID: 362
		public long TotalBytesOut;

		// Token: 0x0400016B RID: 363
		public int WindowBits = 15;
	}
}
