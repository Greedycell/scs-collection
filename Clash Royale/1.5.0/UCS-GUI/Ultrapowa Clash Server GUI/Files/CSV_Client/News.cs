namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class News : Data
    {
        public News(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public int ID
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string ItemSWF
        {
            get; set;
        }

        public string ItemExportName
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string ImageSWF
        {
            get; set;
        }

        public string ImageExportName
        {
            get; set;
        }

        public string ButtonUrl
        {
            get; set;
        }

        public string ButtonTID
        {
            get; set;
        }
    }
}
