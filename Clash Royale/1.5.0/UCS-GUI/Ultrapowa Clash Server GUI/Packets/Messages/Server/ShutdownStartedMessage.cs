namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    // Packet 20161
    internal class ShutdownStartedMessage : Message
    {
        public const ushort PacketID = 20161;

        private int m_vCode;

        public ShutdownStartedMessage(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        public override void Encode()
        {
            
            this.Writer.AddInt(this.m_vCode);
            
        }

        public void SetCode(int code)
        {
            this.m_vCode = code;
        }
    }
}