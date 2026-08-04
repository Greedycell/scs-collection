using System;
using System.Collections.Generic;
using System.IO;

namespace CRS.Files.CSV
{
	// Token: 0x0200000F RID: 15
	internal class CSVTable
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public CSVTable(string filePath)
		{
			this.m_vCSVRows = new List<CSVRow>();
			this.m_vColumnHeaders = new List<string>();
			this.m_vColumnTypes = new List<string>();
			this.m_vCSVColumns = new List<CSVColumn>();
			using (StreamReader streamReader = new StreamReader(filePath))
			{
				foreach (string text in streamReader.ReadLine().Replace("\"", string.Empty).Replace(" ", string.Empty)
					.Split(new char[] { ',' }))
				{
					this.m_vColumnHeaders.Add(text);
					this.m_vCSVColumns.Add(new CSVColumn());
				}
				foreach (string text2 in streamReader.ReadLine().Replace("\"", string.Empty).Split(new char[] { ',' }))
				{
					this.m_vColumnTypes.Add(text2);
				}
				while (!streamReader.EndOfStream)
				{
					string[] array2 = streamReader.ReadLine().Replace("\"", string.Empty).Split(new char[] { ',' });
					if (array2[0] != string.Empty)
					{
						this.CreateRow();
					}
					for (int j = 0; j < this.m_vColumnHeaders.Count; j++)
					{
						this.m_vCSVColumns[j].Add(array2[j]);
					}
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F30 File Offset: 0x00001130
		public void AddRow(CSVRow row)
		{
			this.m_vCSVRows.Add(row);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F3E File Offset: 0x0000113E
		public void CreateRow()
		{
			new CSVRow(this);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F48 File Offset: 0x00001148
		public int GetArraySizeAt(CSVRow row, int columnIndex)
		{
			int num = this.m_vCSVRows.IndexOf(row);
			if (num == -1)
			{
				return 0;
			}
			CSVColumn csvcolumn = this.m_vCSVColumns[columnIndex];
			int num2;
			if (num + 1 >= this.m_vCSVRows.Count)
			{
				num2 = csvcolumn.GetSize();
			}
			else
			{
				num2 = this.m_vCSVRows[num + 1].GetRowOffset();
			}
			return CSVColumn.GetArraySize(row.GetRowOffset(), num2);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FB0 File Offset: 0x000011B0
		public int GetColumnIndexByName(string name)
		{
			return this.m_vColumnHeaders.IndexOf(name);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002FBE File Offset: 0x000011BE
		public string GetColumnName(int index)
		{
			return this.m_vColumnHeaders[index];
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FCC File Offset: 0x000011CC
		public int GetColumnRowCount()
		{
			int num = 0;
			if (this.m_vCSVColumns.Count > 0)
			{
				num = this.m_vCSVColumns[0].GetSize();
			}
			return num;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FFC File Offset: 0x000011FC
		public CSVRow GetRowAt(int index)
		{
			return this.m_vCSVRows[index];
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000300A File Offset: 0x0000120A
		public int GetRowCount()
		{
			return this.m_vCSVRows.Count;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003018 File Offset: 0x00001218
		public string GetValue(string name, int level)
		{
			int num = this.m_vColumnHeaders.IndexOf(name);
			return this.GetValueAt(num, level);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000303A File Offset: 0x0000123A
		public string GetValueAt(int column, int row)
		{
			return this.m_vCSVColumns[column].Get(row);
		}

		// Token: 0x04000015 RID: 21
		private readonly List<string> m_vColumnHeaders;

		// Token: 0x04000016 RID: 22
		private readonly List<string> m_vColumnTypes;

		// Token: 0x04000017 RID: 23
		private readonly List<CSVColumn> m_vCSVColumns;

		// Token: 0x04000018 RID: 24
		private readonly List<CSVRow> m_vCSVRows;
	}
}
