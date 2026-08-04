namespace CRS.Network
{
    #region Usings

    using System;
    using System.Net.Sockets;

    #endregion

    public class SocketRead
    {
        public delegate void IncomingReadHandler(SocketRead read, byte[] data);

        public delegate void IncomingReadErrorHandler(SocketRead read, Exception exception);

        public const int kBufferSize = 256;

        private Socket socket;

        private IncomingReadHandler readHandler;

        private IncomingReadErrorHandler errorHandler;

        private byte[] buffer = new byte[kBufferSize];

        public Socket Socket
        {
            get
            {
                return this.socket;
            }
        }

        private SocketRead(Socket socket, IncomingReadHandler readHandler, IncomingReadErrorHandler errorHandler = null)
        {
            this.socket = socket;
            this.readHandler = readHandler;
            this.errorHandler = errorHandler;

            this.BeginReceive();
        }

        private void BeginReceive()
        {
            this.socket.BeginReceive(this.buffer, 0, kBufferSize, SocketFlags.None, this.OnReceive, this);
        }

        public static SocketRead Begin(
            Socket socket,
            IncomingReadHandler readHandler,
            IncomingReadErrorHandler errorHandler = null)
        {
            return new SocketRead(socket, readHandler, errorHandler);
        }

        private void OnReceive(IAsyncResult result)
        {
            try
            {
                if (result.IsCompleted)
                {
                    int bytesRead = this.socket.EndReceive(result);

                    if (bytesRead > 0)
                    {
                        byte[] read = new byte[bytesRead];
                        Array.Copy(this.buffer, 0, read, 0, bytesRead);

                        this.readHandler(this, read);
                        Begin(this.socket, this.readHandler, this.errorHandler);
                    }
                }
            }
            catch (Exception e)
            {
                if (this.errorHandler != null)
                {
                    this.errorHandler(this, e);
                }
            }
        }
    }
}