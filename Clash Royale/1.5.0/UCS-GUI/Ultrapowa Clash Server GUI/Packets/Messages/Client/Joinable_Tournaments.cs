namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.Binary;

    #endregion

    internal class Joinable_Tournaments : Message
    {
        public const ushort PacketID = 16103;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Joinable_Tournaments" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Joinable_Tournaments(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Joinable_Tournaments.
        }
    }
}