namespace CRS.Core.API
{
    #region Usings

    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;

    using CRS.Core.Settings;

    #endregion Usings

    internal class Request
    {
        public Request()
        {
            this.Context = null;
            this.Requests = new NameValueCollection();
            this.Depth = 0;
            this.Response = Encoding.UTF8.GetBytes("OK");
        }

        public Request(HttpListenerContext _Context)
        {
            this.Context = _Context;
            this.Requests = _Context.Request.QueryString;
            this.Depth = _Context.Request.QueryString.Count <= Constants.MaxDepth ? _Context.Request.QueryString.Count : 0;
            this.Response = Encoding.UTF8.GetBytes("OK");
        }

        public HttpListenerContext Context
        {
            get;
            set;
        }

        public int Depth
        {
            get;
            set;
        }

        public NameValueCollection Requests
        {
            get;
            set;
        }

        public byte[] Response
        {
            get;
            set;
        }

        public virtual void Answer()
        {
            this.Context.Response.StatusCode = 200;
            this.Context.Response.StatusDescription = "PROCESSED";
            this.Context.Response.ContentType = "text/plain";
            this.Context.Response.ContentLength64 = this.Response.LongLength;
            this.Context.Response.KeepAlive = false;
            this.Context.Response.AddHeader("Content-Type", "text/plain");
            this.Context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
            this.Context.Response.OutputStream.Write(this.Response, 0, this.Response.Length);
            this.Context.Response.Close();
        }

        public virtual void Process()
        {
        }
    }
}