namespace CRS.Core.Network.UDP
{
    #region Usings

    using System.Net.Sockets;
    using System.Threading.Tasks;

    #endregion

    internal abstract class UDPBase
    {
        protected UdpClient _Client;

        protected UDPBase()
        {
            this._Client = new UdpClient();
        }

        public async Task<Received> Receive()
        {
            UdpReceiveResult _Result = await this._Client.ReceiveAsync();
            return new Received
            {
                Message = _Result.Buffer,
                Sender  = _Result.RemoteEndPoint
            };
        }
    }
}