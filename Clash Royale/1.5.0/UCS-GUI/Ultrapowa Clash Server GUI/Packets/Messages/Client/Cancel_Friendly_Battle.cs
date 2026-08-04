namespace CRS.Packets
{
    #region Usings

    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class Cancel_Friendly_Battle : Message
    {
        public const ushort PacketID = 14423;

        /// <summary>
        ///     Initialize a new instance of the
        ///     <see cref="Cancel_Friendly_Battle" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Cancel_Friendly_Battle(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Cancel_Friendly_Battle.
            
        }

        /// <summary>
        ///     <see cref="Cancel_Friendly_Battle.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Cancel_Battle_OK(this.Client));
        }
    }
}