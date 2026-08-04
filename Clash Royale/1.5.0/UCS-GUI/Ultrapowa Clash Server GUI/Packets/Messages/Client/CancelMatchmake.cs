namespace CRS.Packets
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class CancelMatchmake : Message
    {
        public const ushort PacketID = 14107;

        /// <summary>
        ///     Initialize a new instance of the <see cref="CancelMatchmake" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public CancelMatchmake(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Cancel_Battle.
            
        }

        /// <summary>
        ///     <see cref="CancelMatchmake.Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            if (ResourcesManager.Battles.Waiting.Contains(this.Client.GetLevel()))
            {
                ResourcesManager.Battles.Waiting.Remove(this.Client.GetLevel());
                PacketManager.Send(new Cancel_Battle_OK(this.Client));
            }
        }
    }
}