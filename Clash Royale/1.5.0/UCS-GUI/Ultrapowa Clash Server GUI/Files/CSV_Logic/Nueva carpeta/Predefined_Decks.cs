using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Predefined_Decks : Data
	{
		public Predefined_Decks(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public string Spells { get; set; }
		public int SpellLevel { get; set; }
		public string RandomSpellSets { get; set; }
		public string Description { get; set; }
		public string TID { get; set; }
	}
}