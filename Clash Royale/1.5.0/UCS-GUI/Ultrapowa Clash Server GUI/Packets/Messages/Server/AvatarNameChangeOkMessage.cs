namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    // Packet 24111
    internal class AvatarNameChangeOkMessage : Message
    {
        public const ushort PacketID = 24111;

        private string m_vAvatarName;

        private int m_vServerCommandType;

        public AvatarNameChangeOkMessage(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);

            this.m_vServerCommandType = 0x03;
            this.m_vAvatarName = "JJBreaker";
        }

        public string GetAvatarName()
        {
            return this.m_vAvatarName;
        }

        public void SetAvatarName(string name)
        {
            this.m_vAvatarName = name;
        }

        public override void Encode()
        {
            this.Writer.AddInt(this.m_vServerCommandType);
            this.Writer.AddString(this.m_vAvatarName);
            this.Writer.AddInt(1);
            this.Writer.AddInt(-1);
        }
    }
}