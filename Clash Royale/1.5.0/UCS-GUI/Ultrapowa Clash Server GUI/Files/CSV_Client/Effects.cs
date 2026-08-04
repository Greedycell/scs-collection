namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Effects : Data
    {
        public Effects(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public bool Loop
        {
            get; set;
        }

        public bool FollowParent
        {
            get; set;
        }

        public int ShakeScreen
        {
            get; set;
        }

        public int Time
        {
            get; set;
        }

        public int RenderableScale
        {
            get; set;
        }

        public string Sound
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ParticleEmitterName
        {
            get; set;
        }

        public string Effect
        {
            get; set;
        }

        public string Layer
        {
            get; set;
        }

        public int Scale
        {
            get; set;
        }

        public string TextInstanceName
        {
            get; set;
        }

        public string TextParentInstanceName
        {
            get; set;
        }

        public string EnemyVersion
        {
            get; set;
        }

        public int FlashWidth
        {
            get; set;
        }

        public bool KillLoopingSoundsOnEnd
        {
            get; set;
        }

        public string OutputEvent
        {
            get; set;
        }

        public int ParentLookAtOffsetRadius
        {
            get; set;
        }
    }
}
