using System;
using System.Collections.Generic;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000070 RID: 112
	internal class LocalAlliancesMessage : Message
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00018BFE File Offset: 0x00016DFE
		public LocalAlliancesMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24402);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00018C14 File Offset: 0x00016E14
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(1); // Count

			list.AddRange(Message.AddVInt(1));
			list.AddRange(Message.AddVInt(1));
			list.AddString("GobelinLand");
			list.AddRange(Message.AddVInt(1));
			list.AddRange(Message.AddVInt(5000));
			list.AddRange(Message.AddVInt(1));
			list.Add(16);
			list.Add(26);
			list.AddRange(Message.AddVInt(0));
			list.AddRange(Message.AddVInt(5)); // MemberCount
			list.AddRange(Message.AddVInt(5));
			base.Encrypt(list.ToArray());
		}
	}
}
