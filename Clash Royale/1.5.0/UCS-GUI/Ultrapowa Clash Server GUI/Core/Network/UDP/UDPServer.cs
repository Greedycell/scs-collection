namespace CRS.Core.Network.UDP
{
    #region Usings

    using System.Net;
    using System.Net.Sockets;

    using CRS.Core.Settings;

    #endregion

    internal class UDPServer : UDPBase
    {
        private readonly IPEndPoint _Endpoint = null;

        public UDPServer() : this(new IPEndPoint(IPAddress.Any, Constants.ServerPort))
        {
            // UDP Server.
        }

        public UDPServer(IPEndPoint _EP)
        {
            this._Endpoint  = _EP;
            this._Client    = new UdpClient(this._Endpoint);
        }

        public void Reply(byte[] _Packet, IPEndPoint _EP)
        {
            this._Client.Send(_Packet, _Packet.Length, _EP);
        }
    }
}