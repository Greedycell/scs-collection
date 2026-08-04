namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class InboxOpened : Message
    {
        public const ushort PacketID = 10905;

        /// <summary>
        ///     Initialize a new instance of the <see cref="InboxOpened" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public InboxOpened(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Check_Inbox.
        }

        /// <summary>
        ///     <see cref="InboxOpened.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Inbox_Data(this.Client));
        }
    }
}