using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Exp_Levels : Data
	{
		public Exp_Levels(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public int ExpToNextLevel { get; set; }
		public int SummonerLevel { get; set; }
		public int TowerLevel { get; set; }
		public int TroopLevel { get; set; }
		public int Decks { get; set; }
		public int SummonerKillGold { get; set; }
		public int TowerKillGold { get; set; }
		public int DiamondReward { get; set; }
	}
}