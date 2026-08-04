using System;
using System.Collections.Generic;
using CRS.Files.CSV;

namespace CRS.Files.Logic
{
	// Token: 0x0200000B RID: 11
	internal class DataTable
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002983 File Offset: 0x00000B83
		public DataTable()
		{
			this.m_vIndex = 0;
			this.m_vData = new List<Data>();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029A0 File Offset: 0x00000BA0
		public DataTable(CSVTable table, int index)
		{
			this.m_vIndex = index;
			this.m_vData = new List<Data>();
			for (int i = 0; i < table.GetRowCount(); i++)
			{
				CSVRow rowAt = table.GetRowAt(i);
				Data data = this.CreateItem(rowAt);
				this.m_vData.Add(data);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029F4 File Offset: 0x00000BF4
		public Data CreateItem(CSVRow row)
		{
			Data data = new Data(row, this);
			int vIndex = this.m_vIndex;
			if (vIndex != 23)
			{
				if (vIndex == 24)
				{
					data = new Data(row, this);
				}
			}
			else
			{
				data = new Data(row, this);
			}
			return data;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A30 File Offset: 0x00000C30
		public Data GetDataByName(string name)
		{
			return this.m_vData.Find((Data d) => d.GetName() == name);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A61 File Offset: 0x00000C61
		public Data GetItemAt(int index)
		{
			return this.m_vData[index];
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A70 File Offset: 0x00000C70
		public Data GetItemById(int id)
		{
			int instanceID = GlobalID.GetInstanceID(id);
			return this.m_vData[instanceID];
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A90 File Offset: 0x00000C90
		public int GetItemCount()
		{
			return this.m_vData.Count;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A9D File Offset: 0x00000C9D
		public int GetTableIndex()
		{
			return this.m_vIndex;
		}

		// Token: 0x0400000D RID: 13
		protected List<Data> m_vData;

		// Token: 0x0400000E RID: 14
		protected int m_vIndex;
	}
}
