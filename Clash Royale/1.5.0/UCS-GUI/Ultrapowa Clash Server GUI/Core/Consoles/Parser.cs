using UCS.Packets;

namespace UCS.Core.Consoles
{
    #region Usings

    using System;
    using System.Threading;

    using UCS.Core.Network;
    using UCS.Core.Network.TCP;
    using UCS.Core.Settings;
    using UCS.Packets.Messages.Server;

    #endregion Usings

    internal class Parser
    {
        public Thread _Thread = null;

        /// <summary>
        /// Initialize a new instance of the <see cref="Parser"/> class.
        /// </summary>
        public Parser()
        {
            Console.WriteLine("The Parser class has been initialized.\n");

            this._Thread = new Thread(() =>
            {
                while (true)
                {
                    ConsoleKeyInfo _Command = Console.ReadKey(false);

                    switch (_Command.Key)
                    {
                        case ConsoleKey.T:
                            {
                                Client.Send();
                                break;
                            }

                        case ConsoleKey.M:
                            {
                                foreach (var _Player in ResourcesManager.Players)
                                {
                                    new Battle_Result(_Player.Value.Client).Send();
                                }
                                break;
                            }

                        case ConsoleKey.Q:
                            {
                                Environment.Exit(0);
                                break;
                            }

                        case ConsoleKey.S:
                            {
                                Console.WriteLine("-----------------------------");
                                Console.WriteLine("Game Server Statistics at " + DateTime.Now.ToString("F") + " :");
                                Console.WriteLine("  -> In-Memory Devices : " + ResourcesManager.Devices.Count);
                                Console.WriteLine("  -> In-Memory Players : " + ResourcesManager.Players.Count);
                                Console.WriteLine("  -> In-Memory Clans   : " + ResourcesManager.Clans.Count);
                                Console.WriteLine("  -> In-Memory Tour.   : " + ResourcesManager.Tournaments.Count);
                                Console.WriteLine("  -> Used Slots        : " + Server._ConnectedSockets);
                                Console.WriteLine("  -> Reserved Slots    : " + Constants.MaxDevices);
                                Console.WriteLine("  -> Free Slots        : " + (Constants.MaxDevices - Server._ConnectedSockets));
                                Console.WriteLine("  -> In-Battle Players : " + ResourcesManager.Battles.Count);
                                Console.WriteLine("  -> Waiting Players   : " + ResourcesManager.Battles.Waiting.Count);
                                Console.WriteLine("-----------------------------");
                                break;
                            }

                        case ConsoleKey.C:
                            {
                                Console.Clear();
                                break;
                            }

                        case ConsoleKey.Enter:
                            {
                                Console.WriteLine();
                                break;
                            }

                        case ConsoleKey.F10:
                            {
                                Console.WriteLine();
                                Console.WriteLine("# ADMINISTRATOR MODE #");
                                Console.WriteLine("    Which command would you execute : ");
                                Console.WriteLine("        - Send a global chat message. - #1");
                                Console.WriteLine("        - Start the debugging mode.   - #2");
                                Console.WriteLine("        - Ban a player using an name. - #3");
                                Console.WriteLine("        - Ban a player using an ID.   - #4");
                                Console.WriteLine("        - Exit this menu.             - #5");

                                ConsoleKeyInfo _Key = Console.ReadKey(false);

                                if (_Key.Key == ConsoleKey.NumPad1)
                                {
                                    Console.WriteLine("This command is not implemented yet.");
                                }
                                else if (_Key.Key == ConsoleKey.NumPad2)
                                {
                                    Settings.Debug = true;
                                }
                                else if (_Key.Key == ConsoleKey.NumPad3)
                                {
                                    Console.WriteLine("This command is not implemented yet.");
                                }
                                else if (_Key.Key == ConsoleKey.NumPad4)
                                {
                                    Console.WriteLine("This command is not implemented yet.");
                                }
                                else if (_Key.Key == ConsoleKey.NumPad5)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("This command does not exist, exiting..");
                                }

                                break;
                            }

                        case ConsoleKey.R:
                            {
                                return;
                            }

                        default:
                            {
                                Console.WriteLine("Unknown command parsed, try again.");
                                break;
                            }
                    }
                }
            });
            this._Thread.Start();
        }
    }
}