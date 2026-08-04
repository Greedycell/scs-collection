using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x0200004F RID: 79
	public class NonceOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00010784 File Offset: 0x0000E984
		public NonceOutOfRangeException()
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001078C File Offset: 0x0000E98C
		public NonceOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00010795 File Offset: 0x0000E995
		public NonceOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001079F File Offset: 0x0000E99F
		public NonceOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
