using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Gamble_Chests : Data
	{
		public Gamble_Chests(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public int GoldPrice { get; set; }
		public string Location { get; set; }
	}
}