using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x02000053 RID: 83
	public class SignatureOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x00010784 File Offset: 0x0000E984
		public SignatureOutOfRangeException()
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001078C File Offset: 0x0000E98C
		public SignatureOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010795 File Offset: 0x0000E995
		public SignatureOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001079F File Offset: 0x0000E99F
		public SignatureOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
