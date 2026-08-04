namespace CRS.Packets.Commands.Battles
{
    #region Usings


    using Client = CRS.Packets.Client;

    #endregion

    internal class Taunt : Command
    {
        public const int CommandID = 1;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Taunt" /> class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        public Taunt(Client _Client)
            : base(_Client)
        {
            this.ID = CommandID;
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
        }
    }
}