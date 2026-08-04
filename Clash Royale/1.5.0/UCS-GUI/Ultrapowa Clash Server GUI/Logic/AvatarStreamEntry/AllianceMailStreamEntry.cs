namespace CRS.Logic
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using CRS.Extensions.List;
    using CRS.Helpers;

    #endregion

    class AllianceMailStreamEntry : AvatarStreamEntry
    {
        private string m_vMessage;

        private long m_vSenderId;

        private string m_vAllianceName;

        private int m_vAllianceBadgeData;

        private long m_vAllianceId;

        public string GetMessage()
        {
            return this.m_vMessage;
        }

        public override int GetStreamEntryType()
        {
            return 6;
        }

        //00 00 00 02 
        //00 00 00 0E 
        //63 6F 72 72 69 67 C3 A9 20 66 61 75 74 65 
        //01 
        //00 00 00 15 00 58 4D 94 
        //00 00 00 00 
        //00 1A 8E 98 
        //00 00 00 0C 
        //74 68 65 20 38 30 30 20 63 6C 75 62 
        //5B 00 09 53
        public override byte[] Encode()
        {
            List<Byte> data = new List<Byte>();

            data.AddRange(base.Encode());
            data.AddInt32(2);
            data.AddString(this.m_vMessage);
            data.Add(1);
            data.AddInt64(this.m_vSenderId);
            data.AddInt64(this.m_vAllianceId);
            data.AddString(this.m_vAllianceName);
            data.AddInt32(this.m_vAllianceBadgeData);

            return data.ToArray();
        }

        public void SetAllianceId(long id)
        {
            this.m_vAllianceId = id;
        }

        public void SetAllianceName(string name)
        {
            this.m_vAllianceName = name;
        }

        public void SetAllianceBadgeData(int data)
        {
            this.m_vAllianceBadgeData = data;
        }

        public void SetMessage(string message)
        {
            this.m_vMessage = message;
        }

        public void SetSenderId(long id)
        {
            this.m_vSenderId = id;
        }
    }
}