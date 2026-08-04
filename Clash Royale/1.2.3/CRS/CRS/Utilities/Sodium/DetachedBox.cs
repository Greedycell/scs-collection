using System;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000037 RID: 55
	public class DetachedBox
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x000073DF File Offset: 0x000055DF
		public DetachedBox()
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000D7EB File Offset: 0x0000B9EB
		public DetachedBox(byte[] cipherText, byte[] mac)
		{
			this.CipherText = cipherText;
			this.Mac = mac;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000D801 File Offset: 0x0000BA01
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000D809 File Offset: 0x0000BA09
		public byte[] CipherText { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000D812 File Offset: 0x0000BA12
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000D81A File Offset: 0x0000BA1A
		public byte[] Mac { get; set; }
	}
}
