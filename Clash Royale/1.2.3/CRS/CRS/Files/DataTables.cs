using System;
using System.Collections.Generic;
using UCS.Logic;

namespace UCS.Files
{
	// Token: 0x0200002C RID: 44
	internal class DataTables
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000CF94 File Offset: 0x0000B194
		public DataTables()
		{
			this.m_vDataTables = new List<DataTable>();
			for (int i = 0; i < 41; i++)
			{
				this.m_vDataTables.Add(new DataTable());
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		public Data GetDataById(int id)
		{
			int num = GlobalID.GetClassID(id) - 1;
			return this.m_vDataTables[num].GetItemById(id);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		public DataTable GetTable(int i)
		{
			return this.m_vDataTables[i];
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000D006 File Offset: 0x0000B206
		public void InitDataTable(CSVTable t, int index)
		{
			this.m_vDataTables[index] = new DataTable(t, index);
		}

		// Token: 0x04000165 RID: 357
		private readonly List<DataTable> m_vDataTables;
	}
}
