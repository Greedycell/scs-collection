using System;
using System.Data.Entity;

namespace CRS.Database
{
	// Token: 0x02000010 RID: 16
	internal class ucsdbEntities : DbContext
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000304E File Offset: 0x0000124E
		public ucsdbEntities(string connectionString)
			: base("name=" + connectionString)
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003061 File Offset: 0x00001261
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003069 File Offset: 0x00001269
		public virtual DbSet<clan> clan { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003072 File Offset: 0x00001272
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000307A File Offset: 0x0000127A
		public virtual DbSet<player> player { get; set; }
	}
}
