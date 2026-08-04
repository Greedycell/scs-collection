using System;
using System.IO;

namespace CRS.Utilities.LZMA.Compress.LzmaAlone
{
	// Token: 0x0200004F RID: 79
	public class CDoubleStream : Stream
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0001238F File Offset: 0x0001058F
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000036C8 File Offset: 0x000018C8
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000036C8 File Offset: 0x000018C8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00012392 File Offset: 0x00010592
		public override long Length
		{
			get
			{
				return this.s1.Length + this.s2.Length - this.skipSize;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000123B2 File Offset: 0x000105B2
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000123B6 File Offset: 0x000105B6
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void Flush()
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000123B8 File Offset: 0x000105B8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			while (count > 0)
			{
				if (this.fileIndex == 0)
				{
					int num2 = this.s1.Read(buffer, offset, count);
					offset += num2;
					count -= num2;
					num += num2;
					if (num2 == 0)
					{
						this.fileIndex++;
					}
				}
				if (this.fileIndex == 1)
				{
					return num + this.s2.Read(buffer, offset, count);
				}
			}
			return num;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00012420 File Offset: 0x00010620
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new Exception("can't Write");
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0001242C File Offset: 0x0001062C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new Exception("can't Seek");
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00012438 File Offset: 0x00010638
		public override void SetLength(long value)
		{
			throw new Exception("can't SetLength");
		}

		// Token: 0x0400024A RID: 586
		public Stream s1;

		// Token: 0x0400024B RID: 587
		public Stream s2;

		// Token: 0x0400024C RID: 588
		public int fileIndex;

		// Token: 0x0400024D RID: 589
		public long skipSize;
	}
}
