using System.Collections.Generic;
using System.IO;

namespace CRS.GameFiles
{
    class CSVTable
    {
        private List<CSVRow> m_vCSVRows;
        private List<string> m_vColumnHeaders;
        private List<string> m_vColumnTypes;
        private List<CSVColumn> m_vCSVColumns;

        public CSVTable(string filePath)
        {
            this.m_vCSVRows = new List<CSVRow>();
            this.m_vColumnHeaders = new List<string>();
            this.m_vColumnTypes = new List<string>();
            this.m_vCSVColumns = new List<CSVColumn>();

            using (var sr = new StreamReader(filePath))
            {
                var columns = sr.ReadLine().Replace("\"", string.Empty).Replace(" ", string.Empty).Split(',');
                foreach (var column in columns)
                {
                    this.m_vColumnHeaders.Add(column);
                    this.m_vCSVColumns.Add(new CSVColumn());
                }

                var types = sr.ReadLine().Replace("\"", string.Empty).Split(',');
                foreach (var type in types)
                {
                    this.m_vColumnTypes.Add(type);
                }

                while (!sr.EndOfStream)
                {
                    var values = sr.ReadLine().Replace("\"", string.Empty).Split(',');
                    
                    if(values[0] != string.Empty)
                    {
                        this.CreateRow();
                    }

                    for (int i = 0; i < this.m_vColumnHeaders.Count;i++ )
                    {
                        this.m_vCSVColumns[i].Add(values[i]);
                    }
                }
            }
        }

        public int GetArraySizeAt(CSVRow row, int columnIndex)
        {
            int rowIndex = this.m_vCSVRows.IndexOf(row);
            if (rowIndex == -1)
                return 0;
            CSVColumn c = this.m_vCSVColumns[columnIndex];
            int nextOffset = 0;
            if(rowIndex + 1 >= this.m_vCSVRows.Count)
            {
                nextOffset = c.GetSize();
            }
            else
            {
                CSVRow nextRow = this.m_vCSVRows[rowIndex + 1];
                nextOffset = nextRow.GetRowOffset();
            }

            int currentOffset = row.GetRowOffset();
            return c.GetArraySize(currentOffset, nextOffset);
        }

        public int GetRowCount()
        {
            return this.m_vCSVRows.Count;
        }

        public CSVRow GetRowAt(int index)
        {
            return this.m_vCSVRows[index];
        }

        public int GetColumnIndexByName(string name)
        {
            return this.m_vColumnHeaders.IndexOf(name);
        }

        public string GetColumnName(int index)
        {
            return this.m_vColumnHeaders[index];
        }

        public string GetValueAt(int column, int row)
        {
            return this.m_vCSVColumns[column].Get(row);
        }

        public string GetValue(string name, int level)
        {
            int index = this.m_vColumnHeaders.IndexOf(name);
            return this.GetValueAt(index, level);
        }

        public void CreateRow()
        {
            new CSVRow(this);
        }

        public void AddRow(CSVRow row)
        {
            this.m_vCSVRows.Add(row);
        }

        public int GetColumnRowCount()
        {
            int result = 0;
            if (this.m_vCSVColumns.Count > 0)
                result = this.m_vCSVColumns[0].GetSize();
            return result;
        }
    }

}
