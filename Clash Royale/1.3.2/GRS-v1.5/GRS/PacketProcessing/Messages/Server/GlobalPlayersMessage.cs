using System;
using System.Collections.Generic;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000072 RID: 114
	internal class GlobalPlayersMessage : Message
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00018D36 File Offset: 0x00016F36
		public GlobalPlayersMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24403);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00018D4C File Offset: 0x00016F4C
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(Message.AddVInt(0/*1*/)); // Count

			/*list.AddRange(Message.AddVInt(1)); // HighId
			list.AddRange(Message.AddVInt(1)); // LowId
			list.AddString("Berkan"); // Name
			list.AddRange(Message.AddVInt(1));
			list.AddRange(Message.AddVInt(5000));
			list.AddRange(Message.AddVInt(1));
			list.AddRange(Message.AddVInt(0));
			list.AddRange(Message.AddVInt(0));
			list.AddRange(Message.AddVInt(0));
			list.AddRange(Message.AddVInt(12));
			list.AddRange(Message.AddVInt(1));
			list.AddString("DE"); // Region
			list.AddRange(Message.AddVInt(1)); // HighId
			list.AddRange(Message.AddVInt(1)); // LowId
			list.AddRange(Message.AddVInt(1)); // HasAlliance
			list.AddRange(Message.AddVInt(1)); // HighId
			list.AddRange(Message.AddVInt(1)); // LowId
			list.AddString("GobelinLand");
			list.AddRange(Message.AddVInt(16));
			list.AddRange(Message.AddVInt(26)); // Badge*/

			list.AddRange(Message.AddVInt((int)TimeSpan.FromDays(1.0).TotalSeconds)); // SeasonTimer
			base.Encrypt(list.ToArray());
		}
	}
}
