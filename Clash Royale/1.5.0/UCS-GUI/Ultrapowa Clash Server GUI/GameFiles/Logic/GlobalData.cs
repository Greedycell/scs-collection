namespace CRS.GameFiles
{
    using System.Collections.Generic;

   
    

    internal class GlobalData : Data
    {
        public GlobalData(CSVRow row, DataTable dt) : base(row, dt)
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

        public List<string> StringArray
        {
            get; set;
        }

        public List<int> NumberArray
        {
            get; set;
        }
    }
}
