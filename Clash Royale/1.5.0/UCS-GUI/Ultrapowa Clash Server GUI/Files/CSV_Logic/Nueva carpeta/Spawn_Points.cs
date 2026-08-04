using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Spawn_Points : Data
	{
		public Spawn_Points(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public string FileName { get; set; }
		public string ExportName { get; set; }
		public int FirstWait { get; set; }
		public string Characters { get; set; }
		public int Wait { get; set; }
		public bool DontLoop { get; set; }
	}
}