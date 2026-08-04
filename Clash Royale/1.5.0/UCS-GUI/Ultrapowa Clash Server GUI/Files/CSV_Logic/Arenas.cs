namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Arenas : Data
    {
        public Arenas(CSVRow row, DataTable dt) : base(row, dt)
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

        public string SubtitleTID
        {
            get; set;
        }

        public int Arena
        {
            get; set;
        }

        public bool IsInUse
        {
            get; set;
        }

        public bool TrainingCamp
        {
            get; set;
        }

        public int TrophyLimit
        {
            get; set;
        }

        public int DemoteTrophyLimit
        {
            get; set;
        }

        public int ChestRewardMultiplier
        {
            get; set;
        }

        public int ChestShopPriceMultiplier
        {
            get; set;
        }

        public int RequestSize
        {
            get; set;
        }

        public int MaxDonationCountCommon
        {
            get; set;
        }

        public int MaxDonationCountRare
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

        public int MatchmakingMinTrophyDelta
        {
            get; set;
        }

        public int MatchmakingMaxTrophyDelta
        {
            get; set;
        }

        public int MatchmakingMaxSeconds
        {
            get; set;
        }

        public string PvpLocation
        {
            get; set;
        }

        public int DailyDonationCapacityLimit
        {
            get; set;
        }

        public int BattleRewardGold
        {
            get; set;
        }

        public string LoopingEffect
        {
            get; set;
        }

        public string LoopingEffectRegularTime
        {
            get; set;
        }

        public string LoopingEffectOvertime
        {
            get; set;
        }
    }
}
