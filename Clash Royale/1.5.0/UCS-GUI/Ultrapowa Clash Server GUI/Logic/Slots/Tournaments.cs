namespace CRS.Logic.Slots
{
    using System.Collections.Generic;

    internal class Tournaments : Dictionary<long, Tournament>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Tournaments"/> class.
        /// </summary>
        public Tournaments() : base()
        {
            // Tournaments.
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Tournaments"/> class.
        /// </summary>
        /// <param name="_Max">The maximum of tournaments in the list.</param>
        public Tournaments(int _Max) : base(_Max)
        {
            // Tournaments.
        }

#pragma warning disable CS0109 // El miembro 'Tournaments.Add(Tournament)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Add the specified tournament to the list.
        /// </summary>
        /// <param name="_Tournament">The tournament.</param>
        public new void Add(Tournament _Tournament)
#pragma warning restore CS0109 // El miembro 'Tournaments.Add(Tournament)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            if (this.ContainsKey(_Tournament.TournamentID))
            {
                this[_Tournament.TournamentID] = _Tournament;
            }
            else
            {
                this.Add(_Tournament.TournamentID, _Tournament);
            }
        }

#pragma warning disable CS0109 // El miembro 'Tournaments.Remove(Tournament)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        /// <summary>
        /// Remove the specified tournament from the list.
        /// </summary>
        /// <param name="_Tournament">The tournament.</param>
        public new void Remove(Tournament _Tournament)
#pragma warning restore CS0109 // El miembro 'Tournaments.Remove(Tournament)' no oculta un miembro accesible. La palabra clave new no es necesaria.
        {
            if (this.ContainsKey(_Tournament.TournamentID))
            {
                this.Remove(_Tournament.TournamentID);
            }
        }
    }
}
