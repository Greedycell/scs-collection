namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Spells_Other : Data
    {
        public Spells_Other(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string IconFile
        {
            get; set;
        }

        public string UnlockArena
        {
            get; set;
        }

        public string Rarity
        {
            get; set;
        }

        public int ManaCost
        {
            get; set;
        }

        public bool ManaCostFromSummonerMana
        {
            get; set;
        }

        public bool NotInUse
        {
            get; set;
        }

        public bool Mirror
        {
            get; set;
        }

        public int CustomDeployTime
        {
            get; set;
        }

        public string SummonCharacter
        {
            get; set;
        }

        public int SummonNumber
        {
            get; set;
        }

        public int SummonCharacterLevelIndex
        {
            get; set;
        }

        public string SummonCharacterSecond
        {
            get; set;
        }

        public int SummonRadius
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        public string Projectile
        {
            get; set;
        }

        public bool ProjectileAsDeploy
        {
            get; set;
        }

        public int InstantDamage
        {
            get; set;
        }

        public int DurationSeconds
        {
            get; set;
        }

        public int InstantHeal
        {
            get; set;
        }

        public int HealPerSecond
        {
            get; set;
        }

        public string Effect
        {
            get; set;
        }

        public string DamageType
        {
            get; set;
        }

        public int Pushback
        {
            get; set;
        }

        public int MultipleProjectiles
        {
            get; set;
        }

        public string CustomFirstProjectile
        {
            get; set;
        }

        public int BuffTime
        {
            get; set;
        }

        public int BuffTimeIncreasePerLevel
        {
            get; set;
        }

        public int BuffNumber
        {
            get; set;
        }

        public string BuffType
        {
            get; set;
        }

        public string BuffOnDamage
        {
            get; set;
        }

        public bool OnlyOwnTroops
        {
            get; set;
        }

        public bool OnlyEnemies
        {
            get; set;
        }

        public bool CanDeployOnEnemySide
        {
            get; set;
        }

        public string CastSound
        {
            get; set;
        }

        public string AreaEffectObject
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string TID_INFO
        {
            get; set;
        }

        public string IndicatorEffect
        {
            get; set;
        }

        public string DestIndicatorEffect
        {
            get; set;
        }

        public string ReleaseDate
        {
            get; set;
        }

        public int ElixirProductionStopTime
        {
            get; set;
        }
    }
}
