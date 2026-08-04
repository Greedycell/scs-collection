namespace CRS.Core.Network.TCP
{
    #region Usings

    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using Core.Settings;

    using Extensions.Binary;

    #endregion

    internal sealed class Server
    {
        public static int _ConnectedSockets;

        private readonly Semaphore _AcceptedClients;

        private readonly int _BufferSize;

        private readonly Socket _Listener;

        private readonly int _MaxConnection;

        private readonly Mutex _Mutex = new Mutex();

        private readonly SocketAsyncEventArgsPool _ReadWritePool;

        private readonly SocketAsyncEventArgsPool _WritePool;

        /// <summary>
        /// Initialize a new instance of the <see cref="Server" /> class.
        /// </summary>
        internal Server()
        {
            this._MaxConnection = Constants.MaxDevices;
            this._BufferSize = Constants.MaxBuffer;

            this._ReadWritePool = new SocketAsyncEventArgsPool(this._MaxConnection);
            this._WritePool = new SocketAsyncEventArgsPool(this._MaxConnection);

            this._AcceptedClients = new Semaphore(this._MaxConnection, this._MaxConnection);

            for (int i = 0; i < this._MaxConnection; i++)
            {
                SocketAsyncEventArgs _ReadWriteEvent = new SocketAsyncEventArgs();

                _ReadWriteEvent.Completed += this.OnIOCompleted;
                _ReadWriteEvent.SetBuffer(new byte[this._BufferSize], 0, this._BufferSize);

                this._ReadWritePool.Push(_ReadWriteEvent);

                /* ----------------------------------------------------------- */
                SocketAsyncEventArgs _WriteEvent = new SocketAsyncEventArgs();

                _WriteEvent.Completed += this.OnIOCompleted;
                _WriteEvent.SetBuffer(new byte[this._BufferSize], 0, this._BufferSize);

                this._WritePool.Push(_WriteEvent);
            }

            IPAddress[] _Addresses = Dns.GetHostEntry(Environment.MachineName).AddressList;
            IPEndPoint _LocalEP = new IPEndPoint(_Addresses[_Addresses.Length - 1], Constants.ServerPort);

            this._Listener = new Socket(_LocalEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            this._Listener.ReceiveBufferSize = this._BufferSize;
            this._Listener.SendBufferSize = this._BufferSize;

            if (Constants.Local)
            {
                this._Listener.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), Constants.ServerPort));
            }
            else
            {
                if (_LocalEP.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    this._Listener.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
                    this._Listener.Bind(new IPEndPoint(IPAddress.IPv6Any, _LocalEP.Port));
                }
                else
                {
                    this._Listener.Bind(_LocalEP);
                }
            }

            this._Listener.Listen(this._MaxConnection);

            Console.WriteLine("Server is listening on " + IPAddress.Any + ":" + _LocalEP.Port + ", let's play !\n");

            this.StartAccept(null);
            this._Mutex.WaitOne();
        }

        public void Dispose()
        {
            this._Listener.Dispose();
            this._ReadWritePool.Dispose();

            // TODO : Disconnect everyone before disposing the Semaphore. this._AcceptedClients.Dispose();
            this._Mutex.Dispose();
        }

        /// <summary>
        /// <see cref="Stop"/> this instance.
        /// </summary>
        internal void Stop()
        {
            this._Listener.Close();
            this._Mutex.ReleaseMutex();
        }

        private void CloseClientSocket(SocketAsyncEventArgs _AsyncEvent)
        {
            Token _Token = _AsyncEvent.UserToken as Token;
            this.CloseClientSocket(_Token, _AsyncEvent);
        }

        private void CloseClientSocket(Token _Token, SocketAsyncEventArgs _AsyncEvent)
        {
            ResourcesManager.DropClient(_Token._Connection.Handle.ToInt64());
            _Token.Dispose();
            this._AcceptedClients.Release();
            Interlocked.Decrement(ref _ConnectedSockets);
            this._ReadWritePool.Push(_AsyncEvent);
        }

        private void OnAcceptCompleted(object _Sender, SocketAsyncEventArgs _AsyncEvent)
        {
            this.ProcessAccept(_AsyncEvent);
        }

        private void OnIOCompleted(object _Sender, SocketAsyncEventArgs _AsyncEvent)
        {
            switch (_AsyncEvent.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                {
                    this.ProcessReceive(_AsyncEvent);
                    break;
                }

                case SocketAsyncOperation.Send:
                {
                    this.ProcessSend(_AsyncEvent);
                    break;
                }

                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        private void ProcessAccept(SocketAsyncEventArgs _AsyncEvent)
        {
            Socket _Socket = _AsyncEvent.AcceptSocket;
            if (_Socket.Connected)
            {
                try
                {
                    SocketAsyncEventArgs _ReadEvent = this._ReadWritePool.Pop();

                    if (_ReadEvent != null)
                    {
                        _ReadEvent.UserToken = new Token(_Socket);

                        Interlocked.Increment(ref _ConnectedSockets);
                        ResourcesManager.AddClient(_Socket, ((IPEndPoint)_Socket.RemoteEndPoint).Address.ToString());

                        if (!_Socket.ReceiveAsync(_ReadEvent))
                        {
                            this.ProcessReceive(_ReadEvent);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no more available sockets to allocate.");
                    }
                }
                catch (SocketException ex)
                {
                    Token _Token = _AsyncEvent.UserToken as Token;
                    Console.WriteLine("Error when processing data received from " + _Token._Connection.RemoteEndPoint
                                      + ":\r\n" + ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                this.StartAccept(_AsyncEvent);
            }
        }

        private void ProcessError(SocketAsyncEventArgs _AsyncEvent)
        {
            Token _Token = _AsyncEvent.UserToken as Token;
            IPEndPoint _LocalEP = _Token._Connection.LocalEndPoint as IPEndPoint;

            this.CloseClientSocket(_Token, _AsyncEvent);
            ResourcesManager.DropClient(_Token._Connection.Handle.ToInt64());

            Console.WriteLine("Socket error " + _AsyncEvent.SocketError + " on endpoint " + _LocalEP + " during "
                              + _AsyncEvent.LastOperation);
        }

        private void ProcessReceive(SocketAsyncEventArgs _AsyncEvent)
        {
            if (_AsyncEvent.BytesTransferred > 0)
            {
                if (_AsyncEvent.SocketError == SocketError.Success)
                {
                    Token _Token = _AsyncEvent.UserToken as Token;
                    _Token.SetData(_AsyncEvent);

                    Socket _Socket = _Token._Connection;
                    if (_Socket.Available == 0)
                    {
                        _Token.ProcessData(_AsyncEvent);

                        if (!_Socket.SendAsync(_AsyncEvent))
                        {
                            this.ProcessSend(_AsyncEvent);
                        }
                    }
                    else if (!_Socket.ReceiveAsync(_AsyncEvent))
                    {
                        Console.WriteLine("Process Receive Again !");
                        this.ProcessReceive(_AsyncEvent);
                    }
                }
                else
                {
                    this.ProcessError(_AsyncEvent);
                }
            }
            else
            {
                this.CloseClientSocket(_AsyncEvent);
            }
        }

        private void ProcessSend(SocketAsyncEventArgs _AsyncEvent)
        {
            if (_AsyncEvent.SocketError == SocketError.Success)
            {
                Token _Token = _AsyncEvent.UserToken as Token;

                if (!_Token._Connection.ReceiveAsync(_AsyncEvent))
                {
                    this.ProcessReceive(_AsyncEvent);
                }
            }
            else
            {
                this.ProcessError(_AsyncEvent);
            }
        }

        private void StartAccept(SocketAsyncEventArgs _AcceptEvent)
        {
            if (_AcceptEvent == null)
            {
                _AcceptEvent = new SocketAsyncEventArgs();
                _AcceptEvent.Completed += this.OnAcceptCompleted;
            }
            else
            {
                _AcceptEvent.AcceptSocket = null;
            }

            this._AcceptedClients.WaitOne();

            if (!this._Listener.AcceptAsync(_AcceptEvent))
            {
                this.ProcessAccept(_AcceptEvent);
            }
        }
    }
}