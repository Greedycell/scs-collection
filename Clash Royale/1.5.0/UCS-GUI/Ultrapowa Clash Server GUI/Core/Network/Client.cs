namespace UCS.Core.Network
{
    #region Usings

    using System;
    using System.Net.Sockets;
    using System.Threading;

    using Settings;

    #endregion Usings

    internal class Client : IDisposable
    {
        private static Socket _Socket;
        private Thread _Thread;

        public Client()
        {
            this._Thread = new Thread(() =>
            {
                _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _Socket.Connect(Constants.ServerAddr, Constants.ServerPort);

                Console.WriteLine("The Client class has been initialized.");
            });
            this._Thread.Start();
        }

        public static void Send()
        {
            if ((_Socket != null) && _Socket.Connected)
            {
                _Socket.Send(new byte[1024]); 
            }
        }

        public void Dispose()
        {
            _Socket.Dispose();
            this._Thread.Abort();
        }
    }
}