using UCS.GameFiles;

namespace UCS.Files.CSV_Reader
{
    #region Usings

    using System;
    using System.Collections.Generic;

    //using UCS.GameFiles;
    using UCS.Logic.Enums;

    #endregion Usings

    internal class Gamefiles : IDisposable
    {
        private readonly List<DataTable> DataTables = new List<DataTable>();

        /// <summary>
        /// Initialize a new instance of the <see cref="Gamefiles"/> class.
        /// </summary>
        public Gamefiles()
        {
            if (CSV.Gamefiles.Count > 0)
            {
                for (int i = 0; i < CSV.Gamefiles.Count; i++)
                {
                    this.DataTables.Add(new DataTable());
                }
            }
        }

        /// <summary>
        /// Get the DataTable at the specified index.
        /// </summary>
        /// <param name="_Index">The index.</param>
        /// <returns>The Data Table.</returns>
        public DataTable Get(Gamefile _Index)
        {
            return this.DataTables[(int) _Index - 1];
        }

        /// <summary>
        /// Initialize the specified table, using the specified index.
        /// </summary>
        /// <param name="_Table">The table.</param>
        /// <param name="_Index">The index.</param>
        public void Initialize(Table _Table, int _Index)
        {
            this.DataTables[_Index - 1] = new DataTable(_Table, _Index);
        }

        /// <summary>
        /// Dispose this instance.
        /// </summary>
        public void Dispose()
        {
            this.DataTables.Clear();
        }
    }
}