using System;
using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
	// Token: 0x02000085 RID: 133
	internal class LoginOkMessage : Message
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00011BA9 File Offset: 0x0000FDA9
		public LoginOkMessage(Client client)
			: base(client)
		{
			base.SetMessageType(20104);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00011BC8 File Offset: 0x0000FDC8
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		public string Unknown11 { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00011BD9 File Offset: 0x0000FDD9
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00011BE1 File Offset: 0x0000FDE1
		public string Unknown9 { get; set; }

		// Token: 0x06000378 RID: 888 RVA: 0x00011BEC File Offset: 0x0000FDEC
		public override void Encode()
		{
			List<byte> list = new List<byte>();
			list.AddInt64(this.m_vAccountId);
			list.AddInt64(this.m_vAccountId);
			list.AddString(this.m_vPassToken);
			list.AddString(this.m_vFacebookId);
			list.AddString(this.m_vGamecenterId);
			list.AddInt32(this.m_vServerMajorVersion);
			list.AddInt32(this.m_vServerBuild);
			list.AddInt32(this.m_vContentVersion);
			list.AddString(this.m_vServerEnvironment);
			list.AddInt32(this.m_vSessionCount);
			list.AddInt32(this.m_vPlayTimeSeconds);
			list.AddInt32(0);
			list.AddString(this.m_vFacebookAppID);
			list.AddString(this.m_vStartupCooldownSeconds.ToString());
			list.AddString(this.m_vAccountCreatedDate);
			list.AddInt32(0);
			list.AddString(this.m_vGoogleID.ToString());
			list.AddString(null);
			list.AddString(this.m_vCountryCode);
			list.AddString("someid2");
			base.Encrypt(list.ToArray());
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011CF5 File Offset: 0x0000FEF5
		public void SetAccountCreatedDate(string date)
		{
			this.m_vAccountCreatedDate = date;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011CFE File Offset: 0x0000FEFE
		public void SetAccountId(long id)
		{
			this.m_vAccountId = id;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00011D07 File Offset: 0x0000FF07
		public void SetContentVersion(int version)
		{
			this.m_vContentVersion = version;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00011D10 File Offset: 0x0000FF10
		public void SetCountryCode(string code)
		{
			this.m_vCountryCode = code;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011D19 File Offset: 0x0000FF19
		public void SetDaysSinceStartedPlaying(int days)
		{
			this.m_vDaysSinceStartedPlaying = days;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00011D22 File Offset: 0x0000FF22
		public void SetFacebookId(string id)
		{
			this.m_vFacebookId = id;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00011D2B File Offset: 0x0000FF2B
		public void SetGamecenterId(string id)
		{
			this.m_vGamecenterId = id;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011D34 File Offset: 0x0000FF34
		public void SetPassToken(string token)
		{
			this.m_vPassToken = token;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011D3D File Offset: 0x0000FF3D
		public void SetPlayTimeSeconds(int seconds)
		{
			this.m_vPlayTimeSeconds = seconds;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011D46 File Offset: 0x0000FF46
		public void SetServerBuild(int build)
		{
			this.m_vServerBuild = build;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011D4F File Offset: 0x0000FF4F
		public void SetServerEnvironment(string env)
		{
			this.m_vServerEnvironment = env;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011D58 File Offset: 0x0000FF58
		public void SetServerMajorVersion(int version)
		{
			this.m_vServerMajorVersion = version;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00011D61 File Offset: 0x0000FF61
		public void SetServerTime(string time)
		{
			this.m_vServerTime = time;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00011D6A File Offset: 0x0000FF6A
		public void SetSessionCount(int count)
		{
			this.m_vSessionCount = count;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011D73 File Offset: 0x0000FF73
		public void SetStartupCooldownSeconds(int seconds)
		{
			this.m_vStartupCooldownSeconds = seconds;
		}

		// Token: 0x04000243 RID: 579
		private readonly string m_vFacebookAppID = "297484437009394";

		// Token: 0x04000244 RID: 580
		private string m_vAccountCreatedDate;

		// Token: 0x04000245 RID: 581
		private long m_vAccountId;

		// Token: 0x04000246 RID: 582
		private int m_vContentVersion;

		// Token: 0x04000247 RID: 583
		private string m_vCountryCode;

		// Token: 0x04000248 RID: 584
		private int m_vDaysSinceStartedPlaying;

		// Token: 0x04000249 RID: 585
		private string m_vFacebookId;

		// Token: 0x0400024A RID: 586
		private string m_vGamecenterId;

		// Token: 0x0400024B RID: 587
		private int m_vGoogleID;

		// Token: 0x0400024C RID: 588
		private string m_vPassToken;

		// Token: 0x0400024D RID: 589
		private int m_vPlayTimeSeconds;

		// Token: 0x0400024E RID: 590
		private int m_vServerBuild;

		// Token: 0x0400024F RID: 591
		private string m_vServerEnvironment;

		// Token: 0x04000250 RID: 592
		private int m_vServerMajorVersion;

		// Token: 0x04000251 RID: 593
		private string m_vServerTime;

		// Token: 0x04000252 RID: 594
		private int m_vSessionCount;

		// Token: 0x04000253 RID: 595
		private int m_vStartupCooldownSeconds;
	}
}
