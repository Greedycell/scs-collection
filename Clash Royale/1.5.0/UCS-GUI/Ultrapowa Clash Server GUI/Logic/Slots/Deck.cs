namespace CRS.Logic.Slots
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    using CRS.Core;
    using CRS.Extensions.List;
    using CRS.Files;
    using CRS.Logic.Slots.Items;

    public class Deck : List<Card>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Deck"/> class.
        /// </summary>
        /// <remarks>
        /// Use the default deck, by reading the starting_home.json file.
        /// </remarks>
        public Deck()
        {
            this.Initialize();
        }

        /// <summary>
        /// Agrega la carta especificada a la baraja.
        /// </summary>
        /// <param name="_Card">La carta.</param>
        public new void Add(Card _Card)
        {
            if (this.Contains(_Card))
            {
                int _Index = this.FindIndex(Card => Card.GlobalID == _Card.GlobalID);

                if (_Index > -1)
                {
                    this[_Index].Count += _Card.Count;
                }
                else
                {
                    base.Add(_Card);
                }
            }
            else
            {
                base.Add(_Card);
            }
        }

#pragma warning disable CS0109 // El miembro 'Deck.Add(byte, byte, int, byte, byte)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Add the specified card to the deck.
        /// </summary>
        /// <param name="_Type">The type.</param>
        /// <param name="_ID">The identifier.</param>
        /// <param name="_Count">The count.</param>
        /// <param name="_Level">The level.</param>
        /// <param name="_isNew">The is new.</param>
        public new void Add(byte _Type, byte _ID, int _Count, byte _Level, byte _isNew)
#pragma warning restore CS0109 // El miembro 'Deck.Add(byte, byte, int, byte, byte)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            Card _Card = new Card(_Type, _ID, _Count, _Level, _isNew);

            if (this.Contains(_Card))
            {
                int _Index = this.FindIndex(Card => Card.GlobalID == _Card.GlobalID);

                if (_Index > -1)
                {
                    this[_Index].Count += _Card.Count;
                }
                else
                {
                    base.Add(_Card);
                }
            }
            else
            {
                base.Add(_Card);
            }
        }

        /// <summary>
        /// Invert the specified identifier, with the card at the specified position.
        /// </summary>
        /// <param name="_ID">The identifier.</param>
        /// <param name="_Position">The position.</param>
        public void Invert(int _ID, int _Position)
        {
            Card _Old       = this[_Position];
            this[_Position] = this[_ID + 8];
            this[_ID + 8]   = _Old;
        }

        /// <summary>
        /// Get the player's deck.
        /// </summary>
        /// <returns>The player's deck in byte array.</returns>
        public byte[] ToBytes()
        {
            List<byte> _Packet = new List<byte>();

            foreach (Card _Card in this.GetRange(0, 8))
            {
                _Packet.AddVInt(_Card.Type);    // Card Type
                _Packet.AddVInt(_Card.ID);      // Card ID
                _Packet.AddVInt(_Card.Level);   // Card Level
                _Packet.AddVInt(0);             // Unknown
                _Packet.AddVInt(_Card.Count);   // Card Count
                _Packet.AddVInt(0);             // Unknown
                _Packet.AddVInt(0);             // Unknown
                _Packet.AddVInt(_Card.New);     // New Card = 2
            }

            return _Packet.ToArray();
        }

        public byte[] Hand()
        {
            List<byte> _Packet = new List<byte>();

            foreach (Card _Card in this.GetRange(0, 8).OrderBy(_Card => ResourcesManager.Random.Next()))
            {
                _Packet.AddVInt(_Card.Type);    // Card Type
                _Packet.AddVInt(_Card.ID);      // Card ID
                _Packet.AddVInt(_Card.Level);   // Card Level
            }

            return _Packet.ToArray();
        }

        /// <summary>
        /// Deserialize the specified json.
        /// </summary>
        /// <param name="_JSON">The JSON.</param>
        public void Deserialize(string _JSON)
        {
            JObject _Object = JObject.Parse(_JSON);
            JArray _Deck = (JArray) _Object["decks"];

            this.Clear();

            foreach (JToken _JToken in _Deck)
            {
                Card _Card = new Card((JObject)_JToken);
                this.Add(_Card);
            }
        }

        /// <summary>
        /// Serialize this instance.
        /// </summary>
        /// <returns>The JSON.</returns>
        public JObject Serialize()
        {
            JObject _JSON   = new JObject();
            JArray _Deck = new JArray();

            this.ForEach(_Card =>
            {
                _Deck.Add(_Card.Serialize());
            });

            _JSON.Add("decks", _Deck);
            return _JSON;
        }

        /// <summary>
        /// Initialize this instance by setting the default deck.
        /// </summary>
        public void Initialize()
        {
            this.Deserialize(Home.Starting_Home);
        }
    }
}
