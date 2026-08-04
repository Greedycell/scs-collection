using System;

namespace GL.Servers.CR.Files.CSV_Logic
     {
			using GL.Servers.CR.Files.CSV_Helpers;
			using GL.Servers.CR.Files.CSV_Reader;
     class Rarities : Data
       	{
       		public Rarities(CSVRow row, DataTable dt) : base(row, dt)
         	{
             	LoadData(this, this.GetType(), row);
         	}

        public string Name { get; set; }
        public int LevelCount { get; set; }
        public int RelativeLevel { get; set; }
        public int MirrorRelativeLevel { get; set; }
        public int CloneRelativeLevel { get; set; }
        public int DonateCapacity { get; set; }
        public int SortCapacity { get; set; }
        public int DonateReward { get; set; }
        public int DonateXP { get; set; }
        public int GoldConversionValue { get; set; }
        public int ChanceWeight { get; set; }
        public int BalanceMultiplier { get; set; }
        public int UpgradeExp { get; set; }
        public int UpgradeMaterialCount { get; set; }
        public int UpgradeCost { get; set; }
        public int PowerLevelMultiplier { get; set; }
        public int RefundGems { get; set; }
        public string TID { get; set; }
        public string CardBaseFileName { get; set; }
        public string BigFrameExportName { get; set; }
        public string CardBaseExportName { get; set; }
        public string StackedCardExportName { get; set; }
        public string CardRewardExportName { get; set; }
        public string CastEffect { get; set; }
        public string InfoTitleExportName { get; set; }
        public string CardRarityBGExportName { get; set; }
        public int SortOrder { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public string AppearEffect { get; set; }
        public string BuySound { get; set; }
        public string LoopEffect { get; set; }
        public int CardTxtBgFrameIdx { get; set; }
        public string CardGlowInstanceName { get; set; }
        public string SpellSelectedSound { get; set; }
        public string SpellAvailableSound { get; set; }
        public string RotateExportName { get; set; }
       	}
}

