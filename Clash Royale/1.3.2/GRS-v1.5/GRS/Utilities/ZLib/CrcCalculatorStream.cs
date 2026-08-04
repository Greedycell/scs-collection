using System;
using System.IO;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000014 RID: 20
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000035E4 File Offset: 0x000017E4
		public CrcCalculatorStream(Stream stream)
			: this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000035F4 File Offset: 0x000017F4
		public CrcCalculatorStream(Stream stream, bool leaveOpen)
			: this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003604 File Offset: 0x00001804
		public CrcCalculatorStream(Stream stream, long length)
			: this(true, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003620 File Offset: 0x00001820
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
			: this(leaveOpen, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000363C File Offset: 0x0000183C
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32)
			: this(leaveOpen, length, stream, crc32)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003659 File Offset: 0x00001859
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = crc32 ?? new CRC32();
			this._lengthLimit = length;
			this.LeaveOpen = leaveOpen;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003690 File Offset: 0x00001890
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000369D File Offset: 0x0000189D
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000036AA File Offset: 0x000018AA
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000036B2 File Offset: 0x000018B2
		public bool LeaveOpen { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000036BB File Offset: 0x000018BB
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000036C8 File Offset: 0x000018C8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000036CB File Offset: 0x000018CB
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000036D8 File Offset: 0x000018D8
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003690 File Offset: 0x00001890
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000036F9 File Offset: 0x000018F9
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

		// Token: 0x06000096 RID: 150 RVA: 0x00003700 File Offset: 0x00001900
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003708 File Offset: 0x00001908
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

		// Token: 0x06000098 RID: 152 RVA: 0x00003776 File Offset: 0x00001976
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003798 File Offset: 0x00001998
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000036F9 File Offset: 0x000018F9
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000036F9 File Offset: 0x000018F9
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000037A5 File Offset: 0x000019A5
		public override void Close()
		{
			base.Close();
			if (!this.LeaveOpen)
			{
				this._innerStream.Close();
			}
		}

		// Token: 0x0400002B RID: 43
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x0400002C RID: 44
		private readonly CRC32 _Crc32;

		// Token: 0x0400002D RID: 45
		private readonly long _lengthLimit = -99L;

		// Token: 0x0400002E RID: 46
		internal Stream _innerStream;
	}
}
