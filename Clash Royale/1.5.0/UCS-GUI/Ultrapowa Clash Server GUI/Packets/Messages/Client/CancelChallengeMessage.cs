namespace CRS.Packets
{
    #region Usings

    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class CancelChallengeMessage : Message
    {
        public const ushort PacketID = 14111;

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="CancelChallengeMessage" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public CancelChallengeMessage(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Cancel_Challenge.
            
        }

        /// <summary>
        ///     <see cref="CancelChallengeMessage.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            PacketManager.Send(new Cancel_Battle_OK(this.Client));
        }
    }
}