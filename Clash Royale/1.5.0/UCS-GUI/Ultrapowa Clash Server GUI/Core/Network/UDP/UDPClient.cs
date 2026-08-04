namespace CRS.Core.Network.UDP
{
    class UDPClient : UDPBase
    {
        private UDPClient()
        {
            // UDP Client.
        }

        public static UDPClient ConnectTo(string _Address, int _Port)
        {
            UDPClient _Socket = new UDPClient();
            _Socket._Client.Connect(_Address, _Port);
            return _Socket;
        }

        public void Send(byte[] _Packet)
        {
            this._Client.Send(_Packet, _Packet.Length);
        }
    }
}