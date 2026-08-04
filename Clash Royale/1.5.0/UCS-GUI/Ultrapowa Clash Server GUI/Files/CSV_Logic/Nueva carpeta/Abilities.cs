using System;

namespace GL.Servers.CR.Files.CSV_Logic
     {
			using GL.Servers.CR.Files.CSV_Helpers;
			using GL.Servers.CR.Files.CSV_Reader;
     class Abilities : Data
       	{
       		public Abilities(CSVRow row, DataTable dt) : base(row, dt)
         	{
             	LoadData(this, this.GetType(), row);
         	}

        public string Name { get; set; }
        public string IconFile { get; set; }
        public string TID { get; set; }
        public string AreaEffectObject { get; set; }
        public string Buff { get; set; }
        public int BuffTime { get; set; }
        public string Effect { get; set; }
       	}
}

