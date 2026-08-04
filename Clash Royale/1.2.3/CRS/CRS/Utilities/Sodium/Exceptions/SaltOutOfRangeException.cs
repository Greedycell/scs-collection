using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x02000051 RID: 81
	public class SaltOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x00010784 File Offset: 0x0000E984
		public SaltOutOfRangeException()
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001078C File Offset: 0x0000E98C
		public SaltOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00010795 File Offset: 0x0000E995
		public SaltOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001079F File Offset: 0x0000E99F
		public SaltOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
