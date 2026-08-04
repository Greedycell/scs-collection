namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;

    #endregion

    internal class JoinAlliance : Message
    {
        public const ushort PacketID = 14305;

        /// <summary>
        ///     Initialize a new instance of the <see cref="JoinAlliance" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public JoinAlliance(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Get_Device_Token.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                var test = this.Reader.ReadInt64();
                var test2 = this.Reader.ReadInt32();
                Debug.Write("test: " + test);
                Debug.Write("test2: " + test2);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}