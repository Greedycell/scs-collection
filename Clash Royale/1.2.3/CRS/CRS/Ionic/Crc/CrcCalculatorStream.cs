using System;
using System.IO;

namespace Ionic.Crc
{
	// Token: 0x0200001F RID: 31
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x0600012A RID: 298 RVA: 0x0000B160 File Offset: 0x00009360
		public CrcCalculatorStream(Stream stream)
			: this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000B170 File Offset: 0x00009370
		public CrcCalculatorStream(Stream stream, bool leaveOpen)
			: this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000B180 File Offset: 0x00009380
		public CrcCalculatorStream(Stream stream, long length)
			: this(true, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000B19C File Offset: 0x0000939C
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
			: this(leaveOpen, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32)
			: this(leaveOpen, length, stream, crc32)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000B1D5 File Offset: 0x000093D5
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = crc32 ?? new CRC32();
			this._lengthLimit = length;
			this.LeaveOpen = leaveOpen;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000B20C File Offset: 0x0000940C
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000B219 File Offset: 0x00009419
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000B226 File Offset: 0x00009426
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000B22E File Offset: 0x0000942E
		public bool LeaveOpen { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000B237 File Offset: 0x00009437
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000475F File Offset: 0x0000295F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000B244 File Offset: 0x00009444
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000B251 File Offset: 0x00009451
		public override long Length
		{
			get
			{
				if (this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit)
				{
					return this._innerStream.Length;
				}
				return this._lengthLimit;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000B20C File Offset: 0x0000940C
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00008EDD File Offset: 0x000070DD
		public override long Position
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000B272 File Offset: 0x00009472
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000B27C File Offset: 0x0000947C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = count;
			if (this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit)
			{
				if (this._Crc32.TotalBytesRead >= this._lengthLimit)
				{
					return 0;
				}
				long num2 = this._lengthLimit - this._Crc32.TotalBytesRead;
				if (num2 < (long)count)
				{
					num = (int)num2;
				}
			}
			int num3 = this._innerStream.Read(buffer, offset, num);
			if (num3 > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, num3);
			}
			return num3;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000B2EA File Offset: 0x000094EA
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000B30C File Offset: 0x0000950C
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00008EDD File Offset: 0x000070DD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00008EDD File Offset: 0x000070DD
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000B319 File Offset: 0x00009519
		public override void Close()
		{
			base.Close();
			if (!this.LeaveOpen)
			{
				this._innerStream.Close();
			}
		}

		// Token: 0x0400014A RID: 330
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x0400014B RID: 331
		private readonly CRC32 _Crc32;

		// Token: 0x0400014C RID: 332
		private readonly long _lengthLimit = -99L;

		// Token: 0x0400014D RID: 333
		internal Stream _innerStream;
	}
}
