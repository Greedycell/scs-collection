namespace CRS.Logic.Slots.Items
{
    #region Usings

    using System.Collections.Generic;
    using System.Timers;

    using CRS.Core;
    using CRS.Network;
    using CRS.Packets;

    #endregion

    internal class Battle
    {
        public int BattleID = 0;

        public int Checksum = 0;

        public Queue<Command> Commands = new Queue<Command>();

        public Level Player1 = null;

        public Level Player2 = null;

        public int Tick = 0;

        public Performance Time = new Performance();

        public Timer Timer = new Timer();

        /// <summary>
        /// Initialize a new instance of the <see cref="Battle" /> class.
        /// </summary>
        public Battle()
        {
            // Battle.
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Battle" /> class.
        /// </summary>
        /// <param name="_Player1">The player1.</param>
        /// <param name="_Player2">The player2.</param>
        public Battle(Level _Player1, Level _Player2)
        {
            this.BattleID = ResourcesManager.Battles.Seed++;

            this.Player1 = _Player1;
            this.Player2 = _Player2;

            this.Time = new Performance();
            this.Commands = new Queue<Command>();
            this.Timer = new Timer();
        }

        /// <summary>
        /// <see cref="Begin"/> this fucking battle.
        /// </summary>
        public void Begin()
        {
            this.Timer.Interval = 500;
            this.Timer.AutoReset = true;
            this.Timer.Elapsed += (Fuck, CRS) =>
            {
                PacketManager.Send(new Battle_Command_Data(this.Player1.GetClient()) { Battle = this });
                PacketManager.Send(new Battle_Command_Data(this.Player2.GetClient()) { Battle = this });
            };
            this.Timer.Start();
        }

        /// <summary>
        /// <see cref="Stop"/> this fucking battle.
        /// </summary>
        public void Stop()
        {
            this.Timer.Close();
        }
    }
}