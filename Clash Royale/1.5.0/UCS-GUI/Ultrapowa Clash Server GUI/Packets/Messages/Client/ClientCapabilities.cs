namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    #endregion

    internal class ClientCapabilities : Message
    {
        public const ushort PacketID = 10107;

        public ClientCapabilities(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Client_Capabilities.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.Client.Ping = this.Reader.ReadVInt();
                this.Client.Interface = this.Reader.ReadString();
                Debug.Write("Client.Ping: " + this.Client.Ping);
                Debug.Write("Client.Interface: " + this.Client.Interface);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}