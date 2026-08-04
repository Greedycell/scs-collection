namespace CRS.Core.API.Requests
{
    #region Usings

    using System;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #endregion Usings

    internal class PlayerRequest : Request
    {
        public PlayerRequest(HttpListenerContext _Context)
            : base(_Context)
        {
            // PlayerRequest...
        }

        public override void Answer()
        {
            JObject _Json = new JObject();
            _Json.Add("code", 1);
            _Json.Add("message", "GetPlayer request handled.");
            _Json.Add("time", DateTime.Now);
            _Json.Add("value", null);
            this.Response = Encoding.UTF8.GetBytes(_Json.ToString(Formatting.Indented));

            base.Answer();
        }

        public override void Process()
        {
        }
    }
}