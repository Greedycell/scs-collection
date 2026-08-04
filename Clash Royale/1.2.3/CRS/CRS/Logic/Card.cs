using System;
using System.Collections.Generic;

namespace UCS.Logic
{
	// Token: 0x02000093 RID: 147
	internal class Card
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x000130C5 File Offset: 0x000112C5
		public void SetIsNew(bool New)
		{
			this.m_vIsNew = New;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000130CE File Offset: 0x000112CE
		public bool IsNew()
		{
			return this.m_vIsNew;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000130D6 File Offset: 0x000112D6
		public void SetCardId(int Id)
		{
			this.m_vCardId = Id;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000130DF File Offset: 0x000112DF
		public void SetLevel(int Level)
		{
			this.m_vLevel = Level;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000130E8 File Offset: 0x000112E8
		public void SetCount(int Count)
		{
			this.m_vCount = Count;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000130F1 File Offset: 0x000112F1
		public int GetLevel()
		{
			return this.m_vLevel;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000130F9 File Offset: 0x000112F9
		public int GetCardId()
		{
			return this.m_vCardId;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00013101 File Offset: 0x00011301
		public int GetCount()
		{
			return this.m_vCount;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001310C File Offset: 0x0001130C
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

		// Token: 0x04000274 RID: 628
		private int m_vCardId;

		// Token: 0x04000275 RID: 629
		private int m_vCount;

		// Token: 0x04000276 RID: 630
		private bool m_vIsNew;

		// Token: 0x04000277 RID: 631
		private int m_vLevel;
	}
}
