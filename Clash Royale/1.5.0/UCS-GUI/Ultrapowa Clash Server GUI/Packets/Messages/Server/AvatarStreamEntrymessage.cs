namespace CRS.Packets
{
    #region Usings


    using CRS.Logic;

    #endregion

    //Packet 24412
    internal class AvatarStreamEntryMessage : Message
    {
        public const ushort PacketID = 24412;

        private AvatarStreamEntry m_vAvatarStreamEntry;

        public AvatarStreamEntryMessage(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        //00 00 00 06 
        //00 00 00 00 
        //AB 00 3F FA 
        //00 00 00 15 00 58 4D 94 
        //00 00 00 0C 69 69 69 69 69 69 69 69 69 69 69 69 
        //00 00 00 08 
        //00 00 00 00 
        //00 
        //00 00 00 02 
        //00 00 00 0E 
        //63 6F 72 72 69 67 C3 A9 20 66 61 75 74 65 01 00 00 00 15 00 58 4D 94 00 00 00 00 00 1A 8E 98 00 00 00 0C 74 68 65 20 38 30 30 20 63 6C 75 62 5B 00 09 53
        public override void Encode()
        {
            this.Writer.AddRange(this.m_vAvatarStreamEntry.Encode());
        }

        public void SetAvatarStreamEntry(AvatarStreamEntry entry)
        {
            this.m_vAvatarStreamEntry = entry;
        }
    }
}