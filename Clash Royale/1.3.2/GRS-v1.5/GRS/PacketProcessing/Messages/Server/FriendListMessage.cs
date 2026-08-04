using System;
using System.Collections.Generic;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200008F RID: 143
	internal class FriendListMessage : Message
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x0001A6AE File Offset: 0x000188AE
		public FriendListMessage(Device client)
			: base(client)
		{
			base.SetMessageType(20105);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001A6C4 File Offset: 0x000188C4
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(2);
			list.AddInt32(1); // Count

			list.AddInt32(2); // HighId
			list.AddInt32(2); // LowId
			list.AddInt32(2); // HighId
			list.AddInt32(2); // LowId
			list.AddString("Berkan");
			list.AddString("bbb");
			list.AddString("ccc");
			list.AddRange(Message.AddVInt(0));
			list.AddRange(Message.AddVInt(12)); // ExpLevel
			list.AddRange(Message.AddVInt(5000)); // Score
			list.AddRange(Message.AddVInt(1)); // HasAlliance
			list.AddInt32(2); // HighId
			list.AddInt32(2); // LowId
			list.AddString("GobelinLand");
			list.AddRange(Message.AddVInt(16));
			list.AddRange(Message.AddVInt(26)); // Badge
			list.AddString("ddd");
			base.Encrypt(list.ToArray());
		}
	}
}
