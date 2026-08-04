namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Regions : Data
    {
        public Regions(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string DisplayName
        {
            get; set;
        }

        public bool IsCountry
        {
            get; set;
        }

        public bool RegionPopup
        {
            get; set;
        }
    }
}
