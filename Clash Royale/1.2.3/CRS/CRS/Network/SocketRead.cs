using System;
using System.Net.Sockets;

namespace UCS.Network
{
	// Token: 0x02000024 RID: 36
	public class SocketRead
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		private SocketRead(Socket socket, SocketRead.IncomingReadHandler readHandler, SocketRead.IncomingReadErrorHandler errorHandler = null)
		{
			this.Socket = socket;
			this.readHandler = readHandler;
			this.errorHandler = errorHandler;
			this.BeginReceive();
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000C31B File Offset: 0x0000A51B
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000C323 File Offset: 0x0000A523
		public Socket Socket { get; set; }

		// Token: 0x0600015E RID: 350 RVA: 0x0000C32C File Offset: 0x0000A52C
		public static SocketRead Begin(Socket socket, SocketRead.IncomingReadHandler readHandler, SocketRead.IncomingReadErrorHandler errorHandler = null)
		{
			return new SocketRead(socket, readHandler, errorHandler);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000C336 File Offset: 0x0000A536
		private void BeginReceive()
		{
			this.Socket.BeginReceive(this.buffer, 0, 256, SocketFlags.None, new AsyncCallback(this.OnReceive), this);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000C360 File Offset: 0x0000A560
		private void OnReceive(IAsyncResult result)
		{
			try
			{
				if (result.IsCompleted)
				{
					int num = this.Socket.EndReceive(result);
					if (num > 0)
					{
						byte[] array = new byte[num];
						Array.Copy(this.buffer, 0, array, 0, num);
						this.readHandler(this, array);
						SocketRead.Begin(this.Socket, this.readHandler, this.errorHandler);
					}
				}
			}
			catch (Exception ex)
			{
				if (this.errorHandler != null)
				{
					this.errorHandler(this, ex);
				}
			}
		}

		// Token: 0x04000159 RID: 345
		public const int kBufferSize = 256;

		// Token: 0x0400015A RID: 346
		private readonly byte[] buffer = new byte[256];

		// Token: 0x0400015B RID: 347
		private readonly SocketRead.IncomingReadErrorHandler errorHandler;

		// Token: 0x0400015C RID: 348
		private readonly SocketRead.IncomingReadHandler readHandler;

		// Token: 0x020000D0 RID: 208
		// (Invoke) Token: 0x060005DE RID: 1502
		public delegate void IncomingReadErrorHandler(SocketRead read, Exception exception);

		// Token: 0x020000D1 RID: 209
		// (Invoke) Token: 0x060005E2 RID: 1506
		public delegate void IncomingReadHandler(SocketRead read, byte[] data);
	}
}
