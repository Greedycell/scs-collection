using System;
using System.Collections.Generic;

namespace CRS.Logic
{
	// Token: 0x020000CA RID: 202
	internal class Card
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0001C133 File Offset: 0x0001A333
		public void SetIsNew(bool New)
		{
			this.m_vIsNew = New;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001C13C File Offset: 0x0001A33C
		public bool IsNew()
		{
			return this.m_vIsNew;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001C144 File Offset: 0x0001A344
		public void SetCardId(int Id)
		{
			this.m_vCardId = Id;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001C14D File Offset: 0x0001A34D
		public void SetLevel(int Level)
		{
			this.m_vLevel = Level;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001C156 File Offset: 0x0001A356
		public void SetCount(int Count)
		{
			this.m_vCount = Count;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001C15F File Offset: 0x0001A35F
		public int GetLevel()
		{
			return this.m_vLevel;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001C167 File Offset: 0x0001A367
		public int GetCardId()
		{
			return this.m_vCardId;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001C16F File Offset: 0x0001A36F
		public int GetCount()
		{
			return this.m_vCount;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001C178 File Offset: 0x0001A378
		public byte[] Encode()
		{
			return new List<byte>
			{
				26,
				(byte)this.m_vCardId,
				(byte)this.m_vLevel,
				0,
				(byte)this.m_vCount,
				0,
				0,
				(byte)(this.m_vIsNew ? 1 : 0)
			}.ToArray();
		}

		// Token: 0x0400034D RID: 845
		private int m_vCardId;

		// Token: 0x0400034E RID: 846
		private int m_vCount;

		// Token: 0x0400034F RID: 847
		private bool m_vIsNew;

		// Token: 0x04000350 RID: 848
		private int m_vLevel;
	}
}
