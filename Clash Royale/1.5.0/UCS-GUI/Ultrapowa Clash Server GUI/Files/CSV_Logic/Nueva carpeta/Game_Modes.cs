using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Game_Modes : Data
	{
		public Game_Modes(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public string TID { get; set; }
		public string CardLevelAdjustment { get; set; }
		public string DeckSelection { get; set; }
		public int OvertimeSeconds { get; set; }
		public string PredefinedDecks { get; set; }
		public int ElixirProductionMultiplier { get; set; }
		public int ElixirProductionOvertimeMultiplier { get; set; }
		public bool UseStartingElixir { get; set; }
		public int StartingElixir { get; set; }
		public bool AdditionalBridge { get; set; }
		public bool Heroes { get; set; }
		public string ForcedDeckCards { get; set; }
		public string HelpExportName { get; set; }
	}
}