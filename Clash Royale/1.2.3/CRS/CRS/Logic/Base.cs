using System;
using System.Collections.Generic;
using System.IO;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x0200009B RID: 155
	internal class Base
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x00013B64 File Offset: 0x00011D64
		public Base(int unknown1)
		{
			this.m_vUnknown1 = unknown1;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00013B74 File Offset: 0x00011D74
		public virtual void Decode(byte[] baseData)
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(baseData)))
			{
				this.m_vUnknown1 = binaryReader.ReadInt32WithEndian();
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013BB8 File Offset: 0x00011DB8
		public virtual byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.m_vUnknown1);
			return list.ToArray();
		}

		// Token: 0x040002A4 RID: 676
		private int m_vUnknown1;
	}
}
