namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Billing_Packages : Data
    {
        public Billing_Packages(CSVRow row, DataTable dt) : base(row, dt)
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

        public bool Disabled
        {
            get; set;
        }

        public bool ExistsApple
        {
            get; set;
        }

        public bool ExistsAndroid
        {
            get; set;
        }

        public bool ExistsKunlun
        {
            get; set;
        }

        public bool ExistsJupiter
        {
            get; set;
        }

        public int Diamonds
        {
            get; set;
        }

        public int USD
        {
            get; set;
        }

        public int RMB
        {
            get; set;
        }

        public int Order
        {
            get; set;
        }

        public string IconFile
        {
            get; set;
        }

        public string JupiterID
        {
            get; set;
        }

        public string StarterPackName
        {
            get; set;
        }
    }
}
