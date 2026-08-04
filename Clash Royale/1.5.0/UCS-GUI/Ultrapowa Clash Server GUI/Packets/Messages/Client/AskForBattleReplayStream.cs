namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;

    #endregion

    internal class AskForBattleReplayStream : Message
    {
        public const ushort PacketID = 14406;

        /// <summary>
        ///     Initialize a new instance of the <see cref="AskForBattleReplayStream" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public AskForBattleReplayStream(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Battle_Stream.
            
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                int[] unk = new int[2];
                unk[0] = this.Reader.ReadInt32();
                unk[1] = this.Reader.ReadInt32();
                Debug.Write("unk[0]: " + unk[0]);
                Debug.Write("unk[1]: " + unk[1]);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}