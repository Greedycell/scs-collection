namespace CRS.Packets
{
    #region Usings

    using System.Linq;

    using CRS.Extensions.List;
    using CRS.Helpers;
    using CRS.Library.Blake2b;
    using CRS.Library.Sodium;
    using CRS.Logic.Enums;

    #endregion

    internal class LoginOk : Message
    {
        public const ushort PacketID = 20104;

        private string FacebookAppID = string.Empty;

        private string FacebookID = string.Empty;

        private string GamecenterID = string.Empty;

        private string m_vAccountCreatedDate;

        private long m_vAccountId;

        private int m_vContentVersion;

        private string m_vCountryCode;

        private int m_vDaysSinceStartedPlaying;

        private string m_vFacebookId;

        private string m_vGamecenterId;

        private string m_vPassToken;

        private int m_vPlayTimeSeconds;

        private int m_vServerBuild;

        private string m_vServerEnvironment;

        private int m_vServerMajorVersion;

        private string m_vServerTime;

        private int m_vSessionCount;

        private int m_vStartupCooldownSeconds;

        private string Startup = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="LoginOk" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public LoginOk(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
            this.Client.State = (int)State.LOGGED;
            MainWindow.RemoteWindow.UpdateTheListPlayers();
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {

            this.Writer.AddInt64(this.m_vAccountId);
            this.Writer.AddInt64(this.m_vAccountId);
            this.Writer.AddString(this.m_vPassToken);
            this.Writer.AddString(this.m_vFacebookId);
            this.Writer.AddString(this.m_vGamecenterId);
            this.Writer.AddInt32(this.m_vServerMajorVersion);
            this.Writer.AddInt32(this.m_vServerBuild);
            this.Writer.AddInt32(this.m_vContentVersion);
            this.Writer.AddString(this.m_vServerEnvironment);
            this.Writer.AddInt32(this.m_vSessionCount);
            this.Writer.AddInt32(this.m_vPlayTimeSeconds);
            this.Writer.AddInt32(0);
            this.Writer.AddString(this.FacebookAppID);
            this.Writer.AddString(this.m_vStartupCooldownSeconds.ToString());
            this.Writer.AddString(this.m_vAccountCreatedDate);
            this.Writer.AddInt32(0);
            this.Writer.AddString(this.GamecenterID.ToString());
            this.Writer.AddString(null);
            this.Writer.AddString(this.m_vCountryCode);
            this.Writer.AddString("someid2");
        }

        /// <summary>
        /// Encrypt this instance.
        /// </summary>
        public override void Encrypt()
        {
            Blake2BHasher _Hasher = new Blake2BHasher();

            _Hasher.Update(this.Client.CSNonce);
            _Hasher.Update(this.Client.CPublicKey);
            _Hasher.Update(Keys.Sodium.PublicKey);

            byte[] _Nonce = _Hasher.Finish();

            this.SetData(this.Client.CRNonce.Concat(this.Client.CPublicKey).Concat(this.Writer).ToArray());
            this.SetData(Sodium.Encrypt(this.GetData(), _Nonce, Keys.Sodium.PrivateKey, this.Client.CPublicKey));
        }

        public void SetAccountCreatedDate(string date)
        {
            this.m_vAccountCreatedDate = date;
        }

        public void SetAccountId(long id)
        {
            this.m_vAccountId = id;
        }

        public void SetContentVersion(int version)
        {
            this.m_vContentVersion = version;
        }

        public void SetCountryCode(string code)
        {
            this.m_vCountryCode = code;
        }

        public void SetDaysSinceStartedPlaying(int days)
        {
            this.m_vDaysSinceStartedPlaying = days;
        }

        public void SetFacebookId(string id)
        {
            this.m_vFacebookId = id;
        }

        public void SetGamecenterId(string id)
        {
            this.m_vGamecenterId = id;
        }

        public void SetPassToken(string token)
        {
            this.m_vPassToken = token;
        }

        public void SetPlayTimeSeconds(int seconds)
        {
            this.m_vPlayTimeSeconds = seconds;
        }

        public void SetServerBuild(int build)
        {
            this.m_vServerBuild = build;
        }

        public void SetServerEnvironment(string env)
        {
            this.m_vServerEnvironment = env;
        }

        public void SetServerMajorVersion(int version)
        {
            this.m_vServerMajorVersion = version;
        }

        public void SetServerTime(string time)
        {
            this.m_vServerTime = time;
        }

        public void SetSessionCount(int count)
        {
            this.m_vSessionCount = count;
        }

        public void SetStartupCooldownSeconds(int seconds)
        {
            this.m_vStartupCooldownSeconds = seconds;
        }
    }
}