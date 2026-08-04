namespace CRS.GameFiles
{
   
    

    internal class Predefined_Decks : Data
    {
        public Predefined_Decks(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
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
