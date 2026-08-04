using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x0200004C RID: 76
	public class BytesOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00010784 File Offset: 0x0000E984
		public BytesOutOfRangeException()
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001078C File Offset: 0x0000E98C
		public BytesOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00010795 File Offset: 0x0000E995
		public BytesOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001079F File Offset: 0x0000E99F
		public BytesOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
