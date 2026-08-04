namespace CRS.Core.Events
{
    #region Usings

    using System;
    using System.Runtime.InteropServices;

    using CRS.Database;
    using CRS.Logic.Slots;

    using Logic;
    using Logic.Enums;

    using NLog;

    #endregion Usings

    internal class EventsHandler
    {
        private static EventHandler _Handler;

        /// <summary>
        /// The current class logger, used to write logs using <see cref="Checker"/> tag.
        /// </summary>
        private Logger Logger = LogManager.GetCurrentClassLogger();

        private delegate void EventHandler(Exits _Type);

        /// <summary>
        /// Initialize a new instance of the <see cref="EventsHandler"/> class.
        /// </summary>
        public EventsHandler()
        {
            _Handler += this.Handler;
            SetConsoleCtrlHandler(_Handler, true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Logger = null;
        }

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler _Handler, bool _Enabled);

        /// <summary>
        /// Exits the handler.
        /// </summary>
        private void ExitHandler()
        {
            /* TODO: Set the Server in Maintenance Mode for 10 minutes.
             * TODO: Save all players...
             * TODO: Save all alliances...
             * TODO: Clean Up the Memcached Server.
             * TODO: Close. */
            Debug.Write("The Server is currently saving all players and clans, before shutting down.");
            this.Logger.Debug("The Server is currently saving all players and clans, before shutting down.");

            using (ucsdbEntities ucsdbEntities = new ucsdbEntities())
            {
                foreach (Level _Player in ResourcesManager.m_vClients.Values)
                {
                    Players _Data = Database.Players.Find(_Player.GetId());

                    _Data.Status = (int)_Player.GetStatus();
                    _Data.Rank      = (int)_Player.GetRank();
                    _Data.Avatar    = _Player.Serialize().ToString();
                    _Data.Objects   = _Player.Deck.Serialize().ToString();
                }

                Database.SaveChanges();
            }
        }

        private void Handler(Exits _Type)
        {
            switch (_Type)
            {
                default:
                {
                    this.ExitHandler();
                    break;
                }
            }
        }
    }
}