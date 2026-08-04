namespace CRS.Logic.Slots
{
    using System.Collections.Generic;

    using CRS.Logic.Slots.Items;

    internal class Members : Dictionary<long, Member>
    {
#pragma warning disable CS0649 // El campo 'Members.m_vAvatarId' nunca se asigna y siempre tendrá el valor predeterminado 0
        private long m_vAvatarId;
#pragma warning restore CS0649 // El campo 'Members.m_vAvatarId' nunca se asigna y siempre tendrá el valor predeterminado 0

        /// <summary>
        /// Initialize a new instance of the <see cref="Members"/> class.
        /// </summary>
        public Members() : base()
        {
            // Members.
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Members"/> class.
        /// </summary>
        /// <param name="_Max">The maximum of players in the list.</param>
        public Members(int _Max) : base(_Max)
        {
            // Members.
        }

#pragma warning disable CS0109 // El miembro 'Members.Add(Member)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Add the specified member to the clan.
        /// </summary>
        /// <param name="_Member">The member.</param>
        public new void Add(Member _Member)
#pragma warning restore CS0109 // El miembro 'Members.Add(Member)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            if (this.ContainsKey(_Member.PlayerID))
            {
                this[_Member.PlayerID] = _Member;
            }
            else
            {
                this.Add(_Member.PlayerID, _Member);
            }
        }

        /// <summary>
        /// Add the specified player to the clan.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public void Add(ClientAvatar _Player)
        {
            Member _Member = new Member(_Player);

            if (this.ContainsKey(_Member.PlayerID))
            {
                this[_Member.PlayerID] = _Member;
            }
            else
            {
                this.Add(_Member.PlayerID, _Member);
            }
        }

#pragma warning disable CS0109 // El miembro 'Members.Remove(ClientAvatar)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Remove the specified player from the clan.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public new void Remove(ClientAvatar _Player)
#pragma warning restore CS0109 // El miembro 'Members.Remove(ClientAvatar)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            if (this.ContainsKey(_Player.GetId()))
            {
                this.Remove(_Player.GetId());
            }
        }
        

#pragma warning disable CS0109 // El miembro 'Members.Remove(Member)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Remove the specified member from the clan.
        /// </summary>
        /// <param name="_Member">The member.</param>
        public new void Remove(Member _Member)
#pragma warning restore CS0109 // El miembro 'Members.Remove(Member)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            if (this.ContainsKey(_Member.PlayerID))
            {
                this.Remove(_Member.PlayerID);
            }
        }

        public long GetAvatarId()
        {
            return this.m_vAvatarId;
        }
    }
}
