namespace CRS.Packets
{
    #region Usings

    using System.Linq;

    using CRS.Extensions.List;
    using CRS.Library.Blake2b;
    using CRS.Library.Sodium;
    using CRS.Logic.Enums;

    #endregion

    internal class Authentification_Failed : Message
    {
        public const ushort PacketID = 20103;

        /// <summary>
        ///     Reason why login failed.
        /// </summary>
        private CodesFail m_vErrorCode;

        // 8  : Nueva versión del juego disponible(removeupdateurl)
        // 10 : maintenance
        // 11 : Bloqueado temporalmente
        // 12 : Jugó demasiado
        // 13 : Cuenta bloqueada
        private string m_vReason;

        private string m_vUpdateURL;

        private string m_vContentURL; //60

        private string m_vRedirectDomain; //56

        private string m_vResourceFingerprintData; //52

        private int m_vRemainingTime;

        /// <summary>
        ///     Initialize a new instance of the
        ///     <see cref="Authentification_Failed" /> class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Authentification_Failed(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        public void SetErrorCode(CodesFail code)
        {
            this.m_vErrorCode = code;
        }

        public void SetReason(string reason)
        {
            this.m_vReason = reason;
        }

        public void RemainingTime(int code)
        {
            this.m_vRemainingTime = code;
        }

        public void SetContentURL(string url)
        {
            this.m_vContentURL = url;
        }

        public void SetRedirectDomain(string domain)
        {
            this.m_vRedirectDomain = domain;
        }

        public void SetResourceFingerprintData(string data)
        {
            this.m_vResourceFingerprintData = data;
        }

        public void SetUpdateURL(string url)
        {
            this.m_vUpdateURL = url;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.Add((byte)this.m_vErrorCode);
            this.Writer.AddString(this.m_vResourceFingerprintData);
            this.Writer.AddString(this.m_vRedirectDomain);
            this.Writer.AddString(this.m_vContentURL);
            this.Writer.AddString(this.m_vUpdateURL);
            this.Writer.AddString(this.m_vReason); // \t
            this.Writer.AddInt(this.m_vRemainingTime);
        }

        /// <summary>
        /// Encrypt this instance.
        /// </summary>
        public override void Encrypt()
        {
            if (this.Client.State > (int)State.SESSION_OK)
            {
                Blake2BHasher _Blake = new Blake2BHasher();

                _Blake.Update(this.Client.CSNonce);
                _Blake.Update(this.Client.CPublicKey);
                _Blake.Update(Keys.Sodium.PublicKey);

                byte[] _Nonce = _Blake.Finish();

                this.SetData(this.Client.CRNonce.Concat(this.Client.CPublicKey).Concat(this.Writer).ToArray());
                this.SetData(Sodium.Encrypt(this.GetData(), _Nonce, Keys.Sodium.PrivateKey, this.Client.CPublicKey));
            }
            else
            {
                this.SetData(this.Writer.ToArray());
            }
        }
    }
}