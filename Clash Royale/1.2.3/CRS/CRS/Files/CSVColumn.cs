using System;
using System.Collections.Generic;

namespace UCS.Files
{
	// Token: 0x02000029 RID: 41
	internal class CSVColumn
	{
		// Token: 0x06000186 RID: 390 RVA: 0x0000CC10 File Offset: 0x0000AE10
		public CSVColumn()
		{
			this.m_vValues = new List<string>();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000CC23 File Offset: 0x0000AE23
		public void Add(string value)
		{
			this.m_vValues.Add(value);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000CC31 File Offset: 0x0000AE31
		public string Get(int row)
		{
			return this.m_vValues[row];
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000CC3F File Offset: 0x0000AE3F
		public static int GetArraySize(int currentOffset, int nextOffset)
		{
			return nextOffset - currentOffset;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000CC44 File Offset: 0x0000AE44
		public int GetSize()
		{
			return this.m_vValues.Count;
		}

		// Token: 0x0400015E RID: 350
		private readonly List<string> m_vValues;
	}
}
