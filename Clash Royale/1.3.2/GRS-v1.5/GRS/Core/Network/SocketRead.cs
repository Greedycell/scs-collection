using System;
using System.Net.Sockets;

namespace CRS.Core.Network
{
	// Token: 0x020000E2 RID: 226
	public class SocketRead
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00020549 File Offset: 0x0001E749
		private SocketRead(Socket socket, SocketRead.IncomingReadHandler readHandler)
		{
			this._socket = socket;
			this._readHandler = readHandler;
			this.BeginReceive();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00020578 File Offset: 0x0001E778
		private void BeginReceive()
		{
			try
			{
				if ((!this._socket.Poll(1000, SelectMode.SelectRead) || this._socket.Available != 0) && this._socket.Connected)
				{
					this._socket.BeginReceive(this.buffer, 0, 2048, SocketFlags.None, new AsyncCallback(this.OnReceive), this);
				}
			}
			catch (Exception)
			{
				if ((!this._socket.Poll(1000, SelectMode.SelectRead) || this._socket.Available != 0) && this._socket.Connected)
				{
					this._socket.Close();
				}
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00020628 File Offset: 0x0001E828
		public static SocketRead Begin(Socket socket, SocketRead.IncomingReadHandler readHandler)
		{
			return new SocketRead(socket, readHandler);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00020634 File Offset: 0x0001E834
		private void OnReceive(IAsyncResult result)
		{
			try
			{
				SocketError socketError;
				int num = this._socket.EndReceive(result, out socketError);
				if (socketError == SocketError.Success && num > 0)
				{
					byte[] array = new byte[num];
					Array.Copy(this.buffer, 0, array, 0, num);
					this._readHandler(this, array);
					SocketRead.Begin(this._socket, this._readHandler);
				}
			}
			catch (Exception)
			{
				if ((!this._socket.Poll(1000, SelectMode.SelectRead) || this._socket.Available != 0) && this._socket.Connected)
				{
					this._socket.Close();
				}
			}
		}

		// Token: 0x040003DA RID: 986
		public Socket _socket;

		// Token: 0x040003DB RID: 987
		private readonly SocketRead.IncomingReadHandler _readHandler;

		// Token: 0x040003DC RID: 988
		private byte[] buffer = new byte[2048];

		// Token: 0x02000115 RID: 277
		// (Invoke) Token: 0x06000661 RID: 1633
		public delegate void IncomingReadHandler(SocketRead read, byte[] data);
	}
}
