namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class KeepAlive : Message
    {
        public const ushort PacketID = 10108;

        /// <summary>
        ///     Initialize a new instance of the <see cref="KeepAlive" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public KeepAlive(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Keep_Alive.
        }

        /// <summary>
        ///     <see cref="KeepAlive.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Keep_Alive_OK(this.Client));
        }
    }
}