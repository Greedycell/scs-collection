namespace CRS.Network
{
    #region Usings

    using System;
    using System.Collections.Concurrent;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using CRS.Core;
    using CRS.Core.Settings;
    using CRS.Logic;
    using CRS.Packets;

    #endregion

    internal class PacketManager : IDisposable
    {
        private delegate void IncomingProcessingDelegate();
        private static EventWaitHandle m_vIncomingWaitHandle = new AutoResetEvent(false);
        private delegate void OutgoingProcessingDelegate();
        private static EventWaitHandle m_vOutgoingWaitHandle = new AutoResetEvent(false);

        private static ConcurrentQueue<Message> m_vIncomingPackets;
        private static ConcurrentQueue<Message> m_vOutgoingPackets;

        private bool m_vIsRunning;

        public PacketManager()
        {
            m_vIncomingPackets = new ConcurrentQueue<Message>();
            m_vOutgoingPackets = new ConcurrentQueue<Message>();

            m_vIsRunning = false;
        }
        public void Dispose()
        {
            PacketManager.m_vIncomingWaitHandle.Dispose();
            GC.SuppressFinalize(this);
            PacketManager.m_vOutgoingWaitHandle.Dispose();
        }

        public void Start()
        {
            IncomingProcessingDelegate incomingProcessing = new IncomingProcessingDelegate(IncomingProcessing);
            incomingProcessing.BeginInvoke(null, null);

            OutgoingProcessingDelegate outgoingProcessing = new OutgoingProcessingDelegate(OutgoingProcessing);
            outgoingProcessing.BeginInvoke(null, null);

            m_vIsRunning = true;

            Console.WriteLine("Packet Manager started");
        }

        private void IncomingProcessing()
        {
            while (this.m_vIsRunning)
            {
                m_vIncomingWaitHandle.WaitOne();
                Message p;
                while (m_vIncomingPackets.TryDequeue(out p))
                {
                    //p.Client.Decrypt(p.GetData());
                    p.GetData();
                    //Console.WriteLine("R " + p.GetMessageType().ToString() + " (" + p.GetLength().ToString() + ")");
                    Debug.Write("R " + p.GetMessageType().ToString() + " (" + p.GetLength().ToString() + ")");
                    Logger.WriteLine(p, "R");
                    MessageManager.ProcessPacket(p);
                }
            }
        }

        public static void ProcessIncomingPacket(Message p)
        {
            m_vIncomingPackets.Enqueue(p);
            m_vIncomingWaitHandle.Set();
        }

        private void OutgoingProcessing()
        {
            while (this.m_vIsRunning)
            {
                m_vOutgoingWaitHandle.WaitOne();
                Message p;
                while (m_vOutgoingPackets.TryDequeue(out p))
                {
                    Logger.WriteLine(p, "S");
                    //p.GetData();
                    try
                    {
                        if (p.Client.Socket != null)
                        {
                            p.Client.Socket.Send(p.GetRawData());
                        }
                        else
                        {
                            ResourcesManager.DropClient(p.Client.GetSocketHandle());
                        }
                    }
                    catch (Exception)
                    {
                        //example: client connection closed
                        try
                        {
                            ResourcesManager.DropClient(p.Client.GetSocketHandle());
                            p.Client.Socket.Shutdown(SocketShutdown.Both);
                            p.Client.Socket.Close();
                        }
                        catch (Exception e)
                        {
                            //Console.WriteLine(exs.ToString());
                            Debug.Write(Debug.FlattenException(e));
                        }
                    }
                }
            }
        }

        public static void Send(Message p)
        {
            p.Encode();
            p.Encrypt();
            try
            {
                Level pl = p.Client.GetLevel();
                string player = "";
                if (pl != null)
                    player += " (" + pl.GetPlayerAvatar().GetId() + ", " + pl.GetPlayerAvatar().GetName() + ")";
                Debugger.WriteLine("[S] " + p.GetMessageType() + " " + p.GetType().Name + player);
                m_vOutgoingPackets.Enqueue(p);
                m_vOutgoingWaitHandle.Set();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            Socket m_vClient = (Socket)ar.AsyncState;
            Message _Message = ar.AsyncState as Message;

            if (Settings.Debug)
            {
                Debug.Write("SERVER -> " + _Message.GetType() + ": "  + _Message.GetType().Name + " -> " + ((IPEndPoint)_Message.Client.Socket.RemoteEndPoint).Address.ToString() + " -> " + _Message.Client.Interface);
            }
        }
        
        public static Command Handle(Command _Command)
        {
            _Command.Encode();
            _Command.Process();

            return _Command;
        }
    }
}