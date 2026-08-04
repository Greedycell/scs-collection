namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Treasure_Chests : Data
    {
        public Treasure_Chests(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string BaseChest
        {
            get; set;
        }

        public string Arena
        {
            get; set;
        }

        public bool InShop
        {
            get; set;
        }

        public bool InArenaInfo
        {
            get; set;
        }

        public bool TournamentChest
        {
            get; set;
        }

        public bool SurvivalChest
        {
            get; set;
        }

        public int ShopPriceWithoutSpeedUp
        {
            get; set;
        }

        public int TimeTakenDays
        {
            get; set;
        }

        public int TimeTakenHours
        {
            get; set;
        }

        public int TimeTakenMinutes
        {
            get; set;
        }

        public int TimeTakenSeconds
        {
            get; set;
        }

        public int RandomSpells
        {
            get; set;
        }

        public int DifferentSpells
        {
            get; set;
        }

        public int ChestCountInChestCycle
        {
            get; set;
        }

        public int RareChance
        {
            get; set;
        }

        public int EpicChance
        {
            get; set;
        }

        public int LegendaryChance
        {
            get; set;
        }

        public string GuaranteedSpells
        {
            get; set;
        }

        public int MinGoldPerCard
        {
            get; set;
        }

        public int MaxGoldPerCard
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ShopExportName
        {
            get; set;
        }

        public string GainedExportName
        {
            get; set;
        }

        public string AnimExportName
        {
            get; set;
        }

        public string OpenInstanceName
        {
            get; set;
        }

        public string SlotLandEffect
        {
            get; set;
        }

        public string OpenEffect
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public string TapSoundShop
        {
            get; set;
        }

        public string DescriptionTID
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string NotificationTID
        {
            get; set;
        }

        public string SpellSet
        {
            get; set;
        }

        public int Exp
        {
            get; set;
        }

        public int SortValue
        {
            get; set;
        }

        public bool SpecialOffer
        {
            get; set;
        }
    }
}
