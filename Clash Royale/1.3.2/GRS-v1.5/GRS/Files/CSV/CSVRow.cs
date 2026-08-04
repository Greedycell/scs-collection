using System;

namespace CRS.Files.CSV
{
	// Token: 0x0200000E RID: 14
	internal class CSVRow
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002D0F File Offset: 0x00000F0F
		public CSVRow(CSVTable table)
		{
			this.m_vCSVTable = table;
			this.m_vRowStart = this.m_vCSVTable.GetColumnRowCount();
			this.m_vCSVTable.AddRow(this);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D3C File Offset: 0x00000F3C
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

		// Token: 0x06000049 RID: 73 RVA: 0x00002D6B File Offset: 0x00000F6B
		public string GetName()
		{
			return this.m_vCSVTable.GetValueAt(0, this.m_vRowStart);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D7F File Offset: 0x00000F7F
		public int GetRowOffset()
		{
			return this.m_vRowStart;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D87 File Offset: 0x00000F87
		public string GetValue(string name, int level)
		{
			return this.m_vCSVTable.GetValue(name, level + this.m_vRowStart);
		}

		// Token: 0x04000013 RID: 19
		private readonly CSVTable m_vCSVTable;

		// Token: 0x04000014 RID: 20
		private readonly int m_vRowStart;
	}
}
