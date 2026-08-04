namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class VisitHome : Message
    {
        public const ushort PacketID = 14113;

        public long UserID;

        /// <summary>
        ///     Initialize a new instance of the <see cref="VisitHome" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public VisitHome(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Ask_Profile.
            
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.UserID = this.Reader.ReadInt64();
                Debug.Write("UserID: " + this.UserID);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Profile_Data(this.Client) { UserID = this.UserID });
        }
    }
}