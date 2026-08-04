namespace CRS.GameFiles
{
   
    

    internal class Decos : Data
    {
        public Decos(CSVRow row, DataTable dt) : base(row, dt)
        {
            this.LoadData(this, this.GetType(), row);
        }

        public string Name
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

        public string Layer
        {
            get; set;
        }

        public string LowendLayer
        {
            get; set;
        }

        public int ShadowScale
        {
            get; set;
        }

        public int ShadowX
        {
            get; set;
        }

        public int ShadowY
        {
            get; set;
        }

        public int ShadowSkew
        {
            get; set;
        }

        public int CollisionRadius
        {
            get; set;
        }

        public string Effect
        {
            get; set;
        }
    }
}
