namespace CRS.Logic
{
    #region Usings

    using System.Collections.Generic;
    using System.IO;

    using CRS.Core;
    using CRS.Extensions.List;
    using CRS.Helpers;

    using Newtonsoft.Json.Linq;

    #endregion

    internal class AllianceMemberEntry
    {
        // private long m_vHomeId;
        private long m_vAvatarId;

        // private string m_vName;
        private int m_vRole; // 1 : member, 2 : chef, 3 : aîné, 4 : chef adjoint

        private readonly int[] m_vRoleTable = { 1, 1, 4, 2, 3 }; // mapping roles so comparison is easier

        // private int m_vExpLevel;
        // private int m_vScore;
        private int m_vOrder;

        private int m_vPreviousOrder;

        private int m_vReceivedTroops;

        private int m_vDonatedTroops;

        private byte m_vIsNewMember;

        private int m_vWarCooldown;

        private int m_vWarOptInStatus;

        public AllianceMemberEntry(long avatarId)
        {
            this.m_vAvatarId = avatarId;
            this.m_vIsNewMember = 0;
            this.m_vOrder = 1;
            this.m_vPreviousOrder = 1;
            this.m_vRole = 1;
            this.m_vDonatedTroops = 200;
            this.m_vReceivedTroops = 100;
            this.m_vWarCooldown = 0;
            this.m_vWarOptInStatus = 1;
        }

        public void Decode(byte[] avatarData)
        {
            using (var br = new BinaryReader(new MemoryStream(avatarData)))
            {
            }
        }

        // 00 00 00 2A 00 17 E8 BD 
        // 00 00 00 06 
        // 6B 61 69 73 65 72 
        // 00 00 00 02 
        // 00 00 00 58 
        // 00 00 00 00 
        // 00 00 0B 0C 
        // 00 00 00 83 
        // 00 00 00 5B 
        // 00 00 00 01 
        // 00 00 00 01 
        // 00 
        // 00 01 15 7A
        // 00 00 00 01 
        // 01 
        // 00 00 00 2A 00 17 E8 BD
        public byte[] Encode()
        {
            List<byte> data = new List<byte>();

            Level avatar = ResourcesManager.GetPlayer(this.m_vAvatarId);
            data.AddInt64(this.m_vAvatarId);
            data.AddString(avatar.GetPlayerAvatar().GetName());
            data.AddInt32(this.m_vRole);
            data.AddInt32(avatar.GetPlayerAvatar().GetLevel());
            data.AddInt64(avatar.GetPlayerAvatar().GetClanID());

            // data.AddInt32(avatar.GetPlayerAvatar().GetScore());
            data.AddInt32(this.m_vDonatedTroops);
            data.AddInt32(this.m_vReceivedTroops);
            data.AddInt32(this.m_vOrder);
            data.AddInt32(this.m_vPreviousOrder);
            data.Add(this.m_vIsNewMember);
            data.AddInt32(this.m_vWarCooldown);
            data.AddInt32(this.m_vWarOptInStatus);
            data.Add(1);
            data.AddInt64(this.m_vAvatarId);

            return data.ToArray();
        }

        public long GetAvatarId()
        {
            return this.m_vAvatarId;
        }

        public int GetDonations()
        {
            return 150;
        }

        /*public long GetHomeid()
        {
            return m_vHomeId;
        }

        public int GetExpLevel()
        {
            return m_vExpLevel;
        }

        public string GetName()
        {
            return m_vName;
        }*/

        public int GetOrder()
        {
            return this.m_vOrder;
        }

        public int GetPreviousOrder()
        {
            return this.m_vPreviousOrder;
        }

        public int GetRole()
        {
            return this.m_vRole;
        }

        /*public int GetScore()
        {
            return m_vScore;
        }*/

        public bool HasLowerRoleThan(int role)
        {
            bool result = true;
            if (role < this.m_vRoleTable.Length && this.m_vRole < this.m_vRoleTable.Length)
            {
                if (this.m_vRoleTable[this.m_vRole] >= this.m_vRoleTable[role]) result = false;
            }

            return result;
        }

        public byte IsNewMember()
        {
            return this.m_vIsNewMember;
        }

        public JObject Save(JObject jsonObject)
        {
            jsonObject.Add("avatar_id", this.m_vAvatarId);
            jsonObject.Add("role", this.m_vRole);
            return jsonObject;
        }

        public void Load(JObject jsonObject)
        {
            this.m_vAvatarId = jsonObject["avatar_id"].ToObject<long>();
            this.m_vRole = jsonObject["role"].ToObject<int>();
        }

        public void SetAvatarId(long id)
        {
            this.m_vAvatarId = id;
        }

        /*public void SetExpLevel(int level)
        {
            m_vExpLevel = level;
        }

        public void SetHomeId(long id)
        {
            m_vHomeId = id;
        }

        public void SetName(string name)
        {
            m_vName = name;
        }*/

        public void SetOrder(int order)
        {
            this.m_vOrder = order;
        }

        public void SetPreviousOrder(int order)
        {
            this.m_vPreviousOrder = order;
        }

        public void SetRole(int role)
        {
            this.m_vRole = role;
        }

        /*public void SetScore(int score)
        {
            m_vScore = score;
        } */
    }
}