namespace GL.Servers.CR.Files.CSV_Logic
{
    #region

    using CSV_Helpers;
    using CSV_Reader;

    #endregion

    internal class Projectiles : Data
    {
        public Projectiles(Row _Row, DataTable _DataTable)
            : base(_Row, _DataTable)
        {
            LoadData(this, this.GetType(), row);
        }

        public bool AoeToAir { get; set; }

        public bool AoeToGround { get; set; }

        public int BuffTime { get; set; }

        public int BuffTimeIncreasePerLevel { get; set; }

        public string ChainedHitEndEffect { get; set; }

        public int ChainedHitRadius { get; set; }

        public int ConstantHeight { get; set; }

        public int CrownTowerDamagePercent { get; set; }

        public int Damage { get; set; }

        public string DamageType { get; set; }

        public string DeathEffect { get; set; }

        public string ExportName { get; set; }

        public string FileName { get; set; }

        public int Gravity { get; set; }

        public int HealPercentage { get; set; }

        public bool HeightFromTargetRadius { get; set; }

        public string HitEffect { get; set; }

        public string HitSoundWhenParentAlive { get; set; }

        public bool Homing { get; set; }

        public int MaxDistance { get; set; }

        public int MaximumTargets { get; set; }

        public int MinDistance { get; set; }

        public string Name { get; set; }

        public bool OnlyEnemies { get; set; }

        public bool OnlyOwnTroops { get; set; }

        public string PingpongDeathEffect { get; set; }

        public int PingpongDelay { get; set; }

        public int PingpongSlowDistance { get; set; }

        public int PingpongVisualTime { get; set; }

        public int ProjectileRadius { get; set; }

        public int ProjectileRadiusY { get; set; }

        public int ProjectileRange { get; set; }

        public int Pushback { get; set; }

        public bool PushbackAll { get; set; }

        public int Radius { get; set; }

        public int RadiusY { get; set; }

        public string Rarity { get; set; }

        public string RedExportName { get; set; }

        public string RedShadowExportName { get; set; }

        public bool ShadowDisableRotate { get; set; }

        public string ShadowExportName { get; set; }

        public bool ShakesTargets { get; set; }

        public string SpawnAreaEffectObject { get; set; }

        public string SpawnCharacter { get; set; }

        public int SpawnCharacterCount { get; set; }

        public int SpawnCharacterDeployTime { get; set; }

        public int SpawnCharacterLevelIndex { get; set; }

        public string SpawnProjectile { get; set; }

        public int Speed { get; set; }

        public string TargetBuff { get; set; }

        public bool TargetToEdge { get; set; }

        public string TrailEffect { get; set; }

        public bool use360Frames { get; set; }
    }
}