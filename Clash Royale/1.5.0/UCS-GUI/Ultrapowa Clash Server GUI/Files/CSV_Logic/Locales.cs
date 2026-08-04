namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Locales : Data
    {
        public Locales(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public bool HasEvenSpaceCharacters
        {
            get; set;
        }

        public string UsedSystemFont
        {
            get; set;
        }

        public string HelpshiftSDKLanguage
        {
            get; set;
        }

        public string TermsAndServiceUrl
        {
            get; set;
        }

        public string ParentsGuideUrl
        {
            get; set;
        }

        public string PrivacyPolicyUrl
        {
            get; set;
        }

        public string HelpshiftSDKLanguageAndroid
        {
            get; set;
        }

        public int SortOrder
        {
            get; set;
        }

        public bool TestLanguage
        {
            get; set;
        }

        public string TestExcludes
        {
            get; set;
        }

        public string RegionListFile
        {
            get; set;
        }
    }
}
