namespace UCS.Files.CSV_Helpers
{
    #region Usings

    using UCS.Files.CSV_Reader;

    using System;
    using System.Collections.Generic;
    using System.Reflection;

    #endregion Usings

    internal class Data
    {
        protected DataTable DataTable   = null;
        protected Row Row               = null;
        private readonly int ID         = 0;

        /// <summary>
        /// Initialize a new instance of the <see cref="Data"/> class.
        /// </summary>
        /// <param name="_Row">The row.</param>
        /// <param name="_DataTable">The data table.</param>
        public Data(Row _Row, DataTable _DataTable)
        {
            this.Row        = _Row;
            this.DataTable  = _DataTable;
            this.ID         = GlobalID.CreateGlobalID(_DataTable.GetIndex() + 1, _DataTable.Count());
        }

        public static void LoadData(Data _Data, Type _Type, Row _Row)
        {
            foreach (PropertyInfo _Property in _Type.GetProperties())
            {
                if (_Property.PropertyType.IsGenericType)
                {
                    Type ListType               = typeof(List<>);
                    Type[] Generic              = _Property.PropertyType.GetGenericArguments();
                    Type ConcreteType           = ListType.MakeGenericType(Generic);
                    object NewList              = Activator.CreateInstance(ConcreteType);
                    MethodInfo Add              = ConcreteType.GetMethod("Add");
                    string IndexerName          = ((DefaultMemberAttribute) NewList.GetType().GetCustomAttributes(typeof(DefaultMemberAttribute), true)[0]).MemberName;
                    PropertyInfo IndexProperty  = NewList.GetType().GetProperty(IndexerName);

                    for (int i = _Row.GetRowOffset(); i < _Row.GetRowOffset() + _Row.GetArraySize(_Property.Name); i++)
                    {
                        string _Value           = _Row.GetValue(_Property.Name, i - _Row.GetRowOffset());

                        if (_Value == string.Empty && i != _Row.GetRowOffset())
                        {
                            _Value = IndexProperty.GetValue(NewList, new object[]
                            {
                                i - _Row.GetRowOffset() - 1
                            }).ToString();
                        }

                        if (string.IsNullOrEmpty(_Value))
                        {
                            object _Object = Generic[0].IsValueType ? Activator.CreateInstance(Generic[0]) : string.Empty;

                            Add.Invoke(NewList, new[]
                            {
                                _Object
                            });
                        }
                        else
                        {
                            Add.Invoke(NewList, new[]
                            {
                                Convert.ChangeType(_Value, Generic[0])
                            });
                        }
                    }

                    _Property.SetValue(_Data, NewList);
                }
                else
                {
                    _Property.SetValue(_Data, _Row.GetValue(_Property.Name, 0) == string.Empty ? null : Convert.ChangeType(_Row.GetValue(_Property.Name, 0), _Property.PropertyType), null);
                }
            }
        }

        public int GetDataType()
        {
            return this.DataTable.GetIndex() - 3;
        }

        /// <summary>
        /// Get the global identifier.
        /// </summary>
        /// <returns>The Global Identifier.</returns>
        public int GetGlobalID()
        {
            return this.ID;
        }

        /// <summary>
        /// Get the instance identifier.
        /// </summary>
        /// <returns>The instance indentifier.</returns>
        public int GetInstanceID()
        {
            return GlobalID.GetInstanceID(this.ID);
        }

        /// <summary>
        /// Get the name.
        /// </summary>
        /// <returns>The name.</returns>
        public string GetName()
        {
            return this.Row.GetName();
        }
    }
}