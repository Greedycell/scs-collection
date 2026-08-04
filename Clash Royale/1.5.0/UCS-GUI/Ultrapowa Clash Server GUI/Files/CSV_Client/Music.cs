namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Music : Data
    {
        public Music(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public int Volume
        {
            get; set;
        }

        public bool Loop
        {
            get; set;
        }

        public int PlayCount
        {
            get; set;
        }

        public int FadeOutTimeSec
        {
            get; set;
        }

        public int DurationSec
        {
            get; set;
        }
    }
}
