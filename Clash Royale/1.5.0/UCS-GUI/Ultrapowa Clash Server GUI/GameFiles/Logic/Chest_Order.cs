namespace CRS.GameFiles
{
   
    

    internal class Chest_Order : Data
    {
        public Chest_Order(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string Chest
        {
            get; set;
        }
    }
}
