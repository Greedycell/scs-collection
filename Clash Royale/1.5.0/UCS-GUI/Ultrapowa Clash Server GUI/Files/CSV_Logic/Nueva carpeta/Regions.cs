using System;

namespace GL.Servers.CR.Files.CSV_Logic
     {
			using GL.Servers.CR.Files.CSV_Helpers;
			using GL.Servers.CR.Files.CSV_Reader;
     class Regions : Data
       	{
       		public Regions(CSVRow row, DataTable dt) : base(row, dt)
         	{
             	LoadData(this, this.GetType(), row);
         	}

        public string Name { get; set; }
        public string TID { get; set; }
        public string DisplayName { get; set; }
        public bool IsCountry { get; set; }
        public bool RegionPopup { get; set; }
       	}
}

