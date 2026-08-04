namespace CRS.Logic
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;

    using CRS.Core;
    using CRS.Extensions;
    using CRS.Extensions.Binary;
    using CRS.Logic.Enums;
    using CRS.Packets;

    #endregion

    internal class Device
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="Device" /> class.
        /// </summary>
        /// <param name="_Socket">The socket.</param>
        public Device(Socket _Socket)
        {
            this.Connection = _Socket;
            this.m_vSocketHandle = _Socket.Handle.ToInt64();
            this.State = State.DISCONNECTED;
            this.Stream = new List<byte>();
            this.PublicKey = new byte[32];
            this.RNonce = new byte[24];
            this.SNonce = new byte[24];
        }
        private readonly long m_vSocketHandle;
        public Socket Connection { get; set; }

        public byte[] PublicKey { get; set; }

        public byte[] SharedKey { get; set; }
        public string CIPAddress {  get;  set; }

        private Level m_vLevel;

        public void SetLevel(Level l)
        {
            this.m_vLevel = l;
        }

        public Level GetLevel()
        {
            return this.m_vLevel;
        }
        public long GetSocketHandle()
        {
            return this.m_vSocketHandle;
        }
        public byte[] RNonce { get; set; }

        public byte[] SNonce { get; set; }

        public State State { get; set; }

        public List<byte> Stream { get; set; }

        public int Errors { get; set; }

        public int Ping { get; set; }

        public string Interface { get; set; }

        /// <summary>
        ///     <see cref="Process" /> the data in the specified range.
        /// </summary>
        /// <param name="_Range">The data range.</param>
        /// <exception cref="TargetInvocationException">
        ///     El constructor que se llama inicia una excepción.
        /// </exception>
        public void Process(int _Range)
        {
            int[] _Header = new int[3];

            if (_Range >= 7)
            {
                using (Reader _Reader = new Reader(this.Stream.ToArray()))
                {
                    _Header[0] = _Reader.ReadUInt16(); // ID
                    _Reader.BaseStream.Seek(1, SeekOrigin.Current);

                    _Header[1] = _Reader.ReadUInt16(); // Length
                    _Header[2] = _Reader.ReadUInt16(); // Version
                    //Debug.Write("_Header[0]: " +_Header[0]);
                    //Debug.Write("_Header[1]: " + _Header[1]);
                    //Debug.Write("_Header[2]: " + _Header[2]);
                    if (this.Stream.Count - 7 >= _Header[1])
                    {
                        if (MessageFactory.m_vMessages.ContainsKey(_Header[0]))
                        {
                            Message _Message =
                                Activator.CreateInstance(MessageFactory.m_vMessages[_Header[0]], this, _Reader, _Header)
                                    as Message;
                            Debug.Write("Instancia: " + _Header[0] + " Creanda");
                            if (_Message != null)
                            {
                                if (Core.Settings.Settings.Debug)
                                {
                                    Debug.Write(
                                        ConsolePad.Padding(
                                            ((IPEndPoint)this.Connection.RemoteEndPoint).Address.ToString()) + " -> "
                                        + ConsolePad.Padding(_Message.GetType().Name)
                                        + " -> SERVER                  -> " + this.Interface);
                                }

                                try
                                {
                                    if (_Header[0] != 10108 && _Header[0] != 10107)
                                    {
                                        Debug.Write(
                                            "Decrypt: " + _Header[0] + " " + _Message.GetType().Name + "  Iniciado...");
                                    }
                                    _Message.Decrypt();
                                    if (_Header[0] != 10108 && _Header[0] != 10107)
                                    {
                                        Debug.Write(
                                            "Decrypt: " + _Header[0] + " " + _Message.GetType().Name + "  terminado");
                                    }
                                    if (_Header[0] != 10108 && _Header[0] != 10107)
                                    {
                                        Debug.Write(
                                            "Decode: " + _Header[0] + " " + _Message.GetType().Name + "  Iniciado...");
                                    }

                                    _Message.Decode();
                                    if (_Header[0] != 10108 && _Header[0] != 10107)
                                    {
                                        Debug.Write(
                                            "Decode: " + _Header[0] + " " + _Message.GetType().Name + "  terminado");
                                    }
                                    if (_Header[0] != 10108 && _Header[0] != 10107)
                                    {
                                        Debug.Write(
                                            "Process: " + _Header[0] + " " + _Message.GetType().Name + "  Iniciado...");
                                    }

                                    _Message.Process(_Message.Client.GetLevel());
                                    if (_Header[0] != 10108 && _Header[0] != 10107)
                                    {
                                        Debug.Write("Process: " + _Header[0] + "  terminado");
                                    }
                                }
                                catch (Exception _Error)
                                {
                                    Debug.Write("The player " + (string.IsNullOrEmpty(_Message.Client.GetLevel().GetPlayerAvatar().GetAvatarName()) ? "with ID " + _Message.Client.GetLevel().GetPlayerAvatar().GetId() : "with name '" + _Message.Client.GetLevel().GetPlayerAvatar().GetAvatarName()) + " throwed an exception." + _Error);
                                    Debug.Write(Debug.FlattenException(_Error));
                                    if (this.SNonce != null)
                                    {
                                        Debug.Write("this.SNonce != null");
                                        if (this.State >= State.LOGGED)
                                        {
                                            Debug.Write("this.State >= State.LOGGED" + this.State);
                                            // this.SNonce.Increment();
                                        }
                                        else
                                        {
                                            Debug.Write("Login Failed");
                                            // Login Failed.
                                        }
                                    }
                                    else
                                    {
                                        Debug.Write("SNonce=Null");
                                        if (this.Errors > 5)
                                        {
                                            Debug.Write("this.Errors: " + this.Errors);
                                            ResourcesManager.DropClient(this.Connection.Handle.ToInt64());
                                        }
                                        else
                                        {
                                            this.Errors = this.Errors + 1;
                                            Debug.Write("this.Errors: " + this.Errors);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Debug.Write("Message is null!!");
                            }
                        }
                        else
                        {
                            Debug.Write("Message Factory no contain: " + _Header[0] + " can't be processed.");
                            this.SNonce.Increment();
                            Debug.Write("The message " + _Header[0] + " can't be processed.");
                        }

                        this.Stream.RemoveRange(0, _Header[1] + 7);
                    }
                    else
                    {
                        Debug.Write("this.Stream.Count - 7 >= _Header[1] = false");
                    }
                }
            }
            else
            {
                Debug.Write("_Range > 7 NADA QUE HACER!");
            }
        }
    }
}