namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Survival_Modes : Data
    {
        public Survival_Modes(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string WinsIconExportName
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        public int JoinCost
        {
            get; set;
        }

        public string JoinCostResource
        {
            get; set;
        }

        public int MaxWins
        {
            get; set;
        }

        public int MaxLoss
        {
            get; set;
        }

        public int RewardCards
        {
            get; set;
        }

        public int RewardGold
        {
            get; set;
        }
    }
}
