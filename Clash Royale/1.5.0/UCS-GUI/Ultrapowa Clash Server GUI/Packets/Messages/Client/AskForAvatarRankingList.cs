namespace CRS.Packets
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class AskForAvatarRankingList : Message
    {
        public const ushort PacketID = 14403;

        /// <summary>
        ///     Initialize a new instance of the <see cref="AskForAvatarRankingList" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public AskForAvatarRankingList(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Top_Global_Players.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Top_Global_Players_Data(this.Client));
            //PacketManager.Send(new Profile_Data(this.Client));
        }
    }
}