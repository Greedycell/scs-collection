using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x0200001D RID: 29
	public class ZlibStream : Stream
	{
		// Token: 0x060000FC RID: 252 RVA: 0x0000A93A File Offset: 0x00008B3A
		public ZlibStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000A946 File Offset: 0x00008B46
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000A952 File Offset: 0x00008B52
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000A95E File Offset: 0x00008B5E
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000A97B File Offset: 0x00008B7B
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000A988 File Offset: 0x00008B88
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000A9A9 File Offset: 0x00008BA9
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				if (this._baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000AA24 File Offset: 0x00008C24
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000AA36 File Offset: 0x00008C36
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000AA48 File Offset: 0x00008C48
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000475F File Offset: 0x0000295F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000AA6D File Offset: 0x00008C6D
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00008EDD File Offset: 0x000070DD
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000AA94 File Offset: 0x00008C94
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00008EDD File Offset: 0x000070DD
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000AB28 File Offset: 0x00008D28
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000AB70 File Offset: 0x00008D70
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000AC44 File Offset: 0x00008E44
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000AC64 File Offset: 0x00008E64
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008EDD File Offset: 0x000070DD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008EDD File Offset: 0x000070DD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000AC87 File Offset: 0x00008E87
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x04000142 RID: 322
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000143 RID: 323
		private bool _disposed;
	}
}
