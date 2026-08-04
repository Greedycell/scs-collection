namespace CRS.Core
{
    #region Usings

    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    using CRS.Logic;
    using CRS.Packets;

    #endregion

    internal class MessageManager
    {
        private static ConcurrentQueue<Message> m_vPackets;

        private static readonly EventWaitHandle m_vWaitHandle = new AutoResetEvent(false);

        private bool m_vIsRunning;

        private delegate void PacketProcessingDelegate();

        public MessageManager()
        {
            m_vPackets = new ConcurrentQueue<Message>();
            this.m_vIsRunning = false;
        }

        public void Start()
        {
            //PacketProcessingDelegate packetProcessing = this.PacketProcessing;
            //packetProcessing.BeginInvoke(null, null);
            new PacketProcessingDelegate(this.PacketProcessing).BeginInvoke(null, null);

            this.m_vIsRunning = true;

            Console.WriteLine("Message Manager started");
        }

        private void PacketProcessing()
        {
            while (this.m_vIsRunning)
            {
                m_vWaitHandle.WaitOne();

                Message p;
                while (m_vPackets.TryDequeue(out p))
                {
                    Level pl = p.Client.GetLevel();
                    string player = "";
                    if (pl != null) player += " (" + pl.GetPlayerAvatar().GetId() + ", " + pl.GetPlayerAvatar().GetName() + ")";
                    try
                    {
                        Debugger.WriteLine("[R] " + p.GetMessageType() + " " + p.GetType().Name + player);
                        p.Decrypt();
                        p.Decode();
                        p.Process(pl);
                        //Debug.Write("finished processing of message " + p.GetType().Name + player);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Debugger.WriteLine(
                            "An exception occured during processing of message " + p.GetType().Name + player,
                            ex);
                        Console.ResetColor();
                    }
                }
            }
        }

        public static void ProcessPacket(Message p)
        {
            m_vPackets.Enqueue(p);
            m_vWaitHandle.Set();
        }
    }
}