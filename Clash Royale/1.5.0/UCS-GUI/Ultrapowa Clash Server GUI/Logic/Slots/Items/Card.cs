using CRS.Core;

namespace CRS.Logic.Slots.Items
{
    #region Usings

    using Newtonsoft.Json.Linq;
    using CRS.GameFiles;
    using CRS.Logic.Enums;

    #endregion

    public class Card
    {
        public int Count    = 0;

        public byte ID      = 0;
        public byte Level   = 0;
        public byte New     = 0;
        public byte Type    = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card(JObject _JSON)
        {
            this.Deserialize(_JSON);
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="_Type">The type.</param>
        /// <param name="_ID">The identifier.</param>
        /// <param name="_Count">The count.</param>
        /// <param name="_Level">The level.</param>
        /// <param name="_isNew">The new.</param>
        public Card(byte _Type, byte _ID, int _Count, byte _Level, byte _isNew)
        {
            this.Type   = _Type;
            this.ID     = _ID;
            this.Count  = _Count;
            this.Level  = _Level;
            this.New    = _isNew;
        }

        public int GlobalID
        {
            get
            {
                return this.Type * 1000000 + this.ID;
            }
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        /// <param name="_Json">The json.</param>
        public void Deserialize(JObject _Json)
        {
            this.Type   = _Json["type"].ToObject<byte>();
            this.ID     = _Json["id"].ToObject<byte>();
            this.Count  = _Json["count"].ToObject<int>();
            this.Level  = _Json["level"].ToObject<byte>();
            this.New    = _Json["new"].ToObject<byte>();
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns>The json.</returns>
        public JObject Serialize()
        {
            JObject _Json = new JObject();

            _Json.Add("type", this.Type);
            _Json.Add("id", this.ID);
            _Json.Add("global_id", this.GlobalID);
            _Json.Add("count", this.Count);
            _Json.Add("level", this.Level);
            _Json.Add("new", this.New);

            return _Json;
        }

        /// <summary>
        /// Upgrade this instance / card.
        /// </summary>
        public void Upgrade()
        {
            if (this.Type == 26)
            {
                Spells_Characters _Card =
                    ObjectManager.DataTables.GetTable((int)Gamefile.Spells_Characters).GetItemById(this.ID) as
                        Spells_Characters;

                    Rarities _Rarity =
                    ObjectManager.DataTables.GetTable((int)Gamefile.Rarities).GetDataByName(_Card.Rarity) as Rarities;
                if (this.Level < _Rarity.LevelCount)
                {
                    this.Count -= _Rarity.UpgradeMaterialCount[this.Level];
                    this.Level++;
                }
            }
            else if (this.Type == 27)
            {
                Spells_Buildings _Card = ObjectManager.DataTables.GetTable((int)Gamefile.Spells_Buildings).GetItemById(this.ID) as Spells_Buildings;
                Rarities _Rarity = ObjectManager.DataTables.GetTable((int)Gamefile.Rarities).GetDataByName(_Card.Rarity) as Rarities;

                if (this.Level < _Rarity.LevelCount)
                {
                    this.Count -= _Rarity.UpgradeMaterialCount[this.Level];
                    this.Level++;
                }
            }
            else if (this.Type == 28)
            {
                Spells_Other _Card = ObjectManager.DataTables.GetTable((int)Gamefile.Spells_Other).GetItemById(this.ID) as Spells_Other;
                Rarities _Rarity = ObjectManager.DataTables.GetTable((int)Gamefile.Rarities).GetDataByName(_Card.Rarity) as Rarities;

                if (this.Level < _Rarity.LevelCount)
                {
                    this.Count -= _Rarity.UpgradeMaterialCount[this.Level];
                    this.Level++;
                }
            }
        }
    }
}