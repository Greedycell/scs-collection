using System;
using System.Collections.Generic;
using CRS.Files.CSV;

namespace CRS.Files.Logic
{
	// Token: 0x0200000A RID: 10
	internal class DataTables
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000028FC File Offset: 0x00000AFC
		public DataTables()
		{
			this.m_vDataTables = new List<DataTable>();
			for (int i = 0; i < 41; i++)
			{
				this.m_vDataTables.Add(new DataTable());
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002938 File Offset: 0x00000B38
		public Data GetDataById(int id)
		{
			int num = GlobalID.GetClassID(id) - 1;
			return this.m_vDataTables[num].GetItemById(id);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002960 File Offset: 0x00000B60
		public DataTable GetTable(int i)
		{
			return this.m_vDataTables[i];
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000296E File Offset: 0x00000B6E
		public void InitDataTable(CSVTable t, int index)
		{
			this.m_vDataTables[index] = new DataTable(t, index);
		}

		// Token: 0x0400000C RID: 12
		private readonly List<DataTable> m_vDataTables;
	}
}
