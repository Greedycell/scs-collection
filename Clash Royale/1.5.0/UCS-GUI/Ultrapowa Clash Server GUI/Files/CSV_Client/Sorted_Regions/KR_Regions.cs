namespace CRS.Files.CSV_Client.Sorted_Regions
{
    using CRS.GameFiles;

    internal class KR_Regions : Data
    {
        public KR_Regions(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string Dummy
        {
            get; set;
        }
    }
}
