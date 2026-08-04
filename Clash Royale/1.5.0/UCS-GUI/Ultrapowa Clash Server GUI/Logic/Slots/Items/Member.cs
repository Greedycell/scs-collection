namespace CRS.Logic.Slots.Items
{
    #region Usings

    using Newtonsoft.Json.Linq;

    using CRS.Logic.Enums;

    #endregion Usings

    internal class Member
    {
        public long PlayerID    = 0;

        public Role Role        = Role.Member;

        /// <summary>
        /// Initialize a new instance of the <see cref="Member"/> class.
        /// </summary>
        public Member()
        {
            // Member.
        }

        public Member(ClientAvatar _Player)
        {
            this.PlayerID = _Player.GetId();
        }

        /// <summary>
        /// Deserialize the specified json.
        /// </summary>
        /// <param name="_Json">The json.</param>
        public void Deserialize(JObject _Json)
        {
            this.PlayerID   = _Json["player_id"].ToObject<long>();
            this.Role       = (Role) _Json["role"].ToObject<int>();
        }

        /// <summary>
        /// Serialize this instance into a JSON.
        /// </summary>
        /// <returns>The JSON.</returns>
        public JObject Serialize()
        {
            JObject _Json = new JObject();

            _Json.Add("player_id", this.PlayerID);
            _Json.Add("role", (int) this.Role);

            return _Json;
        }
    }
}