namespace UCS.Files.CSV_Helpers
{
    #region Usings

    using System.Collections.Generic;

    using UCS.Files.CSV_Logic;
    using UCS.Files.CSV_Reader;

    #endregion

    internal class DataTable
    {
        protected List<Data> Data   = null;
        protected int Index         = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="DataTable"/> class.
        /// </summary>
        public DataTable()
        {
            this.Data   = new List<Data>();
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="DataTable"/> class.
        /// </summary>
        /// <param name="_Table">The table.</param>
        /// <param name="_Index">The index.</param>
        public DataTable(Table _Table, int _Index)
        {
            this.Index  = _Index;
            this.Data   = new List<Data>();

            for (int i = 0; i < _Table.GetRowCount(); i++)
            {
                Row _Row    = _Table.GetRowAt(i);
                Data _Data  = this.Create(_Row);

                this.Data.Add(_Data);
            }
        }

        /// <summary>
        /// Get the count of data.
        /// </summary>
        /// <returns>The count.</returns>
        public int Count()
        {
            if (this.Data != null)
            {
                return this.Data.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Create the item.
        /// </summary>
        /// <param name="_Row">The row.</param>
        /// <returns>The Data.</returns>
        public Data Create(Row _Row)
        {
            Data _Data;

            switch (this.Index)
            {
                case 1:
                {
                    _Data = new Achievements(_Row, this);
                    break;
                }

                case 2:
                {
                    _Data = new Alliance_Badges(_Row, this);
                    break;
                }

                case 3:
                {
                    _Data = new Alliance_Roles(_Row, this);
                    break;
                }

                case 4:
                {
                    _Data = new Area_Effect_Objects(_Row, this);
                    break;
                }

                case 5:
                {
                    _Data = new Arenas(_Row, this);
                    break;
                }

                case 6:
                {
                    _Data = new Buildings(_Row, this);
                    break;
                }

                case 7:
                {
                    _Data = new Character_Buffs(_Row, this);
                    break;
                }

                case 8:
                {
                    _Data = new Characters(_Row, this);
                    break;
                }

                case 9:
                {
                    _Data = new Chest_Order(_Row, this);
                    break;
                }

                case 10:
                {
                    _Data = new Content_Tests(_Row, this);
                    break;
                }

                case 11:
                {
                    _Data = new Damage_Types(_Row, this);
                    break;
                }

                case 12:
                {
                    _Data = new Decos(_Row, this);
                    break;
                }

                case 13:
                {
                    _Data = new Exp_Levels(_Row, this);
                    break;
                }

                case 14:
                {
                    _Data = new Gamble_Chests(_Row, this);
                    break;
                }

                case 15:
                {
                    _Data = new Globals(_Row, this);
                    break;
                }

                case 16:
                {
                    _Data = new Locales(_Row, this);
                    break;
                }

                case 17:
                {
                    _Data = new Locations(_Row, this);
                    break;
                }

                case 18:
                {
                    _Data = new Npcs(_Row, this);
                    break;
                }

                case 19:
                {
                    _Data = new Predefined_Decks(_Row, this);
                    break;
                }

                case 20:
                {
                    _Data = new Projectiles(_Row, this);
                    break;
                }

                case 21:
                {
                    _Data = new Rarities(_Row, this);
                    break;
                }

                case 22:
                {
                    _Data = new Regions(_Row, this);
                    break;
                }

                case 23:
                {
                    _Data = new Resource_Packs(_Row, this);
                    break;
                }

                case 24:
                {
                    _Data = new Resources(_Row, this);
                    break;
                }

                case 25:
                {
                    _Data = new Shop(_Row, this);
                    break;
                }

                case 26:
                {
                    _Data = new Spawn_Points(_Row, this);
                    break;
                }

                case 27:
                {
                    _Data = new Spell_Sets(_Row, this);
                    break;
                }

                case 28:
                {
                    _Data = new Spells_Buildings(_Row, this);
                    break;
                }

                case 29:
                {
                    _Data = new Spells_Characters(_Row, this);
                    break;
                }

                case 30:
                {
                    _Data = new Spells_Other(_Row, this);
                    break;
                }

                case 31:
                {
                    _Data = new Survival_Modes(_Row, this);
                    break;
                }

                case 32:
                {
                    _Data = new Taunts(_Row, this);
                    break;
                }

                case 33:
                {
                    _Data = new Tournament_Tiers(_Row, this);
                    break;
                }

                case 34:
                {
                    _Data = new Treasure_Chests(_Row, this);
                    break;
                }

                case 35:
                {
                    _Data = new Tutorials_Home(_Row, this);
                    break;
                }

                case 36:
                {
                    _Data = new Tutorials_Npc(_Row, this);
                    break;
                }

                default:
                {
                    _Data = new Data(_Row, this);
                    break;
                }
            }

            return _Data;
        }

        /// <summary>
        /// Return all the datas available in the datatable.
        /// </summary>
        /// <returns>A list of data.</returns>
        public List<Data> GetDatas()
        {
            return this.Data;
        }

        public Data GetDataWithID(int _ID)
        {
            int _InstanceID = GlobalID.GetInstanceID(_ID);
            return this.Data[_InstanceID];
        }

        public Data GetDataWithInstanceID(int _ID)
        {
            return this.Data[_ID];
        }

        /// <summary>
        /// Get the data using the specified name.
        /// </summary>
        /// <param name="_Name">The name.</param>
        /// <returns>The Data.</returns>
        public Data GetData(string _Name)
        {
            return this.Data.Find(_Data => _Data.GetName() == _Name);
        }

        /// <summary>
        /// Get the index.
        /// </summary>
        /// <returns>The index.</returns>
        public int GetIndex()
        {
            return this.Index;
        }
    }
}