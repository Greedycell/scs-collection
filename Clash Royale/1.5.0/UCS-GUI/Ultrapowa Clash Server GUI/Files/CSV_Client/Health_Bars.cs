namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Health_Bars : Data
    {
        public Health_Bars(CSVRow row, DataTable dt) : base(row, dt)
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

        public string PlayerExportName
        {
            get; set;
        }

        public string EnemyExportName
        {
            get; set;
        }

        public string NoDamagePlayerExportName
        {
            get; set;
        }

        public string NoDamageEnemyExportName
        {
            get; set;
        }

        public int MinimumHitpointValue
        {
            get; set;
        }

        public bool ShowOwnAlways
        {
            get; set;
        }

        public bool ShowEnemyAlways
        {
            get; set;
        }

        public int YOffset
        {
            get; set;
        }
    }
}
