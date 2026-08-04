namespace CRS.Logic.Slots
{
    #region Usings

    using System.Collections.Generic;

    // using Core.Database.Models;

    #endregion

    internal class Players : Dictionary<long, Level>
	{
		/// <summary>
		/// The seed is the latest player id stored in the database, plus one.
		/// </summary>
		public long Seed = 0;

#pragma warning disable CS0109 // El miembro 'Players.Add(Level)' no oculta un miembro accesible. La palabra clave new no es necesaria.
		public new void Add(Level _Player)
#pragma warning restore CS0109 // El miembro 'Players.Add(Level)' no oculta un miembro accesible. La palabra clave new no es necesaria.
		{
			if (this.ContainsKey(_Player.GetPlayerAvatar().GetId()))
			{
				this[_Player.GetPlayerAvatar().GetId()] = _Player;
			}
			else
			{
				this.Add(_Player.GetPlayerAvatar().GetId(), _Player);
			}
		}

#pragma warning disable CS0109 // El miembro 'Players.Remove(Level)' no oculta un miembro accesible. La palabra clave new no es necesaria.
		/// <summary>
		/// <see cref="Remove" /> the specified player from the list.
		/// </summary>
		/// <param name="_Player">The player.</param>
		public new void Remove(Level _Player)
#pragma warning restore CS0109 // El miembro 'Players.Remove(Level)' no oculta un miembro accesible. La palabra clave new no es necesaria.
		{
			if (this.ContainsKey(_Player.GetPlayerAvatar().GetId()))
			{
				this.Remove(_Player.GetPlayerAvatar().GetId());
			}
		}

		/// <summary>
		/// <see cref="Remove" /> the specified player from the list.
		/// </summary>
		/// <param name="_PlayerID">The player identifier.</param>
		public new void Remove(long _PlayerID)
		{
			if (this.ContainsKey(_PlayerID))
			{
				base.Remove(_PlayerID);
			}
		}
	}
}