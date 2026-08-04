using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x02000014 RID: 20
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	public class ZlibException : Exception
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00009505 File Offset: 0x00007705
		public ZlibException()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000950D File Offset: 0x0000770D
		public ZlibException(string s)
			: base(s)
		{
		}
	}
}
