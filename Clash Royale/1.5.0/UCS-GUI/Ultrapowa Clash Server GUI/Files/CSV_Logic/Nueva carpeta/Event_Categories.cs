using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Event_Categories : Data
	{
		public Event_Categories(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public string CSVFiles { get; set; }
		public string CSVRows { get; set; }
		public string CustomNames { get; set; }
	}
}