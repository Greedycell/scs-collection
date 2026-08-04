namespace CRS.Core
{
    #region Usings

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    using CRS.Database;
    using CRS.Files;
    using CRS.Helpers;
    using CRS.Logic;
    using CRS.Logic.Slots;
    using CRS.Packets;

    #endregion

    internal class ResourcesManager : IDisposable
    {
        /// <summary>
        ///     The list of all in memory
        ///     <see cref="CRS.Core.ResourcesManager.Battles" /> s.
        /// </summary>
        public static Battles Battles;

        //public static Devices Devices;
        public static bool IsClientConnected(long socketHandle) => m_vClients[socketHandle] != null && m_vClients[socketHandle].IsClientSocketConnected();
        public static ConcurrentDictionary<long, Client> m_vClients;
        private static ConcurrentDictionary<long, Clan> m_vInMemoryAlliances;
        //public static Players Players = null;

        public static Random Random;

        /// <summary>
        ///     The list of all in memory
        ///     <see cref="CRS.Core.ResourcesManager.Clans" /> s.
        /// </summary>
        private static Clans Clans;

        /// <summary>
        ///     The current class logger, used to write logs using
        ///     <see cref="ResourcesManager" /> tag.
        /// </summary>
        private static DatabaseManager m_vDatabase;

        private static ConcurrentDictionary<long, Level> m_vInMemoryLevels;
        private Home _Home = null;
        private static List<Level> m_vOnlinePlayers;

        private static object m_vOnlinePlayersLock = new object();

        /// <summary>
        ///     The list of all in memory <see cref="CRS.Logic.Tournament" /> s.
        /// </summary>
        private static Tournaments Tournaments;

        private bool m_vTimerCanceled;

        private Timer TimerReference;

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="ResourcesManager" />
        ///     </para>
        ///     <para>class.</para>
        /// </summary>
        public ResourcesManager()
        {
            m_vDatabase = new DatabaseManager();
            m_vClients = new ConcurrentDictionary<long, Client>();
            m_vInMemoryLevels = new ConcurrentDictionary<long, Level>();
            m_vInMemoryAlliances = new ConcurrentDictionary<long, Clan>();
            m_vOnlinePlayers = new List<Level>();
            this._Home = new Home();
            //Devices = new Devices();
            //Players = new Players();
            Clans = new Clans();
            Battles = new Battles();
            Tournaments = new Tournaments();
            Random = new Random();
            
            this.m_vTimerCanceled = false;
            TimerCallback TimerDelegate = this.ReleaseOrphans;
            Timer TimerItem = new Timer(TimerDelegate, null, 60000, 60000);
            this.TimerReference = TimerItem;
            Console.WriteLine("The Resources Manager class has been initialized.");
        }

        /// <summary>
        ///     <see cref="ResourcesManager.Add(CRS.Logic.Clan)" /> the specified
        ///     clan to the in memory <see cref="CRS.Logic.Clan" /> s list.
        /// </summary>
        /// <param name="_Clan">The clan.</param>
        public static void Add(Clan _Clan)
        {
            if (Clans.ContainsKey(_Clan.ClanID))
            {
                Clans[_Clan.ClanID] = _Clan;
            }
            else
            {
                Clans.Add(_Clan.ClanID, _Clan);
            }
        }

        public static bool SocketContainsLevel(long handle)
        {
            if (m_vClients[handle].GetLevel() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RemoveSocketFromServer(long handle)
        {
            Client c;
            Socket s = m_vClients[handle].Socket;
            try
            {
                s.Shutdown(SocketShutdown.Both);
                s.Close();

                m_vClients.TryRemove(handle, out c);
                Debug.Write("Socket handle " + handle + " dropped");
            }
            catch (Exception e)
            {
                Debug.Write("Dropping Socket failed: " + Debug.FlattenException(e));
            }
        }

        public static void AddClient(Client c)
        {
            long socketHandle = c.Socket.Handle.ToInt64();
            if (!m_vClients.ContainsKey(socketHandle))
            {
                try
                {
                    m_vClients.TryAdd(socketHandle, c);
                }
                catch (OverflowException overflowException)
                {
                    Debug.Write(Debug.FlattenException(overflowException));
                }
            }

            try
            {
                c.CIPAddress = ((System.Net.IPEndPoint)c.Socket.RemoteEndPoint).Address.ToString();
            }
            catch (SocketException socketException)
            {
                Debug.Write(Debug.FlattenException(socketException));
            }
            //try
            //{
            //    m_vClients.TryAdd(c.Socket.Handle.ToInt64(), c);
            //}
            //catch (OverflowException overflowException)
            //{
            //    Debug.Write(Debug.FlattenException(overflowException));
            //}
        }

        /// <summary>
        ///     <see cref="Cache" /> the specified player to the redis database.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public static void Cache(Level _Player)
        {
            var id = _Player.GetPlayerAvatar().GetId();
            Redis.Players.StringSet(id.ToString(), _Player.SaveToJSON());
        }

        /// <summary>
        ///     <see cref="Cache" /> the specified Clan to the redis database.
        /// </summary>
        /// <param name="_Clan">The Clan.</param>
        public static void Cache(Clan _Clan)
        {
            Redis.Clans.StringSet(_Clan.ClanID.ToString(), _Clan.SaveToJSON());
        }

        /// <summary>
        ///     <see cref="Cache" /> the specified Tournament to the redis database.
        /// </summary>
        /// <param name="_Tournament">The Tournament.</param>
        public static void Cache(Tournament _Tournament)
        {
            Redis.Tournaments.StringSet(_Tournament.TournamentID.ToString(), _Tournament.Serialize().ToString());
        }

        public static List<Client> GetConnectedClients()
        {
            List<Client> clients = new List<Client>();
            clients.AddRange(m_vClients.Values);
            return clients;
        }

        public static List<Level> GetInMemoryLevels()
        {
            List<Level> levels = new List<Level>();
            lock (m_vOnlinePlayersLock)
            {
                levels.AddRange(m_vInMemoryLevels.Values);
            }
            return levels;
        }

        public static List<Level> GetOnlinePlayers()
        {
            List<Level> onlinePlayers = new List<Level>();
            lock (m_vOnlinePlayersLock)
            {
                onlinePlayers = m_vOnlinePlayers.ToList();
            }

            return onlinePlayers;
        }

        public static void GetAllPlayersFromDB()
        {
            var players = m_vDatabase.GetAllPlayers();
            foreach (var t in players)
            {
                if (!m_vInMemoryLevels.ContainsKey(t.Key))
                {
                    try
                    {
                        m_vInMemoryLevels.TryAdd(t.Key, t.Value);
                    }
                    catch (OverflowException overflowException)
                    {
                        Debug.Write(Debug.FlattenException(overflowException));
                    }
                }
            }
        }

        public static bool IsPlayerOnline(Level l)
        {
            return m_vOnlinePlayers.Contains(l);
        }

        public static void LoadLevel(Level level)
        {
            var id = level.GetPlayerAvatar().GetId();
            if (!m_vInMemoryLevels.ContainsKey(id))
            {
                try
                {
                    m_vInMemoryLevels.TryAdd(id, level);
                }
                catch (OverflowException overflowException)
                {
                    Debug.Write(Debug.FlattenException(overflowException));
                }
            }
        }

        public static void LogPlayerOut(Level level)
        {
            DatabaseManager.Singelton.Save(level);
            m_vOnlinePlayers.Remove(level);
            m_vInMemoryLevels.TryRemove(level.GetPlayerAvatar().GetId());
            m_vClients.TryRemove(level.GetClient().GetSocketHandle());
        }

        public static void DropClient(long socketHandle)
        {
            try
            {
                Client client;
                m_vClients.TryRemove(socketHandle, out client);
                if (client.GetLevel() != null)
                {
                    ResourcesManager.LogPlayerOut(client.GetLevel());
                }
            }
            catch (Exception ex)
            {
                Debugger.WriteLine("[CRS]    Error dropping client: ", ex, 4);
                Debug.Write(Debug.FlattenException(ex));
            }
        }

        public static Client GetClient(long socketHandle)
        {
            return ResourcesManager.m_vClients[socketHandle];
        }

        public static void RemoveClan(Clan _Clan)
        {
            if (Clans.ContainsKey(_Clan.ClanID))
            {
                Clans.Remove(_Clan.ClanID);
            }
        }

        /// <summary>
        ///     Forget the specified player from the redis database.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public static void Uncache(Level _Player)
        {
            if (Redis.Players.KeyExists(_Player.GetPlayerAvatar().GetId().ToString()))
            {
                Redis.Players.KeyDelete(_Player.GetPlayerAvatar().GetId().ToString());
            }
        }

        /// <summary>
        ///     Forget the specified Clan from the redis database.
        /// </summary>
        /// <param name="_Clan">The Clan.</param>
        public static void Uncache(Clan _Clan)
        {
            if (Redis.Clans.KeyExists(_Clan.ClanID.ToString()))
            {
                Redis.Clans.KeyDelete(_Clan.ClanID.ToString());
            }
        }

        /// <summary>
        ///     Forget the specified Tournament from the redis database.
        /// </summary>
        /// <param name="_Tournament">The Tournament.</param>
        public static void Uncache(Tournament _Tournament)
        {
            if (Redis.Tournaments.KeyExists(_Tournament.TournamentID.ToString()))
            {
                Redis.Tournaments.KeyDelete(_Tournament.TournamentID.ToString());
            }
        }

        public void Dispose()
        {
            if (this.TimerReference != null)
            {
                this.TimerReference.Dispose();
                this.TimerReference = null;
                Clans.Clear();
                m_vInMemoryLevels.Clear();
                Debug.Write("The class has been reset / cleared.");
            }
        }

        private static Level GetInMemoryPlayer(long id)
        {
            Level result = null;
            lock (m_vOnlinePlayersLock)
            {
                if (m_vInMemoryLevels.ContainsKey(id))
                {
                    result = m_vInMemoryLevels[id];
                }
            }
            return result;
        }

        public static void LogPlayerIn(Level level, Client client)
        {
            level.SetClient(client);
            client.SetLevel(level);
            level.SetIPAddress(client.CIPAddress);
            if (!m_vOnlinePlayers.Contains(level))
            {
                m_vOnlinePlayers.Add(level);
                LoadLevel(level);
            }
            else
            {
                int i = m_vOnlinePlayers.IndexOf(level);
                m_vOnlinePlayers[i] = level;
            }
        }

        public static Level GetPlayer(long id, bool persistent = false)
        {
            Level result = GetInMemoryPlayer(id);
            if (result == null)
            {
                result = m_vDatabase.GetAccount(id);
                if (persistent)
                {
                    LoadLevel(result);
                }
            }
            return result;
        }

        private void ReleaseOrphans(object state)
        {
            if (this.m_vTimerCanceled)
            {
                this.TimerReference.Dispose();
            }
        }
    }
}