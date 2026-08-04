using System;
using System.Collections.Generic;
using CRS.Helpers;
using CRS.PacketProcessing.Messages.Client;
using CRS.Utilities.Blake2b;
using CRS.PacketProcessing;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000096 RID: 150
	internal class SessionSuccess : Message
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0001AC18 File Offset: 0x00018E18
		public SessionSuccess(Device client, SessionRequest cka)
			: base(client)
		{
			base.SetMessageType(20100);
			SessionSuccess.Blake.Init();
			SessionSuccess.Blake.Update(Key.Crypto.PublicKey);
			this.SessionKey = SessionSuccess.Blake.Finish();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001AC68 File Offset: 0x00018E68
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(this.SessionKey.Length);
			list.AddRange(this.SessionKey);
			base.SetData(list.ToArray());
		}

		// Token: 0x040002F9 RID: 761
		public byte[] SessionKey;

		// Token: 0x040002FA RID: 762
		private static readonly Hasher Blake = Blake2B.Create(new Blake2BConfig
		{
			OutputSizeInBytes = 24
		});
	}
}
