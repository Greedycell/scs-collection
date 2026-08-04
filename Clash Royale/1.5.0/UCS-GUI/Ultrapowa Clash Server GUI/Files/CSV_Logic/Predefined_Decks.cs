namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Predefined_Decks : Data
    {
        public Predefined_Decks(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string Spells
        {
            get; set;
        }

        public int SpellLevel
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }
    }
}
