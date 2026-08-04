using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Content_Tests : Data
	{
		public Content_Tests(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public string SourceData { get; set; }
		public string TargetData { get; set; }
		public string Stat1 { get; set; }
		public string Operator { get; set; }
		public string Stat2 { get; set; }
		public int Result { get; set; }
		public bool Enabled { get; set; }
	}
}