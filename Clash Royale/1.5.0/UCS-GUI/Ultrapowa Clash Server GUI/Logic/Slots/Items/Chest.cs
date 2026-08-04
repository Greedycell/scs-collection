namespace CRS.Logic.Slots.Items
{
    #region Usings

    using System;

    using Newtonsoft.Json.Linq;

    #endregion Usings

    public class Chest
    {
        public byte Emplacement     = 0;
        public int ID               = 0;
        public bool Unlock          = false;
        public DateTime  UnlockTime = DateTime.Now;
        public bool New             = false;

        /// <summary>
        /// Initialize a new instance of the <see cref="Chest"/> class.
        /// </summary>
        public Chest(JObject _JSON)
        {
            this.Deserialize(_JSON);
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Chest"/> class.
        /// </summary>
        /// <param name="_Emplacement">The chest emplacement.</param>
        /// <param name="_ID">The identifier.</param>
        /// <param name="_Unlock">if chest is unlocked</param>
        /// <param name="_UnlockTime">the unlock date</param>
        /// <param name="_isNew">The new.</param>
        public Chest(byte _Emplacement, int _ID, bool _Unlock, DateTime _UnlockTime, bool _isNew)
        {
            this.Emplacement  = _Emplacement;
            this.ID           = _ID;
            this.Unlock       = _Unlock;
            this.UnlockTime   = _UnlockTime;
            this.New          = _isNew;
        }

        /// <summary>
        /// Deserialize the specified json.
        /// </summary>
        /// <param name="_Json">The json.</param>
        public void Deserialize(JObject _Json)
        {
            this.Emplacement = _Json["emplacement"].ToObject<byte>();
            this.ID          = _Json["id"].ToObject<int>();
            this.Unlock      = _Json["unlock"].ToObject<bool>();
            this.UnlockTime  = _Json["unlock_time"].ToObject<DateTime>();
            this.New         = _Json["new"].ToObject<bool>();
        }

        /// <summary>
        /// Serialize this instance.
        /// </summary>
        /// <returns>The json.</returns>
        public JObject Serialize()
        {
            JObject _Json = new JObject();

            _Json.Add("emplacement", this.Emplacement);
            _Json.Add("id", this.ID);
            _Json.Add("unlock", this.Unlock);
            _Json.Add("unlock_time", this.UnlockTime);
            _Json.Add("new", this.New);

            return _Json;
        }
    }
}
