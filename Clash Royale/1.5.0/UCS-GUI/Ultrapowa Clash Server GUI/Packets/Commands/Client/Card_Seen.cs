namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    internal class Card_Seen : Command
    {
        private int CardID;

        private string CardName;

        private string CardRarity;

        private int tickStart;

        private int tickEnd;

        private long accountId;

        private byte testbyte;

        private int Type;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Card_Seen" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Card_Seen(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Card_Seen.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.tickStart = this.Reader.ReadVInt();
            this.tickEnd = this.Reader.ReadVInt();
            this.accountId = this.Reader.ReadInt16(); //ReadInt16
            var ReadVInt = this.Reader.ReadVInt();
            this.Type = this.Reader.ReadVInt();
            this.CardID = this.Reader.ReadVInt();
            var test = this.Reader.ReadVInt();
            var test2 = this.Reader.ReadVInt();

            //if (this.Type == 26)
            //{
            //    Spells_Characters _Card =
            //        ObjectManager.DataTables.GetTable((int)Gamefile.Spells_Characters).GetItemById(this.ID) as
            //            Spells_Characters;

            //    Rarities _Rarity =
            //        ObjectManager.DataTables.GetTable((int)Gamefile.Rarities).GetDataByName(_Card.Rarity) as Rarities;
            //    this.CardName = _Card.Name;
            //    this.CardRarity = _Rarity.Name;
            //}
            //else if (this.Type == 27)
            //{
            //    Spells_Buildings _Card = ObjectManager.DataTables.GetTable((int)Gamefile.Spells_Buildings).GetItemById(this.ID) as Spells_Buildings;
            //    Rarities _Rarity = ObjectManager.DataTables.GetTable((int)Gamefile.Rarities).GetDataByName(_Card.Rarity) as Rarities;
            //    this.CardName = _Card.Name;
            //    this.CardRarity = _Rarity.Name;
            //}
            //else if (this.Type == 28)
            //{
            //    Spells_Other _Card = ObjectManager.DataTables.GetTable((int)Gamefile.Spells_Other).GetItemById(this.ID) as Spells_Other;
            //    Rarities _Rarity = ObjectManager.DataTables.GetTable((int)Gamefile.Rarities).GetDataByName(_Card.Rarity) as Rarities;
            //    this.CardName = _Card.Name;
            //    this.CardRarity = _Rarity.Name;
            //}

            Debug.Write("tickStart: " + this.tickStart);
            Debug.Write("tickEnd: " + this.tickEnd);
            Debug.Write("accountId: " + this.accountId);
            Debug.Write("ReadVInt: " + ReadVInt);
            Debug.Write("Type: " + this.Type + " CardRarity: " + this.CardRarity);
            Debug.Write("CardID: " + this.CardID + " CardName: " + this.CardName);
            Debug.Write("test: " + test);
            Debug.Write("test2: " + test2);
        }

        public override void Process()
        {
            int _Index =
                this.Client.GetLevel()
                    .GetPlayerAvatar()
                    .Deck.FindIndex(_Card => _Card.Type == this.Type && _Card.ID == this.CardID);
            this.Client.GetLevel().GetPlayerAvatar().Deck[_Index].New = 0;
        }
    }
}