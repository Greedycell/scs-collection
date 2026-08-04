namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Hints : Data
    {
        public Hints(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public bool NotBeenInClan
        {
            get; set;
        }

        public bool NotBeenInTournament
        {
            get; set;
        }

        public bool NotCreatedTournament
        {
            get; set;
        }

        public int MinNpcWins
        {
            get; set;
        }

        public int MaxNpcWins
        {
            get; set;
        }

        public int MinArena
        {
            get; set;
        }

        public int MaxArena
        {
            get; set;
        }

        public int MinTrophies
        {
            get; set;
        }

        public int MaxTrophies
        {
            get; set;
        }

        public int MinExpLevel
        {
            get; set;
        }

        public int MaxExpLevel
        {
            get; set;
        }

        public string iOSTID
        {
            get; set;
        }

        public string AndroidTID
        {
            get; set;
        }
    }
}
