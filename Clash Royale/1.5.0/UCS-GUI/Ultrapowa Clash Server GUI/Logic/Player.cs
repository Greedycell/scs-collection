namespace UCS.Logic {
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions.List;
    using Enums;
    using Slots;
    using Slots.Items;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using Resource = Slots.Items.Resource;

    #endregion Usings

    internal class Player {
        public Device Client = null;

        public long m_Id = 0;
        public long HomeID = 0;
        public long BackupID = 0;
        public long m_vAllianceId = 0;

        public int BattleID = 0;

        public int Level = 1;
        public int Experience = 0;
        public int Trophies = 0;

        public int Wins = 0;
        public int Loses = 0;
        public int Report = 0;
        public int Donations = 0;

        public int Legendary_Trophies = 0;

        public byte Tutorial = 0x06;
        public byte Changes = 0x00;
        public byte NameSet = 0x00;

        public Arena Arena = Arena.ARENA_L;
        public Rank Rank = Rank.Player;
        public Status Status = Status.ACTIVE;

        public string Token = string.Empty;
        public string Pass = string.Empty;
        private string m_vAvatarName;
        public string Region = string.Empty;

        public bool Android = false;
        public bool Clan = false;
        public bool Banned = false;
        public bool Muted = false;

        public DateTime Update = DateTime.UtcNow;
        public DateTime Created = DateTime.UtcNow;
        public DateTime BanTime = DateTime.UtcNow;
        public DateTime MuteTime = DateTime.UtcNow;

        //public Achievements Achievements = new Achievements();
        //public Resources Resources = new Resources();
        //public Boutique Boutique = new Boutique();
        //public Chests Chests = new Chests();
        //public Deck Deck = new Deck();

        public Player() {
            this.Achievements = new Achievements();
            this.Resources = new Resources();
            this.Boutique = new Boutique();
            this.Chests = new Chests();
            this.Deck = new Deck();
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        /// <param name="_PlayerID">The player identifier.</param>
        public Player(Device _Device, long _PlayerID) : this() {
            Client = _Device;

            m_Id = _PlayerID;
            HomeID = _PlayerID;
            BackupID = _PlayerID;

            Resources.Set(Enums.Resource.MAX_TROPHIES, Trophies);
            Resources.Set(Enums.Resource.CARD_COUNT, Deck.Count);

            Boutique.Add(new Shop(1, 0, 26, 39, 0, DateTime.Today.AddDays(1)));
            Boutique.Add(new Shop(4, 0, 26, 0, 0, DateTime.Today.AddDays(1)));
            Boutique.Add(new Shop(1, 0, 26, 1, 0, DateTime.Today.AddDays(1)));
            Boutique.Add(new Shop(1, 0, 26, 2, 0, DateTime.Today.AddDays(1)));
            Boutique.Add(new Shop(4, 1, 26, 3, 0, DateTime.Today.AddDays(1)));
            Boutique.Add(new Shop(1, 2, 26, 4, 0, DateTime.Today.AddDays(1)));
            Boutique.Add(new Shop(3, 0, 0, 0, 0, DateTime.Today.AddDays(1)));
        }

        public byte[] Data_Part1() {
            int TimeStamp = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            List<byte> _Packet = new List<byte>();

            _Packet.AddLong(m_Id);

            _Packet.Add(16);
            _Packet.Add(0);
            _Packet.AddVInt(1698340);
            _Packet.AddVInt(1727920);
            _Packet.AddVInt(TimeStamp);
            _Packet.Add(0);

            _Packet.Add(1);
            {
                _Packet.AddVInt(8);

                foreach (Card _Card in Deck.GetRange(0, 8)) {
                    _Packet.AddVInt(_Card.GlobalID);
                }
            }

            _Packet.Add(255);
            _Packet.AddRange(Deck.ToBytes());

            _Packet.AddVInt(Deck.Count - 8);
            foreach (Card _Card in Deck.Skip(8)) {
                _Packet.AddVInt(_Card.Type);
                _Packet.AddVInt(_Card.ID);
                _Packet.AddVInt(_Card.Level);
                _Packet.AddVInt(0);
                _Packet.AddVInt(_Card.Count);
                _Packet.AddVInt(0);
                _Packet.AddVInt(0);
                _Packet.AddVInt(_Card.New);
            }

            _Packet.Add(0);
            _Packet.Add(4);

            _Packet.AddRange(Chests.Encode());

            _Packet.AddVInt(287600);
            _Packet.AddVInt(288000);

            _Packet.AddVInt(TimeStamp);

            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(127);

            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);

            _Packet.AddVInt(5); // Crown
            _Packet.AddVInt(0); // 0 = Unlocked    1 = locked
            _Packet.AddVInt(360 * 20); // Time from unlock crown chest

            _Packet.AddRange("A4F4D201".HexaToBytes());
            _Packet.AddVInt(TimeStamp);
            _Packet.AddVInt(0);

            _Packet.AddRange("A4F4D201".HexaToBytes());
            _Packet.AddVInt(TimeStamp);
            _Packet.AddVInt(0);

            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(127);

            _Packet.AddVInt(1); // 0 = Tuto Upgrade Spell
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddVInt(2); // 0, 1 = Animation Page Card (Tuto)

            _Packet.AddVInt(Level);
            _Packet.Add(0x36);
            _Packet.AddVInt((int) Arena);

            _Packet.AddVInt(736968123); // Shop ID
            _Packet.AddVInt((int) DateTime.UtcNow.DayOfWeek + 1);
            _Packet.AddVInt((int) Update.DayOfWeek + 1);

            int _Time = (int) (DateTime.UtcNow.AddDays(1) - DateTime.UtcNow).TotalSeconds;
            _Packet.AddVInt(20 * _Time);
            _Packet.AddVInt(20 * _Time);

            _Packet.AddVInt(TimeStamp);

            _Packet.AddRange(Boutique.EncodeCard());
            _Packet.AddRange(Boutique.EncodeOffer());
            _Packet.AddRange(new byte[]{
                0x00, 0x00, 0x7F,
                0x00, 0x00, 0x7F,
                0x00, 0x00, 0x7F
            });

            _Packet.AddInt(0);
            _Packet.AddInt(0);
            _Packet.AddInt(9);
            _Packet.AddInt(0);

            _Packet.AddRange("F801".HexaToBytes()); // Prefixe from Deck

            _Packet.AddRange(new byte[]{
                0x1A, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1A, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1A, 0x0D, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1C, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1C, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x1A, 0x03, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00
            });

            _Packet.AddVInt(TimeStamp);
            _Packet.AddVInt(0);
            _Packet.AddVInt(0);
            _Packet.AddInt(1);
            _Packet.AddVInt(TimeStamp);

            return _Packet.ToArray();
        }

        /// <summary>
        /// Encode the second part.
        /// </summary>
        /// <returns>THe encoded second part.</returns>
        public byte[] Data_Part2() {
            List<byte> _Packet = new List<byte>();

            _Packet.AddVInt(m_Id);
            _Packet.AddVInt(m_Id);
            _Packet.AddVInt(m_Id);

            _Packet.AddString(m_vAvatarName);
            _Packet.AddVInt(Changes);
            _Packet.AddVInt(0x36); // Arena Data
            _Packet.AddVInt((int) Arena);
            _Packet.AddVInt(Trophies);

            _Packet.AddInt(0);

            _Packet.Add(0);
            _Packet.AddVInt(0); // Rank
            _Packet.AddVInt(Trophies);
            _Packet.AddVInt(Legendary_Trophies); // Legendary Trophies

            _Packet.AddVInt(Resources.Count);
            _Packet.AddVInt(Resources.Count);

            foreach (Resource _Resource in Resources.OrderBy(r => r.Data)) {
                _Packet.AddVInt(_Resource.Type);
                _Packet.AddVInt(_Resource.Data);
                _Packet.AddVInt(_Resource.Value);
            }

            _Packet.Add(0);

            _Packet.AddVInt(Achievements.Count);
            foreach (Achievement _Achievement in Achievements) {
                _Packet.AddVInt(_Achievement.Type);
                _Packet.AddVInt(_Achievement.Data);
                _Packet.AddVInt(_Achievement.Value);
            }

            _Packet.AddVInt(0); // Completed Achievements
            _Packet.AddVInt(0); // Unknown Count

            _Packet.Add(0);

            _Packet.Add(0);

            _Packet.AddVInt(Resources[0].Value);
            _Packet.AddVInt(Resources[0].Value);
            _Packet.AddVInt(Experience);
            _Packet.AddVInt(Level);

            _Packet.Add(0);

            if (Clan) {
                // 8 = Set name popup + clan
                // 9 = Name already set + clan
                // < 8 =  Set name popup

                _Packet.Add(9);

                _Packet.AddVInt(ClanID);
                _Packet.AddString("GobelinLand");
                _Packet.AddVInt(0x10);
                _Packet.AddVInt(16);
            }
            else {
                _Packet.Add(0);
            }

            _Packet.Add(NameSet);

            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);

            _Packet.AddVInt(Tutorial);

            _Packet.Add(0);
            _Packet.Add(0);

            return _Packet.ToArray();
        }

        public string GetAvatarName() {
            return m_vAvatarName;
        }

        public long GetId() {
            return m_Id;
        }

        /// <summary>
        /// Deserialize the specified JSON.
        /// </summary>
        /// <param name="_Data">The JSON.</param>
        public void Deserialize(string _Data) {
            JObject _Json = JObject.Parse(_Data);

            m_Id = _Json["player_id"].ToObject<long>();
            HomeID = _Json["home_id"].ToObject<long>();
            BackupID = _Json["backup_id"].ToObject<long>();
            m_vAllianceId = _Json["clan_id"].ToObject<long>();

            Token = _Json["token"].ToObject<string>();
            Pass = _Json["password"].ToObject<string>();
            m_vAvatarName = _Json["name"].ToObject<string>();
            Region = _Json["region"].ToObject<string>();

            Level = _Json["level"].ToObject<int>();
            Experience = _Json["experience"].ToObject<int>();
            Trophies = _Json["trophies"].ToObject<int>();
            Wins = _Json["wins"].ToObject<int>();
            Loses = _Json["loses"].ToObject<int>();
            Report = _Json["report"].ToObject<int>();
            Donations = _Json["donations"].ToObject<int>();
            Legendary_Trophies = _Json["legendary_trophies"].ToObject<int>();

            Arena = (Arena) _Json["arena"].ToObject<int>();
            Rank = (Rank) _Json["rank"].ToObject<int>();

            Tutorial = _Json["tutorial"].ToObject<byte>();
            Changes = _Json["changes"].ToObject<byte>();
            NameSet = _Json["name_set"].ToObject<byte>();

            Android = _Json["android"].ToObject<bool>();
            Clan = _Json["clan"].ToObject<bool>();
            Banned = _Json["banned"].ToObject<bool>();
            Muted = _Json["muted"].ToObject<bool>();

            Update = _Json["update"].ToObject<DateTime>();
            Created = _Json["created"].ToObject<DateTime>();
            BanTime = _Json["ban_time"].ToObject<DateTime>();
            MuteTime = _Json["mute_time"].ToObject<DateTime>();
        }

        /// <summary>
        /// Serialize this instance.
        /// </summary>
        /// <returns>The player data in JSON.</returns>
        public string SaveToJSON() {
            JObject _JSON = new JObject();

            _JSON.Add("player_id", m_Id);
            _JSON.Add("home_id", HomeID);
            _JSON.Add("backup_id", BackupID);
            _JSON.Add("clan_id", m_vAllianceId);

            _JSON.Add("token", Token);
            _JSON.Add("password", Pass);
            _JSON.Add("name", m_vAvatarName);
            _JSON.Add("region", Region);

            _JSON.Add("level", Level);
            _JSON.Add("experience", Experience);
            _JSON.Add("trophies", Trophies);
            _JSON.Add("wins", Wins);
            _JSON.Add("loses", Loses);
            _JSON.Add("report", Report);
            _JSON.Add("donations", Donations);
            _JSON.Add("legendary_trophies", Legendary_Trophies);

            _JSON.Add("arena", (int) Arena);
            _JSON.Add("rank", (int) Rank);

            _JSON.Add("tutorial", Tutorial);
            _JSON.Add("changes", Changes);
            _JSON.Add("name_set", NameSet);

            _JSON.Add("android", Android);
            _JSON.Add("clan", Clan);
            _JSON.Add("banned", Banned);
            _JSON.Add("muted", Muted);

            _JSON.Add("update", Update);
            _JSON.Add("created", Created);
            _JSON.Add("ban_time", BanTime);
            _JSON.Add("mute_time", MuteTime);

            return JsonConvert.SerializeObject(_JSON);
        }

        public Achievements Achievements { get; set; }
        public Resources Resources { get; set; }
        public Boutique Boutique { get; set; }
        public Chests Chests { get; set; }
        public Deck Deck { get; set; }

        /// <summary>
        /// Tick this instance.
        /// </summary>
        public void Tick() {
            // Tick components...
        }
    }
}