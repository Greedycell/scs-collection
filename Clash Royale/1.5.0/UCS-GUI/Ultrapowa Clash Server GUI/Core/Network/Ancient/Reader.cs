namespace CRS.Core.Network.Ancient
{
    #region Usings

    using System;
    using System.Net.Sockets;

    using Logic;

    using Settings;

    #endregion

    public class Reader
    {
        public Socket Socket = null;

        private readonly byte[] Buffer = new byte[Constants.MaxBuffer];

        private readonly IncomingReadHandler RHandler;

        /// <summary>
        /// Initialize a new instance of the <see cref="Reader" /> class.
        /// </summary>
        /// <param name="_Socket">The socket.</param>
        /// <param name="_RHandler">The read handler.</param>
        public Reader(Socket _Socket, IncomingReadHandler _RHandler)
        {
            this.Socket = _Socket;
            this.RHandler = _RHandler;
            this.Socket.BeginReceive(this.Buffer, 0, Constants.MaxBuffer, 0, this.OnReceive, this);
        }

        public delegate void IncomingReadHandler(Reader _Read, byte[] _Data);

        private void OnReceive(IAsyncResult _Ar)
        {
            try
            {
                SocketError _Status = 0;
                int _Readed = this.Socket.EndReceive(_Ar, out _Status);

                if (_Status == SocketError.Success && _Readed > 0)
                {
                    byte[] _Read = new byte[_Readed];
                    Array.Copy(this.Buffer, 0, _Read, 0, _Readed);
                    this.RHandler(this, _Read);
                    this.Socket.BeginReceive(this.Buffer, 0, Constants.MaxBuffer, 0, this.OnReceive, this);
                }
            }
            catch (Exception)
            {
                //long socketHandle = this.Socket.Handle.ToInt64();
                Device _Device = ResourcesManager.Devices[this.Socket.Handle];

                if (_Device != null)
                {
                    _Device.Errors = _Device.Errors + 1;

                    if (_Device.GetLevel() != null)
                    {
                        Debug.Write("The player " + _Device.GetLevel().GetPlayerAvatar().GetId() + " ["
                                    + _Device.GetLevel().GetPlayerAvatar().GetAvatarName() + "] throwed an exception.");
                    }
                    else
                    {
                        Debug.Write("The player with IP " + this.Socket.RemoteEndPoint + " throwed an exception.");
                    }
                }
                else
                {
                    Debug.Write("The player with IP " + this.Socket.RemoteEndPoint + " throwed an exception.");
                }
            }
        }
    }
}