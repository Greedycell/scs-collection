namespace CRS.Logic
{
    #region Usings

    using System;

    using CRS.Logic.Enums;
    using CRS.Packets;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #endregion

    internal class Level
    {
        public GameObjectManager GameObjectManager; // a1 + 44

        private byte m_vAccountPrivileges;

        private byte m_vAccountStatus;

        private Client m_vClient;

        private ClientAvatar m_vClientAvatar;

        private DateTime m_vTime; // a1 + 40

        private string m_vIPAddress;


        public Level()
        {
            this.GameObjectManager = new GameObjectManager(this);
            this.m_vClientAvatar = new ClientAvatar();
            this.m_vAccountPrivileges = (byte)Rank.Player;
            this.m_vAccountStatus = (byte)Status.ACTIVE;
            this.m_vIPAddress = "0.0.0.0";
        }

        public Level(long id, string token)
        {
            this.GameObjectManager = new GameObjectManager(this);
            this.m_vClientAvatar = new ClientAvatar(id, token);
            this.m_vTime = DateTime.UtcNow;
            this.m_vAccountPrivileges = (byte)Rank.Player;
            this.m_vAccountStatus = (byte)Status.ACTIVE;
            this.m_vIPAddress = "0.0.0.0";
        }

        public byte GetAccountPrivileges()
        {
            return this.m_vAccountPrivileges;
        }

        public byte GetAccountStatus()
        {
            return this.m_vAccountStatus;
        }

        public Client GetClient()
        {
            return this.m_vClient;
        }

        public ClientAvatar GetHomeOwnerAvatar()
        {
            return this.m_vClientAvatar;
        }

        public ClientAvatar GetPlayerAvatar()
        {
            return this.m_vClientAvatar;
        }

        public DateTime GetTime()
        {
            return this.m_vTime;
        }

        public void LoadFromJSON(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);
            this.GameObjectManager.Load(jsonObject);
        }

        public string SaveToJSON()
        {
            return JsonConvert.SerializeObject(this.GameObjectManager.Save());
        }

        public void SetAccountPrivileges(byte privileges)
        {
            this.m_vAccountPrivileges = privileges;
        }

        public void SetAccountStatus(byte status)
        {
            this.m_vAccountStatus = status;
        }

        public void SetClient(Client _Client)
        {
            this.m_vClient = _Client;
        }

        public void SetHome(string jsonHome)
        {
            this.GameObjectManager.Load(JObject.Parse(jsonHome));
        }

        public void SetTime(DateTime t)
        {
            this.m_vTime = t;
        }

        public string GetIPAddress()
        {
            return this.m_vIPAddress;
        }

        public void SetIPAddress(string IP)
        {
            this.m_vIPAddress = IP;
        }
        public void Tick()
        {
            this.SetTime(DateTime.UtcNow);
            this.GameObjectManager.Tick();
        }
    }
}