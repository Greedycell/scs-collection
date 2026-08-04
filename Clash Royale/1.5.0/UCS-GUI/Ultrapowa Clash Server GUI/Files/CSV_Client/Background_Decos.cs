namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Background_Decos : Data
    {
        public Background_Decos(CSVRow row, DataTable dt) : base(row, dt)
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

        public string ExportName
        {
            get; set;
        }

        public string Layer
        {
            get; set;
        }
    }
}
