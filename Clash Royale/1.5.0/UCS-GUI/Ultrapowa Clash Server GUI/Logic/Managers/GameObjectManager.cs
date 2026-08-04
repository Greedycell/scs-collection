namespace CRS.Logic
{
    #region Usings

    using Newtonsoft.Json.Linq;

    using CRS.Logic.Slots.Items;

    #endregion

    /// <summary>
    ///     The game object manager.
    /// </summary>
    internal class GameObjectManager
    {
        /// <summary>
        ///     The player.
        /// </summary>
        private static Level player;

        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="GameObjectManager" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        public GameObjectManager()
        {
            // Objects.
        }

        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="GameObjectManager" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        /// <param name="l">The l.</param>
        public GameObjectManager(Level l)
        {
            player = l;
        }

        /// <summary>
        ///     The tick.
        /// </summary>
        public void Tick()
        {

        }

        public JObject Save()
        {
            JObject json = new JObject();

            JArray deck = new JArray();

            player.GetPlayerAvatar().Deck.ForEach(card => { deck.Add(card.Serialize()); });

            json.Add("decks", deck);
            return json;
        }

        public void Load(JObject jsonObject)
        {
            JArray deck = (JArray)jsonObject["decks"];

            foreach (JToken jToken in deck)
            {
                Card card = new Card((JObject)jToken);
                player.GetPlayerAvatar().Deck.Add(card);
            }
        }
    }
}