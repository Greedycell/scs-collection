using System;

namespace GL.Servers.CR.Files.CSV_Logic
     {
			using GL.Servers.CR.Files.CSV_Helpers;
			using GL.Servers.CR.Files.CSV_Reader;
     class Npcs : Data
       	{
       		public Npcs(CSVRow row, DataTable dt) : base(row, dt)
         	{
             	LoadData(this, this.GetType(), row);
         	}

        public string Name { get; set; }
        public string Location { get; set; }
        public string PredefinedDeck { get; set; }
        public int Trophies { get; set; }
        public int ManaRegenMs { get; set; }
        public int ManaRegenMsEnd { get; set; }
        public int ManaRegenMsOvertime { get; set; }
        public int ExpLevel { get; set; }
        public bool CanReplay { get; set; }
        public string TID { get; set; }
        public int ExpReward { get; set; }
        public int Seed { get; set; }
        public bool FullDeckNotNeeded { get; set; }
        public int ManaReserve { get; set; }
        public int StartingMana { get; set; }
        public int WizardHpMultiplier { get; set; }
        public string StartTaunt { get; set; }
        public string OwnTowerDestroyedTaunt { get; set; }
        public bool HighlightTargetsOnManaFull { get; set; }
       	}
}

