using System;

namespace GL.Servers.CR.Files.CSV_Logic
     {
			using GL.Servers.CR.Files.CSV_Helpers;
			using GL.Servers.CR.Files.CSV_Reader;
     class Achievements : Data
       	{
       		public Achievements(CSVRow row, DataTable dt) : base(row, dt)
         	{
             	LoadData(this, this.GetType(), row);
         	}

        public string Name { get; set; }
        public int Level { get; set; }
        public string TID { get; set; }
        public string InfoTID { get; set; }
        public string Action { get; set; }
        public int ActionCount { get; set; }
        public int ExpReward { get; set; }
        public int DiamondReward { get; set; }
        public int SortIndex { get; set; }
        public bool Hidden { get; set; }
        public string AndroidID { get; set; }
        public string Type { get; set; }
       	}
}

