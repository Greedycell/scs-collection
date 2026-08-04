#region Usings



#endregion

namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    internal class Upgrade_Card : Command
    {
        public int CardID;

        public int Tick;

        public int Type;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Upgrade_Card" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Upgrade_Card(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Upgrade_Card.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            this.Type = this.Reader.Read();
            this.CardID = this.Reader.Read();
            Debug.Write("CardID: " + this.CardID);
        }

        /// <summary>
        ///     Processe this instance.
        /// </summary>
        /// <param name="level"></param>
        public override void Process()
        {
            int _Index =
                this.Client.GetLevel()
                    .GetPlayerAvatar()
                    .Deck.FindIndex(_Card => _Card.Type == this.Type && _Card.ID == this.CardID);

            if (_Index > -1)
            {
                this.Client.GetLevel().GetPlayerAvatar().Deck[_Index].Upgrade();
            }
        }
    }
}