using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x02000050 RID: 80
	public class PersonalOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00010784 File Offset: 0x0000E984
		public PersonalOutOfRangeException()
		{
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001078C File Offset: 0x0000E98C
		public PersonalOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010795 File Offset: 0x0000E995
		public PersonalOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001079F File Offset: 0x0000E99F
		public PersonalOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
