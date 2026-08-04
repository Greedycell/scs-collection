using GL.Servers.CR.Core;
using GL.Servers.CR.Files.CSV_Logic;
namespace GL.Servers.CR.Logic
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.CR.Extensions.List;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Logic.Slots;
    using GL.Servers.CR.Logic.Slots.Items;

    using Newtonsoft.Json.Linq;

    using Resource = GL.Servers.CR.Logic.Slots.Items.Resource;
    using System.ComponentModel;
    using Files;
    using Extensions.Binary;

    #endregion Usings

    internal class Player : INotifyPropertyChanged
    {
        #region Constants
        private static readonly PropertyChangedEventArgs s_namedChanged = new PropertyChangedEventArgs(nameof(Name));
        private static readonly PropertyChangedEventArgs s_isNamedChanged = new PropertyChangedEventArgs(nameof(IsNamed));
        private static readonly PropertyChangedEventArgs s_idChanged = new PropertyChangedEventArgs(nameof(Id));
        private static readonly PropertyChangedEventArgs s_allianceChanged = new PropertyChangedEventArgs(nameof(Alliance));
        private static readonly PropertyChangedEventArgs s_leagueChanged = new PropertyChangedEventArgs(nameof(League));
        private static readonly PropertyChangedEventArgs s_expLevelChanged = new PropertyChangedEventArgs(nameof(ExpLevels));
        private static readonly PropertyChangedEventArgs s_expPointsChanged = new PropertyChangedEventArgs(nameof(ExpPoints));
        private static readonly PropertyChangedEventArgs s_gemsChanged = new PropertyChangedEventArgs(nameof(Gems));
        private static readonly PropertyChangedEventArgs s_freeGemsChanged = new PropertyChangedEventArgs(nameof(FreeGems));
        private static readonly PropertyChangedEventArgs s_trophiesChanged = new PropertyChangedEventArgs(nameof(Trophies));
        private static readonly PropertyChangedEventArgs s_attkWonChanged = new PropertyChangedEventArgs(nameof(Wins));
        private static readonly PropertyChangedEventArgs s_attkLostChanged = new PropertyChangedEventArgs(nameof(Loses));
        private static readonly PropertyChangedEventArgs s_clanIDChanged = new PropertyChangedEventArgs(nameof(ClanID));
        private static readonly PropertyChangedEventArgs s_battleIDChanged = new PropertyChangedEventArgs(nameof(BattleID));
        private static readonly PropertyChangedEventArgs s_reportChanged = new PropertyChangedEventArgs(nameof(Report));
        private static readonly PropertyChangedEventArgs s_donationsChanged = new PropertyChangedEventArgs(nameof(Donations));
        private static readonly PropertyChangedEventArgs s_legendary_TrophiesChanged = new PropertyChangedEventArgs(nameof(Legendary_Trophies));
        private static readonly PropertyChangedEventArgs s_tutorialChanged = new PropertyChangedEventArgs(nameof(Tutorial));
        private static readonly PropertyChangedEventArgs s_changesChanged = new PropertyChangedEventArgs(nameof(Changes));
        private static readonly PropertyChangedEventArgs s_nameSetChanged = new PropertyChangedEventArgs(nameof(NameSet));
        private static readonly PropertyChangedEventArgs s_arenaChanged = new PropertyChangedEventArgs(nameof(Arena));
        private static readonly PropertyChangedEventArgs s_rankChanged = new PropertyChangedEventArgs(nameof(Rank));
        private static readonly PropertyChangedEventArgs s_statusChanged = new PropertyChangedEventArgs(nameof(Status));
        private static readonly PropertyChangedEventArgs s_tokenChanged = new PropertyChangedEventArgs(nameof(Token));
        private static readonly PropertyChangedEventArgs s_passChanged = new PropertyChangedEventArgs(nameof(Pass));
        private static readonly PropertyChangedEventArgs s_regionChanged = new PropertyChangedEventArgs(nameof(Region));
        private static readonly PropertyChangedEventArgs s_androidChanged = new PropertyChangedEventArgs(nameof(Android));
        private static readonly PropertyChangedEventArgs s_clanChanged = new PropertyChangedEventArgs(nameof(Clan));
        private static readonly PropertyChangedEventArgs s_bannedChanged = new PropertyChangedEventArgs(nameof(Banned));
        private static readonly PropertyChangedEventArgs s_mutedChanged = new PropertyChangedEventArgs(nameof(Muted));
        private static readonly PropertyChangedEventArgs s_updateChanged = new PropertyChangedEventArgs(nameof(DateLastSave));
        private static readonly PropertyChangedEventArgs s_playTimeChanged = new PropertyChangedEventArgs(nameof(PlayTime));
        private static readonly PropertyChangedEventArgs s_createdChanged = new PropertyChangedEventArgs(nameof(DateCreated));
        private static readonly PropertyChangedEventArgs s_banTimeChanged = new PropertyChangedEventArgs(nameof(BanTime));
        private static readonly PropertyChangedEventArgs s_muteTimeChanged = new PropertyChangedEventArgs(nameof(MuteTime));
        #endregion

        #region Fields & Properties
        public Device Client = null;

        //public long PlayerID    = 0;
        //public long HomeID      = 0;
        //public long BackupID    = 0;
        //public long ClanID      = 0;

        //public int BattleID     = 0;

        //public int Level        = 1;
        //public int Experience   = 0;
        //public int Trophies     = 0;

        //public int Wins         = 0;
        //public int Loses        = 0;
        //public int Report       = 0;
        //public int Donations    = 0;

        //public int Legendary_Trophies = 0;

        //public byte Tutorial    = 0x00; //0x06
        //public byte Changes     = 0x00;
        //public byte NameSet     = 0x00;

        //public Arena Arena      = Arena.TRAINING_CAMP;//Arena.ARENA_L
        //public Rank Rank        = Rank.Player;
        //public Status Status    = Status.ACTIVE;

        //public string Token     = string.Empty;
        //public string Pass      = string.Empty;

        //public string Region    = string.Empty;

        //public bool Android     = false;
        //public bool Clan        = false;
        //public bool Banned      = false;
        //public bool Muted       = false;

        //public DateTime Update      = DateTime.UtcNow;
        //public DateTime Created     = DateTime.UtcNow;
        //public DateTime BanTime     = DateTime.UtcNow;
        //public DateTime MuteTime    = DateTime.UtcNow;

        public Achievements Achievements    = new Achievements();
        public Resources Resources          = new Resources();
        public Boutique Boutique            = new Boutique();
        public Chests Chests                = new Chests();
        public Deck Deck                    = new Deck();

        private string _name = "unnamed";
        private bool _isNamed = false;
        private long _id;
        private Clan _alliance;
        private int _league;
        private int _expLevel = 1;
        private int _expPoints = 0;
        private int _gems;
        private int _freeGems;
        private int _trophies;
        private int _attkWon;
        private int _attkLost;
        private long _clanID; 
        private int _battleID;
        private int _report;
        private int _donations;
        private int _legendary_Trophies;
        private byte _tutorial;
        private byte _changes;
        private byte _nameSet;
        private Arena _arena = Arena.TRAINING_CAMP;
        private Rank _rank;
        private Status _status = Status.ACTIVE;
        private string _token;
        private string _pass;
        private string _region;
        private bool _android;
        private bool _clan;
        private bool _banned;
        private bool _muted;
        private DateTime _update;
        private TimeSpan _playTime;
        private DateTime _created;
        private DateTime _banTime;
        private DateTime _muteTime;

        public bool IsPropertyChangedEnabled { get; set; }

        /// <summary>
        /// The event raised when a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the username of the <see cref="Player"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                // Client crashes if name is null.
                if (_name == null)
                    throw new ArgumentNullException(nameof(value));

                if (_name == value)
                    return;
                DebugWrite.write(this._name + " -> " + value);
                _name = value;
                OnPropertyChanged(s_namedChanged);
            }
        }
        /// <summary>
        /// Gets or sets whether the <see cref="Player"/> has been named.
        /// </summary>
        public bool IsNamed
        {
            get
            {
                return _isNamed;
            }
            set
            {
                if (_isNamed == value)
                    return;

                _isNamed = value;
                OnPropertyChanged(s_isNamedChanged);
            }
        }
        /// <summary>
        /// Gets or sets the user ID of the <see cref="Player"/>.
        /// </summary>
        public long Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value)
                    return;

                _id = value;
                OnPropertyChanged(s_idChanged);
            }
        }
        /// <summary>
        /// Gets or sets the <see cref="Clan"/> associated with this <see cref="Player"/>.
        /// </summary>
        public Clan Alliance
        {
            get
            {
                return _alliance;
            }
            set
            {
                if (_alliance == value)
                    return;

                _alliance = value;
                OnPropertyChanged(s_allianceChanged);
            }
        }
        /// <summary>
        /// Gets or sets the league of the <see cref="Player"/>.
        /// </summary>
        public int League
        {
            get
            {
                return _league;
            }
            set
            {
                if (_league == value)
                    return;

                _league = value;
                OnPropertyChanged(s_leagueChanged);
            }
        }
        /// <summary>
        /// Gets or sets the level of the <see cref="Player"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than 1.</exception>
        public int ExpLevels
        {
            get
            {
                return _expLevel;
            }
            set
            {
                if (_expLevel == value)
                    return;

                // Clash of Clans client crashes when level is less than 1.
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", "value cannot be less than 1.");

                _expLevel = value;
                OnPropertyChanged(s_expLevelChanged);
            }
        }
        /// <summary>
        /// Gets or sets the experience of the <see cref="Player"/>.
        /// </summary>
        public int ExpPoints
        {
            get
            {
                return _expPoints;
            }
            set
            {
                if (_expPoints == value)
                    return;

                _expPoints = value;
                OnPropertyChanged(s_expPointsChanged);
            }
        }
        /// <summary>
        /// Gets or sets the amount of gems of the <see cref="Player"/>.
        /// </summary>
        public int Gems
        {
            get
            {
                return _gems;
            }
            set
            {
                if (_gems == value)
                    return;

                _gems = value;
                OnPropertyChanged(s_gemsChanged);
            }
        }
        /// <summary>
        /// Gets or sets the amount of free gems of the <see cref="Player"/>.
        /// </summary>
        public int FreeGems
        {
            get
            {
                return _freeGems;
            }
            set
            {
                if (_freeGems == value)
                    return;

                _freeGems = value;
                OnPropertyChanged(s_freeGemsChanged);
            }
        }
        /// <summary>
        /// Gets or sets the amount of trophies of the <see cref="Player"/>.
        /// </summary>
        public int Trophies
        {
            get
            {
                return _trophies;
            }
            set
            {
                if (_trophies == value)
                    return;

                _trophies = value;
                OnPropertyChanged(s_trophiesChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks won by the <see cref="Player"/>.
        /// </summary>
        public int Wins
        {
            get
            {
                return _attkWon;
            }
            set
            {
                if (_attkWon == value)
                    return;

                _attkWon = value;
                OnPropertyChanged(s_attkWonChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public long ClanID
        {
            get
            {
                return _clanID;
            }
            set
            {
                if (_clanID == value)
                    return;

                _clanID = value;
                OnPropertyChanged(s_clanIDChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public int Loses
        {
            get
            {
                return _attkLost;
            }
            set
            {
                if (_attkLost == value)
                    return;

                _attkLost = value;
                OnPropertyChanged(s_attkLostChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public int BattleID
        {
            get
            {
                return _battleID;
            }
            set
            {
                if (_battleID == value)
                    return;

                _battleID = value;
                OnPropertyChanged(s_battleIDChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public int Report
        {
            get
            {
                return _report;
            }
            set
            {
                if (_report == value)
                    return;

                _report = value;
                OnPropertyChanged(s_reportChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public int Donations
        {
            get
            {
                return _donations;
            }
            set
            {
                if (_donations == value)
                    return;

                _donations = value;
                OnPropertyChanged(s_donationsChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public int Legendary_Trophies
        {
            get
            {
                return _legendary_Trophies;
            }
            set
            {
                if (_legendary_Trophies == value)
                    return;

                _legendary_Trophies = value;
                OnPropertyChanged(s_legendary_TrophiesChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public byte Tutorial
        {
            get
            {
                return _tutorial;
            }
            set
            {
                if (_tutorial == value)
                    return;

                _tutorial = value;
                OnPropertyChanged(s_tutorialChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public byte Changes
        {
            get
            {
                return _changes;
            }
            set
            {
                if (_changes == value)
                    return;

                _changes = value;
                OnPropertyChanged(s_changesChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public byte NameSet
        {
            get
            {
                return _nameSet;
            }
            set
            {
                if (_nameSet == value)
                    return;

                _nameSet = value;
                OnPropertyChanged(s_nameSetChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public Arena Arena
        {
            get
            {
                return _arena;
            }
            set
            {
                if (_arena == value)
                    return;

                _arena = value;
                OnPropertyChanged(s_arenaChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public Rank Rank
        {
            get
            {
                return _rank;
            }
            set
            {
                if (_rank == value)
                    return;

                _rank = value;
                OnPropertyChanged(s_rankChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public Status Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status == value)
                    return;

                _status = value;
                OnPropertyChanged(s_statusChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                if (_token == value)
                    return;

                _token = value;
                OnPropertyChanged(s_tokenChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public string Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                if (_pass == value)
                    return;

                _pass = value;
                OnPropertyChanged(s_passChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public string Region
        {
            get
            {
                return _region;
            }
            set
            {
                if (_region == value)
                    return;

                _region = value;
                OnPropertyChanged(s_regionChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public bool Android
        {
            get
            {
                return _android;
            }
            set
            {
                if (_android == value)
                    return;

                _android = value;
                OnPropertyChanged(s_androidChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public bool Clan
        {
            get
            {
                return _clan;
            }
            set
            {
                if (_clan == value)
                    return;

                _clan = value;
                OnPropertyChanged(s_clanChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public bool Banned
        {
            get
            {
                return _banned;
            }
            set
            {
                if (_banned == value)
                    return;

                _banned = value;
                OnPropertyChanged(s_bannedChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public bool Muted
        {
            get
            {
                return _muted;
            }
            set
            {
                if (_muted == value)
                    return;

                _muted = value;
                OnPropertyChanged(s_mutedChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public DateTime DateLastSave
        {
            get
            {
                return _update;
            }
            set
            {
                if (_update == value)
                    return;

                _update = value;
                OnPropertyChanged(s_updateChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public TimeSpan PlayTime
        {
            get
            {
                return _playTime;
            }
            set
            {
                if (_playTime == value)
                    return;

                _playTime = value;
                OnPropertyChanged(s_playTimeChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return _created;
            }
            set
            {
                if (_created == value)
                    return;

                _created = value;
                OnPropertyChanged(s_createdChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public DateTime BanTime
        {
            get
            {
                return _banTime;
            }
            set
            {
                if (_banTime == value)
                    return;

                _banTime = value;
                OnPropertyChanged(s_banTimeChanged);
            }
        }
        /// <summary>
        /// Gets or sets the number of attacks lost by the <see cref="Player"/>.
        /// </summary>
        public DateTime MuteTime
        {
            get
            {
                return _muteTime;
            }
            set
            {
                if (_muteTime == value)
                    return;

                _muteTime = value;
                OnPropertyChanged(s_muteTimeChanged);
            }
        }
        #endregion

        /// <summary>
        /// Initialize a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        /// <param name="Id">The player identifier.</param>
        public Player(Device _Device, long Id)
        {
                this.Id = Id;
                //_name = "unnamed";
                //_expLevel = 1;
                //_rank = Rank.Player;
                //_status = Status.ACTIVE;
                //_arena = Arena.TRAINING_CAMP;
            this.Client     = _Device;

            //this._id = Id;
            //if (string.IsNullOrEmpty(_Device.Level.Name))
            //{
            //    Name = "unnamed";
            //}
            //this._name           = "unnamed";

            //this._expLevel = 1;
            //this._rank = Rank.Player;
            //this._status = Status.ACTIVE;
            //this._arena = Arena.TRAINING_CAMP;
            //this.HomeID     = _PlayerID;
            //this.BackupID   = _PlayerID;

            //this.Resources.Set(Enums.Resource.MAX_TROPHIES, this.Trophies);
            //this.Resources.Set(Enums.Resource.CARD_COUNT, this.Deck.Count);

            //this.Boutique.Add(new Shop(1, 0, 26, 39, 0, DateTime.Today.AddDays(1)));
            //this.Boutique.Add(new Shop(4, 0, 26, 0, 0, DateTime.Today.AddDays(1)));
            //this.Boutique.Add(new Shop(1, 0, 26, 1, 0, DateTime.Today.AddDays(1)));
            //this.Boutique.Add(new Shop(1, 0, 26, 2, 0, DateTime.Today.AddDays(1)));
            //this.Boutique.Add(new Shop(4, 1, 26, 3, 0, DateTime.Today.AddDays(1)));
            //this.Boutique.Add(new Shop(1, 2, 26, 4, 0, DateTime.Today.AddDays(1)));
            //this.Boutique.Add(new Shop(3, 0, 0, 0, 0, DateTime.Today.AddDays(1)));
        }

        public byte[] Data_Part1()
        {
            int TimeStamp = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            List<byte> _Packet = new List<byte>();

            _Packet.AddLong(this.Id);

            _Packet.Add(16);
            _Packet.Add(0);
            _Packet.AddVInt(1698340);
            _Packet.AddVInt(1727920);
            _Packet.AddVInt(TimeStamp);
            _Packet.Add(0);

            _Packet.Add(1);
            {
                _Packet.AddVInt(8);

                foreach (Card _Card in this.Deck.GetRange(0, 8))
                {
                    _Packet.AddVInt(_Card.GlobalID);
                }
            }

            _Packet.Add(255);
            _Packet.AddRange(this.Deck.ToBytes());

            _Packet.AddVInt(this.Deck.Count - 8);
            foreach (Card _Card in this.Deck.Skip(8))
            {
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

            _Packet.AddRange(this.Chests.Encode());

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
            _Packet.AddVInt(0); // 0, 1 = Animation Page Card (Tuto)

            _Packet.AddVInt(this.ExpLevels);//Level
            _Packet.Add(0x36);
            _Packet.AddVInt((int)this.Arena);

            _Packet.AddVInt(736968123); // Shop ID
            _Packet.AddVInt((int)DateTime.UtcNow.DayOfWeek + 1);
            _Packet.AddVInt((int)this.DateLastSave.DayOfWeek + 1);

            int _Time = (int)(DateTime.UtcNow.AddDays(1) - DateTime.UtcNow).TotalSeconds;
            _Packet.AddVInt(20 * _Time);
            _Packet.AddVInt(20 * _Time);

            _Packet.AddVInt(TimeStamp);

            _Packet.AddRange(this.Boutique.EncodeCard());
            _Packet.AddRange(this.Boutique.EncodeOffer());
            _Packet.AddRange(new byte[]
            {
                0x00, 0x00, 0x7F,
                0x00, 0x00, 0x7F,
                0x00, 0x00, 0x7F
            });

            _Packet.AddInt(0);
            _Packet.AddInt(0);
            _Packet.AddInt(9);
            _Packet.AddInt(0);

            _Packet.AddRange("F801".HexaToBytes()); // Prefixe from Deck

            _Packet.AddRange(new byte[]
            {
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
        public byte[] Data_Part2()
        {
            List<byte> _Packet = new List<byte>();

            _Packet.AddVInt(this.Id);//PlayerID
            _Packet.AddVInt(this.Id);//HomeID
            _Packet.AddVInt(this.Id);//BackupID

            _Packet.AddString(this.Name);
            _Packet.AddVInt(this.Changes);
            _Packet.AddVInt(0x36); // Arena Data
            _Packet.AddVInt((int)this.Arena);
            _Packet.AddVInt(this.Trophies);

            _Packet.AddInt(0);

            _Packet.Add(0);
            _Packet.AddVInt((int)this.Rank); // Rank 0
            _Packet.AddVInt(this.Trophies);
            _Packet.AddVInt(this.Legendary_Trophies); // Legendary Trophies

            _Packet.AddVInt(this.Resources.Count);
            _Packet.AddVInt(this.Resources.Count);

            foreach (Resource _Resource in this.Resources.OrderBy(r => r.Data))
            {
                _Packet.AddVInt(_Resource.Type);
                _Packet.AddVInt(_Resource.Data);
                _Packet.AddVInt(_Resource.Value);
            }

            _Packet.Add(0);

            _Packet.AddVInt(this.Achievements.Count);
            foreach (Achievement _Achievement in this.Achievements)
            {
                _Packet.AddVInt(_Achievement.Type);
                _Packet.AddVInt(_Achievement.Data);
                _Packet.AddVInt(_Achievement.Value);
            }

            _Packet.AddVInt(0); // Completed Achievements
            _Packet.AddVInt(0); // Unknown Count

            _Packet.Add(0);

            _Packet.Add(0);

            _Packet.AddVInt(this.Resources[0].Value);
            _Packet.AddVInt(this.Resources[0].Value);
            _Packet.AddVInt(this.ExpPoints);
            _Packet.AddVInt(this.ExpLevels);//Level

            _Packet.Add(0);

            if (this.Clan)
            {
                // 8 = Set name popup + clan
                // 9 = Name already set + clan
                // < 8 =  Set name popup

                _Packet.Add(9);

                _Packet.AddVInt(this.ClanID);
                _Packet.AddString("ClashRoyaleSpain");
                _Packet.AddVInt(0x10);
                _Packet.AddVInt(16);
            }
            else
            {
                _Packet.Add(0);
            }

            _Packet.Add(this.NameSet);

            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);
            _Packet.Add(0);

            _Packet.AddVInt(this.Tutorial);

            _Packet.Add(0);
            _Packet.Add(0);

            return _Packet.ToArray();
        }

        /// <summary>
        /// Deserialize the specified JSON.
        /// </summary>
        /// <param name="_Data">The JSON.</param>
        public void Deserialize(string _Data)
        {
            JObject _Json = JObject.Parse(_Data);

            this.Id = _Json["player_id"].ToObject<long>();
            this.Id = _Json["home_id"].ToObject<long>();
            this.Id = _Json["backup_id"].ToObject<long>();
            this.ClanID = _Json["clan_id"].ToObject<long>();

            this.Token = _Json["token"].ToObject<string>();
            this.Pass = _Json["password"].ToObject<string>();
            this.Name = _Json["name"].ToObject<string>();
            this.Region = _Json["region"].ToObject<string>();

            this.ExpLevels = _Json["level"].ToObject<int>();
            this.ExpPoints = _Json["experience"].ToObject<int>();
            this.Trophies = _Json["trophies"].ToObject<int>();
            this.Wins = _Json["wins"].ToObject<int>();
            this.Loses = _Json["loses"].ToObject<int>();
            this.Report = _Json["report"].ToObject<int>();
            this.Donations = _Json["donations"].ToObject<int>();
            this.Legendary_Trophies = _Json["legendary_trophies"].ToObject<int>();

            this.Arena = (Arena)_Json["arena"].ToObject<int>();
            this.Rank = (Rank)_Json["rank"].ToObject<int>();

            this.Tutorial = _Json["tutorial"].ToObject<byte>();
            this.Changes = _Json["changes"].ToObject<byte>();
            this.NameSet = _Json["name_set"].ToObject<byte>();

            this.Android = _Json["android"].ToObject<bool>();
            this.Clan = _Json["clan"].ToObject<bool>();
            this.Banned = _Json["banned"].ToObject<bool>();
            this.Muted = _Json["muted"].ToObject<bool>();

            this.DateLastSave = _Json["update"].ToObject<DateTime>();
            this.DateCreated = _Json["created"].ToObject<DateTime>();
            this.BanTime = _Json["ban_time"].ToObject<DateTime>();
            this.MuteTime = _Json["mute_time"].ToObject<DateTime>();
        }

        /// <summary>
        /// Serialize this instance.
        /// </summary>
        /// <returns>The player data in JSON.</returns>
        public JObject Serialize()
        {
            JObject _JSON = new JObject();

            _JSON.Add("player_id", this.Id);
            _JSON.Add("home_id", this.Id);
            _JSON.Add("backup_id", this.Id);
            _JSON.Add("clan_id", this.ClanID);

            _JSON.Add("token", this.Token);
            _JSON.Add("password", this.Pass);
            _JSON.Add("name", this.Name);
            _JSON.Add("region", this.Region);

            _JSON.Add("level", this.ExpLevels);
            _JSON.Add("experience", this.ExpPoints);
            _JSON.Add("trophies", this.Trophies);
            _JSON.Add("wins", this.Wins);
            _JSON.Add("loses", this.Loses);
            _JSON.Add("report", this.Report);
            _JSON.Add("donations", this.Donations);
            _JSON.Add("legendary_trophies", this.Legendary_Trophies);

            _JSON.Add("arena", (int) this.Arena);
            _JSON.Add("rank", (int) this.Rank);

            _JSON.Add("tutorial", this.Tutorial);
            _JSON.Add("changes", this.Changes);
            _JSON.Add("name_set", this.NameSet);

            _JSON.Add("android", this.Android);
            _JSON.Add("clan", this.Clan);
            _JSON.Add("banned", this.Banned);
            _JSON.Add("muted", this.Muted);

            _JSON.Add("update", this.DateLastSave);
            _JSON.Add("created", this.DateCreated);
            _JSON.Add("ban_time", this.BanTime);
            _JSON.Add("mute_time", this.MuteTime);

            return _JSON;
        }

        /// <summary>
        /// Tick this instance.
        /// </summary>
        public void Tick()
        {
            // Tick components...
        }
        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event with the specified <see cref="PropertyChangedEventArgs"/>.
        /// </summary>
        /// <param name="args">The data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (IsPropertyChangedEnabled && PropertyChanged != null)
            {
                PropertyChanged(this, args);
            }
        }
    }
}
