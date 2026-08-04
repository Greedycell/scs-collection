using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using UCS.Logic;
using UCS.Utilities.Sodium;

namespace UCS.PacketProcessing
{
	// Token: 0x02000089 RID: 137
	internal class Client
	{
		// Token: 0x06000392 RID: 914 RVA: 0x00011EB8 File Offset: 0x000100B8
		public Client(Socket so)
		{
			this.Socket = so;
			this.m_vSocketHandle = so.Handle.ToInt64();
			this.DataStream = new List<byte>();
			this.CState = 0;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00011EF8 File Offset: 0x000100F8
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00011F00 File Offset: 0x00010100
		public int ClientSeed { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00011F09 File Offset: 0x00010109
		// (set) Token: 0x06000396 RID: 918 RVA: 0x00011F11 File Offset: 0x00010111
		public byte[] CPublicKey { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00011F1A File Offset: 0x0001011A
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00011F22 File Offset: 0x00010122
		public byte[] CSessionKey { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00011F2B File Offset: 0x0001012B
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00011F33 File Offset: 0x00010133
		public byte[] CSNonce { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00011F3C File Offset: 0x0001013C
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00011F44 File Offset: 0x00010144
		public int CState { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00011F4D File Offset: 0x0001014D
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00011F55 File Offset: 0x00010155
		public string CIPAddress { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00011F5E File Offset: 0x0001015E
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00011F66 File Offset: 0x00010166
		public byte[] CRNonce { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00011F6F File Offset: 0x0001016F
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00011F77 File Offset: 0x00010177
		public byte[] CSharedKey { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00011F80 File Offset: 0x00010180
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00011F88 File Offset: 0x00010188
		public List<byte> DataStream { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00011F91 File Offset: 0x00010191
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00011F99 File Offset: 0x00010199
		public byte[] IncomingPacketsKey { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00011FA2 File Offset: 0x000101A2
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00011FAA File Offset: 0x000101AA
		public byte[] OutgoingPacketsKey { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00011FB3 File Offset: 0x000101B3
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00011FBB File Offset: 0x000101BB
		public Socket Socket { get; set; }

		// Token: 0x060003AB RID: 939 RVA: 0x00011FC4 File Offset: 0x000101C4
		public static ClashKeyPair GenerateKeyPair()
		{
			KeyPair keyPair = PublicKeyBox.GenerateKeyPair();
			return new ClashKeyPair(keyPair.PublicKey, keyPair.PrivateKey);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011FE8 File Offset: 0x000101E8
		public static byte[] GenerateSessionKey()
		{
			return PublicKeyBox.GenerateNonce();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00011FEF File Offset: 0x000101EF
		public Level GetLevel()
		{
			return this.m_vLevel;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011FF7 File Offset: 0x000101F7
		public long GetSocketHandle()
		{
			return this.m_vSocketHandle;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012000 File Offset: 0x00010200
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

		// Token: 0x060003B0 RID: 944 RVA: 0x00012054 File Offset: 0x00010254
		public void SetLevel(Level l)
		{
			this.m_vLevel = l;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012060 File Offset: 0x00010260
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
					object obj = null;
					using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.DataStream.Take(7 + num).ToArray<byte>())))
					{
						obj = MessageFactory.Read(this, binaryReader, (int)num2);
					}
					if (obj != null)
					{
						p = (Message)obj;
						flag = true;
					}
					else
					{
						this.DataStream.Skip(7).Take(num).ToArray<byte>();
					}
					this.DataStream.RemoveRange(0, 7 + num);
				}
			}
			return flag;
		}

		// Token: 0x04000259 RID: 601
		private readonly long m_vSocketHandle;

		// Token: 0x0400025A RID: 602
		private Level m_vLevel;
	}
}
