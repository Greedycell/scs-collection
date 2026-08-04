namespace CRS.Logic
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using CRS.Extensions.List;
    using CRS.Helpers;

    #endregion

    internal class StreamEntry
    {
        private int m_vId;

        private long m_vSenderId;

        private long m_vHomeId;

        private string m_vSenderName;

        private long m_vSenderLeagueId;

        private int m_vSenderLevel;

        private int m_vSenderRole;

        private DateTime m_vMessageTime;

        public StreamEntry()
        {
            this.m_vMessageTime = DateTime.UtcNow;
        }

        public int GetAgeSeconds()
        {
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                   - (int)this.m_vMessageTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public int GetId()
        {
            return this.m_vId;
        }

        public long GetHomeId()
        {
            return this.m_vHomeId;
        }

        public long GetSenderLeagueId()
        {
            return this.m_vSenderLeagueId;
        }

        public long GetSenderId()
        {
            return this.m_vSenderId;
        }

        public int GetSenderLevel()
        {
            return this.m_vSenderLevel;
        }

        public string GetSenderName()
        {
            return this.m_vSenderName;
        }

        public int GetSenderRole()
        {
            return this.m_vSenderRole;
        }

        public virtual int GetStreamEntryType()
        {
            return -1;
        }

        public virtual byte[] Encode()
        {
            List<byte> data = new List<byte>();

            data.AddInt32(this.GetStreamEntryType()); // chatstreamentry
            data.AddInt32(0);
            data.AddInt32(this.m_vId);
            data.Add(3);
            data.AddInt64(this.m_vSenderId);
            data.AddInt64(this.m_vHomeId);
            data.AddString(this.m_vSenderName);
            data.AddInt32(this.m_vSenderLevel);
            data.AddInt64(this.m_vSenderLeagueId);
            data.AddInt32(this.m_vSenderRole);
            data.AddInt32(this.GetAgeSeconds());

            return data.ToArray();
        }

        public void SetAvatar(ClientAvatar avatar)
        {
            this.m_vSenderId = avatar.GetId();
            this.m_vHomeId = avatar.GetId();
            this.m_vSenderName = avatar.GetName();
            this.m_vSenderLeagueId = avatar.GetClanID();
            this.m_vSenderLevel = avatar.GetLevel();
            this.m_vSenderRole = 1;
        }

        public void SetHomeId(long id)
        {
            this.m_vHomeId = id;
        }

        public void SetId(int id)
        {
            this.m_vId = id;
        }

        public void SetSenderId(long id)
        {
            this.m_vSenderId = id;
        }

        public void SetSenderLeagueId(int leagueId)
        {
            this.m_vSenderLeagueId = leagueId;
        }

        public void SetSenderLevel(int level)
        {
            this.m_vSenderLevel = level;
        }

        public void SetSenderName(string name)
        {
            this.m_vSenderName = name;
        }

        public void SetSenderRole(int role)
        {
            this.m_vSenderRole = role;
        }
    }
}