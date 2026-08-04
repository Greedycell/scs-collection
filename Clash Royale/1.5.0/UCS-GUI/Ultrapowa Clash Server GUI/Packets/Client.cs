namespace CRS.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    using CRS.Core;
    using CRS.Core.Settings;
    using CRS.Extensions;
    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Utilities.Sodium;

    #endregion

    internal class Client
    {
        private readonly long m_vSocketHandle;

        private Level m_vLevel;

        public int Ping { get; set; }

        public string Interface { get; set; }

        public int ClientSeed { get; set; }

        public byte[] CPublicKey { get; set; }

        public byte[] CSessionKey { get; set; }

        public byte[] CSNonce { get; set; }

        public int State { get; set; }

        public string CIPAddress { get; set; }

        public byte[] CRNonce { get; set; }

        public byte[] CSharedKey { get; set; }

        public List<byte> DataStream { get; set; }

        public Socket Socket { get; set; }

        public Client(Socket so)
        {
            this.Socket = so;
            this.m_vSocketHandle = so.Handle.ToInt64();
            this.DataStream = new List<byte>();
            this.State = 0;
        }

        public static ClashKeyPair GenerateKeyPair()
        {
            KeyPair keyPair = PublicKeyBox.GenerateKeyPair();
            return new ClashKeyPair(keyPair.PublicKey, keyPair.PrivateKey);
        }

        public static byte[] GenerateSessionKey() => PublicKeyBox.GenerateNonce();

        public Level GetLevel() => this.m_vLevel;

        public long GetSocketHandle()
        {
            return this.m_vSocketHandle;
        }

        public bool IsClientSocketConnected()
        {
            bool result;
            try
            {
                result = (!this.Socket.Poll(1000, SelectMode.SelectRead) || this.Socket.Available != 0)
                         && this.Socket.Connected;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public void SetLevel(Level l)
        {
            this.m_vLevel = l;
        }

        public bool TryGetPacket(out Message p)
        {
            p = null;
            bool result = false;
            int[] _Header = new int[3];
            if (this.DataStream.Count >= 7)
            {
                object obj = null;
                byte[] packet = null;
                using (Reader _Reader = new Reader(this.DataStream.ToArray()))
                {
                    _Header[0] = _Reader.ReadUInt16(); // ID
                    Debug.Write("ID: " + _Header[0]);
                    try
                    {
                        _Reader.BaseStream.Seek(1, SeekOrigin.Current);
                    }
                    catch (IOException ioException)
                    {
                        Debug.Write(Debug.FlattenException(ioException));
                    }

                    _Header[1] = _Reader.ReadUInt16(); // Length
                    Debug.Write("Length: " + _Header[1]);
                    _Header[2] = _Reader.ReadUInt16(); // Version
                    Debug.Write("Version: " + _Header[2]);
                    if (this.DataStream.Count - 7 >= _Header[1])
                    {
                        if (MessageFactory.m_vMessages.ContainsKey(_Header[0]))
                        {
                            packet = this.DataStream.Take(7 + _Header[1]).ToArray();

                            obj = MessageFactory.Read(this, _Reader, _Header[0], _Header);
                        }
                        else
                        {
                            this.CSNonce.Increment();
                        }
                    }

                    try
                    {
                        if (obj != null)
                        {
                            p = (Message)obj;
                            if (Settings.Debug)
                            {
                                Debug.Write(
                                    ((IPEndPoint)this.Socket.RemoteEndPoint).Address + " -> " + p.GetType().Name
                                    + " -> SERVER -> " + this.Interface);
                            }

                            result = true;
                        }
                        else
                        {
                            Debug.Write(_Header[0] + " No existe Length: " + _Header[1]);
                            if (!Directory.Exists("unhandled-message-dumps"))
                            {
                                Directory.CreateDirectory("unhandled-message-dumps");
                            }

                            var time = DateTime.Now;
                            var fileName = $"{time.ToString("yy_MM_dd__hh_mm_ss")} - ({_Header[1]})({_Header[0]}).bin";
                            var path = Path.Combine("unhandled-message-dumps", fileName);
                            if (packet != null)
                            {
                                File.WriteAllBytes(path, packet);
                            }
                            else
                            {

                            }

                            this.CSNonce.Increment();
                        }
                    }
                    catch (Exception e)
                    {
                        //Debug.Write(Debug.FlattenException(e));
                    }

                    this.DataStream.RemoveRange(0, 7 + _Header[1]);
                }
            }
            return result;
        }
    }
}