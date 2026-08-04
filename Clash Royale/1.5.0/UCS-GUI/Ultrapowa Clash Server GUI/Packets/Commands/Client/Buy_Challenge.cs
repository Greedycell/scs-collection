namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    internal class Buy_Challenge : Command
    {
        public int Tick;

        public int Type;

        public int Mode;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Buy_Challenge" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Buy_Challenge(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Buy_Challenge.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            this.Type = this.Reader.ReadVInt();
            this.Mode = this.Reader.ReadVInt();
        }
    }
}