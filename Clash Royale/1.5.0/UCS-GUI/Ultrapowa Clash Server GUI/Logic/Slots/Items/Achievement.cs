namespace CRS.Logic.Slots.Items
{
    #region Usings

    using Newtonsoft.Json.Linq;

    #endregion Usings

    public class Achievement
    {
        public int Type         = 0x3C;// 60
        public int Data         = 0;
        public int Value        = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Achievement"/> class.
        /// </summary>
        /// <param name="_Data">The data.</param>
        /// <param name="_Value">The value.</param>
        public Achievement(int _Data, int _Value)
        {
            this.Data   = _Data;
            this.Value  = _Value;
        }

        /// <summary>
        /// Deserialize the specified JSON.
        /// </summary>
        /// <param name="_Json">The JSON.</param>
        public void Deserialize(JObject _Json)
        {
            this.Data   = _Json["data"].ToObject<int>();
            this.Value  = _Json["value"].ToObject<int>();
        }

        /// <summary>
        /// Serialize this instance.
        /// </summary>
        /// <returns>The JSON.</returns>
        public JObject Serialize()
        {
            JObject _Json = new JObject();
            _Json.Add("data", this.Data);
            _Json.Add("value", this.Value);
            return _Json;
        }
    }
}
