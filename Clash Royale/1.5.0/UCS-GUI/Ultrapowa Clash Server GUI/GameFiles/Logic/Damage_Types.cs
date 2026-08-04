namespace CRS.GameFiles
{
   
    

    internal class Damage_Types : Data
    {
        public Damage_Types(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public int Efficiency
        {
            get; set;
        }

        public int NumberOfJumps
        {
            get; set;
        }

        public int MinBuffChance
        {
            get; set;
        }

        public int MaxBuffChance
        {
            get; set;
        }

        public string Buff
        {
            get; set;
        }

        public string DamageEffect
        {
            get; set;
        }

        public int BuffTime
        {
            get; set;
        }
    }
}
