namespace CRS.Network
{
    #region Usings

    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using CRS.Core;
    using CRS.Core.Settings;
    using CRS.Packets;

    #endregion

    internal class Gateway
    {
        private IPAddress ip;

        private static ManualResetEvent AllDone = new ManualResetEvent(false);

        public Gateway()
        {
            this.ip = Dns.GetHostByName(Dns.GetHostName()).AddressList[0];
        }

        private static Socket m_vServerSocket;

        public IPAddress IP => this.ip ?? (this.ip = (from entry in Dns.GetHostEntry(Dns.GetHostName()).AddressList
                                                      where entry.AddressFamily == AddressFamily.InterNetwork
                                                      select entry).FirstOrDefault());

        public void Start()
        {
            if (this.Host(Constants.ServerPort))
            {
                Debug.Write("Gateway started on port " + Constants.ServerPort);
            }
        }

        private void Disconnect()
        {
            if (m_vServerSocket != null)
            {
                m_vServerSocket.BeginDisconnect(false, this.OnEndHostComplete, m_vServerSocket);
            }
        }

        private void OnClientConnect(IAsyncResult result)
        {
            try
            {
                Socket clientSocket = m_vServerSocket.EndAccept(result);
                Console.WriteLine("Client connected (" + ((IPEndPoint)clientSocket.RemoteEndPoint).Address + ":" + ((IPEndPoint)clientSocket.RemoteEndPoint).Port + ")");
                ResourcesManager.AddClient(new Client(clientSocket));
                SocketRead.Begin(clientSocket, OnReceive, OnReceiveError);
            }
            catch (Exception e)
            {
                Debug.Write("[CRS]    Exception when accepting incoming connection: " + Debug.FlattenException(e));
            }

            try
            {
                m_vServerSocket.BeginAccept(this.OnClientConnect, m_vServerSocket);
            }
            catch (Exception e)
            {
                Debug.Write("Exception when starting new accept process: " + Debug.FlattenException(e));
            }
        }

        private void OnReceive(SocketRead read, byte[] data)
        {
            try
            {
                long socketHandle = read.Socket.Handle.ToInt64();
                Client c = ResourcesManager.GetClient(socketHandle);
                c.DataStream.AddRange(data);

                Message p;
                while (c.TryGetPacket(out p))
                {
                    PacketManager.ProcessIncomingPacket(p);
                }
            }
            catch (Exception ex)
            {
                // Client may not exist anymore
                // Debug.Write(Debug.FlattenException(ex));
            }
        }

        private void OnReceiveError(SocketRead read, Exception exception)
        {
            Debug.Write("Error received: "+ exception);
            Debug.Write(Debug.FlattenException(exception));
        }

        private void OnEndHostComplete(IAsyncResult result)
        {
            m_vServerSocket = null;
        }

        public bool Host(int port)
        {
            m_vServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                m_vServerSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                m_vServerSocket.Listen(Constants.kHostConnectionBacklog);
                m_vServerSocket.BeginAccept(this.OnClientConnect, m_vServerSocket);
            }
            catch (Exception e)
            {
                Debug.Write("Exception when attempting to host (" + port + "): " + Debug.FlattenException(e));

                m_vServerSocket = null;

                return false;
            }

            return true;
        }
    }
}