using System;
using System.Data.Entity;

namespace CRS.Database
{
	// Token: 0x02000031 RID: 49
	public partial class ucsdbEntities : DbContext
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		public ucsdbEntities(string connectionString)
			: base("name=" + connectionString)
		{
		}
	}
}
