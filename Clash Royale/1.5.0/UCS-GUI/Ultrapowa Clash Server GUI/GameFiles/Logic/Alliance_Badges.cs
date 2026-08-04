namespace CRS.GameFiles
{
   
    

    internal class Alliance_Badges : Data
    {
        public Alliance_Badges(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
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

        public string Category
        {
            get; set;
        }
    }
}
