namespace CRS.Logic.Slots
{
    using System.Collections.Generic;

    internal class Clans : Dictionary<long, Clan>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Clans"/> class.
        /// </summary>
        public Clans() : base()
        {
            // Clans.
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Clans"/> class.
        /// </summary>
        /// <param name="_Max">The maximum of clans in the list.</param>
        public Clans(int _Max) : base(_Max)
        {
            // Clans.
        }

#pragma warning disable CS0109 // El miembro 'Clans.Add(Clan)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Add the specified clan to the list.
        /// </summary>
        /// <param name="_Clan">The clan.</param>
        public new void Add(Clan _Clan)
#pragma warning restore CS0109 // El miembro 'Clans.Add(Clan)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            if (this.ContainsKey(_Clan.ClanID))
            {
                this[_Clan.ClanID] = _Clan;
            }
            else
            {
                this.Add(_Clan.ClanID, _Clan);
            }
        }

#pragma warning disable CS0109 // El miembro 'Clans.Remove(Clan)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Remove the specified clan from the list.
        /// </summary>
        /// <param name="_Clan">The clan.</param>
        public new void Remove(Clan _Clan)
#pragma warning restore CS0109 // El miembro 'Clans.Remove(Clan)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            if (this.ContainsKey(_Clan.ClanID))
            {
                this.Remove(_Clan.ClanID);
            }
        }
    }
}
