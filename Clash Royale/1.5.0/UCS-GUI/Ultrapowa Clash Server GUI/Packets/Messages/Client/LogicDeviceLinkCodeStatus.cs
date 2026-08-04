namespace CRS.Packets
{
    #region Usings

    using System;

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Helpers;

    #endregion

    internal class LogicDeviceLinkCodeStatus : Message
    {
        public const ushort PacketID = 16000;

        /// <summary>
        ///     Initialize a new instance of the <see cref="LogicDeviceLinkCodeStatus" />
        ///     class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public LogicDeviceLinkCodeStatus(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Device_Link_Code.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                Debug.Write("Device_Link_Code : " + BitConverter.ToString(this.Reader.ReadAllBytes()));
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }
    }
}