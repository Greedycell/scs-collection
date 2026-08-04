namespace CRS.Packets
{
    #region Usings

    using CRS.Helpers;
    using CRS.Logic.Enums;

    #endregion

    internal class ServerHello : Message
    {
        public const ushort PacketID = 20100;

        private byte[] Key = new byte[24];

        /// <summary>
        /// Initialize a new instance of the
        ///     <see cref="ServerHello"/> class.
        /// </summary>
        /// <param name="_Client">
        /// </param>
        public ServerHello(Client _Client) : base(_Client)
        {
            this.SetMessageType(PacketID);
            this.Key = Client.GenerateSessionKey();
            this.Client.State = (int)State.SESSION_OK;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddInt32(this.Key.Length);
            this.Writer.AddRange(this.Key);
            this.SetData(this.Writer.ToArray());
        }

        /// <summary>
        /// Encrypt this instance.
        /// </summary>
        public override void Encrypt()
        {
            this.SetData(this.Writer.ToArray());
        }
    }
}