namespace CRS.Files.CSV_Reader
{
    #region Usings

    using System.Collections.Generic;
    using System.IO;

    #endregion Usings

    internal class Table
    {
        private readonly List<Column> Columns;
        private readonly List<string> Headers;
        private readonly List<Row> Rows;
        private readonly List<string> Types;

        public Table(string _Path)
        {
            this.Rows = new List<Row>();
            this.Headers = new List<string>();
            this.Types = new List<string>();
            this.Columns = new List<Column>();

            using (StreamReader _Reader = new StreamReader(_Path))
            {
                string[] _Columns = _Reader.ReadLine().Replace("\"", string.Empty).Replace(" ", string.Empty).Split(',');
                foreach (string _Column in _Columns)
                {
                    this.Headers.Add(_Column);
                    this.Columns.Add(new Column());
                }

                string[] types = _Reader.ReadLine().Replace("\"", string.Empty).Split(',');
                foreach (string type in types)
                {
                    this.Types.Add(type);
                }

                while (!_Reader.EndOfStream)
                {
                    string[] _Values = _Reader.ReadLine().Replace("\"", string.Empty).Split(',');

                    if (!string.IsNullOrEmpty(_Values[0]))
                    {
                        new Row(this);
                    }

                    for (int i = 0; i < this.Headers.Count; i++)
                    {
                        this.Columns[i].Add(_Values[i]);
                    }
                }
            }
        }

        public void AddRow(Row _Row)
        {
            this.Rows.Add(_Row);
        }

        public int GetArraySizeAt(Row row, int columnIndex)
        {
            int _Index = this.Rows.IndexOf(row);
            if (_Index == -1)
            {
                return 0;
            }

            Column c = this.Columns[columnIndex];
            int _NextOffset = 0;
            if (_Index + 1 >= this.Rows.Count)
            {
                _NextOffset = c.GetSize();
            }
            else
            {
                Row _NextRow = this.Rows[_Index + 1];
                _NextOffset = _NextRow.GetRowOffset();
            }

            int _Offset = row.GetRowOffset();
            return Column.GetArraySize(_Offset, _NextOffset);
        }

        public int GetColumnIndexByName(string _Name)
        {
            return this.Headers.IndexOf(_Name);
        }

        public string GetColumnName(int _Index)
        {
            return this.Headers[_Index];
        }

        public int GetColumnRowCount()
        {
            if (this.Columns.Count > 0)
            {
                return this.Columns[0].GetSize();
            }

            return 0;
        }

        public Row GetRowAt(int _Index)
        {
            return this.Rows[_Index];
        }

        public int GetRowCount()
        {
            return this.Rows.Count;
        }

        public string GetValue(string _Name, int _Level)
        {
            int _Index = this.Headers.IndexOf(_Name);
            return this.GetValueAt(_Index, _Level);
        }

        public string GetValueAt(int _Column, int _Row)
        {
            return this.Columns[_Column].Get(_Row);
        }
    }
}