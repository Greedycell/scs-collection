namespace UCS.Logic.Managers
{
	#region Usings

	using Slots.Items;

	using Newtonsoft.Json.Linq;

	#endregion

	internal class Objects
	{
		private readonly Level Player = null;

		/// <summary>
		/// Initialize a new instance of the <see cref="Objects"/> class.
		/// </summary>
		public Objects()
		{
			// Objects.
		}

		/// <summary>
		/// Initialize a new instance of the <see cref="Objects"/> class.
		/// </summary>
		/// <param name="_Player">The player.</param>
		public Objects(Level _Player)
		{
			this.Player = _Player;
		}

		/// <summary>
		/// Deserialize the specified json.
		/// </summary>
		/// <param name="_JSON">The json.</param>
		public void Deserialize(JObject _JSON)
		{
			JArray _Deck = (JArray)_JSON["decks"];

			foreach (JToken _JToken in _Deck)
			{
				Card _Card = new Card((JObject)_JToken);
				this.Player.GetPlayerAvatar().Deck.Add(_Card);
			}
		}

		/// <summary>
		/// Serialize this instance.
		/// </summary>
		/// <returns>The objects in JSON.</returns>
		public JObject Serialize()
		{
			JObject _JSON = new JObject();

			JArray _Deck = new JArray();

			this.Player.GetPlayerAvatar().Deck.ForEach(_Card =>
			{
				_Deck.Add(_Card.Serialize());
			});

			_JSON.Add("decks", _Deck);
			return _JSON;
		}
	}
}