using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UCS.PacketProcessing;

namespace UCS.Logic
{
	// Token: 0x020000AC RID: 172
	internal class Level
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x00014858 File Offset: 0x00012A58
		public Level()
		{
			this.GameObjectManager = new GameObjectManager(this);
			this.m_vClientAvatar = new ClientAvatar();
			this.m_vAccountPrivileges = 0;
			this.m_vAccountStatus = 0;
			this.m_vIPAddress = "0.0.0.0";
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00014890 File Offset: 0x00012A90
		public Level(long id)
		{
			this.GameObjectManager = new GameObjectManager(this);
			this.m_vClientAvatar = new ClientAvatar(id);
			this.m_vTime = DateTime.UtcNow;
			this.m_vAccountPrivileges = 0;
			this.m_vAccountStatus = 0;
			this.m_vIPAddress = "0.0.0.0";
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000148DF File Offset: 0x00012ADF
		public byte GetAccountPrivileges()
		{
			return this.m_vAccountPrivileges;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000148E7 File Offset: 0x00012AE7
		public string GetIPAddress()
		{
			return this.m_vIPAddress;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000148EF File Offset: 0x00012AEF
		public void SetIPAddress(string IP)
		{
			this.m_vIPAddress = IP;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000148F8 File Offset: 0x00012AF8
		public byte GetAccountStatus()
		{
			return this.m_vAccountStatus;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00014900 File Offset: 0x00012B00
		public Client GetClient()
		{
			return this.m_vClient;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00014908 File Offset: 0x00012B08
		public ClientAvatar GetHomeOwnerAvatar()
		{
			return this.m_vClientAvatar;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00014908 File Offset: 0x00012B08
		public ClientAvatar GetPlayerAvatar()
		{
			return this.m_vClientAvatar;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00014910 File Offset: 0x00012B10
		public DateTime GetTime()
		{
			return this.m_vTime;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00014918 File Offset: 0x00012B18
		public void LoadFromJSON(string jsonString)
		{
			JObject jobject = JObject.Parse(jsonString);
			this.GameObjectManager.Load(jobject);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00014938 File Offset: 0x00012B38
		public string SaveToJSON()
		{
			return JsonConvert.SerializeObject(this.GameObjectManager.Save());
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001494A File Offset: 0x00012B4A
		public void SetAccountPrivileges(byte privileges)
		{
			this.m_vAccountPrivileges = privileges;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00014953 File Offset: 0x00012B53
		public void SetAccountStatus(byte status)
		{
			this.m_vAccountStatus = status;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001495C File Offset: 0x00012B5C
		public void SetClient(Client client)
		{
			this.m_vClient = client;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00014965 File Offset: 0x00012B65
		public void SetHome(string jsonHome)
		{
			this.GameObjectManager.Load(JObject.Parse(jsonHome));
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00014978 File Offset: 0x00012B78
		public void SetTime(DateTime t)
		{
			this.m_vTime = t;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00014981 File Offset: 0x00012B81
		public void Tick()
		{
			this.SetTime(DateTime.UtcNow);
		}

		// Token: 0x040002CF RID: 719
		private readonly ClientAvatar m_vClientAvatar;

		// Token: 0x040002D0 RID: 720
		public GameObjectManager GameObjectManager;

		// Token: 0x040002D1 RID: 721
		private byte m_vAccountPrivileges;

		// Token: 0x040002D2 RID: 722
		private byte m_vAccountStatus;

		// Token: 0x040002D3 RID: 723
		private Client m_vClient;

		// Token: 0x040002D4 RID: 724
		private string m_vIPAddress;

		// Token: 0x040002D5 RID: 725
		private DateTime m_vTime;
	}
}
