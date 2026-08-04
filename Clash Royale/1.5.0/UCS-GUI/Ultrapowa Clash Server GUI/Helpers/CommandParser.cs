#region Usings

using CRS.Core.Settings;
using CRS.Logic.Enums;

#endregion

namespace CRS.Helpers
{
    #region Usings

    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Windows;

    using CRS.Core;
    using CRS.Core.Threading;
    using CRS.GameFiles;
    using CRS.Logic;
    using CRS.Network;
    using CRS.Packets;
    using CRS.Sys;

    #endregion

    internal class CommandParser
    {
        public static void CommandRead(string cmd)
        {
            if (cmd == null)
            {
                if (ConfUCS.IsConsoleMode)
                {
                    ManageConsole();
                }
            }

            try
            {
                switch (cmd.ToLower())
                {
                    case "/help":
                        Console.WriteLine("/start                             <-- Inicia el servidor");
                        Console.WriteLine("/ban <PlayerID>                    <-- Banear un jugador");

                        Console.WriteLine("/banip <PlayerID>                  <-- Banear un jugador mediante IP");
                        Console.WriteLine("/unban <PlayerID>                  <-- Unbanear un jugador");

                        Console.WriteLine("/unbanip <PlayerID>                <-- Unbanear un jugador");
                        Console.WriteLine("/tempban <PlayerID> <Seconds>      <-- Banea un jugador temporalmente");
                        Console.WriteLine("/tempbanip <PlayerID> <Seconds>    <-- Banea temporalmente un jugador mediante IP");
                        Console.WriteLine("/kick <PlayerID>                   <-- Expulsa un jugador del servidor");

                        Console.WriteLine("/mute <PlayerID>                   <-- Silencia a un jugador");
                        Console.WriteLine("/unmute <PlayerID>                 <-- Quita silencio a jugador");
                        Console.WriteLine("/setlevel <PlayerID> <Level>       <-- Fija un level a un jugador");
                        Console.WriteLine("/update                            <-- Comprueba si hay actualizaciónes disponibles");

                        Console.WriteLine("/say <Text>                        <-- Envia un mensaje a todos los jugadores");
                        Console.WriteLine("/sayplayer <PlayerID> <Text>       <-- Envia un mensaje al jugador especificado");
                        Console.WriteLine("/stop  or   /shutdown              <-- Detiene el servidor y guarda todos los datos");
                        Console.WriteLine("/forcestop                         <-- Fuerza el cierre del servidor !! ATENCION SE PIERDEN LOS DATOS!!");
                        Console.WriteLine("/restart                           <-- Guarda los datos y reinicia el servidor");
                        Console.WriteLine("/send sysinfo                      <-- Envia la información del servidor a los jugadores conectados");
                        Console.WriteLine("/status                            <-- Obtiene el estado el servidor");
                        Console.WriteLine("/uptime                            <-- Obtiene el tiempo activo del servidor");
                        Console.WriteLine("/switch                            <-- Cambiar modo GUI/Console");
                        Console.WriteLine("/clear                             <-- Limpia todos los mensajes de a consola");
                        break;

                    case "/start":

                        if (!ConfUCS.IsServerOnline)
                        {
                            ConsoleThread CT = new ConsoleThread();
                            CT.Start();
                        }
                        else
                            Core.Debug.Write("El servidor esta activo!");
                        break;

                    case "/stop":
                    case "/shutdown":

                        Core.Debug.Write("Apagando servidor... Guardando datos, esperare...");

                        foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                        {
                            var p = new ShutdownStartedMessage(onlinePlayer.GetClient());
                            p.SetCode(5);
                            PacketManager.Send(p);
                        }

                        ConsoleManage.FreeConsole();
                        Environment.Exit(0);
                        break;

                    case "/forcestop":

                        Core.Debug.Write("Terminado proceso del servidor... los ultimos datos se perderan!");
                        Process.GetCurrentProcess().Kill();
                        break;

                    case "/uptime":

                        Core.Debug.Write("Tiempo servidor iniciado: " + ControlTimer.ElapsedTime);
                        break;

                    case "/restart":

                        Core.Debug.Write("Reiniciando servidor....");

                        var mail = new AllianceMailStreamEntry();
                        mail.SetId((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                        mail.SetSenderId(0);
                        mail.SetSenderAvatarId(0);
                        mail.SetSenderName("System Manager");
                        mail.SetIsNew(0);
                        mail.SetAllianceId(0);
                        mail.SetAllianceBadgeData(0);
                        mail.SetAllianceName("JJBreaker Administrador");
                        mail.SetMessage("El servidor se reiniciará en breve.");
                        mail.SetSenderLevel(500);
                        mail.SetSenderLeagueId(22);

                        foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                        {
                            var pm = new GlobalChatLineMessage(onlinePlayer.GetClient());
                            var ps = new ShutdownStartedMessage(onlinePlayer.GetClient());
                            var p = new AvatarStreamEntryMessage(onlinePlayer.GetClient());
                            ps.SetCode(5);
                            p.SetAvatarStreamEntry(mail);
                            pm.SetChatMessage("El servidor se reiniciará en breve.");
                            pm.SetPlayerId(0);
                            pm.SetLeagueId(22);
                            pm.SetPlayerName("System Manager");
                            PacketManager.Send(p);
                            PacketManager.Send(ps);
                            PacketManager.Send(pm);
                        }
                        Console.WriteLine("Guaradndo datos...");

                        DatabaseManager.Singelton.Save(ResourcesManager.GetInMemoryLevels());

                        Console.WriteLine("Restarting now");

                        Process.Start(Application.ResourceAssembly.Location);
                        Process.GetCurrentProcess().Kill();
                        break;

                    case "/clear":

                        Core.Debug.Write("Consola limpiada");
                        if (ConfUCS.IsConsoleMode)
                            Console.Clear();
                        else
                            MainWindow.RemoteWindow.RTB_Console.Clear();
                        break;

                    case "/status":

                        Console.WriteLine("Server IP: " + ConfUCS.GetIP() + " on port 9339");
                        Console.WriteLine("IP Address (public): "
                                          + new WebClient().DownloadString("http://bot.whatismyipaddress.com/"));
                        Console.WriteLine("Online Player: " + ResourcesManager.GetOnlinePlayers().Count);
                        Console.WriteLine("Connected Player: " + ResourcesManager.GetConnectedClients().Count);
                        Console.WriteLine("Starting Gold: " + Settings.StartingGold);
                        Console.WriteLine("Starting Gems: " + Settings.StartingGems);
                        var versionData = FingerPrint.version.Split('.');
                        Console.WriteLine("CRS Version: " + versionData[0] + "." + versionData[1] + "." + versionData[2]);
                        if (Convert.ToBoolean(Constants.Patching))
                        {
                            Console.WriteLine("Patch: Active");
                            Console.WriteLine("Patching Server: " + Constants.PatchURL);
                        }
                        else
                        {
                            Console.WriteLine("Patch: Disable");
                        }

                        if (Convert.ToBoolean(Settings.Maintenance))
                        {
                            Console.WriteLine("Maintance Mode: Active");
                            Console.WriteLine("Maintance time: " + Convert.ToInt32(Settings.MaintenanceDuration)
                                              + " Seconds");
                        }
                        else
                        {
                            Core.Debug.Write("Maintance Mode: Disable");
                        }

                        break;

                    case "/send sysinfo":

                        Console.WriteLine("Server Status is now sent to all online players");

                        var mail1 = new AllianceMailStreamEntry();
                        mail1.SetId((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                        mail1.SetSenderId(0);
                        mail1.SetSenderAvatarId(0);
                        mail1.SetSenderName("System Manager");
                        mail1.SetIsNew(0);
                        mail1.SetAllianceId(0);
                        mail1.SetAllianceBadgeData(0);
                        mail1.SetAllianceName("Legendary Administrator");
                        mail1.SetMessage("Latest Server Status:\nConnected Players:" +
                        ResourcesManager.GetConnectedClients().Count + "\nIn Memory Alliances:" +
                        ObjectManager.GetInMemoryAlliances().Count + "\nIn Memory Levels:" +
                        ResourcesManager.GetInMemoryLevels().Count);
                        mail1.SetSenderLeagueId(22);
                        mail1.SetSenderLevel(500);

                        foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                        {
                            var p = new AvatarStreamEntryMessage(onlinePlayer.GetClient());
                            var pm = new GlobalChatLineMessage(onlinePlayer.GetClient());
                            pm.SetChatMessage("Our current Server Status is now sent at your mailbox!");
                            pm.SetPlayerId(0);
                            pm.SetLeagueId(22);
                            pm.SetPlayerName("System Manager");
                            p.SetAvatarStreamEntry(mail1);
                            PacketManager.Send(p);
                            PacketManager.Send(pm);
                        }
                        break;

                    case "/update":
                        UpdateChecker.Check();
                        break;

                    case "/kick":
                        var CommGet = cmd.Split(' ');
                        if (CommGet.Length >= 2)
                        {
                            try
                            {
                                var id = Convert.ToInt64(CommGet[1]);
                                var l = ResourcesManager.GetPlayer(id);
                                if (ResourcesManager.IsPlayerOnline(l))
                                {
                                    ResourcesManager.LogPlayerOut(l);
                                    PacketManager.Send(new OutOfSyncMessage(l.GetClient()));
                                }
                                else
                                {
                                    Console.WriteLine("Kick failed: id " + id + " not found");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("The given id is not a valid number");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Kick failed with error: " + ex);
                            }
                        }
                        else Console.WriteLine("Not enough arguments");
                        break;

                    case "/ban":
                        var CommGet1 = cmd.Split(' ');
                        if (CommGet1.Length >= 2)
                        {
                            try
                            {
                                var id = Convert.ToInt64(CommGet1[1]);
                                var l = ResourcesManager.GetPlayer(id);
                                if (l != null)
                                {
                                    l.SetAccountStatus((byte)Status.BANNED);
                                    l.SetAccountPrivileges((byte)Rank.Inactive);
                                    if (ResourcesManager.IsPlayerOnline(l))
                                    {
                                        PacketManager.Send(new OutOfSyncMessage(l.GetClient()));
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ban failed: id " + id + " not found");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("The given id is not a valid number");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Ban failed with error: " + ex);
                            }
                        }
                        else Console.WriteLine("Not enough arguments");
                        break;

                    case "/unban":
                        var CommGet2 = cmd.Split(' ');
                        if (CommGet2.Length >= 2)
                        {
                            try
                            {
                                var id = Convert.ToInt64(CommGet2[1]);
                                var l = ResourcesManager.GetPlayer(id);
                                if (l != null)
                                {
                                    l.SetAccountStatus(0);
                                }
                                else
                                {
                                    Core.Debug.Write("Unban failed: id " + id + " not found");
                                }
                            }
                            catch (FormatException)
                            {
                                Core.Debug.Write("The given id is not a valid number");
                            }
                            catch (Exception ex)
                            {
                                Core.Debug.Write("Unban failed with error: " + ex);
                            }
                        }
                        else
                            Core.Debug.Write("Not enough arguments");
                        break;

                    case "/switch":
                        if (ConfUCS.IsConsoleFirst)
                        {
                            Core.Debug.Write("Sorry, you need to launch CRS in GUI mode first.");
                        }
                        else
                        {
                            if (ConfUCS.IsConsoleMode)
                            {
                                ConfUCS.IsConsoleMode = false;
                                ConsoleManage.HideConsole();
                                InterfaceThread.Start();
                                Core.Debug.Write("Switched to GUI");
                                ControlTimer.SwitchTimer();
                            }
                            else
                            {
                                ConfUCS.IsConsoleMode = true;
                                ConsoleManage.ShowConsole();
                                Console.SetOut(AllocateConsole.StandardConsole);
                                MainWindow.RemoteWindow.Hide();
                                Console.Title = ConfUCS.UnivTitle;
                                Core.Debug.Write("Switched to Console");
                                ControlTimer.SwitchTimer();
                                ManageConsole();
                            }
                        }

                        break;

                    default:
                        Core.Debug.Write("Unknown command ( " + cmd + " ). Type \"/help\" for a list containing all available commands.");
                        break;
                }
            }
            catch (Exception)
            {
                Core.Debug.Write("Something wrong happens...");

                // throw;
            }

            // else if (cmd.ToLower().StartsWith("/mute"))
            // {
            // var CommGet = cmd.Split(' ');
            // if (CommGet.Length >= 2)
            // {
            // try
            // {
            // var id = Convert.ToInt64(CommGet[1]);
            // var l = ResourcesManager.GetPlayer(id);
            // if (ResourcesManager.IsPlayerOnline(l))
            // {
            // var p = new BanChatTrigger(l.GetClient());
            // p.SetCode(999999999);
            // PacketManager.ProcessOutgoingPacket(p);
            // }
            // else
            // {
            // Console.WriteLineDebug("Chat Mute failed: id " + id + " not found", CoreWriter.level.DEBUGLOG);
            // }
            // }
            // catch (FormatException)
            // {
            // Console.WriteLineDebug("The given id is not a valid number", CoreWriter.level.DEBUGFATAL);
            // }
            // catch (Exception ex)
            // {
            // Console.WriteLineDebug("Chat Mute failed with error: " + ex, CoreWriter.level.DEBUGFATAL);
            // }
            // }
            // else Console.WriteLineDebug("Not enough arguments", CoreWriter.level.DEBUGFATAL);
            // }

            if (ConfUCS.IsConsoleMode)
            {
                ManageConsole();
            }
        }

        public static void ManageConsole()
        {
            CommandRead(Console.ReadLine());
        }
    }
}