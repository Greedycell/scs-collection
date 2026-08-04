namespace CRS.Core {
    #region Usings

    using System;
    using System.Timers;
    using Settings;

    #endregion Usings

    /// <summary>
    /// The <see cref="Checker"/> is used to do some work each X seconds.
    /// </summary>
    internal class Checker : IDisposable {
        /// <summary>
        /// Get or set the <see cref="System.Timers.Timer"/>, which is used by the <see cref="Checker"/> for do a action each x ms.
        /// </summary>
        private readonly Timer Timer = null;

        /// <summary>
        /// Initialize a new instance of the <see cref="Checker"/> class.
        /// </summary>
        public Checker()
        {
            this.Timer = new Timer();
            this.Timer.Interval = Constants.CheckerInterval;
            this.Timer.AutoReset = true;

            // this.Timer.Elapsed      += (_Sender, _Args) =>
            // {
            // using (GRS_MySQL Database = new GRS_MySQL())
            // {
            // foreach (ClientAvatar _Player in ResourcesManager.Players.Values)
            // {
            // Players _Data   = Database.Players.Find(_Player.GetId());

            // _Data.Rank      = (int)_Player.GetRank();
            // _Data.Status    = (int)_Player.GetStatus();
            // _Data.Avatar    = _Player.Serialize().ToString();
            // _Data.Objects   = _Player.Deck.Serialize().ToString();
            // }

            // Database.SaveChanges();
            // }
            // };
            this.Timer.Start();
        }

        /// <summary>
        /// Dispose this instance.
        /// </summary>
        public void Dispose() {
            this.Timer.Stop();
        }
    }
}