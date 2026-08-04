namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Taunts : Data
    {
        public Taunts(CSVRow row, DataTable dt) : base(row, dt)
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

        public bool TauntMenu
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

        public string IconExportName
        {
            get; set;
        }

        public string BtnExportName
        {
            get; set;
        }

        public string Sound
        {
            get; set;
        }
    }
}
