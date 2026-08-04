using System;
using System.Collections.Generic;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200008B RID: 139
	internal class UdpConnectionInfoMessage : Message
	{
		// Token: 0x060003AE RID: 942 RVA: 0x0001A53D File Offset: 0x0001873D
		public UdpConnectionInfoMessage(Device client)
			: base(client)
		{
			base.SetMessageType(24112);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001A554 File Offset: 0x00018754
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddRange(Message.AddVInt(9339));
			list.AddString("178.32.9.216");
			list.AddInt32(10);
			list.AddRange(Helpers.Helpers.HexaToBytes("5F63B4147EA0B3DC712F"));
			list.AddString("ZAOwIjH3h1Gc8rXpoGExh0sIkQeEg5au5wsVI8qvN2Y");
			base.Encrypt(list.ToArray());
		}
	}
}
