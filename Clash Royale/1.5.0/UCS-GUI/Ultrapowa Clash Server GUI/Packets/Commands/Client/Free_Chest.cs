namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;
    using CRS.Network;
    using CRS.Packets.Commands.Server;
    using CRS.Packets;

    #endregion

    internal class Free_Chest : Command
    {
        public int Chest_ID = 0;

        public int Tick;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Free_Chest" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Free_Chest(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Free_Chest.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();
            Debug.Write("Tick: " + this.Tick);
        }

        /// <summary>
        ///     Processe this instance.
        /// </summary>
        public override void Process()
        {
            PacketManager.Send(new Server_Commands(this.Client) { _Command = PacketManager.Handle(new Buy_Chest_Callback(this.Client) { ChestID = 7 }) });
        }
    }
}