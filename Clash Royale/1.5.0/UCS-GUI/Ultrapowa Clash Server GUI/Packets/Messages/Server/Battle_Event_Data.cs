namespace CRS.Packets
{
    #region Usings


    using CRS.Extensions.List;

    #endregion

    internal class Battle_Event_Data : Message
    {
        public const ushort PacketID = 22952;

        public long CommandSender = 0;

        public int CommandID = 0;

        public int CommandValue = 0;

        public int CommandTick = 0;

        public int CommandUnk = 0;

        public int CommandUnk2 = 0;

        /// <summary>
        ///     Initialize a new instance of the <see cref="Battle_Event_Data" />
        ///     class.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public Battle_Event_Data(Client _Client)
            : base(_Client)
        {
            this.SetMessageType(PacketID);
        }

        /// <summary>
        ///     <see cref="Encode" /> this instance.
        /// </summary>
        public override void Encode()
        {
            
            /* this.Writer.AddVInt(this.CommandID);     // 01
            this.Writer.AddVInt(this.CommandSender); // 07-AA-C6-F7-01
            this.Writer.AddVInt(this.CommandUnk);    // 01
            this.Writer.AddVInt(this.CommandTick);   // A3-32
            this.Writer.AddVInt(this.CommandUnk2);   // 00-01
            this.Writer.AddVInt(this.CommandValue);  // 01 */
            this.Writer.AddVInt(this.CommandID);
            this.Writer.AddVInt(this.CommandSender);
            this.Writer.AddVInt(1);
            this.Writer.AddRange("A3-32".HexaToBytes());
            this.Writer.AddRange("00-01".HexaToBytes());
            this.Writer.AddVInt(this.CommandValue);
            
        }
    }
}