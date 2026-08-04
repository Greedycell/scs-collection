namespace CRS.Files.CSV_Reader
{
    internal class Row
    {
        private readonly int RowStart;
        private readonly Table Table;

        public Row(Table _Table)
        {
            this.Table = _Table;
            this.RowStart = this.Table.GetColumnRowCount();

            this.Table.AddRow(this);
        }

        public int GetArraySize(string _Name)
        {
            int _Index = this.Table.GetColumnIndexByName(_Name);
            return _Index != -1 ? this.Table.GetArraySizeAt(this, _Index) : 0;
        }

        public string GetName()
        {
            return this.Table.GetValueAt(0, this.RowStart);
        }

        public int GetRowOffset()
        {
            return this.RowStart;
        }

        public string GetValue(string _Name, int _Level)
        {
            return this.Table.GetValue(_Name, _Level + this.RowStart);
        }
    }
}