namespace CRS.Logic.Slots
{
    #region Usings

    using System.Collections.Generic;

    using Enums;

    using Items;

    #endregion Usings

    internal class Battles : Dictionary<long, Battle>
    {
        /// <summary>
        /// The Battle ID, incremented at each battle.
        /// </summary>
        public int Seed                 = 1;

        /// <summary>
        /// The list of all players waiting for an opponent.
        /// </summary>
        public List<Level> Waiting     = null;

        /// <summary>
        /// Initialize a new instance of the <see cref="Battles"/> class.
        /// </summary>
        public Battles()
        {
            this.Waiting = new List<Level>();
        }

        /// <summary>
        /// Add the specified battle to the list.
        /// </summary>
        /// <param name="_Battle">The battle.</param>
        public void Add(Battle _Battle)
        {
            if (this.ContainsKey(_Battle.BattleID))
            {
                this[_Battle.BattleID] = _Battle;
            }
            else
            {
                base.Add(_Battle.BattleID, _Battle);
            }
        }

        public new void Remove(long _BattleID)
        {
            if (this.ContainsKey(_BattleID))
            {
                this[_BattleID].Player1.GetClient().State = (int)State.LOGGED;
                this[_BattleID].Player2.GetClient().State = (int)State.LOGGED;
            }
        }

        /// <summary>
        /// Dequeue a player from the waiting list.
        /// </summary>
        /// <returns>A Player.</returns>
        public Level Dequeue()
        {
            Level _Player = null;

            lock (this.Waiting)
            {
                _Player = this.Waiting[0];
                this.Waiting.RemoveAt(0);
            }

            return _Player;
        }

        /// <summary>
        /// Enqueue the specified player to the waiting list.
        /// </summary>
        /// <param name="_Player">The player.</param>
        public void Enqueue(Level _Player)
        {
            lock (this.Waiting)
            {
                this.Waiting.Add(_Player);
            }
        }

        public Level GetEnemy(int _BattleID, long _PlayerID)
        {
            if (this.ContainsKey(_BattleID))
            {
                return this[_BattleID].Player1.GetPlayerAvatar().GetId() == _PlayerID ? this[_BattleID].Player2 : this[_BattleID].Player1;
            }
            else
            {
                return null;
            }
        }
    }
}