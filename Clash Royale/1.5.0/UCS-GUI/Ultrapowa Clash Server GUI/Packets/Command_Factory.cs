namespace CRS.Packets
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using CRS.Packets.Commands.Client;
    using CRS.Packets.Commands.Server;

    #endregion

    internal class Command_Factory : IDisposable
    {
        /// <summary>
        ///     A list of all available <see cref="Command" /> s.
        /// </summary>
        public static Dictionary<int, Type> Commands = new Dictionary<int, Type>();

        static Command_Factory()
        {
            Commands = new Dictionary<int, Type>();

            Commands.Add(1, typeof(Place_Troop));

            Commands.Add(201, typeof(Name_Change_Callback));
            Commands.Add(210, typeof(Buy_Chest_Callback));
            // Commands.Add(212, typeof(Clan_Settings_Changed));

            Commands.Add(500, typeof(EditCardPositionCommand));
            Commands.Add(501, typeof(Change_Deck));
            //Commands.Add(502, typeof(Start_Exploring));
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

        //public static object Read(Reader br, Client _Client)
        //{
        //    int num = br.ReadVInt();
        //    if (Command_Factory.Commands.ContainsKey(num))
        //    {
        //        Debug.Write("Command " + num + ": " + Commands[num].Name + " is handled");
        //        //return Activator.CreateInstance(Commands[num], new object[]
        //        //{
        //        //    br
        //        //});
        //        return Activator.CreateInstance(Commands[num], br, _Client);
        //    }

        //    Debug.Write("[CRS]    The command " + num + " is unhandled");
        //    return null;
        //}


        /// <summary>
        ///     <see cref="Dispose" /> the class.
        /// </summary>
        public void Dispose()
        {
            Commands.Clear();
        }
    }
}