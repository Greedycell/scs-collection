namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Gamble_Chests : Data
    {
        public Gamble_Chests(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public int GoldPrice
        {
            get; set;
        }

        public string Location
        {
            get; set;
        }
    }
}
