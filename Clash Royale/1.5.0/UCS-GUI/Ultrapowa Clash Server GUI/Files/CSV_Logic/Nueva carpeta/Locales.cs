using System;

namespace GL.Servers.CR.Files.CSV_Logic
     {
			using GL.Servers.CR.Files.CSV_Helpers;
			using GL.Servers.CR.Files.CSV_Reader;
     class Locales : Data
       	{
       		public Locales(CSVRow row, DataTable dt) : base(row, dt)
         	{
             	LoadData(this, this.GetType(), row);
         	}

        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
        public bool HasEvenSpaceCharacters { get; set; }
        public string UsedSystemFont { get; set; }
        public string HelpshiftSDKLanguage { get; set; }
        public string TermsAndServiceUrl { get; set; }
        public string ParentsGuideUrl { get; set; }
        public string PrivacyPolicyUrl { get; set; }
        public string HelpshiftSDKLanguageAndroid { get; set; }
        public int SortOrder { get; set; }
        public bool TestLanguage { get; set; }
        public string TestExcludes { get; set; }
        public string RegionListFile { get; set; }
        public string RoyalBoxURL { get; set; }
        public string RoyalBoxStageURL { get; set; }
        public string BoomBoxURL { get; set; }
       	}
}

