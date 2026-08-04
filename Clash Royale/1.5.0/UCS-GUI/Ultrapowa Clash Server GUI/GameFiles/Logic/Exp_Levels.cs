namespace CRS.GameFiles
{
   
    

    internal class Exp_Levels : Data
    {
        public Exp_Levels(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public int ExpToNextLevel
        {
            get; set;
        }

        public int SummonerLevel
        {
            get; set;
        }

        public int TowerLevel
        {
            get; set;
        }

        public int TroopLevel
        {
            get; set;
        }

        public int Decks
        {
            get; set;
        }

        public int SummonerKillGold
        {
            get; set;
        }

        public int TowerKillGold
        {
            get; set;
        }

        public int DiamondReward
        {
            get; set;
        }
    }
}
