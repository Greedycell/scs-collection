namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Resource_Packs : Data
    {
        public Resource_Packs(CSVRow row, DataTable dt) : base(row, dt)
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

        public string Resource
        {
            get; set;
        }

        public int Amount
        {
            get; set;
        }

        public string IconFile
        {
            get; set;
        }
    }
}
