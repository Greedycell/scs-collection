namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;
    using CRS.Logic.Enums;

    #endregion

    internal class Clan_Data : Message
    {
        public const ushort PacketID = 24301;

        public long ClanID = 0;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Clan_Data" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Clan_Data(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            if (this.ClanID > 0)
            {
                
                this.Writer.AddLong(this.ClanID);
                this.Writer.AddString("ClashRoyaleSpain");
                this.Writer.AddVInt(0x10);
                this.Writer.AddVInt(0x10);
                this.Writer.AddVInt(1);
                this.Writer.AddVInt(1);
                this.Writer.AddVInt(0);
                this.Writer.AddVInt(0);
                this.Writer.AddInt(128);
                this.Writer.AddVInt(1);
                this.Writer.AddVInt(0);
                this.Writer.AddRange(new byte[] { 0x82, 0x8A, 0x01, 0x01, 0x00, 0x39, 0x97, 0x01 });
                this.Writer.AddString("ClashRoyaleSpain Official Clan !");

                this.Writer.AddVInt(1);

                this.Writer.AddLong(19);
                this.Writer.AddString(null); // Unknown
                this.Writer.AddString("[ADMIN] JJBreaker"); // Name
                this.Writer.Add(0x36); // Arena Prefix
                this.Writer.AddVInt((int)Arena.ARENA_L); // Arena
                this.Writer.AddVInt(2); // Role
                this.Writer.AddVInt(13); // Level
                this.Writer.AddVInt(5000); // Score
                this.Writer.AddVInt(0); // Donations
                this.Writer.Add(0);
                this.Writer.AddInt(0);
                this.Writer.Add(1);
                this.Writer.AddLong(1);
                
            }
        }
    }
}