namespace CRS.Core.Network.TCP
{
    #region Usings

    using System;
    using System.Linq;
    using System.Net.Sockets;

    using Packets;

    #endregion

    internal delegate void ProcessData(SocketAsyncEventArgs _AsyncEvent);

    internal sealed class Token : IDisposable
    {
        internal Socket _Connection = null;

        private int _Index = 0;

        private Message _Message = null;

        internal Token(Socket _Connection)
        {
            this._Connection = _Connection;
        }

        public void Dispose()
        {
            try
            {
                this._Connection.Shutdown(SocketShutdown.Send);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
            finally
            {
                this._Connection.Close();
            }
        }

        internal void ProcessData(SocketAsyncEventArgs _AsyncEvent)
        {
            ResourcesManager.Devices[this._Connection.Handle].Process(this._Index);

            this._Index = 0;
        }

        internal void SetData(SocketAsyncEventArgs _AsyncEvent)
        {
            this._Index += _AsyncEvent.BytesTransferred;
            ResourcesManager.Devices[this._Connection.Handle].Stream.AddRange(
                                                                              _AsyncEvent.Buffer.Take(
                                                                                                      _AsyncEvent
                                                                                                          .BytesTransferred));
        }
    }
}