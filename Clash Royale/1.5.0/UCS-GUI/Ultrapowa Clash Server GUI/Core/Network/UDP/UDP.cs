namespace CRS.Core.Network.UDP
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    internal class UDP
    {
        public UDPServer _Server = null;
        public UDPClient _Client = null;

        public UDP()
        {
            this._Server = new UDPServer();
            
            Task.Factory.StartNew(async () => 
            {
                while (true)
                {
                    Received _Packet = await this._Server.Receive();
                    Console.WriteLine("UDP Packet : " + BitConverter.ToString(_Packet.Message) + " | "
                                      + Encoding.UTF8.GetString(_Packet.Message) + " [" + _Packet.Sender.Address + "]");

                    // this._Server.Reply(_Packet.Message, _Packet.Sender);
                }
            });

            /* UDPClient.ConnectTo(Constants.ServerAddr, Constants.ServerPort);

            Task.Factory.StartNew(async () => 
            {
                while (true)
                {
                    Received _Packet = await this._Client.Receive();
                    Console.WriteLine(_Packet.Message);
                }
            }); */
        }
    }
}