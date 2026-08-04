using System;
using System.Collections.Generic;
using System.IO;
using CRS.Helpers;

namespace CRS.Logic
{
	// Token: 0x020000CD RID: 205
	internal class Base
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0001C4DC File Offset: 0x0001A6DC
		public Base(int unknown1)
		{
			this.m_vUnknown1 = unknown1;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		public virtual void Decode(byte[] baseData)
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(baseData)))
			{
				this.m_vUnknown1 = binaryReader.ReadInt32WithEndian();
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001C530 File Offset: 0x0001A730
		public virtual byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.m_vUnknown1);
			return list.ToArray();
		}

		// Token: 0x04000364 RID: 868
		private int m_vUnknown1;
	}
}
