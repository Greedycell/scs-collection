namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Client_Globals : Data
    {
        public Client_Globals(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public int NumberValue
        {
            get; set;
        }

        public bool BooleanValue
        {
            get; set;
        }

        public string TextValue
        {
            get; set;
        }

        public string StringArray
        {
            get; set;
        }

        public int NumberArray
        {
            get; set;
        }
    }
}
