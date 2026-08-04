using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x0200004D RID: 77
	public class KeyOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00010784 File Offset: 0x0000E984
		public KeyOutOfRangeException()
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001078C File Offset: 0x0000E98C
		public KeyOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010795 File Offset: 0x0000E995
		public KeyOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001079F File Offset: 0x0000E99F
		public KeyOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
