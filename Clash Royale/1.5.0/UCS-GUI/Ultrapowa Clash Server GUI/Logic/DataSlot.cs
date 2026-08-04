namespace CRS.Logic
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using CRS.GameFiles;
    using CRS.Helpers;

    using Newtonsoft.Json.Linq;

    using CRS.Core;
    using CRS.Extensions.Binary;

    #endregion

    internal class DataSlot
    {
        public Data Data;

        public int Value;

        public DataSlot(Data d, int value)
        {
            this.Data = d;
            this.Value = value;
        }

        public void Decode(Reader br)
        {
            this.Data = br.ReadDataReference();
            this.Value = br.ReadInt32WithEndian();
            Debug.Write(this.Data);
            Debug.Write(this.Value);
        }

        public byte[] Encode()
        {
            List<Byte> data = new List<Byte>();
            data.AddInt32(this.Data.GetGlobalID());
            data.AddInt32(this.Value);
            return data.ToArray();
        }

        public JObject Save(JObject jsonObject)
        {
            jsonObject.Add("global_id", this.Data.GetGlobalID());
            jsonObject.Add("value", this.Value);
            return jsonObject;
        }

        public void Load(JObject jsonObject)
        {
            this.Data = ObjectManager.DataTables.GetDataById(jsonObject["global_id"].ToObject<int>());
            this.Value = jsonObject["value"].ToObject<int>();
        }
    }
}