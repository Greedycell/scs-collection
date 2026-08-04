namespace CRS.Logic
{
    #region Usings

    using System.Collections.Generic;

    using Enums;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Slots;

    #endregion Usings

    internal class Clan
    {
        public long ClanID = 0;

        public long BackupID = 0;

        public int Trophies = 0;

        public int Required_Trophies = 0;

        public int Origin = 0;

        public int Badge = 0;

        public string Name = string.Empty;

        public string Description = string.Empty;

        public Hiring Type = Hiring.OPEN;

        // public Members Members          = new Members(50);
        private Dictionary<long, Members> Members;

        private const int m_vMaxAllianceMembers = 50;

        public Clan()
        {
            // m_vChatMessages = new List<StreamEntry>();
            this.Members = new Dictionary<long, Members>();
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Clan"/> class.
        /// </summary>
        /// <param name="_ClanID">The clan identifier.</param>
        public Clan(long _ClanID)
        {
            this.ClanID = _ClanID;
            this.BackupID = _ClanID;
        }

        public long GetAllianceId()
        {
            return this.ClanID;
        }

        /// <summary>
        /// Deserialize the specified JSON.
        /// </summary>
        /// <param name="_Data">The JSON.</param>
        public void LoadFromJSON(string _Data)
        {
            JObject _Json = JObject.Parse(_Data);

            this.ClanID = _Json["clan_id"].ToObject<long>();
            this.BackupID = _Json["backup_id"].ToObject<long>();

            this.Trophies = _Json["trophies"].ToObject<int>();
            this.Required_Trophies = _Json["required_trophies"].ToObject<int>();
            this.Origin = _Json["origin"].ToObject<int>();
            this.Badge = _Json["badge"].ToObject<int>();

            this.Name = _Json["name"].ToObject<string>();
            this.Description = _Json["description"].ToObject<string>();

            this.Type = (Hiring)_Json["type"].ToObject<int>();
        }

        /// <summary>
        /// Serialize this instance into a JSON.
        /// </summary>
        /// <returns>The JSON.</returns>
        public string SaveToJSON()
        {
            JObject _JSON = new JObject();

            _JSON.Add("clan_id", this.ClanID);
            _JSON.Add("backup_id", this.BackupID);

            _JSON.Add("trophies", this.Trophies);
            _JSON.Add("required_trophies", this.Required_Trophies);
            _JSON.Add("origin", this.Origin);
            _JSON.Add("badge", this.Badge);

            _JSON.Add("type", (int)this.Type);

            _JSON.Add("name", this.Name);
            _JSON.Add("description", this.Description);

            return JsonConvert.SerializeObject(_JSON);
        }
    }
}