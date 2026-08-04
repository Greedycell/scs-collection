using System;
using System.Collections.Generic;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200006F RID: 111
	internal class GlobalAlliancesMessage : Message
	{
		// Token: 0x0600034F RID: 847 RVA: 0x00018B3F File Offset: 0x00016D3F
		public GlobalAlliancesMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24401);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00018B54 File Offset: 0x00016D54
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(Message.AddVInt(1)); // Count

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
