namespace CRS.Logic.Slots.Items
{
    #region Usings

    using System;

    using Newtonsoft.Json.Linq;

    #endregion Usings

    public class Shop
    {
        public byte Rarity = 0;
        public int Count = 0;
        public int ID = 0;
        public byte Type = 0;
        public byte ShopType = 0;
        public DateTime EndOffer = DateTime.UtcNow;

        /// <summary>
        /// Initialize a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Shop(JObject _JSON)
        {
            this.Deserialize(_JSON);
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="_ShopType">The ShopType.</param>
        /// <param name="_Rarity">The Card Rarity</param>
        /// <param name="_Type">The type.</param>
        /// <param name="_ID">The identifier.</param>
        /// <param name="_Count">The count.</param>
        /// <param name="_EndOffer">The date</param>
        public Shop(byte _ShopType, byte _Rarity, byte _Type, int _ID, int _Count, DateTime _EndOffer)
        {
            this.ShopType = _ShopType;
            this.Type = _Type;
            this.Rarity = _Rarity;
            this.ID = _ID;
            this.Count = _Count;
            this.EndOffer = _EndOffer;
        }

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        /// <param name="_Json">The json.</param>
        public void Deserialize(JObject _Json)
        {
            this.ShopType = _Json["shop_type"].ToObject<byte>();
            this.Rarity = _Json["card_rarity"].ToObject<byte>();
            this.Type = _Json["type"].ToObject<byte>();
            this.ID = _Json["id"].ToObject<int>();
            this.Count = _Json["count"].ToObject<int>();
            this.EndOffer = _Json["end_offer"].ToObject<DateTime>();
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns>The json.</returns>
        public JObject Serialize()
        {
            JObject _Json = new JObject();

            _Json.Add("shop_type", this.Type);
            _Json.Add("card_rarity", this.ID);
            _Json.Add("type", this.Count);
            _Json.Add("id", this.Type);
            _Json.Add("count", this.ID);
            _Json.Add("end_offer", this.Count);

            return _Json;
        }
    }
}
