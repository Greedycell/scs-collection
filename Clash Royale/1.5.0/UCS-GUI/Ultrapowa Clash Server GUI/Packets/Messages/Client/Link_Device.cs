namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.Binary;

    #endregion

    internal class Link_Device : Message
    {
        public const ushort PacketID = 16002;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Link_Device" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public Link_Device(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Link_Device.
        }
    }
}