using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRS.Database;
using CRS.Logic;

namespace CRS.Core
{
	// Token: 0x020000DD RID: 221
	internal class DatabaseManager
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x0001EFF0 File Offset: 0x0001D1F0
		public DatabaseManager()
		{
			this.m_vConnectionString = ConfigurationManager.AppSettings["database"];
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001F00D File Offset: 0x0001D20D
		public static DatabaseManager Singelton
		{
			get
			{
				DatabaseManager databaseManager;
				if ((databaseManager = DatabaseManager.singelton) == null)
				{
					databaseManager = (DatabaseManager.singelton = new DatabaseManager());
				}
				return databaseManager;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001F024 File Offset: 0x0001D224
		public void CreateAccount(Level l)
		{
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					ucsdbEntities.player.Add(new player
					{
						PlayerId = l.GetPlayerAvatar().GetId(),
						AccountStatus = l.GetAccountStatus(),
						AccountPrivileges = l.GetAccountPrivileges(),
						LastUpdateTime = l.GetTime(),
						IPAddress = l.GetIPAddress(),
						Avatar = l.GetPlayerAvatar().SaveToJSON(),
						GameObjects = l.SaveToJSON()
					});
					ucsdbEntities.SaveChanges();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		public void CreateAlliance(Alliance a)
		{
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					ucsdbEntities.clan.Add(new clan
					{
						ClanId = a.GetAllianceId(),
						LastUpdateTime = DateTime.Now,
						Data = a.SaveToJSON()
					});
					ucsdbEntities.SaveChanges();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001F164 File Offset: 0x0001D364
		public Level GetAccount(long playerId)
		{
			Level level = null;
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					player player = ucsdbEntities.player.Find(new object[] { playerId });
					if (player != null)
					{
						level = new Level();
						level.SetAccountStatus(player.AccountStatus);
						level.SetAccountPrivileges(player.AccountPrivileges);
						level.SetTime(player.LastUpdateTime);
						level.SetIPAddress(player.IPAddress);
						level.GetPlayerAvatar().LoadFromJSON(player.Avatar);
						level.LoadFromJSON(player.GameObjects);
					}
				}
			}
			catch (Exception)
			{
			}
			return level;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001F220 File Offset: 0x0001D420
		public List<Alliance> GetAllAlliances()
		{
			List<Alliance> list = new List<Alliance>();
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					IEnumerable<clan> clan = ucsdbEntities.clan;
					int num = 0;
					foreach (clan clan2 in clan)
					{
						Alliance alliance = new Alliance();
						alliance.LoadFromJSON(clan2.Data);
						list.Add(alliance);
						if (num++ >= 500)
						{
							break;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001F2D0 File Offset: 0x0001D4D0
		public ConcurrentDictionary<long, Level> GetAllPlayers()
		{
			ConcurrentDictionary<long, Level> concurrentDictionary = new ConcurrentDictionary<long, Level>();
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					IEnumerable<player> player = ucsdbEntities.player;
					int num = 0;
					foreach (player player2 in player)
					{
						Level level = new Level();
						concurrentDictionary.TryAdd(level.GetPlayerAvatar().GetId(), level);
						if (num++ >= 500)
						{
							break;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return concurrentDictionary;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001F37C File Offset: 0x0001D57C
		public Alliance GetAlliance(long allianceId)
		{
			Alliance alliance = null;
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					clan clan = ucsdbEntities.clan.Find(new object[] { allianceId });
					if (clan != null)
					{
						alliance = new Alliance();
						alliance.LoadFromJSON(clan.Data);
					}
				}
			}
			catch (Exception)
			{
			}
			return alliance;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		public List<long> GetAllPlayerIds()
		{
			List<long> list = new List<long>();
			using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
			{
				list.AddRange(ucsdbEntities.player.Select((player p) => p.PlayerId));
			}
			return list;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001F480 File Offset: 0x0001D680
		public long GetMaxAllianceId()
		{
			using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
			{
				var ids = ucsdbEntities.clan
					.Select(alliance => alliance.ClanId)
					.ToList();

				return ids.Count == 0 ? 0L : ids.Max();
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001F534 File Offset: 0x0001D734
		public long GetMaxPlayerId()
		{
			long num = 0L;
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					num = ucsdbEntities.player.Select((player ep) => ((long?)ep.PlayerId) ?? 0L).DefaultIfEmpty<long>().Max<long>();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("[GRS]    An error occured when connecting to the database.");
				Console.WriteLine(e);
				Console.ReadKey();
				Environment.Exit(0);
			}
			return num;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001F610 File Offset: 0x0001D810
		public void RemoveAlliance(Alliance alliance)
		{
			long allianceId = alliance.GetAllianceId();
			using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
			{
				ucsdbEntities.clan.Remove(ucsdbEntities.clan.Find(new object[] { (int)allianceId }));
				ucsdbEntities.SaveChanges();
			}
			ObjectManager.RemoveInMemoryAlliance(allianceId);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001F680 File Offset: 0x0001D880
		public void Save(Alliance alliance)
		{
			using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
			{
				ucsdbEntities.Configuration.AutoDetectChangesEnabled = false;
				ucsdbEntities.Configuration.ValidateOnSaveEnabled = false;
				clan clan = ucsdbEntities.clan.Find(new object[] { (int)alliance.GetAllianceId() });
				if (clan != null)
				{
					clan.LastUpdateTime = DateTime.Now;
					clan.Data = alliance.SaveToJSON();
					ucsdbEntities.Entry<clan>(clan).State = (System.Data.Entity.EntityState)16;
				}
				else
				{
					ucsdbEntities.clan.Add(new clan
					{
						ClanId = alliance.GetAllianceId(),
						LastUpdateTime = DateTime.Now,
						Data = alliance.SaveToJSON()
					});
				}
				ucsdbEntities.SaveChanges();
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001F754 File Offset: 0x0001D954
		public void Save(Level avatar)
		{
			ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString);
			ucsdbEntities.Configuration.AutoDetectChangesEnabled = false;
			ucsdbEntities.Configuration.ValidateOnSaveEnabled = false;
			player player = ucsdbEntities.player.Find(new object[] { avatar.GetPlayerAvatar().GetId() });
			if (player != null)
			{
				player.LastUpdateTime = avatar.GetTime();
				player.AccountStatus = avatar.GetAccountStatus();
				player.AccountPrivileges = avatar.GetAccountPrivileges();
				player.IPAddress = avatar.GetIPAddress();
				player.Avatar = avatar.GetPlayerAvatar().SaveToJSON();
				player.GameObjects = avatar.SaveToJSON();
				ucsdbEntities.Entry<player>(player).State = (System.Data.Entity.EntityState)16;
			}
			else
			{
				ucsdbEntities.player.Add(new player
				{
					PlayerId = avatar.GetPlayerAvatar().GetId(),
					AccountStatus = avatar.GetAccountStatus(),
					AccountPrivileges = avatar.GetAccountPrivileges(),
					LastUpdateTime = avatar.GetTime(),
					IPAddress = avatar.GetIPAddress(),
					Avatar = avatar.GetPlayerAvatar().SaveToJSON(),
					GameObjects = avatar.SaveToJSON()
				});
			}
			ucsdbEntities.SaveChanges();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001F880 File Offset: 0x0001DA80
		public void Save(List<Level> avatars)
		{
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					ucsdbEntities.Configuration.AutoDetectChangesEnabled = false;
					ucsdbEntities.Configuration.ValidateOnSaveEnabled = false;
					int num = 0;
					foreach (Level level in avatars)
					{
						Level level2 = level;
						lock (level2)
						{
							player player = ucsdbEntities.player.Find(new object[] { level.GetPlayerAvatar().GetId() });
							if (player != null)
							{
								player.LastUpdateTime = level.GetTime();
								player.AccountStatus = level.GetAccountStatus();
								player.AccountPrivileges = level.GetAccountPrivileges();
								player.IPAddress = level.GetIPAddress();
								player.Avatar = level.GetPlayerAvatar().SaveToJSON();
								player.GameObjects = level.SaveToJSON();
								ucsdbEntities.Entry<player>(player).State = (System.Data.Entity.EntityState)16;
							}
							else
							{
								ucsdbEntities.player.Add(new player
								{
									PlayerId = level.GetPlayerAvatar().GetId(),
									AccountStatus = level.GetAccountStatus(),
									AccountPrivileges = level.GetAccountPrivileges(),
									LastUpdateTime = level.GetTime(),
									IPAddress = level.GetIPAddress(),
									Avatar = level.GetPlayerAvatar().SaveToJSON(),
									GameObjects = level.SaveToJSON()
								});
							}
						}
					}
					num++;
					if (num >= 500)
					{
						ucsdbEntities.SaveChanges();
						num = 0;
					}
					ucsdbEntities.SaveChanges();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001FA90 File Offset: 0x0001DC90
		public void Save(List<Alliance> alliances)
		{
			try
			{
				using (ucsdbEntities ucsdbEntities = new ucsdbEntities(this.m_vConnectionString))
				{
					ucsdbEntities.Configuration.AutoDetectChangesEnabled = false;
					ucsdbEntities.Configuration.ValidateOnSaveEnabled = false;
					int num = 0;
					foreach (Alliance alliance in alliances)
					{
						Alliance alliance2 = alliance;
						lock (alliance2)
						{
							clan clan = ucsdbEntities.clan.Find(new object[] { (int)alliance.GetAllianceId() });
							if (clan != null)
							{
								clan.LastUpdateTime = DateTime.Now;
								clan.Data = alliance.SaveToJSON();
								ucsdbEntities.Entry<clan>(clan).State = (System.Data.Entity.EntityState)16;
							}
							else
							{
								ucsdbEntities.clan.Add(new clan
								{
									ClanId = alliance.GetAllianceId(),
									LastUpdateTime = DateTime.Now,
									Data = alliance.SaveToJSON()
								});
							}
						}
					}
					num++;
					if (num >= 500)
					{
						ucsdbEntities.SaveChanges();
						ucsdbEntities.SaveChanges();
					}
					ucsdbEntities.SaveChanges();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x040003C7 RID: 967
		private static DatabaseManager singelton;

		// Token: 0x040003C8 RID: 968
		private readonly string m_vConnectionString;
	}
}
