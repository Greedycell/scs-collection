namespace CRS.Database
{
    #region Usings

    using System;

    using CRS.Core.Settings;

    using Logic.Enums;

    using StackExchange.Redis;

    #endregion Usings

    internal class Redis : IDisposable
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Redis"/> class.
        /// </summary>
        public Redis()
        {
            this._Redis     = ConnectionMultiplexer.Connect(new ConfigurationOptions()
            {
                Password    = "G0B3l1L4nDv55PfDKGE16GG0B3l1L4nD",
                EndPoints   =
                {
                    {
                        Constants.RedisAddr, Constants.RedisPort
                    }
                }
            });

            Players      = this._Redis.GetDatabase((int) Database.Players);
            Clans        = this._Redis.GetDatabase((int) Database.Clans);
            Tournaments  = this._Redis.GetDatabase((int) Database.Tournaments);

            Console.WriteLine("The Redis class has been initialized.");
        }

        /// <summary>
        /// A Variable storing the Redis Client, used to make requests to the Redis server.
        /// </summary>
        private ConnectionMultiplexer _Redis
        {
            get;
            set;
        }

        public static IDatabase Players
        {
            get;
            set;
        }

        public static IDatabase Clans
        {
            get;
            set;
        }

        public static IDatabase Tournaments
        {
            get;
            set;
        }

        /// <summary>
        /// Exécute les tâches définies par l'application associées à la libération ou à la
        /// redéfinition des ressources non managées.
        /// </summary>
        public void Dispose()
        {
            this._Redis = null;
        }
    }
}