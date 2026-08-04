using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UCS.Helpers;
using UCS.Logic;
using UCS.PacketProcessing;

namespace UCS.Core
{
	// Token: 0x020000B9 RID: 185
	internal class ResourcesManager : IDisposable
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x000169BC File Offset: 0x00014BBC
		public ResourcesManager()
		{
			ResourcesManager.m_vDatabase = new DatabaseManager();
			ResourcesManager.m_vClients = new ConcurrentDictionary<long, Client>();
			ResourcesManager.m_vOnlinePlayers = new List<Level>();
			ResourcesManager.m_vInMemoryLevels = new ConcurrentDictionary<long, Level>();
			this.m_vTimerCanceled = false;
			global::System.Threading.Timer timer = new global::System.Threading.Timer(new TimerCallback(this.ReleaseOrphans), null, 40000, 30000);
			this.TimerReference = timer;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00016A22 File Offset: 0x00014C22
		public void Dispose()
		{
			if (this.m_vTimerCanceled)
			{
				this.TimerReference.Dispose();
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00016A38 File Offset: 0x00014C38
		public static void AddClient(Client c, string IP)
		{
			long num = c.Socket.Handle.ToInt64();
			c.CIPAddress = IP;
			if (!ResourcesManager.m_vClients.ContainsKey(num))
			{
				ResourcesManager.m_vClients.TryAdd(num, c);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00016A7C File Offset: 0x00014C7C
		public static void DropClient(long socketHandle)
		{
			try
			{
				Client client;
				ResourcesManager.m_vClients.TryRemove(socketHandle, out client);
				if (client.GetLevel() != null)
				{
					ResourcesManager.LogPlayerOut(client.GetLevel());
				}
			}
			catch (Exception ex)
			{
				Debugger.WriteLine("[CRS]    Error dropping client: ", ex, 4);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00016ACC File Offset: 0x00014CCC
		public static Client GetClient(long socketHandle)
		{
			return ResourcesManager.m_vClients[socketHandle];
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00016AD9 File Offset: 0x00014CD9
		public static List<long> GetAllPlayerIds()
		{
			return ResourcesManager.m_vDatabase.GetAllPlayerIds();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00016AE5 File Offset: 0x00014CE5
		public static List<Client> GetConnectedClients()
		{
			List<Client> list = new List<Client>();
			list.AddRange(ResourcesManager.m_vClients.Values);
			return list;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00016AFC File Offset: 0x00014CFC
		public static List<Level> GetInMemoryLevels()
		{
			List<Level> list = new List<Level>();
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				list.AddRange(ResourcesManager.m_vInMemoryLevels.Values);
			}
			return list;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00016B4C File Offset: 0x00014D4C
		public static List<Level> GetOnlinePlayers()
		{
			List<Level> list = new List<Level>();
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				list = ResourcesManager.m_vOnlinePlayers.ToList<Level>();
			}
			return list;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00016B98 File Offset: 0x00014D98
		public static Level GetPlayer(long id, bool persistent = false)
		{
			Level level = ResourcesManager.GetInMemoryPlayer(id);
			if (level == null)
			{
				level = ResourcesManager.m_vDatabase.GetAccount(id);
				if (persistent)
				{
					ResourcesManager.LoadLevel(level);
				}
			}
			return level;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00016BC5 File Offset: 0x00014DC5
		public static bool IsClientConnected(long socketHandle)
		{
			return ResourcesManager.m_vClients.ContainsKey(socketHandle);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00016BD2 File Offset: 0x00014DD2
		public static bool IsPlayerOnline(Level l)
		{
			return ResourcesManager.m_vOnlinePlayers.Contains(l);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00016BE0 File Offset: 0x00014DE0
		public static void LoadLevel(Level level)
		{
			long id = level.GetPlayerAvatar().GetId();
			if (!ResourcesManager.m_vInMemoryLevels.ContainsKey(id))
			{
				ResourcesManager.m_vInMemoryLevels.TryAdd(id, level);
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00016C14 File Offset: 0x00014E14
		public static void LogPlayerIn(Level level, Client client)
		{
			level.SetClient(client);
			client.SetLevel(level);
			level.SetIPAddress(client.CIPAddress);
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				if (!ResourcesManager.m_vOnlinePlayers.Contains(level))
				{
					ResourcesManager.m_vOnlinePlayers.Add(level);
					ResourcesManager.LoadLevel(level);
				}
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00016C88 File Offset: 0x00014E88
		public static void LogPlayerOut(Level level)
		{
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				ResourcesManager.m_vOnlinePlayers.Remove(level);
			}
			DatabaseManager.Singelton.Save(level);
			ResourcesManager.m_vInMemoryLevels.TryRemove(level.GetPlayerAvatar().GetId());
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00016CF0 File Offset: 0x00014EF0
		private static Level GetInMemoryPlayer(long id)
		{
			Level level = null;
			object vOnlinePlayersLock = ResourcesManager.m_vOnlinePlayersLock;
			lock (vOnlinePlayersLock)
			{
				if (ResourcesManager.m_vInMemoryLevels.ContainsKey(id))
				{
					level = ResourcesManager.m_vInMemoryLevels[id];
				}
			}
			return level;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00016A22 File Offset: 0x00014C22
		private void ReleaseOrphans(object state)
		{
			if (this.m_vTimerCanceled)
			{
				this.TimerReference.Dispose();
			}
		}

		// Token: 0x0400031B RID: 795
		private static readonly object m_vOnlinePlayersLock = new object();

		// Token: 0x0400031C RID: 796
		private static ConcurrentDictionary<long, Client> m_vClients;

		// Token: 0x0400031D RID: 797
		private static DatabaseManager m_vDatabase;

		// Token: 0x0400031E RID: 798
		private static ConcurrentDictionary<long, Level> m_vInMemoryLevels;

		// Token: 0x0400031F RID: 799
		private static List<Level> m_vOnlinePlayers;

		// Token: 0x04000320 RID: 800
		private readonly bool m_vTimerCanceled;

		// Token: 0x04000321 RID: 801
		private readonly global::System.Threading.Timer TimerReference;
	}
}
