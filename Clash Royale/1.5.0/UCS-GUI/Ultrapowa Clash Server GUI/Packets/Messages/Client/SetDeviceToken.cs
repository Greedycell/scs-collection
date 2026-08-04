namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;

    #endregion

    internal class SetDeviceToken : Message
    {
        public const ushort PacketID = 10113;

        public string Token = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="SetDeviceToken" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public SetDeviceToken(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Get_Device_Token.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.Token = this.Reader.ReadString();

                // Client.Level.SetPass(this.Password);
                Debug.Write("Token: " + this.Token);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}