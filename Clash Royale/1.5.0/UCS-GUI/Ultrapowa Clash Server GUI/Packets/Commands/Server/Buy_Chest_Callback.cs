namespace CRS.Packets.Commands.Server
{
    #region Usings

    using CRS.Extensions.Binary;
    using CRS.Extensions.List;
    using CRS.Logic.Slots;
    using CRS.Logic.Slots.Items;

    using Client = CRS.Packets.Client;

    #endregion

    internal class Buy_Chest_Callback : Command
    {
        public const int CommandID = 210;

        public int Tick;

        public int ChestID = 0;

        public int Type = 4;

        public int Gems = 1;

        public int Gold = 1;

        public Deck Cards = new Deck();

        /// <summary>
        ///     Initialize a new instance of the <see cref="Buy_Chest_Callback" />
        ///     class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The packet identifier.</param>
        public Buy_Chest_Callback(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Buy_Chest_Callback.
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="Buy_Chest_Callback" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        public Buy_Chest_Callback(Client _Client)
            : base(_Client)
        {
            this.ID = CommandID;
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Reader.ReadBytes((int)this.Reader.BaseStream.Length - 12);

            // 01-00-00-00-04-02-00
            // 01-00-00-00-04-01-00
            // 01-00-00-32-01-01-00
            // 01-00-00-01-04-02-00
            // 01-00-90-8D-06-04-02-00
            // 01-00-00-04-02-00
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddVInt(1);

            this.Writer.AddVInt(this.Cards.Count);

            foreach (Card _Card in this.Cards)
            {
                this.Writer.AddVInt(_Card.Type);
                this.Writer.AddVInt(_Card.ID);
                this.Writer.AddVInt(_Card.Level);
                this.Writer.AddVInt(0); // Chest Tick
                this.Writer.AddVInt(_Card.Count);
                this.Writer.AddVInt(0);
                this.Writer.AddVInt(0);
                this.Writer.AddVInt(_Card.New);
            }

            this.Writer.AddVInt(0);
            this.Writer.AddVInt(this.Gems);

            this.Writer.AddVInt(0);
            this.Writer.AddVInt(this.Type);
            this.Writer.AddVInt(this.ChestID);

            this.Writer.Add(255);
            this.Writer.Add(255);
            this.Writer.Add(0);
            this.Writer.Add(0);
        }
    }
}