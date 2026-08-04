namespace CRS.GameFiles
{
    internal class ResourceData : Data
    {
        public ResourceData(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string CollectEffect
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public bool PremiumCurrency
        {
            get; set;
        }

        public string CapFullTID
        {
            get; set;
        }

        public int TextRed
        {
            get; set;
        }

        public int TextGreen
        {
            get; set;
        }

        public int TextBlue
        {
            get; set;
        }

        public int Cap
        {
            get; set;
        }

        public string IconFile
        {
            get; set;
        }

        public string ShopIcon
        {
            get; set;
        }
    }
}
