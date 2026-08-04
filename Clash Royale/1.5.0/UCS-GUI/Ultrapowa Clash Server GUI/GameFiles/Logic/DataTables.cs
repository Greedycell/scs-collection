namespace CRS.GameFiles
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using CRS.Core;
    using CRS.Logic;

    #endregion

    internal class DataTables : IDisposable
    {
        private List<DataTable> m_vDataTables;

        public DataTables()
        {
            this.m_vDataTables = new List<DataTable>();

            // for (int i = 0; i < 35; i++)
            // m_vDataTables.Add(new DataTable());
            if (ObjectManager.GameFiles.Count > 0)
            {
                for (int i = 0; i < ObjectManager.GameFiles.Count; i++)
                {
                    this.m_vDataTables.Add(new DataTable());
                }
            }
        }

        /// <summary>
        /// <see cref="Dispose"/> this instance.
        /// </summary>
        public void Dispose()
        {
            this.m_vDataTables.Clear();
        }

        public CharacterData GetCharacterByName(string name)
        {
            DataTable dt = this.m_vDataTables[8];
            return (CharacterData)dt.GetDataByName(name);
        }

        public Data GetDataById(int id)
        {
            int classId = GlobalID.GetClassID(id) - 1;
            DataTable dt = this.m_vDataTables[classId];
            return dt.GetItemById(id);
        }

        public Globals GetGlobals()
        {
            return (Globals)this.m_vDataTables[15];
        }

        // public HeroData GetHeroByName(string name)
        // {
        // DataTable dt = m_vDataTables[8];
        // return (HeroData)dt.GetDataByName(name);
        // }
        public ResourceData GetResourceByName(string name)
        {
            DataTable dt = this.m_vDataTables[24];
            return (ResourceData)dt.GetDataByName(name);
        }

        public DataTable GetTable(int i)
        {
            return this.m_vDataTables[i];
        }

        public void InitDataTable(CSVTable t, int index)
        {
            if (index == 15)
            {
                this.m_vDataTables[index - 1] = new Globals(t, index);
            }
            else
            {
                this.m_vDataTables[index - 1] = new DataTable(t, index);
            }
        }
    }
}