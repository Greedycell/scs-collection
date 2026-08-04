using System;

namespace GL.Servers.CR.Files.CSV_Logic
     {
			using GL.Servers.CR.Files.CSV_Helpers;
			using GL.Servers.CR.Files.CSV_Reader;
     class Resources : Data
       	{
       		public Resources(CSVRow row, DataTable dt) : base(row, dt)
         	{
             	LoadData(this, this.GetType(), row);
         	}

        public string Name { get; set; }
        public string TID { get; set; }
        public string IconSWF { get; set; }
        public string CollectEffect { get; set; }
        public string IconExportName { get; set; }
        public bool PremiumCurrency { get; set; }
        public string CapFullTID { get; set; }
        public int TextRed { get; set; }
        public int TextGreen { get; set; }
        public int TextBlue { get; set; }
        public int Cap { get; set; }
        public string IconFile { get; set; }
        public string ShopIcon { get; set; }
       	}
}

