using CRS.GameFiles;

namespace CRS.Files.CSV_Client.Sorted_Regions
{
    internal class CN_Regions : Data
    {
        public CN_Regions(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string Dummy
        {
            get; set;
        }
    }
}
