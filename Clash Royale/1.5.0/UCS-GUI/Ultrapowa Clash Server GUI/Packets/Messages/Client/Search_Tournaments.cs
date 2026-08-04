namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    #endregion

    internal class Search_Tournaments : Message
    {
        public const ushort PacketID = 16113;

        public string Name = string.Empty;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Search_Tournaments" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Search_Tournaments(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Search_Tournaments.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                this.Name = this.Reader.ReadString();

                var test = this.Reader.ReadVInt();
                var test2 = this.Reader.ReadVInt();
                var test3 = this.Reader.ReadInt32();
                Debug.Write("Name: " + this.Name);
                Debug.Write("test: " + test);
                Debug.Write("test2: " + test2);
                Debug.Write("test3: " + test3);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}