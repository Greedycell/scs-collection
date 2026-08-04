namespace UCS.Files.CSV_Logic
{
    using System.Collections.Generic;

    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Globals : Data
    {
        public Globals(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
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
