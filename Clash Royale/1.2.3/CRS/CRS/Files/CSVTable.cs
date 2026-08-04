using System;
using System.Collections.Generic;
using System.IO;

namespace UCS.Files
{
	// Token: 0x0200002B RID: 43
	internal class CSVTable
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000CCE4 File Offset: 0x0000AEE4
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

		// Token: 0x06000191 RID: 401 RVA: 0x0000CE74 File Offset: 0x0000B074
		public void AddRow(CSVRow row)
		{
			this.m_vCSVRows.Add(row);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000CE82 File Offset: 0x0000B082
		public void CreateRow()
		{
			new CSVRow(this);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000CE8C File Offset: 0x0000B08C
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

		// Token: 0x06000194 RID: 404 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		public int GetColumnIndexByName(string name)
		{
			return this.m_vColumnHeaders.IndexOf(name);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000CF02 File Offset: 0x0000B102
		public string GetColumnName(int index)
		{
			return this.m_vColumnHeaders[index];
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000CF10 File Offset: 0x0000B110
		public int GetColumnRowCount()
		{
			int num = 0;
			if (this.m_vCSVColumns.Count > 0)
			{
				num = this.m_vCSVColumns[0].GetSize();
			}
			return num;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000CF40 File Offset: 0x0000B140
		public CSVRow GetRowAt(int index)
		{
			return this.m_vCSVRows[index];
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000CF4E File Offset: 0x0000B14E
		public int GetRowCount()
		{
			return this.m_vCSVRows.Count;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public string GetValue(string name, int level)
		{
			int num = this.m_vColumnHeaders.IndexOf(name);
			return this.GetValueAt(num, level);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000CF7E File Offset: 0x0000B17E
		public string GetValueAt(int column, int row)
		{
			return this.m_vCSVColumns[column].Get(row);
		}

		// Token: 0x04000161 RID: 353
		private readonly List<string> m_vColumnHeaders;

		// Token: 0x04000162 RID: 354
		private readonly List<string> m_vColumnTypes;

		// Token: 0x04000163 RID: 355
		private readonly List<CSVColumn> m_vCSVColumns;

		// Token: 0x04000164 RID: 356
		private readonly List<CSVRow> m_vCSVRows;
	}
}
