using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x0200001B RID: 27
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public sealed class ZlibCodec
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000A4BC File Offset: 0x000086BC
		public ZlibCodec()
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000A4D4 File Offset: 0x000086D4
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000A52E File Offset: 0x0000872E
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000A536 File Offset: 0x00008736
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000A544 File Offset: 0x00008744
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000A553 File Offset: 0x00008753
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000A564 File Offset: 0x00008764
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

		// Token: 0x060000EC RID: 236 RVA: 0x0000A599 File Offset: 0x00008799
		public int Inflate(FlushType flush)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000A5BA File Offset: 0x000087BA
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

		// Token: 0x060000EE RID: 238 RVA: 0x0000A5E1 File Offset: 0x000087E1
		public int SyncInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000A601 File Offset: 0x00008801
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000A60A File Offset: 0x0000880A
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000A61A File Offset: 0x0000881A
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000A62A File Offset: 0x0000882A
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000A641 File Offset: 0x00008841
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000A658 File Offset: 0x00008858
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

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A6AD File Offset: 0x000088AD
		public int Deflate(FlushType flush)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A6CE File Offset: 0x000088CE
		public int EndDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A6EB File Offset: 0x000088EB
		public void ResetDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A70B File Offset: 0x0000890B
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A72D File Offset: 0x0000892D
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

		// Token: 0x060000FA RID: 250 RVA: 0x0000A764 File Offset: 0x00008964
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

		// Token: 0x060000FB RID: 251 RVA: 0x0000A8B0 File Offset: 0x00008AB0
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

		// Token: 0x04000129 RID: 297
		internal uint _Adler32;

		// Token: 0x0400012A RID: 298
		public int AvailableBytesIn;

		// Token: 0x0400012B RID: 299
		public int AvailableBytesOut;

		// Token: 0x0400012C RID: 300
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x0400012D RID: 301
		internal DeflateManager dstate;

		// Token: 0x0400012E RID: 302
		public byte[] InputBuffer;

		// Token: 0x0400012F RID: 303
		internal InflateManager istate;

		// Token: 0x04000130 RID: 304
		public string Message;

		// Token: 0x04000131 RID: 305
		public int NextIn;

		// Token: 0x04000132 RID: 306
		public int NextOut;

		// Token: 0x04000133 RID: 307
		public byte[] OutputBuffer;

		// Token: 0x04000134 RID: 308
		public CompressionStrategy Strategy;

		// Token: 0x04000135 RID: 309
		public long TotalBytesIn;

		// Token: 0x04000136 RID: 310
		public long TotalBytesOut;

		// Token: 0x04000137 RID: 311
		public int WindowBits = 15;
	}
}
