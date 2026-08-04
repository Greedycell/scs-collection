using System;

namespace UCS.Files
{
	// Token: 0x0200002A RID: 42
	internal class CSVRow
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000CC51 File Offset: 0x0000AE51
		public CSVRow(CSVTable table)
		{
			this.m_vCSVTable = table;
			this.m_vRowStart = this.m_vCSVTable.GetColumnRowCount();
			this.m_vCSVTable.AddRow(this);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public int GetArraySize(string name)
		{
			int columnIndexByName = this.m_vCSVTable.GetColumnIndexByName(name);
			int num = 0;
			if (columnIndexByName != -1)
			{
				num = this.m_vCSVTable.GetArraySizeAt(this, columnIndexByName);
			}
			return num;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000CCAF File Offset: 0x0000AEAF
		public string GetName()
		{
			return this.m_vCSVTable.GetValueAt(0, this.m_vRowStart);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000CCC3 File Offset: 0x0000AEC3
		public int GetRowOffset()
		{
			return this.m_vRowStart;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000CCCB File Offset: 0x0000AECB
		public string GetValue(string name, int level)
		{
			return this.m_vCSVTable.GetValue(name, level + this.m_vRowStart);
		}

		// Token: 0x0400015F RID: 351
		private readonly CSVTable m_vCSVTable;

		// Token: 0x04000160 RID: 352
		private readonly int m_vRowStart;
	}
}
