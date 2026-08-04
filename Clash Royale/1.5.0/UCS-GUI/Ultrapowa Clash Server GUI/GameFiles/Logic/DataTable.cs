namespace CRS.GameFiles
{
    #region Usings
    using System.Collections.Generic;
    using Logic;

    #endregion

    class DataTable
    {
        protected List<Data> m_vData;
        protected int m_vIndex;

        /// <summary>
        /// Initialize a new instance of the <see cref="DataTable"/> class.
        /// </summary>
        public DataTable()
        {
            this.m_vIndex = 0;
            this.m_vData = new List<Data>();
        }

        public DataTable(CSVTable table, int index)
        {
            this.m_vIndex = index;
            this.m_vData = new List<Data>();

            for (int i = 0; i < table.GetRowCount(); i++)
            {
                var row = table.GetRowAt(i);
                var data = this.CreateItem(row);
                this.m_vData.Add(data);
            }
        }

        /// <summary>
        /// Create the item.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>The Data.</returns>
        public Data CreateItem(CSVRow row)
        {
            Data _Data;

            // Data _Data = new Data(row, this);
            switch (this.m_vIndex)
            {
                case 1:
                {
                    _Data = new Achievements(row, this);
                    break;
                }

                case 2:
                {
                    _Data = new Alliance_Badges(row, this);
                    break;
                }

                case 3:
                {
                    _Data = new Alliance_Roles(row, this);
                    break;
                }

                case 4:
                {
                    _Data = new Area_Effect_Objects(row, this);
                    break;
                }

                case 5:
                {
                    _Data = new Arenas(row, this);
                    break;
                }

                case 6:
                {
                    _Data = new Buildings(row, this);
                    break;
                }

                case 7:
                {
                    _Data = new Character_Buffs(row, this);
                    break;
                }

                case 8:
                {
                    _Data = new CharacterData(row, this);
                    break;
                }

                case 9:
                {
                    _Data = new Chest_Order(row, this);
                    break;
                }

                case 10:
                {
                    _Data = new Content_Tests(row, this);
                    break;
                }

                case 11:
                {
                    _Data = new Damage_Types(row, this);
                    break;
                }

                case 12:
                {
                    _Data = new Decos(row, this);
                    break;
                }

                case 13:
                {
                    _Data = new Exp_Levels(row, this);
                    break;
                }

                case 14:
                {
                    _Data = new Gamble_Chests(row, this);
                    break;
                }

                case 15:
                {
                    _Data = new GlobalData(row, this);
                    break;
                }

                case 16:
                {
                    _Data = new Locales(row, this);
                    break;
                }

                case 17:
                {
                    _Data = new Locations(row, this);
                    break;
                }

                case 18:
                {
                    _Data = new Npcs(row, this);
                    break;
                }

                case 19:
                {
                    _Data = new Predefined_Decks(row, this);
                    break;
                }

                case 20:
                {
                    _Data = new Projectiles(row, this);
                    break;
                }

                case 21:
                {
                    _Data = new Rarities(row, this);
                    break;
                }

                case 22:
                {
                    _Data = new Regions(row, this);
                    break;
                }

                case 23:
                {
                    _Data = new Resource_Packs(row, this);
                    break;
                }

                case 24:
                {
                    _Data = new ResourceData(row, this);
                    break;
                }

                case 25:
                {
                    _Data = new Shop(row, this);
                    break;
                }

                case 26:
                {
                    _Data = new Spawn_Points(row, this);
                    break;
                }

                case 27:
                {
                    _Data = new Spell_Sets(row, this);
                    break;
                }

                case 28:
                {
                    _Data = new Spells_Buildings(row, this);
                    break;
                }

                case 29:
                {
                    _Data = new Spells_Characters(row, this);
                    break;
                }

                case 30:
                {
                    _Data = new Spells_Other(row, this);
                    break;
                }

                case 31:
                {
                    _Data = new Survival_Modes(row, this);
                    break;
                }

                case 32:
                {
                    _Data = new Taunts(row, this);
                    break;
                }

                case 33:
                {
                    _Data = new Tournament_Tiers(row, this);
                    break;
                }

                case 34:
                {
                    _Data = new Treasure_Chests(row, this);
                    break;
                }

                case 35:
                {
                    _Data = new Tutorials_Home(row, this);
                    break;
                }

                case 36:
                {
                    _Data = new Tutorials_Npc(row, this);
                    break;
                }

                default:
                {
                    _Data = new Data(row, this);
                    break;
                }

                // default:
                // break;
            }

            return _Data;
        }

        /// <summary>
        /// Return all the datas available in the datatable.
        /// </summary>
        /// <returns>A list of data.</returns>
        public int GetTableIndex()
        {
            return this.m_vIndex;
        }

        public Data GetItemAt(int index)
        {
            return this.m_vData[index];
        }

        public Data GetItemById(int id)
        {
            int instanceId = GlobalID.GetInstanceID(id);
            return this.m_vData[instanceId];
        }

        public int GetItemCount()
        {
            return this.m_vData.Count;
        }

        public Data GetDataByName(string name)
        {
            return this.m_vData.Find(d => d.GetName() == name);
        }

    }
}