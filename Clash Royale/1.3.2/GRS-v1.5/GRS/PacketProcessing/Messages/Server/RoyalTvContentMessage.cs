using System;
using System.Collections.Generic;
using System.Net;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x0200008E RID: 142
	internal class RoyalTvContentMessage : Message
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0001A622 File Offset: 0x00018822
		public RoyalTvContentMessage(Device client, byte Arena)
			: base(client)
		{
			base.SetMessageType(24405);
			this.m_vArena = Arena;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001A640 File Offset: 0x00018840
		public override void Encode()
		{
			List<byte> list = new List<byte>();

			/*
			string text = new WebClient().DownloadString("http://cdn.gobelinland.fr/royaletv/battles").Trim();
			list.Add(1);
			list.AddString(text);
			list.AddRange(Helpers.Helpers.HexaToBytes("0002821A05A3AF09AB0A01929D0A8902010C000000001477E65E36"));
			list.Add(this.m_vArena);
			list.AddRange(Helpers.Helpers.HexaToBytes("0002821A05A3AF09AB0A01929D0A8902010C000000001477E65E360800000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"));
			*/

			list.Add(1); // Count
			list.AddString("{\"player0\":{\"acc_hi\":0,\"acc_lo\":1,\"name\":\"Test 1\",\"alliance\":\"Test\",\"stars\":1,\"score\":0,\"score_p\":30,\"alli_hi\":0,\"alli_lo\":1,\"home_hi\":0,\"home_lo\":1,\"badge\":16000078,\"spells\":[{\"d\":26000006},{\"d\":26000020},{\"d\":28000004},{\"d\":26000018,\"l\":1},{\"d\":26000011},{\"d\":26000003,\"l\":2},{\"d\":26000014,\"l\":1},{\"d\":26000012}]},\"player1\":{\"acc_hi\":0,\"acc_lo\":2,\"name\":\"Test 2\",\"alliance\":\"Test\",\"stars\":3,\"score\":30,\"score_p\":0,\"alli_hi\":0,\"alli_lo\":1,\"home_hi\":0,\"home_lo\":2,\"badge\":16000078,\"spells\":[{\"d\":26000000,\"l\":1},{\"d\":26000007},{\"d\":26000013},{\"d\":26000018},{\"d\":28000000},{\"d\":26000003},{\"d\":26000002},{\"d\":26000015}]},\"player2\":{\"acc_hi\":0,\"acc_lo\":0,\"alli_hi\":0,\"alli_lo\":0,\"home_hi\":0,\"home_lo\":0},\"player3\":{\"acc_hi\":0,\"acc_lo\":0,\"alli_hi\":0,\"alli_lo\":0,\"home_hi\":0,\"home_lo\":0},\"arena\":54000002,\"replayV\":64,\"challenge\":false,\"friendly_challenge\":false,\"survival\":false,\"game_config\":{\"gmt\":1,\"plt\":1,\"gamemode\":72000006,\"t1s\":0,\"t2s\":0}}");
			list.AddRange(Helpers.Helpers.HexaToBytes("0002821A05A3AF09AB0A01929D0A8902010C000000001477E65E36"));
			list.Add(this.m_vArena);
			list.AddRange(Helpers.Helpers.HexaToBytes("0002821A05A3AF09AB0A01929D0A8902010C000000001477E65E360800000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"));
				
			base.Encrypt(list.ToArray());
		}

		// Token: 0x040002DB RID: 731
		public byte m_vArena;
	}
}
