namespace CRS.Packets.Commands.Client
{
    #region Usings

    using System;

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    class Friendly_Battle : Command
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="Friendly_Battle" />
        ///     class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Friendly_Battle(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Friendly_Battle.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            Debug.Write("Friendly_Battle : " + this.Reader.ReadString());
            Debug.Write("Friendly_Battle : " + BitConverter.ToString(this.Reader.ReadAllBytes()));
        }
    }
}