using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Survival_Modes : Data
	{
		public Survival_Modes(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public string IconSWF { get; set; }
		public string IconExportName { get; set; }
		public string GameMode { get; set; }
		public string WinsIconExportName { get; set; }
		public bool Enabled { get; set; }
		public bool EventOnly { get; set; }
		public int JoinCost { get; set; }
		public string JoinCostResource { get; set; }
		public int FreePass { get; set; }
		public int MaxWins { get; set; }
		public int MaxLoss { get; set; }
		public int RewardCards { get; set; }
		public int RewardGold { get; set; }
		public int RewardSpellCount { get; set; }
		public string RewardSpell { get; set; }
		public string ItemExportName { get; set; }
		public string ConfirmExportName { get; set; }
		public string TID { get; set; }
	}
}