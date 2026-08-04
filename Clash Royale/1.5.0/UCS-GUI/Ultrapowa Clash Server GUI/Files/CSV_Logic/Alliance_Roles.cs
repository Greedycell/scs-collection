namespace UCS.Files.CSV_Logic
{
    using UCS.GameFiles;
    using UCS.Files.CSV_Reader;

    internal class Alliance_Roles : Data
    {
        public Alliance_Roles(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, this.GetType(), row);
        }

        public string Name
        {
            get; set;
        }

        public int Level
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public bool CanInvite
        {
            get; set;
        }

        public bool CanSendMail
        {
            get; set;
        }

        public bool CanChangeAllianceSettings
        {
            get; set;
        }

        public bool CanAcceptJoinRequest
        {
            get; set;
        }

        public bool CanKick
        {
            get; set;
        }

        public bool CanBePromotedToLeader
        {
            get; set;
        }

        public bool CanPromoteToOwnLevel
        {
            get; set;
        }
    }
}
