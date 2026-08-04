namespace CRS.Packets
{
    #region Usings

    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class BindGoogleServiceAccount : Message
    {
        public const ushort PacketID = 14262;

        /// <summary>
        ///     Initialize a new instance of the <see cref="BindGoogleServiceAccount" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public BindGoogleServiceAccount(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Device_Information.
            
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Device_Already_Bound(this.Client));
        }
    }
}