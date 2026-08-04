namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class AskForAllianceData : Message
    {
        public const ushort PacketID = 14302;

        public long ClanID;

        /// <summary>
        ///     Initialize a new instance of the <see cref="AskForAllianceData" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        public AskForAllianceData(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            
            // Ask_Clan.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.ClanID = this.Reader.ReadInt64();
                Debug.Write("ClanID: " + this.ClanID);
                Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        public override void Process(Level level)
        {
            PacketManager.Send(new Clan_Data(this.Client) { ClanID = this.ClanID });
        }
    }
}