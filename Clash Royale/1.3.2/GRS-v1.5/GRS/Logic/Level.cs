using System;
using CRS.Logic.Manager;
using CRS.PacketProcessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CRS.Logic
{
	// Token: 0x020000CE RID: 206
	internal class Level
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0001C548 File Offset: 0x0001A748
		public Level()
		{
			this.GameObjectManager = new GameObjectManager(this);
			this.m_vClientAvatar = new ClientAvatar();
			this.m_vAccountPrivileges = 0;
			this.m_vAccountStatus = 0;
			this.m_vIPAddress = "";
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001C580 File Offset: 0x0001A780
		public Level(long id, string token)
		{
			this.GameObjectManager = new GameObjectManager(this);
			this.m_vClientAvatar = new ClientAvatar(id, token);
			this.m_vTime = DateTime.UtcNow;
			this.m_vAccountPrivileges = 0;
			this.m_vAccountStatus = 0;
			this.m_vIPAddress = "";
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		public byte GetAccountPrivileges()
		{
			return this.m_vAccountPrivileges;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		public bool Banned()
		{
			return this.m_vAccountStatus == 99;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		public byte GetAccountStatus()
		{
			return this.m_vAccountStatus;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001C5EC File Offset: 0x0001A7EC
		public Device GetClient()
		{
			return this.m_vClient;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		public ClientAvatar GetHomeOwnerAvatar()
		{
			return this.m_vClientAvatar;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001C5FC File Offset: 0x0001A7FC
		public string GetIPAddress()
		{
			return this.m_vIPAddress;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		public ClientAvatar GetPlayerAvatar()
		{
			return this.m_vClientAvatar;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001C604 File Offset: 0x0001A804
		public DateTime GetTime()
		{
			return this.m_vTime;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001C60C File Offset: 0x0001A80C
		public string SaveToJSON()
		{
			return JsonConvert.SerializeObject(this.GameObjectManager.Save());
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001C61E File Offset: 0x0001A81E
		public void SetAccountPrivileges(byte privileges)
		{
			this.m_vAccountPrivileges = privileges;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001C627 File Offset: 0x0001A827
		public void SetAccountStatus(byte status)
		{
			this.m_vAccountStatus = status;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001C630 File Offset: 0x0001A830
		public void SetClient(Device client)
		{
			this.m_vClient = client;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001C639 File Offset: 0x0001A839
		public void SetHome(string jsonHome)
		{
			this.GameObjectManager.Load(JObject.Parse(jsonHome));
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001C64C File Offset: 0x0001A84C
		public void SetIPAddress(string IP)
		{
			this.m_vIPAddress = IP;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001C655 File Offset: 0x0001A855
		public void SetTime(DateTime t)
		{
			this.m_vTime = t;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001C660 File Offset: 0x0001A860
		public void LoadFromJSON(string jsonString)
		{
			JObject jobject = JObject.Parse(jsonString);
			this.GameObjectManager.Load(jobject);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001C680 File Offset: 0x0001A880
		public void Tick()
		{
			this.SetTime(DateTime.UtcNow);
		}

		// Token: 0x04000365 RID: 869
		public GameObjectManager GameObjectManager;

		// Token: 0x04000366 RID: 870
		private readonly ClientAvatar m_vClientAvatar;

		// Token: 0x04000367 RID: 871
		private byte m_vAccountPrivileges;

		// Token: 0x04000368 RID: 872
		private byte m_vAccountStatus;

		// Token: 0x04000369 RID: 873
		private Device m_vClient;

		// Token: 0x0400036A RID: 874
		private string m_vIPAddress;

		// Token: 0x0400036B RID: 875
		private DateTime m_vTime;
	}
}
