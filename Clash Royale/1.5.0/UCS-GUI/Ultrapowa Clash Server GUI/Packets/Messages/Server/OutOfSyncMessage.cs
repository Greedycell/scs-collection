namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    //Packet 24104
    internal class OutOfSyncMessage : Message
    {
        public const ushort PacketID = 24104;

        public OutOfSyncMessage(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        public override void Encode()
        {
            this.Writer.AddInt(0);
            this.Writer.AddInt(0);
            this.Writer.AddInt(0);
        }
    }
}