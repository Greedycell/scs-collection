using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using CRS.Logic;

namespace CRS.PacketProcessing
{
	// Token: 0x02000069 RID: 105
	internal class Device
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00017960 File Offset: 0x00015B60
		public Device(Socket so)
		{
			this.Socket = so;
			this.m_vSocketHandle = so.Handle.ToInt64();
			this.DataStream = new List<byte>();
			this.CState = 0;
			this.CIPAddress = ((IPEndPoint)so.RemoteEndPoint).Address.ToString();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000179BB File Offset: 0x00015BBB
		// (set) Token: 0x0600030A RID: 778 RVA: 0x000179C3 File Offset: 0x00015BC3
		public string CIPAddress { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000179CC File Offset: 0x00015BCC
		// (set) Token: 0x0600030C RID: 780 RVA: 0x000179D4 File Offset: 0x00015BD4
		public byte[] CPublicKey { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000179DD File Offset: 0x00015BDD
		// (set) Token: 0x0600030E RID: 782 RVA: 0x000179E5 File Offset: 0x00015BE5
		public byte[] CRNonce { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000179EE File Offset: 0x00015BEE
		// (set) Token: 0x06000310 RID: 784 RVA: 0x000179F6 File Offset: 0x00015BF6
		public byte[] CSessionKey { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000179FF File Offset: 0x00015BFF
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00017A07 File Offset: 0x00015C07
		public byte[] CSharedKey { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00017A10 File Offset: 0x00015C10
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00017A18 File Offset: 0x00015C18
		public byte[] CSNonce { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00017A21 File Offset: 0x00015C21
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00017A29 File Offset: 0x00015C29
		public int CState { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00017A32 File Offset: 0x00015C32
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00017A3A File Offset: 0x00015C3A
		public List<byte> DataStream { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00017A43 File Offset: 0x00015C43
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00017A4B File Offset: 0x00015C4B
		public Socket Socket { get; set; }

		// Token: 0x0600031B RID: 795 RVA: 0x00017A54 File Offset: 0x00015C54
		public Level GetLevel()
		{
			return this.m_vLevel;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00017A5C File Offset: 0x00015C5C
		public long GetSocketHandle()
		{
			return this.m_vSocketHandle;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00017A64 File Offset: 0x00015C64
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00017A6C File Offset: 0x00015C6C
		public int ClientSeed { get; set; }

		// Token: 0x0600031F RID: 799 RVA: 0x00017A78 File Offset: 0x00015C78
		public bool IsClientSocketConnected()
		{
			bool flag;
			try
			{
				flag = (!this.Socket.Poll(1000, SelectMode.SelectRead) || this.Socket.Available != 0) && this.Socket.Connected;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00017ACC File Offset: 0x00015CCC
		public void SetLevel(Level l)
		{
			this.m_vLevel = l;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00017AD8 File Offset: 0x00015CD8
		public bool TryGetPacket(out Message p)
		{
			p = null;
			bool flag = false;
			if (this.DataStream.Count<byte>() >= 5)
			{
				int num = 0 | ((int)this.DataStream[2] << 16) | ((int)this.DataStream[3] << 8) | (int)this.DataStream[4];
				ushort num2 = (ushort)(((int)this.DataStream[0] << 8) | (int)this.DataStream[1]);
				if (this.DataStream.Count - 7 >= num)
				{
					object obj;
					using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.DataStream.Take(7 + num).ToArray<byte>())))
					{
						obj = MessageFactory.Read(this, binaryReader, (int)num2);
					}
					if (obj != null)
					{
						p = (Message)obj;
						flag = true;
					}
					this.DataStream.RemoveRange(0, 7 + num);
				}
			}
			return flag;
		}

		// Token: 0x040002A9 RID: 681
		private readonly long m_vSocketHandle;

		// Token: 0x040002AA RID: 682
		private Level m_vLevel;
	}
}
