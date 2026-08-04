namespace CRS.Packets.Messages.Client
{
    #region Usings

    using System;

    using CRS.Core.Settings;
    using CRS.Extensions.Binary;
    using CRS.Logic;
    using CRS.Logic.Enums;
    using CRS.Network;

    using Client = Packets.Client;
    using Debug = Core.Debug;

    #endregion

    internal class ClientHello : Message
    {
        public const ushort PacketID = 10100;

        private int Protocol;

        private int KeyVersion;

        private int[] Version = new int[3];

        private string Hash = string.Empty;

        private int Device;

        private int AppStore;

        /// <summary>
        ///     Initialize a new instance of the
        ///     <see cref="ClientHello" />class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="_Reader">The reader.</param>
        public ClientHello(Client _Client, Reader _Reader, int[] _Header) : base(_Client, _Reader, _Header)
        {
            this.Client.State = (int)State.SESSION;
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.Protocol = this.Reader.ReadInt32(); // Protocol 1
                this.KeyVersion = this.Reader.ReadInt32(); // Key Version 7
                this.Version[0] = this.Reader.ReadInt32(); // Major Version 2
                this.Version[1] = this.Reader.ReadInt32(); // Minor Version 0
                this.Version[2] = this.Reader.ReadInt32(); // Build Version 2000

                this.Hash = this.Reader.ReadString(); // Content Hash
                this.Device = this.Reader.ReadInt32(); // Device Type 2
                this.AppStore = this.Reader.ReadInt32(); // App Store 2

                Debug.Write("Protocol: " + this.Protocol);
                Debug.Write("KeyVersion: " + this.KeyVersion);
                Debug.Write("MajorVersion: " + this.Version[0]);
                Debug.Write("MinorVersion: " + this.Version[1]);
                Debug.Write("Build: " + this.Version[2]);
                Debug.Write("Hash: " + this.Hash);
                Debug.Write("Device: " + this.Device);
                Debug.Write("AppStore: " + this.AppStore);
                //Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        /// <param name="level">
        /// </param>
        /// <exception cref="OverflowException">
        ///     <paramref name="value" /> es menor que <see cref="F:System.TimeSpan.MinValue" /> o mayor que
        ///     <see cref="F:System.TimeSpan.MaxValue" />.O bienEl valor de <paramref name="value" /> es
        ///     <see cref="F:System.Double.PositiveInfinity" />.O bienEl valor de <paramref name="value" /> es
        ///     <see cref="F:System.Double.NegativeInfinity" />.
        /// </exception>
        public override void Process(Level level)
        {
            
            if (this.Version[0] != (int)CVersion.Major && this.Version[2] != (int)CVersion.Build)
            {
                var p = new Authentification_Failed(this.Client);
                this.Client.State = (int)State.DISCONNECTED;
                p.SetErrorCode(CodesFail.NEW_VERSION);
                if (this.Version[0] > (int)CVersion.Major)
                {
                    p.SetReason("Esta versión de cliente es superior a la aceptada por el servidor.");
                }
                else if (this.Version[2] < (int)CVersion.Build)
                {
                    p.SetReason("Esta versión de cliente es menor a la aceptada por el servidor.");
                }
                else
                {
                    p.SetReason("Este cliente no es aceptado por el servidor.");
                }

                PacketManager.Send(p);

                return;
            }

            if (Settings.Maintenance)
            {
                var p = new Authentification_Failed(this.Client);
                this.Client.State = (int)State.DISCONNECTED;
                p.SetErrorCode(CodesFail.MAINTENANCE);
                p.SetReason("Duración del mantenimiento: " + TimeSpan.FromMinutes(Settings.MaintenanceDuration));
                p.RemainingTime(Settings.MaintenanceDuration);
                PacketManager.Send(p);
                return;
            }

            if (this.Hash != Constants.Sha)
            {
                var p = new Authentification_Failed(this.Client);
                p.SetErrorCode(CodesFail.NEW_VERSION);
                p.SetReason("Este cliente no es aceptado por el servidor.");
                PacketManager.Send(p);
            }
            else
            {
                this.Client.State = (int)State.SESSION;
                PacketManager.Send(new ServerHello(this.Client));
            }
        }
    }
}