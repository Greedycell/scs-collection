using System;

namespace UCS.Utilities.Sodium
{
	// Token: 0x0200003B RID: 59
	public class LazyInvoke<T>
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000DBFE File Offset: 0x0000BDFE
		public LazyInvoke(string function, string library)
		{
			this._function = function;
			this._library = library;
			this._missing = true;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000DC1B File Offset: 0x0000BE1B
		public T Method
		{
			get
			{
				if (this._missing)
				{
					this._method = DynamicInvoke.GetDynamicInvoke<T>(this._function, this._library);
					this._missing = false;
				}
				return this._method;
			}
		}

		// Token: 0x0400018C RID: 396
		private readonly string _function;

		// Token: 0x0400018D RID: 397
		private readonly string _library;

		// Token: 0x0400018E RID: 398
		private T _method;

		// Token: 0x0400018F RID: 399
		private bool _missing;
	}
}
