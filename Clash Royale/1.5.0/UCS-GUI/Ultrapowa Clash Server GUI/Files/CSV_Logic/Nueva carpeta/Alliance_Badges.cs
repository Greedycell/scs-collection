using System;

namespace GL.Servers.CR.Files.CSV_Logic
{
	using GL.Servers.CR.Files.CSV_Helpers;
	using GL.Servers.CR.Files.CSV_Reader;
	class Alliance_Badges : Data
	{
		public Alliance_Badges(CSVRow row, DataTable dt) : base(row, dt)
		{
			LoadData(this, this.GetType(), row);
		}

		public string Name { get; set; }
		public string IconSWF { get; set; }
		public string IconExportName { get; set; }
		public string Category { get; set; }
	}
}