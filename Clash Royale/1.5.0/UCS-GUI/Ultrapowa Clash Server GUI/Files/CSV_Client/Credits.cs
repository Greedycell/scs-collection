namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Credits : Data
    {
        public Credits(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }
    }
}
