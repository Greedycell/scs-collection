namespace CRS.GameFiles
{
   
    

    internal class Spell_Sets : Data
    {
        public Spell_Sets(CSVRow row, DataTable dt) : base(row, dt)
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
    }
}
