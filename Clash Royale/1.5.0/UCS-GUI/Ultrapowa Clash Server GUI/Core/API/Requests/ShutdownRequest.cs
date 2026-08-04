namespace CRS.Core.API.Requests
{
    #region Usings

    using System;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;

    using CRS.Core.Settings;

    #endregion Usings

    internal class ShutdownRequest : Request
    {
        public ShutdownRequest(HttpListenerContext _Context)
            : base(_Context)
        {
            // Close...
        }

        public Thread Thread
        {
            get;
            set;
        }

        public override void Answer()
        {
            base.Answer();
        }

        public override void Process()
        {
            if (!Settings.ShuttingDown)
            {
                this.Thread = new Thread(() =>
                {
                    int _Time = 30;

                    if (this.Requests.AllKeys.Contains("time"))
                    {
                        if (int.TryParse(this.Requests["time"], out _Time))
                        {
                            this.Response = Encoding.UTF8.GetBytes("The Server will be shutdown in a few seconds...");
                            Settings.ShuttingDown = true;
                            Thread.Sleep(TimeSpan.FromSeconds(_Time));
                            Environment.Exit(0);
                        }
                        else
                        {
                            this.Response = Encoding.UTF8.GetBytes("The Server will be shutdown in a few seconds...");
                            Settings.ShuttingDown = true;
                            Thread.Sleep(TimeSpan.FromSeconds(_Time));
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        this.Response = Encoding.UTF8.GetBytes("The Server will be shutdown in a few seconds...");
                        Settings.ShuttingDown = true;
                        Thread.Sleep(TimeSpan.FromSeconds(_Time));
                        Environment.Exit(0);
                    }
                });
                this.Thread.Start();
            }
            else
            {
                this.Response = Encoding.UTF8.GetBytes("The server is already shutting down..");
            }
        }
    }
}