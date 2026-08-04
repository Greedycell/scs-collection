namespace CRS.Packets
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class AskForAvatarStream : Message
    {
        public const ushort PacketID = 14405;

        /// <summary>
        ///     Initialize a new instance of the <see cref="AskForAvatarStream" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public AskForAvatarStream(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Avatar_Stream.
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
            
        }
    }
}