namespace CRS.Core.Network.TCP
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    #endregion Usings

    internal sealed class SocketAsyncEventArgsPool : IDisposable
    {
        private readonly Stack<SocketAsyncEventArgs> _Pool;

        internal SocketAsyncEventArgsPool(int _Capacity)
        {
            this._Pool = new Stack<SocketAsyncEventArgs>(_Capacity);
        }

        public void Dispose()
        {
            this._Pool.Clear();
        }

        internal SocketAsyncEventArgs Pop()
        {
            lock (this._Pool)
            {
                if (this._Pool.Count > 0)
                {
                    return this._Pool.Pop();
                }
                else
                {
                    return null;
                }
            }
        }

        internal void Push(SocketAsyncEventArgs _Item)
        {
            lock (this._Pool)
            {
                this._Pool.Push(_Item);
            }
        }
    }
}