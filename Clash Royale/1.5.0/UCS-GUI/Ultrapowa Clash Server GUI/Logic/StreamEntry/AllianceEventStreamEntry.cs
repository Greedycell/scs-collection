using System.Collections.Generic;

using CRS.Helpers;

namespace CRS.Logic
{
    using CRS.Extensions.List;

    class AllianceEventStreamEntry : StreamEntry
    {
        private string m_vAvatarName;
        private int m_vEventType;
        private long m_vAvatarId;

        public AllianceEventStreamEntry()
            : base()
        {
        }

        public override int GetStreamEntryType()
        {
            return 4;
        }

        // 00 00 00 04 
        // 00 00 00 00 
        // 85 7E 9B 70 
        // 03 
        // 00 00 00 19 00 6B 4A A9 
        // 00 00 00 19 00 6B 4A A9 
        // 00 00 00 0E 75 6D 61 72 E2 86 90 E2 86 92 62 6F 72 7A 
        // 00 00 00 39 
        // 00 00 00 06 
        // 00 00 00 01 
        // 00 00 00 00 
        // 00 00 00 01 
        // 00 00 00 15 00 58 4D 94 
        // 00 00 00 0C 69 69 69 69 69 69 69 69 69 69 69 69
        public override byte[] Encode()
        {
            List<byte> data = new List<byte>();

            data.AddRange(base.Encode());
            data.AddInt32(this.m_vEventType);
            data.AddInt64(this.m_vAvatarId);
            data.AddString(this.m_vAvatarName);

            return data.ToArray();
        }

        public void SetAvatarId(long id)
        {
            this.m_vAvatarId = id;
        }

        public void SetAvatarName(string name)
        {
            this.m_vAvatarName = name;
        }

        public void SetEventType(int type)
        {
            this.m_vEventType = type;
        }
    }    
}
