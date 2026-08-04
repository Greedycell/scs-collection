namespace CRS.Packets
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class AskForTVContent : Message
    {
        public const ushort PacketID = 14402;

        public int Arena;

        /// <summary>
        ///     Initialize a new instance of the <see cref="InboxOpened" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public AskForTVContent(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Royale_TV.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                var test = this.Reader.ReadVInt();
                this.Arena = this.Reader.ReadVInt();
                Debug.Write("test: " + test);
                Debug.Write("arena: " + this.Arena);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Royale_TV_Data(this.Client) { Arena = this.Arena });
        }
    }
}