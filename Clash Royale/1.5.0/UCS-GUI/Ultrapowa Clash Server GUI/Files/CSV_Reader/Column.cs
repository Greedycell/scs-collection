namespace CRS.Files.CSV_Reader
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;

    #endregion Usings

    internal class Column
    {
        private readonly List<string> Values;

        public Column()
        {
            this.Values = new List<string>();
        }

        public static int GetArraySize(int _Offset, int _NOffset)
        {
            return _NOffset - _Offset;
        }

        public void Add(string _Value)
        {
            if (_Value == null)
            {
                this.Values.Add(this.Values.Count > 0 ? this.Values.Last() : string.Empty);
            }
            else
            {
                this.Values.Add(_Value);
            }
        }

        public string Get(int _Row)
        {
            return this.Values[_Row];
        }

        public int GetSize()
        {
            return this.Values.Count;
        }
    }
}