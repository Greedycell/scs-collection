namespace CRS.Packets.Messages.Server
{
    using Client = Packets.Client;

    internal class AvatarStream : Message
    {
        public const ushort PacketID = 24411;

        public AvatarStream(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        public override void Encode()
        {
            //this.Writer.AddRange();
        }
    }
}
