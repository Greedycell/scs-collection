using System;

namespace GL.Servers.CR.Files.CSV_Logic
     {
			using GL.Servers.CR.Files.CSV_Helpers;
			using GL.Servers.CR.Files.CSV_Reader;
     class Globals : Data
       	{
       		public Globals(CSVRow row, DataTable dt) : base(row, dt)
         	{
             	LoadData(this, this.GetType(), row);
         	}

        public string Name { get; set; }
        public int NumberValue { get; set; }
        public bool trueValue { get; set; }
        public string TextValue { get; set; }
        public string assdArray { get; set; }
        public int NumberArray { get; set; }
       	}
}

