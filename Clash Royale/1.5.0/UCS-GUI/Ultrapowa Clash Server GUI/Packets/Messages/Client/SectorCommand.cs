namespace CRS.Packets
{
    #region Usings


    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;
    using CRS.Logic;

    #endregion

    internal class SectorCommand : Message
    {
        public const ushort PacketID = 12904;

        public int Command_Count;

        /// <summary>
        ///     Initialize a new instance of the <see cref="SectorCommand" />class.
        /// </summary>
        /// <param name="_Client">The client.</param>
        /// <param name="Reader">The reader.</param>
        /// <param name="_Header">The header.</param>
        public SectorCommand(Client _Client, Reader _Reader, int[] _Header)
            : base(_Client, _Reader, _Header)
        {
            // Sector_Command.
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
                // EE-F0-D0-C0-0C  29  00
                // 01-8F-01-7F-00-12-07-8F-EA-E5-18-00-9C-94-01-84-9E-03
                // 01-8F-01-7F-00-12-07-8F-EA-E5-18-00-9C-94-01-84-9E-03
                // 01-8F-01-7F-00-10-02-8D-EA-E5-18-00-B4-07-A4-DC-03
                // 01-8F-01-7F-00-10-00-80-EA-E5-18-00-94-C3-01-BC-CC-03
                // 01-01-8F-01-7F-00-10-00-80-EA-E5-18-00-8C-F2-01-A4-DC-03
                var test = this.Reader.ReadVInt();
                var test2 = this.Reader.ReadVInt();
                this.Command_Count = this.Reader.ReadVInt();

                if (this.Command_Count > 0 && this.Command_Count < 10)
                {
                    for (int _Index = 0; _Index < this.Command_Count; _Index++)
                    {
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                        this.Reader.ReadVInt();
                    }
                }

                Debug.Write("test: " + test);
                Debug.Write("test2: " + test2);
                Debug.Write("Command_Count: " + this.Command_Count);
            Debug.Write("BaseStream Position: " + this.Reader.BaseStream.Position);
        }

        /// <summary>
        ///     <see cref="Process" /> this instance.
        /// </summary>
        public override void Process(Level level)
        {
            ResourcesManager.Battles[this.Client.GetLevel().GetPlayerAvatar().GetId()].Begin();
        }
    }
}