namespace UCS.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;

    using UCS.Packets.Commands.Client;
    using UCS.Packets.Commands.Server;

    #endregion Usings

    internal class Command_Factory : IDisposable
    {
        /// <summary>
        /// A list of all available <see cref="Command"/> s.
        /// </summary>
        public static Dictionary<int, Type> Commands = new Dictionary<int, Type>();

        /// <summary>
        /// Initialize a new instance of the <see cref="Command_Factory"/> class.
        /// <para>When initialized, it will fill the list with all available <see cref="Command"/> s.</para>
        /// </summary>
        public Command_Factory()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            Commands = new Dictionary<int, Type>();

            Commands.Add(1, typeof(Place_Troop));

            Commands.Add(201, typeof(Name_Change_Callback));
            Commands.Add(210, typeof(Buy_Chest_Callback));

            // Commands.Add(212, typeof(Clan_Settings_Changed));
            Commands.Add(500, typeof(Move_Card));
            Commands.Add(501, typeof(Change_Deck));

            // Commands.Add(502, typeof(Start_Exploring));
            Commands.Add(504, typeof(Upgrade_Card));
            Commands.Add(508, typeof(Level_Up_Seen));
            Commands.Add(509, typeof(Free_Chest));
            Commands.Add(513, typeof(Card_Seen));
            Commands.Add(516, typeof(Buy_Chest));
            Commands.Add(518, typeof(Buy_Card));
            Commands.Add(522, typeof(Friendly_Battle));
            Commands.Add(523, typeof(Claim_Achievement));
            Commands.Add(524, typeof(Logic_Request_Spells));
            Commands.Add(525, typeof(Search_Battle));
            Commands.Add(526, typeof(Chest_Next_Card));
            Commands.Add(527, typeof(New_Card_Seen));
            Commands.Add(537, typeof(Buy_Challenge));
            Commands.Add(539, typeof(Battle_Challenge));
            Commands.Add(544, typeof(Buy_Offer));
            Commands.Add(545, typeof(New_Card_Seen_Deck));
        }

        /// <summary>
        /// Dispose the class.
        /// </summary>
        public void Dispose()
        {
            Commands.Clear();
        }
    }
}