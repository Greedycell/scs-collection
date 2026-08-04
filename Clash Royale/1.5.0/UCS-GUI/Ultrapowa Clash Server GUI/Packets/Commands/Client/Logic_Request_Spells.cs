namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    class Logic_Request_Spells : Command
    {
        public int Count;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Logic_Request_Spells" />
        ///     class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Logic_Request_Spells(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Logic_Request_Spells.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            // 01-01-00
            // 01
            // 7F-8F-80-80-80-08
            this.Reader.ReadBytes(3);

            this.Count = this.Reader.Read();
            Debug.Write("Count: " + this.Count);
            for (int _Index = 0; _Index < this.Count; _Index++)
            {
                this.Reader.Read();
                this.Reader.Read();
                this.Reader.ReadVInt();
            }
        }
    }
}