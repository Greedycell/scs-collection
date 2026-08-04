using System;
using System.IO;
using System.Text;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000019 RID: 25
	public class GZipStream : Stream
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005FCE File Offset: 0x000041CE
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00005FD6 File Offset: 0x000041D6
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005FF2 File Offset: 0x000041F2
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00005FFC File Offset: 0x000041FC
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				if (this._FileName == null)
				{
					return;
				}
				if (this._FileName.IndexOf("/") != -1)
				{
					this._FileName = this._FileName.Replace("/", "\\");
				}
				if (this._FileName.EndsWith("\\"))
				{
					throw new Exception("Illegal filename");
				}
				if (this._FileName.IndexOf("\\") != -1)
				{
					this._FileName = Path.GetFileName(this._FileName);
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000609B File Offset: 0x0000429B
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000060A3 File Offset: 0x000042A3
		public int Crc32 { get; private set; }

		// Token: 0x060000E9 RID: 233 RVA: 0x000060AC File Offset: 0x000042AC
		public GZipStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000060B8 File Offset: 0x000042B8
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000060C4 File Offset: 0x000042C4
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000060D0 File Offset: 0x000042D0
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000060ED File Offset: 0x000042ED
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000060FA File Offset: 0x000042FA
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
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000611B File Offset: 0x0000431B
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00006128 File Offset: 0x00004328
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
					throw new ObjectDisposedException("GZipStream");
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006194 File Offset: 0x00004394
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000061A6 File Offset: 0x000043A6
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000061B8 File Offset: 0x000043B8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
						this.Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006218 File Offset: 0x00004418
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000036C8 File Offset: 0x000018C8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000623D File Offset: 0x0000443D
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006262 File Offset: 0x00004462
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006284 File Offset: 0x00004484
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut + (long)this._headerByteCount;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000062E8 File Offset: 0x000044E8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int num = this._baseStream.Read(buffer, offset, count);
			if (!this._firstReadDone)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return num;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006348 File Offset: 0x00004548
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._baseStream._wantCompress)
				{
					throw new InvalidOperationException();
				}
				this._headerByteCount = this.EmitHeader();
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000063A8 File Offset: 0x000045A8
		private int EmitHeader()
		{
			byte[] array = ((this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment));
			byte[] array2 = ((this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName));
			int num = ((this.Comment == null) ? 0 : (array.Length + 1));
			int num2 = ((this.FileName == null) ? 0 : (array2.Length + 1));
			byte[] array3 = new byte[10 + num + num2];
			int num3 = 0;
			array3[num3++] = 31;
			array3[num3++] = 139;
			array3[num3++] = 8;
			byte b = 0;
			if (this.Comment != null)
			{
				b ^= 16;
			}
			if (this.FileName != null)
			{
				b ^= 8;
			}
			array3[num3++] = b;
			if (this.LastModified == null)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			Array.Copy(BitConverter.GetBytes((int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds), 0, array3, num3, 4);
			num3 += 4;
			array3[num3++] = 0;
			array3[num3++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num3, num2 - 1);
				num3 += num2 - 1;
				array3[num3++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num3, num - 1);
				num3 += num - 1;
				array3[num3++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006544 File Offset: 0x00004744
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000658C File Offset: 0x0000478C
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000065D4 File Offset: 0x000047D4
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006618 File Offset: 0x00004818
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x04000086 RID: 134
		public DateTime? LastModified;

		// Token: 0x04000088 RID: 136
		private int _headerByteCount;

		// Token: 0x04000089 RID: 137
		internal ZlibBaseStream _baseStream;

		// Token: 0x0400008A RID: 138
		private bool _disposed;

		// Token: 0x0400008B RID: 139
		private bool _firstReadDone;

		// Token: 0x0400008C RID: 140
		private string _FileName;

		// Token: 0x0400008D RID: 141
		private string _Comment;

		// Token: 0x0400008E RID: 142
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400008F RID: 143
		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");
	}
}
