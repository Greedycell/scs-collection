namespace CRS.Files.CSV_Client
{
    using CRS.GameFiles;

    internal class Sounds : Data
    {
        public Sounds(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public string FileNames
        {
            get; set;
        }

        public int MinVolume
        {
            get; set;
        }

        public int MaxVolume
        {
            get; set;
        }

        public int MinPitch
        {
            get; set;
        }

        public int MaxPitch
        {
            get; set;
        }

        public int Priority
        {
            get; set;
        }

        public int MaximumByType
        {
            get; set;
        }

        public int MaxRepeatMs
        {
            get; set;
        }

        public bool Loop
        {
            get; set;
        }

        public bool PlayVariationsInSequence
        {
            get; set;
        }

        public bool PlayVariationsInSequenceManualReset
        {
            get; set;
        }

        public int StartDelayMinMs
        {
            get; set;
        }

        public int StartDelayMaxMs
        {
            get; set;
        }

        public bool PlayOnlyWhenInView
        {
            get; set;
        }

        public int MaxVolumeScaleLimit
        {
            get; set;
        }

        public int NoSoundScaleLimit
        {
            get; set;
        }

        public int PadEmpyToEndMs
        {
            get; set;
        }
    }
}
