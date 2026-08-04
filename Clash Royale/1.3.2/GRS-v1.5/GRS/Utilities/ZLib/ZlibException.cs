using System;
using System.Runtime.InteropServices;

namespace CRS.Utilities.ZLib
{
	// Token: 0x02000027 RID: 39
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	public class ZlibException : Exception
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000AC91 File Offset: 0x00008E91
		public ZlibException()
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000AC99 File Offset: 0x00008E99
		public ZlibException(string s)
			: base(s)
		{
		}
	}
}
