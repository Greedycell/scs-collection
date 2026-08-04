namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Damage_Types : Data
    {
        public Damage_Types(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
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
