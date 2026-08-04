using System;
using System.Collections.Generic;

namespace CRS.Files.CSV
{
	// Token: 0x0200000D RID: 13
	internal class CSVColumn
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002CCE File Offset: 0x00000ECE
		public CSVColumn()
		{
			this.m_vValues = new List<string>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CE1 File Offset: 0x00000EE1
		public void Add(string value)
		{
			this.m_vValues.Add(value);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002CEF File Offset: 0x00000EEF
		public string Get(int row)
		{
			return this.m_vValues[row];
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CFD File Offset: 0x00000EFD
		public static int GetArraySize(int currentOffset, int nextOffset)
		{
			return nextOffset - currentOffset;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D02 File Offset: 0x00000F02
		public int GetSize()
		{
			return this.m_vValues.Count;
		}

		// Token: 0x04000012 RID: 18
		private readonly List<string> m_vValues;
	}
}
