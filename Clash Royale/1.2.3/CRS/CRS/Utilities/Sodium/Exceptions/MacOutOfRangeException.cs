using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x0200004E RID: 78
	public class MacOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00010784 File Offset: 0x0000E984
		public MacOutOfRangeException()
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001078C File Offset: 0x0000E98C
		public MacOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00010795 File Offset: 0x0000E995
		public MacOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001079F File Offset: 0x0000E99F
		public MacOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
