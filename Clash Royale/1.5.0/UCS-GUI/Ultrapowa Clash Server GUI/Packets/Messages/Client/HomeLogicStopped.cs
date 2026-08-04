namespace CRS.Packets
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;
    using CRS.Logic;
    using CRS.Network;

    #endregion

    internal class HomeLogicStopped : Message
    {
        public const ushort PacketID = 14105;

        public HomeLogicStopped(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            this.Decrypt2();
            //this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            var test = this.Reader.ReadVInt();
            Debug.Write("test: " + test);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        public override void Process(Level level)
        {
            PacketManager.Send(new UDP_Connection_Info(this.Client));
            PacketManager.Send(new SectorState(this.Client));
        }
    }
}