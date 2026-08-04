using System;
using System.Collections.Generic;
using Ionic.Zlib;
using UCS.Helpers;

namespace UCS.Logic
{
	// Token: 0x020000A0 RID: 160
	internal class ClientHome : Base
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0001422E File Offset: 0x0001242E
		public ClientHome()
			: base(0)
		{
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00014237 File Offset: 0x00012437
		public ClientHome(long id)
			: base(0)
		{
			this.m_vId = id;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014248 File Offset: 0x00012448
		public override byte[] Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(base.Encode());
			list.AddInt64(this.m_vId);
			list.AddInt32(this.m_vRemainingShieldTime);
			list.AddInt32(1800);
			list.AddInt32(0);
			list.AddInt32(1200);
			list.AddInt32(60);
			list.Add(1);
			list.AddInt32(this.m_vSerializedVillage.Length + 4);
			List<byte> list2 = list;
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = byte.MaxValue;
			list2.AddRange(array);
			list.AddRange(this.m_vSerializedVillage);
			return list.ToArray();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000142E9 File Offset: 0x000124E9
		public byte[] GetHomeJSON()
		{
			return this.m_vSerializedVillage;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000142F1 File Offset: 0x000124F1
		public void SetHomeJSON(string json)
		{
			this.m_vSerializedVillage = ZlibStream.CompressString(json);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000142FF File Offset: 0x000124FF
		public void SetShieldDurationSeconds(int seconds)
		{
			this.m_vRemainingShieldTime = seconds;
		}

		// Token: 0x040002BD RID: 701
		private readonly long m_vId;

		// Token: 0x040002BE RID: 702
		private int m_vRemainingShieldTime;

		// Token: 0x040002BF RID: 703
		private byte[] m_vSerializedVillage;
	}
}
