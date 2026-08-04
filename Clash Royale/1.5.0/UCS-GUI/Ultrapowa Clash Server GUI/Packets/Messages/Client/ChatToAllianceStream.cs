namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;

    #endregion

    internal class ChatToAllianceStream : Message
    {
        public const ushort PacketID = 14315;

        public string Message = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ChatToAllianceStream" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public ChatToAllianceStream(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Chat_Alliance.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.Message = this.Reader.ReadString();
                Debug.Write("Message: " + this.Message);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}