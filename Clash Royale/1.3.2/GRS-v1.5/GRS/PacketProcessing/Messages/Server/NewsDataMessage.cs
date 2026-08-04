using System;
using System.Collections.Generic;
using System.Net;
using CRS.Helpers;
using CRS.Logic;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000094 RID: 148
	internal class NewsDataMessage : Message
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0001AB00 File Offset: 0x00018D00
		public NewsDataMessage(Device client, Level level)
			: base(client)
		{
			base.SetMessageType(24445);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001AB14 File Offset: 0x00018D14
		public static string StringDownload(string download)
		{
			return new WebClient().DownloadString("http://90.116.85.112/clash_royale_http/" + download);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001AB2C File Offset: 0x00018D2C
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt32(1);
			list.AddString(NewsDataMessage.StringDownload("icon.png"));
			list.AddString(NewsDataMessage.StringDownload("title.txt"));
			list.AddString(NewsDataMessage.StringDownload("description.txt"));
			list.AddString(NewsDataMessage.StringDownload("buttom.txt"));
			list.AddString("http://www.gobelinland.fr/forum/");
			base.Encrypt(list.ToArray());
		}
	}
}
