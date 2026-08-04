using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CRS.Core.Threading;
using CRS.Helpers;
using CRS.Logic;
using CRS.PacketProcessing;

namespace CRS.Core
{
	// Token: 0x020000DC RID: 220
	internal class ResourcesManager : IDisposable
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0001E6F4 File Offset: 0x0001C8F4
		public ResourcesManager()
		{
			ResourcesManager.m_vDatabase = new DatabaseManager();
			ResourcesManager.m_vClients = new ConcurrentDictionary<long, Device>();
			ResourcesManager.m_vOnlinePlayers = new List<Level>();
			ResourcesManager.m_vInMemoryLevels = new ConcurrentDictionary<long, Level>();
			ResourcesManager.m_vWaitingLevels = new ConcurrentDictionary<long, Level>();
			ResourcesManager.m_vBattleList = new List<KeyValuePair<Device, Device>>();
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001E744 File Offset: 0x0001C944
		public static void AddClient(Device c)
		{
			long num = c.Socket.Handle.ToInt64();
			ConcurrentDictionary<long, Device> vClients = ResourcesManager.m_vClients;
			lock (vClients)
			{
				if (!ResourcesManager.m_vClients.ContainsKey(num))
				{
					ResourcesManager.m_vClients.TryAdd(num, c);
				}
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001E7AC File Offset: 0x0001C9AC
		public static List<Level> GetAllWaitingLevels()
		{
			List<Level> list = new List<Level>();
			ConcurrentDictionary<long, Level> vWaitingLevels = ResourcesManager.m_vWaitingLevels;
			lock (vWaitingLevels)
			{
				list.AddRange(ResourcesManager.m_vWaitingLevels.Values);
			}
			return list;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001E7FC File Offset: 0x0001C9FC
		public static KeyValuePair<long, Level> GetRandomWaitingLevel()
		{
			KeyValuePair<long, Level> keyValuePair = default(KeyValuePair<long, Level>);
			ConcurrentDictionary<long, Level> vWaitingLevels = ResourcesManager.m_vWaitingLevels;
			lock (vWaitingLevels)
			{
				keyValuePair = ResourcesManager.m_vWaitingLevels.First<KeyValuePair<long, Level>>();
				ResourcesManager.m_vWaitingLevels.TryRemove(keyValuePair.Key);
			}
			return keyValuePair;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001E85C File Offset: 0x0001CA5C
		public static void AddWaitingLevel(Level level)
		{
			ConcurrentDictionary<long, Level> vWaitingLevels = ResourcesManager.m_vWaitingLevels;
			lock (vWaitingLevels)
			{
				ResourcesManager.m_vWaitingLevels.TryAdd(level.GetPlayerAvatar().GetId(), level);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001E8AC File Offset: 0x0001CAAC
		public static void DropWaitingLevel(long key)
		{
			ConcurrentDictionary<long, Level> vWaitingLevels = ResourcesManager.m_vWaitingLevels;
			lock (vWaitingLevels)
			{
				ResourcesManager.m_vWaitingLevels.TryRemove(key);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
		public static void DropBattle(int index)
		{
			List<KeyValuePair<Device, Device>> vBattleList = ResourcesManager.m_vBattleList;
			lock (vBattleList)
			{
				ResourcesManager.m_vBattleList.RemoveAt(index);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001E938 File Offset: 0x0001CB38
		public static void AddBattle(Device Cl1, Device Cl2)
		{
			List<KeyValuePair<Device, Device>> vBattleList = ResourcesManager.m_vBattleList;
			lock (vBattleList)
			{
				ResourcesManager.m_vBattleList.Add(new KeyValuePair<Device, Device>(Cl1, Cl2));
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001E984 File Offset: 0x0001CB84
		public static KeyValuePair<Device, Device> GetBattle(Device key)
		{
			List<KeyValuePair<Device, Device>> vBattleList = ResourcesManager.m_vBattleList;
			KeyValuePair<Device, Device> keyValuePair2;
			lock (vBattleList)
			{
				KeyValuePair<Device, Device> keyValuePair = ResourcesManager.m_vBattleList.Find((KeyValuePair<Device, Device> pair) => pair.Key == key);
				if (keyValuePair.Key != null)
				{
					keyValuePair2 = keyValuePair;
				}
				else
				{
					keyValuePair2 = ResourcesManager.m_vBattleList.Find((KeyValuePair<Device, Device> pair) => pair.Value == key);
				}
			}
			return keyValuePair2;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001EA0C File Offset: 0x0001CC0C
		public static Device GetBattleFiltered(EndPoint edp)
		{
			List<KeyValuePair<Device, Device>> vBattleList = ResourcesManager.m_vBattleList;
			Device client;
			lock (vBattleList)
			{
				client = ResourcesManager.m_vBattleList.Find((KeyValuePair<Device, Device> pair) => pair.Key.CIPAddress == edp.ToString()).Value ?? ResourcesManager.m_vBattleList.Find((KeyValuePair<Device, Device> pair) => pair.Value.CIPAddress == edp.ToString()).Key;
			}
			return client;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001EA98 File Offset: 0x0001CC98
		public static List<KeyValuePair<Device, Device>> GetAllBattles()
		{
			List<KeyValuePair<Device, Device>> vBattleList = ResourcesManager.m_vBattleList;
			List<KeyValuePair<Device, Device>> vBattleList2;
			lock (vBattleList)
			{
				vBattleList2 = ResourcesManager.m_vBattleList;
			}
			return vBattleList2;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
		public static void DropClient(long socketHandle)
		{
			try
			{
				ConcurrentDictionary<long, Device> vClients = ResourcesManager.m_vClients;
				Device client;
				lock (vClients)
				{
					ResourcesManager.m_vClients.TryRemove(socketHandle, out client);
				}
				if (client.GetLevel() != null)
				{
					ResourcesManager.LogPlayerOut(client.GetLevel());
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001EB44 File Offset: 0x0001CD44
		public static List<long> GetAllPlayerIds()
		{
			DatabaseManager vDatabase = ResourcesManager.m_vDatabase;
			List<long> allPlayerIds;
			lock (vDatabase)
			{
				allPlayerIds = ResourcesManager.m_vDatabase.GetAllPlayerIds();
			}
			return allPlayerIds;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001EB8C File Offset: 0x0001CD8C
		public static Device GetClient(long socketHandle)
		{
			ConcurrentDictionary<long, Device> vClients = ResourcesManager.m_vClients;
			lock (vClients)
			{
				if (ResourcesManager.m_vClients.ContainsKey(socketHandle))
				{
					return ResourcesManager.m_vClients[socketHandle];
				}
			}
			return null;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public static List<Device> GetConnectedClients()
		{
			ConcurrentDictionary<long, Device> vClients = ResourcesManager.m_vClients;
			List<Device> list;
			lock (vClients)
			{
				list = ResourcesManager.m_vClients.Values.ToList<Device>();
			}
			return list;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001EC30 File Offset: 0x0001CE30
		public static void GetAllPlayersFromDB()
		{
			ConcurrentDictionary<long, Level> concurrentDictionary = new ConcurrentDictionary<long, Level>();
			DatabaseManager vDatabase = ResourcesManager.m_vDatabase;
			lock (vDatabase)
			{
				concurrentDictionary = ResourcesManager.m_vDatabase.GetAllPlayers();
			}
			foreach (KeyValuePair<long, Level> keyValuePair in concurrentDictionary)
			{
				ResourcesManager.m_vInMemoryLevels.TryAdd(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		public static List<Level> GetInMemoryLevels()
		{
			ConcurrentDictionary<long, Level> vInMemoryLevels = ResourcesManager.m_vInMemoryLevels;
			List<Level> list;
			lock (vInMemoryLevels)
			{
				list = ResourcesManager.m_vInMemoryLevels.Values.ToList<Level>();
			}
			return list;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001ED10 File Offset: 0x0001CF10
		public static List<Level> GetOnlinePlayers()
		{
			List<Level> vOnlinePlayers = ResourcesManager.m_vOnlinePlayers;
			List<Level> list;
			lock (vOnlinePlayers)
			{
				list = ResourcesManager.m_vOnlinePlayers.ToList<Level>();
			}
			return list;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001ED58 File Offset: 0x0001CF58
		public static Level GetPlayer(long id, bool persistent = false)
		{
			Level level = ResourcesManager.GetInMemoryPlayer(id);
			if (level == null)
			{
				DatabaseManager vDatabase = ResourcesManager.m_vDatabase;
				lock (vDatabase)
				{
					level = ResourcesManager.m_vDatabase.GetAccount(id);
				}
				if (persistent)
				{
					ResourcesManager.LoadLevel(level);
				}
			}
			return level;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		public static bool IsClientConnected(long socketHandle)
		{
			ConcurrentDictionary<long, Device> vClients = ResourcesManager.m_vClients;
			lock (vClients)
			{
				if (ResourcesManager.m_vClients.ContainsKey(socketHandle))
				{
					return ResourcesManager.m_vClients[socketHandle].IsClientSocketConnected();
				}
			}
			return false;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001EE10 File Offset: 0x0001D010
		public static bool IsPlayerOnline(Level l)
		{
			List<Level> vOnlinePlayers = ResourcesManager.m_vOnlinePlayers;
			bool flag2;
			lock (vOnlinePlayers)
			{
				flag2 = ResourcesManager.m_vOnlinePlayers.Contains(l);
			}
			return flag2;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001EE58 File Offset: 0x0001D058
		public static void LoadLevel(Level level)
		{
			long id = level.GetPlayerAvatar().GetId();
			if (!ResourcesManager.m_vInMemoryLevels.ContainsKey(id))
			{
				ResourcesManager.m_vInMemoryLevels.TryAdd(id, level);
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001EE8C File Offset: 0x0001D08C
		public static void LogPlayerIn(Level l, Device c)
		{
			l.SetClient(c);
			c.SetLevel(l);
			l.SetIPAddress(c.CIPAddress);
			List<Level> vOnlinePlayers = ResourcesManager.m_vOnlinePlayers;
			lock (vOnlinePlayers)
			{
				if (!ResourcesManager.m_vOnlinePlayers.Contains(l))
				{
					ResourcesManager.m_vOnlinePlayers.Add(l);
					ResourcesManager.LoadLevel(l);
				}
			}
			ConsoleThread.TitleU();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001EF04 File Offset: 0x0001D104
		public static void LogPlayerOut(Level level)
		{
			DatabaseManager.Singelton.Save(level);
			List<Level> vOnlinePlayers = ResourcesManager.m_vOnlinePlayers;
			lock (vOnlinePlayers)
			{
				ResourcesManager.m_vOnlinePlayers.Remove(level);
			}
			ConcurrentDictionary<long, Level> vInMemoryLevels = ResourcesManager.m_vInMemoryLevels;
			lock (vInMemoryLevels)
			{
				ResourcesManager.m_vInMemoryLevels.TryRemove(level.GetPlayerAvatar().GetId());
			}
			ConsoleThread.TitleD();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000123B6 File Offset: 0x000105B6
		public void Dispose()
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001EF98 File Offset: 0x0001D198
		private static Level GetInMemoryPlayer(long id)
		{
			Level level = null;
			ConcurrentDictionary<long, Level> vInMemoryLevels = ResourcesManager.m_vInMemoryLevels;
			lock (vInMemoryLevels)
			{
				if (ResourcesManager.m_vInMemoryLevels.ContainsKey(id))
				{
					level = ResourcesManager.m_vInMemoryLevels[id];
				}
			}
			return level;
		}

		// Token: 0x040003C1 RID: 961
		private static ConcurrentDictionary<long, Device> m_vClients;

		// Token: 0x040003C2 RID: 962
		private static DatabaseManager m_vDatabase;

		// Token: 0x040003C3 RID: 963
		private static ConcurrentDictionary<long, Level> m_vInMemoryLevels;

		// Token: 0x040003C4 RID: 964
		private static ConcurrentDictionary<long, Level> m_vWaitingLevels;

		// Token: 0x040003C5 RID: 965
		private static List<KeyValuePair<Device, Device>> m_vBattleList;

		// Token: 0x040003C6 RID: 966
		private static List<Level> m_vOnlinePlayers;
	}
}
