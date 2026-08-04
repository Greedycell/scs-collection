namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;

    #endregion

    internal class KickAllianceMember : Message
    {
        public const ushort PacketID = 14307;

        public long UserID;

        public string Reason = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="KickAllianceMember" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public KickAllianceMember(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Kick_Clan_Member.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.UserID = this.Reader.ReadInt64();
                this.Reason = this.Reader.ReadString();
                Debug.Write("UserID: " + this.UserID);
                Debug.Write("Reason: " + this.Reason);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}