using System;
using System.Linq;

namespace UCS.PacketProcessing
{
	// Token: 0x0200007D RID: 125
	internal class UdpConnectionInfoMessage : Message
	{
		// Token: 0x06000358 RID: 856 RVA: 0x000117B5 File Offset: 0x0000F9B5
		public UdpConnectionInfoMessage(Client client)
			: base(client)
		{
			base.SetMessageType(24112);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000117C9 File Offset: 0x0000F9C9
		public override void Encode()
		{
			base.Encrypt(UdpConnectionInfoMessage.StringToByteArray(UdpConnectionInfoMessage.hex));
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000117DC File Offset: 0x0000F9DC
		public static byte[] StringToByteArray(string hex)
		{
			return (from x in Enumerable.Range(0, hex.Length)
				where x % 2 == 0
				select Convert.ToByte(hex.Substring(x, 2), 16)).ToArray<byte>();
		}

		// Token: 0x0400023A RID: 570
		private static string hex = "BB91010000000E3231322E3139352E39322E3132300000000A97FC1F349C6663EB1D330000002B65597478376D455A576A48765470776B626635347258656D4E683952346C6C6E68516A346D555975746F45";
	}
}
