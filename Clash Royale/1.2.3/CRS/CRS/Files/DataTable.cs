using System;
using System.Collections.Generic;
using UCS.Logic;

namespace UCS.Files
{
	// Token: 0x0200002D RID: 45
	internal class DataTable
	{
		// Token: 0x0600019F RID: 415 RVA: 0x0000D01B File Offset: 0x0000B21B
		public DataTable()
		{
			this.m_vIndex = 0;
			this.m_vData = new List<Data>();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000D038 File Offset: 0x0000B238
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

		// Token: 0x060001A1 RID: 417 RVA: 0x0000D08C File Offset: 0x0000B28C
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

		// Token: 0x060001A2 RID: 418 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
		public Data GetDataByName(string name)
		{
			return this.m_vData.Find((Data d) => d.GetName() == name);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000D0F9 File Offset: 0x0000B2F9
		public Data GetItemAt(int index)
		{
			return this.m_vData[index];
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000D108 File Offset: 0x0000B308
		public Data GetItemById(int id)
		{
			int instanceID = GlobalID.GetInstanceID(id);
			return this.m_vData[instanceID];
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000D128 File Offset: 0x0000B328
		public int GetItemCount()
		{
			return this.m_vData.Count;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000D135 File Offset: 0x0000B335
		public int GetTableIndex()
		{
			return this.m_vIndex;
		}

		// Token: 0x04000166 RID: 358
		protected List<Data> m_vData;

		// Token: 0x04000167 RID: 359
		protected int m_vIndex;
	}
}
