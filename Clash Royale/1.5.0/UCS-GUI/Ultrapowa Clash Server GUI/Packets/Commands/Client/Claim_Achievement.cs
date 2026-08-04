namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    class Claim_Achievement : Command
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="Claim_Achievement" />
        ///     class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Claim_Achievement(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Claim_Achievement.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            var test1 = this.Reader.ReadVInt();
            var test2 = this.Reader.ReadVInt();
            var test3 = this.Reader.ReadVInt();
            var test4 = this.Reader.ReadVInt();
            var test5 = this.Reader.ReadInt16();
            Debug.Write("test1: " + test1);
            Debug.Write("test2: " + test2);
            Debug.Write("test2: " + test2);
            Debug.Write("test4: " + test4);
            Debug.Write("test5: " + test5);
        }
    }
}