using System;
using System.Collections.Generic;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000092 RID: 146
	internal class LoginFailedMessage : Message
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0001A78F File Offset: 0x0001898F
		public LoginFailedMessage(Device client)
			: base(client)
		{
			base.SetMessageType(20103);
			this.SetReason("UCS Developement Team");
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001A7BC File Offset: 0x000189BC
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

		// Token: 0x060003C0 RID: 960 RVA: 0x0001A8EE File Offset: 0x00018AEE
		public void RemainingTime(int code)
		{
			this.m_vRemainingTime = code;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001A8F7 File Offset: 0x00018AF7
		public void SetContentURL(string url)
		{
			this.m_vContentURL = url;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001A900 File Offset: 0x00018B00
		public void SetErrorCode(byte code)
		{
			this.m_vErrorCode = code;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001A909 File Offset: 0x00018B09
		public void SetReason(string reason)
		{
			this.m_vReason = reason;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001A912 File Offset: 0x00018B12
		public void SetRedirectDomain(string domain)
		{
			this.m_vRedirectDomain = domain;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001A91B File Offset: 0x00018B1B
		public void SetResourceFingerprintData(string data)
		{
			this.m_vResourceFingerprintData = data;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001A924 File Offset: 0x00018B24
		public void SetUpdateURL(string url)
		{
			this.m_vUpdateURL = url;
		}

		// Token: 0x040002DD RID: 733
		private string m_vContentURL;

		// Token: 0x040002DE RID: 734
		private byte m_vErrorCode;

		// Token: 0x040002DF RID: 735
		private string m_vReason;

		// Token: 0x040002E0 RID: 736
		private string m_vRedirectDomain;

		// Token: 0x040002E1 RID: 737
		private int m_vRemainingTime;

		// Token: 0x040002E2 RID: 738
		private string m_vResourceFingerprintData = "9bb57e3688e6df1e1e70ba4f927163bb8cbf7cef";

		// Token: 0x040002E3 RID: 739
		private string m_vUpdateURL;
	}
}
