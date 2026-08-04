namespace CRS.Core.API
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Net;

    using CRS.Core.API.Requests;

    #endregion Usings

    internal class Prefixer
    {
        private readonly Dictionary<string, Type> Prefixes = new Dictionary<string, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Prefixer"/> class.
        /// </summary>
        public Prefixer()
        {
            this.Prefixes = new Dictionary<string, Type>();

            this.Prefixes.Add("shutdown", typeof(ShutdownRequest));
            this.Prefixes.Add("player", typeof(PlayerRequest));
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Prefixes.Clear();
        }

        /// <summary>
        /// Reads the specified context.
        /// </summary>
        /// <param name="_Context">The context.</param>
        /// <returns></returns>
        public Request Read(HttpListenerContext _Context)
        {
            string _Command = _Context.Request.QueryString.Get("command");

            if (!string.IsNullOrEmpty(_Command))
            {
                if (this.Prefixes.ContainsKey(_Command))
                {
                    return (Request)Activator.CreateInstance(this.Prefixes[_Command], _Context);
                }

                return null;
            }

            return null;
        }
    }
}