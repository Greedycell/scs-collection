using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CRS.Utilities.ZLib
{
	// Token: 0x0200001A RID: 26
	internal class ZlibBaseStream : Stream
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00009836 File Offset: 0x00007A36
		internal int Crc32
		{
			get
			{
				if (this.crc == null)
				{
					return 0;
				}
				return this.crc.Crc32Result;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009850 File Offset: 0x00007A50
		public ZlibBaseStream(Stream stream, CompressionMode compressionMode, CompressionLevel level, ZlibStreamFlavor flavor, bool leaveOpen)
		{
			this._flushMode = FlushType.None;
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._compressionMode = compressionMode;
			this._flavor = flavor;
			this._level = level;
			if (flavor == ZlibStreamFlavor.GZIP)
			{
				this.crc = new CRC32();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000098C1 File Offset: 0x00007AC1
		protected internal bool _wantCompress
		{
			get
			{
				return this._compressionMode == CompressionMode.Compress;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000098CC File Offset: 0x00007ACC
		private ZlibCodec z
		{
			get
			{
				if (this._z == null)
				{
					bool flag = this._flavor == ZlibStreamFlavor.ZLIB;
					this._z = new ZlibCodec();
					if (this._compressionMode == CompressionMode.Decompress)
					{
						this._z.InitializeInflate(flag);
					}
					else
					{
						this._z.Strategy = this.Strategy;
						this._z.InitializeDeflate(this._level, flag);
					}
				}
				return this._z;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000993C File Offset: 0x00007B3C
		private byte[] workingBuffer
		{
			get
			{
				if (this._workingBuffer == null)
				{
					this._workingBuffer = new byte[this._bufferSize];
				}
				return this._workingBuffer;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00009960 File Offset: 0x00007B60
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.crc != null)
			{
				this.crc.SlurpBlock(buffer, offset, count);
			}
			if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				this._streamMode = ZlibBaseStream.StreamMode.Writer;
			}
			else if (this._streamMode != ZlibBaseStream.StreamMode.Writer)
			{
				throw new ZlibException("Cannot Write after Reading.");
			}
			if (count == 0)
			{
				return;
			}
			this.z.InputBuffer = buffer;
			this._z.NextIn = offset;
			this._z.AvailableBytesIn = count;
			for (; ; )
			{
				this._z.OutputBuffer = this.workingBuffer;
				this._z.NextOut = 0;
				this._z.AvailableBytesOut = this._workingBuffer.Length;
				int num = (this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode));
				if (num != 0 && num != 1)
				{
					break;
				}
				this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
				bool flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
				if (this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress)
				{
					flag = this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0;
				}
				if (flag)
				{
					return;
				}
			}
			throw new ZlibException((this._wantCompress ? "de" : "in") + "flating: " + this._z.Message);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00009AE8 File Offset: 0x00007CE8
		private void finish()
		{
			if (this._z == null)
			{
				return;
			}
			if (this._streamMode == ZlibBaseStream.StreamMode.Writer)
			{
				int num;
				for (; ; )
				{
					this._z.OutputBuffer = this.workingBuffer;
					this._z.NextOut = 0;
					this._z.AvailableBytesOut = this._workingBuffer.Length;
					num = (this._wantCompress ? this._z.Deflate(FlushType.Finish) : this._z.Inflate(FlushType.Finish));
					if (num != 1 && num != 0)
					{
						break;
					}
					if (this._workingBuffer.Length - this._z.AvailableBytesOut > 0)
					{
						this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
					}
					bool flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
					if (this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress)
					{
						flag = this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0;
					}
					if (flag)
					{
						goto Block_12;
					}
				}
				string text = (this._wantCompress ? "de" : "in") + "flating";
				if (this._z.Message == null)
				{
					throw new ZlibException(string.Format("{0}: (rc = {1})", text, num));
				}
				throw new ZlibException(text + ": " + this._z.Message);
			Block_12:
				this.Flush();
				if (this._flavor == ZlibStreamFlavor.GZIP)
				{
					if (this._wantCompress)
					{
						int crc32Result = this.crc.Crc32Result;
						this._stream.Write(BitConverter.GetBytes(crc32Result), 0, 4);
						int num2 = unchecked((int)(this.crc.TotalBytesRead & (long)(ulong)(-1)));
						this._stream.Write(BitConverter.GetBytes(num2), 0, 4);
						return;
					}
					throw new ZlibException("Writing with decompression is not supported.");
				}
			}
			else if (this._streamMode == ZlibBaseStream.StreamMode.Reader && this._flavor == ZlibStreamFlavor.GZIP)
			{
				if (this._wantCompress)
				{
					throw new ZlibException("Reading with compression is not supported.");
				}
				if (this._z.TotalBytesOut == 0L)
				{
					return;
				}
				byte[] array = new byte[8];
				if (this._z.AvailableBytesIn < 8)
				{
					Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, this._z.AvailableBytesIn);
					int num3 = 8 - this._z.AvailableBytesIn;
					int num4 = this._stream.Read(array, this._z.AvailableBytesIn, num3);
					if (num3 != num4)
					{
						throw new ZlibException(string.Format("Missing or incomplete GZIP trailer. Expected 8 bytes, got {0}.", this._z.AvailableBytesIn + num4));
					}
				}
				else
				{
					Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, array.Length);
				}
				int num5 = BitConverter.ToInt32(array, 0);
				int crc32Result2 = this.crc.Crc32Result;
				int num6 = BitConverter.ToInt32(array, 4);
				int num7 = unchecked((int)(this._z.TotalBytesOut & (long)(ulong)(-1)));
				if (crc32Result2 != num5)
				{
					throw new ZlibException(string.Format("Bad CRC32 in GZIP trailer. (actual({0:X8})!=expected({1:X8}))", crc32Result2, num5));
				}
				if (num7 != num6)
				{
					throw new ZlibException(string.Format("Bad size in GZIP trailer. (actual({0})!=expected({1}))", num7, num6));
				}
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00009E38 File Offset: 0x00008038
		private void end()
		{
			if (this.z == null)
			{
				return;
			}
			if (this._wantCompress)
			{
				this._z.EndDeflate();
			}
			else
			{
				this._z.EndInflate();
			}
			this._z = null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009E6C File Offset: 0x0000806C
		public override void Close()
		{
			if (this._stream == null)
			{
				return;
			}
			try
			{
				this.finish();
			}
			finally
			{
				this.end();
				if (!this._leaveOpen)
				{
					this._stream.Close();
				}
				this._stream = null;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00009EBC File Offset: 0x000080BC
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004787 File Offset: 0x00002987
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00009EC9 File Offset: 0x000080C9
		public override void SetLength(long value)
		{
			this._stream.SetLength(value);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009ED8 File Offset: 0x000080D8
		private string ReadZeroTerminatedString()
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			while (this._stream.Read(this._buf1, 0, 1) == 1)
			{
				if (this._buf1[0] == 0)
				{
					flag = true;
				}
				else
				{
					list.Add(this._buf1[0]);
				}
				if (flag)
				{
					byte[] array = list.ToArray();
					return GZipStream.iso8859dash1.GetString(array, 0, array.Length);
				}
			}
			throw new ZlibException("Unexpected EOF reading GZIP header.");
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00009F44 File Offset: 0x00008144
		private int _ReadAndValidateGzipHeader()
		{
			int num = 0;
			byte[] array = new byte[10];
			int num2 = this._stream.Read(array, 0, array.Length);
			if (num2 == 0)
			{
				return 0;
			}
			if (num2 != 10)
			{
				throw new ZlibException("Not a valid GZIP stream.");
			}
			if (array[0] != 31 || array[1] != 139 || array[2] != 8)
			{
				throw new ZlibException("Bad GZIP header.");
			}
			int num3 = BitConverter.ToInt32(array, 4);
			this._GzipMtime = GZipStream._unixEpoch.AddSeconds((double)num3);
			num += num2;
			if ((array[3] & 4) == 4)
			{
				num2 = this._stream.Read(array, 0, 2);
				num += num2;
				short num4 = (short)((int)array[0] + (int)array[1] * 256);
				byte[] array2 = new byte[(int)num4];
				num2 = this._stream.Read(array2, 0, array2.Length);
				if (num2 != (int)num4)
				{
					throw new ZlibException("Unexpected end-of-file reading GZIP header.");
				}
				num += num2;
			}
			if ((array[3] & 8) == 8)
			{
				this._GzipFileName = this.ReadZeroTerminatedString();
			}
			if ((array[3] & 16) == 16)
			{
				this._GzipComment = this.ReadZeroTerminatedString();
			}
			if ((array[3] & 2) == 2)
			{
				this.Read(this._buf1, 0, 1);
			}
			return num;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A064 File Offset: 0x00008264
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._stream.CanRead)
				{
					throw new ZlibException("The stream is not readable.");
				}
				this._streamMode = ZlibBaseStream.StreamMode.Reader;
				this.z.AvailableBytesIn = 0;
				if (this._flavor == ZlibStreamFlavor.GZIP)
				{
					this._gzipHeaderByteCount = this._ReadAndValidateGzipHeader();
					if (this._gzipHeaderByteCount == 0)
					{
						return 0;
					}
				}
			}
			if (this._streamMode != ZlibBaseStream.StreamMode.Reader)
			{
				throw new ZlibException("Cannot Read after Writing.");
			}
			if (count == 0)
			{
				return 0;
			}
			if (this.nomoreinput && this._wantCompress)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < buffer.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.GetLength(0))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._z.OutputBuffer = buffer;
			this._z.NextOut = offset;
			this._z.AvailableBytesOut = count;
			this._z.InputBuffer = this.workingBuffer;
			int num;
			for (; ; )
			{
				if (this._z.AvailableBytesIn == 0 && !this.nomoreinput)
				{
					this._z.NextIn = 0;
					this._z.AvailableBytesIn = this._stream.Read(this._workingBuffer, 0, this._workingBuffer.Length);
					if (this._z.AvailableBytesIn == 0)
					{
						this.nomoreinput = true;
					}
				}
				num = (this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode));
				if (this.nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_20;
				}
				if (((this.nomoreinput || num == 1) && this._z.AvailableBytesOut == count) || this._z.AvailableBytesOut <= 0 || this.nomoreinput || num != 0)
				{
					goto IL_020A;
				}
			}
			return 0;
		Block_20:
			throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", this._wantCompress ? "de" : "in", num, this._z.Message));
		IL_020A:
			if (this._z.AvailableBytesOut > 0)
			{
				if (num == 0)
				{
					int availableBytesIn = this._z.AvailableBytesIn;
				}
				if (this.nomoreinput && this._wantCompress)
				{
					num = this._z.Deflate(FlushType.Finish);
					if (num != 0 && num != 1)
					{
						throw new ZlibException(string.Format("Deflating:  rc={0}  msg={1}", num, this._z.Message));
					}
				}
			}
			num = count - this._z.AvailableBytesOut;
			if (this.crc != null)
			{
				this.crc.SlurpBlock(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000A302 File Offset: 0x00008502
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000A30F File Offset: 0x0000850F
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000A31C File Offset: 0x0000851C
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000A329 File Offset: 0x00008529
		public override long Length
		{
			get
			{
				return this._stream.Length;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004787 File Offset: 0x00002987
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004787 File Offset: 0x00002987
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000A338 File Offset: 0x00008538
		public static void CompressString(string s, Stream compressor)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
			try
			{
				compressor.Write(bytes, 0, bytes.Length);
			}
			finally
			{
				if (compressor != null)
				{
					((IDisposable)compressor).Dispose();
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000A37C File Offset: 0x0000857C
		public static void CompressBuffer(byte[] b, Stream compressor)
		{
			try
			{
				compressor.Write(b, 0, b.Length);
			}
			finally
			{
				if (compressor != null)
				{
					((IDisposable)compressor).Dispose();
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public static string UncompressString(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			System.Text.Encoding utf = System.Text.Encoding.UTF8;
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int num;
					while ((num = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						((IDisposable)decompressor).Dispose();
					}
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				text = new StreamReader(memoryStream, utf).ReadToEnd();
			}
			return text;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000A444 File Offset: 0x00008644
		public static byte[] UncompressBuffer(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int num;
					while ((num = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						((IDisposable)decompressor).Dispose();
					}
				}
				array2 = memoryStream.ToArray();
			}
			return array2;
		}

		// Token: 0x04000117 RID: 279
		protected internal ZlibCodec _z;

		// Token: 0x04000118 RID: 280
		protected internal ZlibBaseStream.StreamMode _streamMode = ZlibBaseStream.StreamMode.Undefined;

		// Token: 0x04000119 RID: 281
		protected internal FlushType _flushMode;

		// Token: 0x0400011A RID: 282
		protected internal ZlibStreamFlavor _flavor;

		// Token: 0x0400011B RID: 283
		protected internal CompressionMode _compressionMode;

		// Token: 0x0400011C RID: 284
		protected internal CompressionLevel _level;

		// Token: 0x0400011D RID: 285
		protected internal bool _leaveOpen;

		// Token: 0x0400011E RID: 286
		protected internal byte[] _workingBuffer;

		// Token: 0x0400011F RID: 287
		protected internal int _bufferSize = 16384;

		// Token: 0x04000120 RID: 288
		protected internal byte[] _buf1 = new byte[1];

		// Token: 0x04000121 RID: 289
		protected internal Stream _stream;

		// Token: 0x04000122 RID: 290
		protected internal CompressionStrategy Strategy;

		// Token: 0x04000123 RID: 291
		private readonly CRC32 crc;

		// Token: 0x04000124 RID: 292
		protected internal string _GzipFileName;

		// Token: 0x04000125 RID: 293
		protected internal string _GzipComment;

		// Token: 0x04000126 RID: 294
		protected internal DateTime _GzipMtime;

		// Token: 0x04000127 RID: 295
		protected internal int _gzipHeaderByteCount;

		// Token: 0x04000128 RID: 296
		private bool nomoreinput;

		// Token: 0x020000CB RID: 203
		internal enum StreamMode
		{
			// Token: 0x04000395 RID: 917
			Writer,
			// Token: 0x04000396 RID: 918
			Reader,
			// Token: 0x04000397 RID: 919
			Undefined
		}
	}
}