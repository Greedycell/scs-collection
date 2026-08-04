namespace CRS.Packets
{
    #region Usings

    using System;
    using System.Linq;

    using CRS.Extensions.List;
    using CRS.Library.Blake2b;
    using CRS.Library.Sodium;
    using CRS.Logic;

    #endregion

    internal class OwnHomeData : Message
    {
        public const ushort PacketID = 24101;

        public ClientAvatar Player
        {
            get;
            set;
        }

        public Client PlayerClient
        {
            get;
            set;
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="OwnHomeData" /> class.
        /// </summary>
        public OwnHomeData(Client _Client, Level level)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
            this.Player = level.GetPlayerAvatar();
            this.PlayerClient = _Client;
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            this.Writer.AddRange(this.Client.GetLevel().GetPlayerAvatar().Data_Part1());
            this.Writer.AddRange(this.Client.GetLevel().GetPlayerAvatar().Data_Part2());
            this.Writer.AddVInt(this.Client.ClientSeed.ToString().Length);
            this.Writer.AddRange(BitConverter.GetBytes(this.PlayerClient.ClientSeed));
        }
    }
}