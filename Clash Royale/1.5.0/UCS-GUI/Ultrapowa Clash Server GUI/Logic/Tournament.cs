namespace CRS.Logic
{
    #region Usings
   
    using Newtonsoft.Json.Linq;

    #endregion Usings

    internal class Tournament
    {
        public long TournamentID    = 0;

        public string Location      = string.Empty;

        /// <summary>
        /// Initialize a new instance of the <see cref="Tournament"/> class.
        /// </summary>
        /// <param name="_TournamentID">The tournament identifier.</param>
        public Tournament(long _TournamentID)
        {
            this.TournamentID   = _TournamentID;
            this.Location       = "Granada, España"; // "Paris, France"
        }

        /// <summary>
        /// Deserialize the specified json.
        /// </summary>
        /// <param name="_Data">The json.</param>
        public void Deserialize(string _Data)
        {
            JObject _Json       = JObject.Parse(_Data);

            this.TournamentID   = _Json["tournament_id"].ToObject<long>();
            this.Location       = _Json["location"].ToObject<string>();
        }

        /// <summary>
        /// Serialize this instance.
        /// </summary>
        /// <returns>The tournament in JSON.</returns>
        public JObject Serialize()
        {
            JObject _JSON = new JObject();

            _JSON.Add("tournament_id", this.TournamentID);
            _JSON.Add("location", this.Location);

            return _JSON;
        }
    }
}