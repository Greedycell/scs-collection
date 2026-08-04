#region Usings



#endregion

namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    internal class EditCardPositionCommand : Command
    {
#pragma warning disable CS0108 // 'Move_Card.ID' oculta el miembro heredado 'Command.ID'. Use la palabra clave new si su intención era ocultarlo.
        public int ID;
#pragma warning restore CS0108 // 'Move_Card.ID' oculta el miembro heredado 'Command.ID'. Use la palabra clave new si su intención era ocultarlo.

        public int Position;

        public int Tick;

        /// <summary>
        ///     Initialize a new instance of the <see cref="EditCardPositionCommand" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public EditCardPositionCommand(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            // Move_Card.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            this.Tick = this.Reader.ReadVInt();
            this.Tick = this.Reader.ReadVInt();
            this.Reader.ReadInt16();

            this.ID = this.Reader.Read();
            this.Position = this.Reader.Read();

            Debug.Write("Position: " + this.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process()
        {
            this.Client.GetLevel().GetPlayerAvatar().Deck.Invert(this.ID, this.Position);
        }
    }
}