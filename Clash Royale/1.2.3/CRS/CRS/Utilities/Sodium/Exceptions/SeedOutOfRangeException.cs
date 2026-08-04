using System;

namespace UCS.Utilities.Sodium.Exceptions
{
	// Token: 0x02000052 RID: 82
	public class SeedOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00010784 File Offset: 0x0000E984
		public SeedOutOfRangeException()
		{
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001078C File Offset: 0x0000E98C
		public SeedOutOfRangeException(string message)
			: base(message)
		{
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00010795 File Offset: 0x0000E995
		public SeedOutOfRangeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001079F File Offset: 0x0000E99F
		public SeedOutOfRangeException(string paramName, object actualValue, string message)
			: base(paramName, actualValue, message)
		{
		}
	}
}
