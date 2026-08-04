using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	// Token: 0x02000084 RID: 132
	internal class LoginFailedMessage : Message
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00011A0B File Offset: 0x0000FC0B
		public LoginFailedMessage(Client client)
			: base(client)
		{
			base.SetMessageType(20103);
			this.SetReason("UCS Developement Team");
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00011A38 File Offset: 0x0000FC38
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			if (base.Client.CState == 0)
			{
				list.Add(this.m_vErrorCode);
				list.AddString(this.m_vResourceFingerprintData);
				list.AddString(this.m_vRedirectDomain);
				list.AddString(this.m_vContentURL);
				list.AddString(this.m_vUpdateURL);
				list.AddString(this.m_vReason);
				list.AddInt32(this.m_vRemainingTime);
				list.AddInt32(-1);
				list.Add(0);
				list.AddString(string.Empty);
				list.AddInt32(-1);
				list.AddInt32(2);
				base.SetData(list.ToArray());
				return;
			}
			list.Add(this.m_vErrorCode);
			list.AddString(this.m_vResourceFingerprintData);
			list.AddString(this.m_vRedirectDomain);
			list.AddString(this.m_vContentURL);
			list.AddString(this.m_vUpdateURL);
			list.AddString(this.m_vReason);
			list.AddInt32(this.m_vRemainingTime);
			list.AddInt32(-1);
			list.Add(0);
			list.AddString(string.Empty);
			list.AddInt32(-1);
			list.AddInt32(2);
			base.Encrypt(list.ToArray());
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00011B6A File Offset: 0x0000FD6A
		public void RemainingTime(int code)
		{
			this.m_vRemainingTime = code;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011B73 File Offset: 0x0000FD73
		public void SetContentURL(string url)
		{
			this.m_vContentURL = url;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public void SetErrorCode(byte code)
		{
			this.m_vErrorCode = code;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011B85 File Offset: 0x0000FD85
		public void SetReason(string reason)
		{
			this.m_vReason = reason;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011B8E File Offset: 0x0000FD8E
		public void SetRedirectDomain(string domain)
		{
			this.m_vRedirectDomain = domain;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011B97 File Offset: 0x0000FD97
		public void SetResourceFingerprintData(string data)
		{
			this.m_vResourceFingerprintData = data;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public void SetUpdateURL(string url)
		{
			this.m_vUpdateURL = url;
		}

		// Token: 0x0400023C RID: 572
		private string m_vContentURL;

		// Token: 0x0400023D RID: 573
		private byte m_vErrorCode;

		// Token: 0x0400023E RID: 574
		private string m_vReason;

		// Token: 0x0400023F RID: 575
		private string m_vRedirectDomain;

		// Token: 0x04000240 RID: 576
		private int m_vRemainingTime;

		// Token: 0x04000241 RID: 577
		private string m_vResourceFingerprintData = "9bb57e3688e6df1e1e70ba4f927163bb8cbf7cef";

		// Token: 0x04000242 RID: 578
		private string m_vUpdateURL;
	}
}
