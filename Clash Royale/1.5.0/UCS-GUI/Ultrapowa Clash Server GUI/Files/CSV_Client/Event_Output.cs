namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Event_Output : Data
    {
        public Event_Output(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }

        public int Channels
        {
            get; set;
        }

        public int DurationMillis
        {
            get; set;
        }
    }
}
