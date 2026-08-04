using System;
using System.IO;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000018 RID: 24
	public class DeflateStream : Stream
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00005C20 File Offset: 0x00003E20
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

		// Token: 0x060000C8 RID: 200 RVA: 0x00005C6C File Offset: 0x00003E6C
		public DeflateStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005C78 File Offset: 0x00003E78
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005C84 File Offset: 0x00003E84
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005C90 File Offset: 0x00003E90
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005CB4 File Offset: 0x00003EB4
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00005CC4 File Offset: 0x00003EC4
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
					throw new ObjectDisposedException("DeflateStream");
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005D30 File Offset: 0x00003F30
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000036C8 File Offset: 0x000018C8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005D55 File Offset: 0x00003F55
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005D7A File Offset: 0x00003F7A
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00005D87 File Offset: 0x00003F87
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
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005DB0 File Offset: 0x00003FB0
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005DA8 File Offset: 0x00003FA8
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005DFC File Offset: 0x00003FFC
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005E09 File Offset: 0x00004009
		public CompressionStrategy Strategy
		{
			get
			{
				return this._baseStream.Strategy;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream.Strategy = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005E2A File Offset: 0x0000402A
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005E3C File Offset: 0x0000403C
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005E50 File Offset: 0x00004050
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005E98 File Offset: 0x00004098
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005EE0 File Offset: 0x000040E0
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005F24 File Offset: 0x00004124
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005F68 File Offset: 0x00004168
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005F88 File Offset: 0x00004188
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005FAB File Offset: 0x000041AB
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x04000083 RID: 131
		private bool _disposed;

		// Token: 0x04000084 RID: 132
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000085 RID: 133
		internal Stream _innerStream;
	}
}
