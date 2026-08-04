using System;
using System.IO;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000030 RID: 48
	public class ZlibStream : Stream
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
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

		// Token: 0x06000199 RID: 409 RVA: 0x0000C114 File Offset: 0x0000A314
		public ZlibStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C120 File Offset: 0x0000A320
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C12C File Offset: 0x0000A32C
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C138 File Offset: 0x0000A338
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000C155 File Offset: 0x0000A355
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000C164 File Offset: 0x0000A364
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000036C8 File Offset: 0x000018C8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000C21A File Offset: 0x0000A41A
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000C227 File Offset: 0x0000A427
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000036F9 File Offset: 0x000018F9
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000C248 File Offset: 0x0000A448
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000036F9 File Offset: 0x000018F9
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000C294 File Offset: 0x0000A494
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000C2A6 File Offset: 0x0000A4A6
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
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

		// Token: 0x060001AA RID: 426 RVA: 0x0000C300 File Offset: 0x0000A500
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

		// Token: 0x060001AB RID: 427 RVA: 0x0000C348 File Offset: 0x0000A548
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

		// Token: 0x060001AC RID: 428 RVA: 0x0000C38C File Offset: 0x0000A58C
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

		// Token: 0x060001AD RID: 429 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000036F9 File Offset: 0x000018F9
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000036F9 File Offset: 0x000018F9
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000C413 File Offset: 0x0000A613
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x04000176 RID: 374
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000177 RID: 375
		private bool _disposed;
	}
}
