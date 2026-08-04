using System;
using System.Collections.Generic;
using CRS.Helpers;

namespace CRS.PacketProcessing.Messages.Server
{
	// Token: 0x02000093 RID: 147
	internal class LoginOkMessage : Message
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0001A92D File Offset: 0x00018B2D
		public LoginOkMessage(Device client)
			: base(client)
		{
			base.SetMessageType(20104);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001A94C File Offset: 0x00018B4C
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001A954 File Offset: 0x00018B54
		public string Unknown11 { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001A95D File Offset: 0x00018B5D
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0001A965 File Offset: 0x00018B65
		public string Unknown9 { get; set; }

		// Token: 0x060003CC RID: 972 RVA: 0x0001A970 File Offset: 0x00018B70
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

		// Token: 0x060003CD RID: 973 RVA: 0x0001AA79 File Offset: 0x00018C79
		public void SetAccountCreatedDate(string date)
		{
			this.m_vAccountCreatedDate = date;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001AA82 File Offset: 0x00018C82
		public void SetAccountId(long id)
		{
			this.m_vAccountId = id;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001AA8B File Offset: 0x00018C8B
		public void SetContentVersion(int version)
		{
			this.m_vContentVersion = version;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001AA94 File Offset: 0x00018C94
		public void SetCountryCode(string code)
		{
			this.m_vCountryCode = code;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001AA9D File Offset: 0x00018C9D
		public void SetDaysSinceStartedPlaying(int days)
		{
			this.m_vDaysSinceStartedPlaying = days;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001AAA6 File Offset: 0x00018CA6
		public void SetFacebookId(string id)
		{
			this.m_vFacebookId = id;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001AAAF File Offset: 0x00018CAF
		public void SetGamecenterId(string id)
		{
			this.m_vGamecenterId = id;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		public void SetPassToken(string token)
		{
			this.m_vPassToken = token;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001AAC1 File Offset: 0x00018CC1
		public void SetPlayTimeSeconds(int seconds)
		{
			this.m_vPlayTimeSeconds = seconds;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001AACA File Offset: 0x00018CCA
		public void SetServerBuild(int build)
		{
			this.m_vServerBuild = build;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001AAD3 File Offset: 0x00018CD3
		public void SetServerEnvironment(string env)
		{
			this.m_vServerEnvironment = env;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001AADC File Offset: 0x00018CDC
		public void SetServerMajorVersion(int version)
		{
			this.m_vServerMajorVersion = version;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001AAE5 File Offset: 0x00018CE5
		public void SetServerTime(string time)
		{
			this.m_vServerTime = time;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001AAEE File Offset: 0x00018CEE
		public void SetSessionCount(int count)
		{
			this.m_vSessionCount = count;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001AAF7 File Offset: 0x00018CF7
		public void SetStartupCooldownSeconds(int seconds)
		{
			this.m_vStartupCooldownSeconds = seconds;
		}

		// Token: 0x040002E4 RID: 740
		private readonly string m_vFacebookAppID = "297484437009394";

		// Token: 0x040002E5 RID: 741
		private string m_vAccountCreatedDate;

		// Token: 0x040002E6 RID: 742
		private long m_vAccountId;

		// Token: 0x040002E7 RID: 743
		private int m_vContentVersion;

		// Token: 0x040002E8 RID: 744
		private string m_vCountryCode;

		// Token: 0x040002E9 RID: 745
		private int m_vDaysSinceStartedPlaying;

		// Token: 0x040002EA RID: 746
		private string m_vFacebookId;

		// Token: 0x040002EB RID: 747
		private string m_vGamecenterId;

		// Token: 0x040002EC RID: 748
		private int m_vGoogleID;

		// Token: 0x040002ED RID: 749
		private string m_vPassToken;

		// Token: 0x040002EE RID: 750
		private int m_vPlayTimeSeconds;

		// Token: 0x040002EF RID: 751
		private int m_vServerBuild;

		// Token: 0x040002F0 RID: 752
		private string m_vServerEnvironment;

		// Token: 0x040002F1 RID: 753
		private int m_vServerMajorVersion;

		// Token: 0x040002F2 RID: 754
		private string m_vServerTime;

		// Token: 0x040002F3 RID: 755
		private int m_vSessionCount;

		// Token: 0x040002F4 RID: 756
		private int m_vStartupCooldownSeconds;
	}
}
