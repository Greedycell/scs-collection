using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x0200004B RID: 75
	public class AdditionalDataOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00010784 File Offset: 0x0000E984
		public AdditionalDataOutOfRangeException()
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001078C File Offset: 0x0000E98C
		public AdditionalDataOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00010795 File Offset: 0x0000E995
		public AdditionalDataOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001079F File Offset: 0x0000E99F
		public AdditionalDataOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
