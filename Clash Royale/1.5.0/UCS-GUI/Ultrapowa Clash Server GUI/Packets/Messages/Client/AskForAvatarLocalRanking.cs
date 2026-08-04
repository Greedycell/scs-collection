namespace CRS.Packets
{
    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Logic;

    internal class AskForAvatarLocalRanking : Message
    {
        public const ushort PacketID = 14404;

        public AskForAvatarLocalRanking(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {

        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        public override void Process(Level level)
        {

        }
    }
}
