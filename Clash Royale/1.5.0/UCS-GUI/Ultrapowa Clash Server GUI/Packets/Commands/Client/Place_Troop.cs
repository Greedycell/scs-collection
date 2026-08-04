namespace CRS.Packets.Commands.Client
{
    #region Usings

    using CRS.Core;
    using CRS.Extensions.Binary;
    using CRS.Extensions.List;

    using Client = CRS.Packets.Client;

    #endregion

    internal class Place_Troop : Command
    {
        public const int CommandID = 1;

        private int TroopID;

        private int[] ints = new int[10];

        private string[] strings = new string[10];

        private long[] longs = new long[10];

        private object[] objects = new object[10];

        /// <summary>
        ///     Initialize a new instance of the <see cref="Place_Troop" /> class.
        /// </summary>
        /// <param name="_Reader">The reader.</param>
        /// <param name="_Client">The client.</param>
        /// <param name="_ID">The identifier.</param>
        public Place_Troop(Reader _Reader, Client _Client, int _ID)
            : base(_Reader, _Client, _ID)
        {
            //Place_Troop 
        }

        /// <summary>
        ///     <see cref="Decode" /> this instance.
        /// </summary>
        public override void Decode()
        {
            // B8-0F-8C-10-00-01-01-81-EA-E5-18-00-94-46-84-A1-02
            // 8B-05-9F-05-00-01-03-83-EA-E5-18-00-B4-07-A4-DC-03
            // 91-07-A5-07-00-01-02-82-EA-E5-18-00-B4-07-A4-DC-03
            // 95-0E-A9-0E-00-01-01-81-EA-E5-18-00-B4-07-A4-DC-03
            // 96-12-AA-12-00-01-00-80-EA-E5-18-00-B4-07-A4-DC-03
            // A6-0A-BA-0A-00-01-01-81-EA-E5-18-00-B4-07-A4-DC-03
            // 82-0B-96-0B-00-01-00-80-EA-E5-18-00-B4-07-A4-DC-03
            // 93-0B-A7-0B-00-01-05-85-EA-E5-18-00-B4-07-A4-DC-03
            this.ints[0] = this.Reader.ReadVInt();
            this.ints[1] = this.Reader.ReadVInt();
            this.ints[2] = this.Reader.ReadInt16();
            this.objects[0] = this.Reader.Read();
            this.TroopID = this.Reader.ReadVInt();
            this.objects[1] = this.Reader.Read();
            this.ints[3] = this.Reader.ReadVInt();
            this.ints[4] = this.Reader.ReadVInt();

            Debug.Write("ints[0]: " + this.ints[0]);
            Debug.Write("ints[1]: " + this.ints[1]);
            Debug.Write("ints[2]: " + this.ints[2]);
            Debug.Write("objects[0]: " + this.objects[0]);
            Debug.Write("TroopID: " + this.TroopID);
            Debug.Write("objects[1]: " + this.objects[1]);
            Debug.Write("ints[3]: " + this.ints[3]);
            Debug.Write("ints[4]: " + this.ints[4]);
        }
    }
}