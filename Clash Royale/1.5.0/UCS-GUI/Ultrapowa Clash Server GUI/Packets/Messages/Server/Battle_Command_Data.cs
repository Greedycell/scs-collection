namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;
    using CRS.Logic.Slots.Items;
    using CRS.Network;

    #endregion

    internal class Battle_Command_Data : Message
    {
        public const ushort PacketID = 21902;

        public Battle Battle = null;

        public long Sender = 0;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Battle_Command_Data" />
        ///     class.
        /// </summary>
        public Battle_Command_Data(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            
            this.Writer.AddVInt(this.Battle.Tick);
            this.Writer.AddVInt(this.Battle.Checksum); // D4-A5-CA-94-0C
            this.Writer.Add(this.Battle.Commands.Count > 0);

            if (this.Battle.Commands.Count > 0)
            {
                this.Writer.AddRange(PacketManager.Handle(this.Battle.Commands.Dequeue()).Writer);
            }
            
        }
    }
}