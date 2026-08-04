namespace CRS
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    using CRS.Core;
    using CRS.Core.Consoles;
    using CRS.Core.Settings;
    using CRS.Core.Threading;
    using CRS.Helpers;
    using CRS.Sys;
    using CRS.UI;

    using Menu = UI.UC.Menu;

    #endregion

    /// <summary>
    ///     The con cat players.
    /// </summary>
    public class ConCatPlayers
    {
        /// <summary>
        ///     Gets or sets the player IDs.
        /// </summary>
        public string PlayerIDs { private get; set; }

        /// <summary>
        ///     Gets or sets the player names.
        /// </summary>
        public string PlayerNames { private get; set; }

        public override string ToString()
        {
            return $"{this.PlayerNames} : {this.PlayerIDs}";
        }
    }

    public partial class MainWindow : Window
    {
        public const int blurValue = 10;

        public static readonly int port = Constants.ServerPort;

        public const RenderingBias RenderQuality = RenderingBias.Performance;

        public static bool IsFocusOk = true;

        public static MainWindow RemoteWindow = new MainWindow();

        private static ConsoleStreamer CS;

        public bool isBlurEnabled = false;

        public List<ConCatPlayers> Players = new List<ConCatPlayers>();

        public DispatcherTimer UpdateInfoGUI = new DispatcherTimer();

        private BlurEffect blurEffect = new BlurEffect();

        private bool ChangeUpdatePopup = false;

        public static List<string> CommandList;

        private DoubleAnimation myDoubleAnimation = new DoubleAnimation();

        private Storyboard myStoryboard = new Storyboard();

        public MainWindow()
        {
            this.InitializeComponent();
            this.Grid_Utility.Visibility = Visibility.Collapsed;
            this.Grid_Commands.Visibility = Visibility.Collapsed;
            this.Grid_Menu.Visibility = Visibility.Collapsed;
            RemoteWindow = this;
            ConfUCS.OnServerOnlineEvent += this.ConfUCS_OnServerOnlineEvent;
            CommandList = new List<string>
                              {
                                  "/start",
                                  "/ban",
                                  "/banip",
                                  "/unban",
                                  "/unbanip",
                                  "/tempban",
                                  "/tempbanip",
                                  "/kick",
                                  "/mute",
                                  "/unmute",
                                  "/setlevel",
                                  "/update",
                                  "/say",
                                  "/sayplayer",
                                  "/stop",
                                  "/shutdown",
                                  "/forcestop",
                                  "/restart",
                                  "/send sysinfo",
                                  "/status",
                                  "/uptime",
                                  "/switch",
                                  "/help",
                                  "/clear"
                              };
            this.SharedShow();

            Console.WriteLine(Properties.Resources.LoadingGUI);
            this.LBL_IP.Content = Properties.Resources.LocalIP + " " + ConfUCS.GetIP() + ":" + port;
            this.CommandLine.TextChanged += this.CommandLine_TextChanged;
        }

        public void PrepareTimer()
        {
            this.UpdateInfoGUI.Tick += this.UpdateInfo_Tick;
            this.UpdateInfoGUI.Interval = new TimeSpan(10000);
        }

        public void SharedShow()
        {
            CS = new ConsoleStreamer(this.RTB_Console);
            Console.SetOut(CS);
            this.Title = ConfUCS.UnivTitle;
        }

        public void UpdateTheListPlayers()
        {
            this.Players.Clear();

            this.Dispatcher.BeginInvoke((Action)delegate { this.listBox.ItemsSource = null; });

            foreach (var x in ResourcesManager.GetOnlinePlayers())
            {
                this.Players.Add(
                    new ConCatPlayers
                        {
                            PlayerIDs = x.GetPlayerAvatar().GetId().ToString(),
                            PlayerNames = x.GetPlayerAvatar().GetName()
                        });
            }
            if (!ConfUCS.IsConsoleMode)
            {
                this.Dispatcher.BeginInvoke((Action)delegate { this.listBox.ItemsSource = this.Players; });
            }
        }

        private void BTN_Enter_Click(object sender, RoutedEventArgs e)
        {
            this.SenderCommand();
        }

        private void BTN_LaunchServer_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfUCS.IsServerOnline)
            {
                this.LaunchConsoleThread();
            }
            else
            {
                Console.WriteLine(Properties.Resources.ServerAlreadyOnline);
            }
        }

        private void CB_Debug_Checked(object sender, RoutedEventArgs e)
        {
            ConfUCS.DebugMode = true;
        }

        private void CB_Debug_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfUCS.DebugMode = false;
        }

        private void CommandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SenderCommand();
            }
        }

        private void CommandLine_TextChanged(object sender, TextChangedEventArgs e)
        {
            string TypedCommand = this.CommandLine.Text;
            List<string> Sug_List = new List<string>();
            Sug_List.Clear();

            foreach (string CM in CommandList) if (!string.IsNullOrEmpty(this.CommandLine.Text)) if (CM.StartsWith(TypedCommand)) Sug_List.Add(CM);

            if (Sug_List.Count > 0)
            {
                this.LB_CommandTypedList.ItemsSource = Sug_List;
                this.LB_CommandTypedList.Visibility = Visibility.Visible;
            }
            else if (Sug_List.Count > 0)
            {
                this.LB_CommandTypedList.ItemsSource = null;
                this.LB_CommandTypedList.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.LB_CommandTypedList.ItemsSource = null;
                this.LB_CommandTypedList.Visibility = Visibility.Collapsed;
            }
        }

        private void ConfUCS_OnServerOnlineEvent(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(
                (Action)delegate
                    {
                        SolidColorBrush m_Color;

                        if (ConfUCS._IsServerOnline)
                        {
                            m_Color = new SolidColorBrush(Color.FromRgb(0x39, 0xb5, 0x4a));
                            this.BTN_LaunchServerText.Text = Properties.Resources.ServerOnline;
                            this.BTN_LaunchServerImage.Source =
                                new BitmapImage(new Uri("/UI/Images/Ok.png", UriKind.Relative));
                            this.BTN_LaunchServer.IsEnabled = true;
                        }
                        else
                        {
                            m_Color = new SolidColorBrush(Color.FromRgb(0x00, 0x77, 0x9f));
                            this.BTN_LaunchServerText.Text = Properties.Resources.LaunchServer;
                            this.BTN_LaunchServerImage.Source =
                                new BitmapImage(new Uri("/UI/Images/Launch.png", UriKind.Relative));
                            this.BTN_LaunchServer.IsEnabled = true;
                        }

                        this.BTN_LaunchServer.Foreground = m_Color;
                        (this.BTN_LaunchServer.Template.FindName("border", this.BTN_LaunchServer) as Border).BorderBrush
                            = m_Color;
                    });
        }

        private void DeBlur()
        {
            if (!this.isBlurEnabled)
            {
                this.myDoubleAnimation.From = 0.2;
                this.myDoubleAnimation.To = 1;
                this.myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.125));
                this.BeginAnimation(OpacityProperty, this.myDoubleAnimation);
                return;
            }

            this.RegisterName("blurEffect", this.blurEffect);
            this.blurEffect.Radius = 0;
            this.blurEffect.RenderingBias = RenderQuality;
            this.Effect = this.blurEffect;

            this.myDoubleAnimation.From = blurValue;
            this.myDoubleAnimation.To = 0;
            this.myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.125));
            this.myDoubleAnimation.AutoReverse = false;

            Storyboard.SetTargetName(this.myDoubleAnimation, "blurEffect");
            Storyboard.SetTargetProperty(this.myDoubleAnimation, new PropertyPath(BlurEffect.RadiusProperty));
            this.myStoryboard.Children.Add(this.myDoubleAnimation);
            this.myStoryboard.Begin(this);
        }

        private void DoAnimation()
        {
            // AYY LMAO
            int DeltaVariation = -100;
            AnimationLib.MoveToTargetY(this.CB_Debug, DeltaVariation, 0.25, 50);
            AnimationLib.MoveToTargetY(this.LBL_UpTime, DeltaVariation, 0.25, 100);
            AnimationLib.MoveToTargetY(this.img_Timer, DeltaVariation, 0.25, 110);
            AnimationLib.MoveToTargetY(this.LBL_IP, DeltaVariation, 0.25, 200);
            AnimationLib.MoveToTargetY(this.img_Internet, DeltaVariation, 0.25, 210);
            AnimationLib.MoveToTargetY(this.MainRectangle, -DeltaVariation, 0.25, 250);
            AnimationLib.MoveToTargetY(this.UC_Menu, -DeltaVariation, 0.25, 300);
            AnimationLib.MoveToTargetY(this.UC_Menu_Background, -DeltaVariation, 0.25, 300);
            AnimationLib.MoveToTargetY(this.RBase, -DeltaVariation, 0.25, 325);
            AnimationLib.MoveToTargetY(this.UC_Commands, -DeltaVariation, 0.25, 350);
            AnimationLib.MoveToTargetY(this.R1, -DeltaVariation, 0.25, 375);
            AnimationLib.MoveToTargetY(this.UC_Utility, -DeltaVariation, 0.25, 400);
            AnimationLib.MoveToTargetY(this.R2, -DeltaVariation, 0.25, 425);
            AnimationLib.MoveToTargetY(this.UC_PlayerInfo, -DeltaVariation, 0.25, 450);
            AnimationLib.MoveToTargetY(this.R3, -DeltaVariation, 0.25, 475);
            AnimationLib.MoveToTargetY(this.UC_Restart, -DeltaVariation, 0.25, 500);

            AnimationLib.MoveToTargetX(this.BTN_LaunchServer, DeltaVariation - 100, 0.25, 100);
            AnimationLib.MoveToTargetX(this.listBox, DeltaVariation - 100, 0.3, 200);
            AnimationLib.MoveToTargetX(this.label_player, DeltaVariation - 100, 0.35, 200);
            AnimationLib.MoveToTargetX(this.img_Players, DeltaVariation - 100, 0.35, 230);
            AnimationLib.MoveToTargetX(this.BTN_Enter, -DeltaVariation * 7, 0.4, 150);
            AnimationLib.MoveToTargetX(this.CommandLine, -DeltaVariation * 7, 0.4, 250);
            AnimationLib.MoveToTargetX(this.img_Text, -DeltaVariation * 7, 0.4, 280);
            AnimationLib.MoveToTargetX(this.RTB_Console, -DeltaVariation * 7, 0.3, 300);
            AnimationLib.MoveToTargetX(this.label_console, -DeltaVariation * 7, 0.35, 300);
            AnimationLib.MoveToTargetX(this.img_Console, -DeltaVariation * 7, 0.35, 330);
        }

        private void DoBlur()
        {
            if (!this.isBlurEnabled)
            {
                this.myDoubleAnimation.From = 1;
                this.myDoubleAnimation.To = 0.2;
                this.myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.125));
                this.BeginAnimation(OpacityProperty, this.myDoubleAnimation);
                return;
            }

            this.RegisterName("blurEffect", this.blurEffect);
            this.blurEffect.Radius = 0;
            this.blurEffect.RenderingBias = RenderQuality;
            this.Effect = this.blurEffect;

            this.myDoubleAnimation.From = 0;
            this.myDoubleAnimation.To = blurValue;
            this.myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.125));
            this.myDoubleAnimation.AutoReverse = false;

            Storyboard.SetTargetName(this.myDoubleAnimation, "blurEffect");
            Storyboard.SetTargetProperty(this.myDoubleAnimation, new PropertyPath(BlurEffect.RadiusProperty));
            this.myStoryboard.Children.Add(this.myDoubleAnimation);
            this.myStoryboard.Begin(this);
        }

        private void LaunchConsoleThread()
        {
            ConsoleThread CT = new ConsoleThread();
            var _Writer = new Prefixed();
            Console.SetOut(_Writer);

            CT.Start();
            this.BTN_LaunchServer.IsEnabled = false;
        }

        private void LB_CommandTypedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.LB_CommandTypedList.ItemsSource != null)
            {
                this.LB_CommandTypedList.Visibility = Visibility.Collapsed;
                this.CommandLine.TextChanged -= this.CommandLine_TextChanged;

                if (this.LB_CommandTypedList.SelectedIndex != -1) this.CommandLine.Text = this.LB_CommandTypedList.SelectedItem.ToString();

                this.CommandLine.TextChanged += this.CommandLine_TextChanged;
            }
        }

        private void RTB_Console_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)delegate { this.RTB_Console.ScrollToEnd(); }, DispatcherPriority.Render);
        }

        private void SenderCommand()
        {
            if (!string.IsNullOrWhiteSpace(this.CommandLine.Text))
            {
                CommandParser.CommandRead(this.CommandLine.Text);
                this.CommandLine.Clear();
            }
        }

        private void SendPopup(int why)
        {
            if (!ConfUCS.IsServerOnline)
            {
                Console.WriteLine(Properties.Resources.ServerNotRunning);
            }
            else
            {
                IsFocusOk = false;
                Popup Popup = new Popup(why);
                Popup.Owner = this;
                Popup.ShowDialog();
            }
        }

        private void UC_Ban_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Commands.Visibility = Visibility.Collapsed;
            this.UC_Commands.IsPressed = false;
            this.SendPopup(Popup.cause.BAN);
        }

        private void UC_CheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Utility.Visibility = Visibility.Collapsed;
            this.UC_Utility.IsPressed = false;
            if (!this.ChangeUpdatePopup)
            {
                IsFocusOk = false;
                PopupUpdater PopupUpdater = new PopupUpdater();
                PopupUpdater.Owner = this;
                PopupUpdater.ShowDialog();
            }
        }

        private void UC_Commands_Click(object sender, RoutedEventArgs e)
        {
            if (this.Grid_Commands.Visibility == Visibility.Visible)
            {
                this.Grid_Commands.Visibility = Visibility.Collapsed;
                this.UC_Commands.IsPressed = false;
            }
            else
            {
                this.Grid_Commands.Visibility = Visibility.Visible;
                this.Grid_Utility.Visibility = Visibility.Collapsed;
                this.Grid_Menu.Visibility = Visibility.Collapsed;
                this.UC_Utility.IsPressed = false;
                this.UC_Commands.IsPressed = true;
            }
        }

        private void UC_Configuration_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Utility.Visibility = Visibility.Collapsed;
            this.UC_Utility.IsPressed = false;
            IsFocusOk = false;
            PopupConfiguration PC = new PopupConfiguration();
            PC.Owner = this;
            PC.ShowDialog();
        }

        private void UC_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UC_Kick_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Commands.Visibility = Visibility.Collapsed;
            this.UC_Commands.IsPressed = false;
            this.SendPopup(Popup.cause.KICK);
        }

        private void UC_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (this.Grid_Menu.Visibility == Visibility.Visible)
            {
                this.Grid_Menu.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Grid_Commands.Visibility = Visibility.Collapsed;
                this.Grid_Menu.Visibility = Visibility.Visible;
                this.Grid_Utility.Visibility = Visibility.Collapsed;
                this.UC_Utility.IsPressed = false;
                this.UC_Commands.IsPressed = false;
            }
        }

        private void UC_Menu_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationLib.ChangeBackgroundBorderColor(this.UC_Menu_Background, Color.FromRgb(0x00, 0x4c, 0x65), 0.2);
        }

        private void UC_Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationLib.ChangeBackgroundBorderColor(this.UC_Menu_Background, Color.FromRgb(0x00, 0x77, 0x9F), 0.2);
        }

        private void UC_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationLib.ChangeBackgroundColor(sender as Menu, Color.FromRgb(0x00, 0x4c, 0x65), 0.2);
        }

        private void UC_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!(sender as Menu).IsPressed) AnimationLib.ChangeBackgroundColor(sender as Menu, Color.FromRgb(0x00, 0x77, 0x9F), 0.2);
        }

        private void UC_PlayerInfo_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Commands.Visibility = Visibility.Collapsed;
            this.Grid_Menu.Visibility = Visibility.Collapsed;
            this.Grid_Utility.Visibility = Visibility.Collapsed;
            this.UC_Commands.IsPressed = false;
            this.UC_Utility.IsPressed = false;

            IsFocusOk = false;
            var Popup = new PlayerInfo();
            Popup.Owner = this;
            Popup.ShowDialog();
        }

        private void UC_Restart_Click(object sender, RoutedEventArgs e)
        {
            CommandParser.CommandRead("/restart");
        }

        private void UC_Unban_Click(object sender, RoutedEventArgs e)
        {
            this.Grid_Commands.Visibility = Visibility.Collapsed;
            this.UC_Commands.IsPressed = false;
            this.SendPopup(Popup.cause.UNBAN);
        }

        private void UC_Utility_Click(object sender, RoutedEventArgs e)
        {
            if (this.Grid_Utility.Visibility == Visibility.Visible)
            {
                this.Grid_Utility.Visibility = Visibility.Collapsed;
                this.UC_Utility.IsPressed = false;
            }
            else
            {
                this.Grid_Commands.Visibility = Visibility.Collapsed;
                this.Grid_Menu.Visibility = Visibility.Collapsed;
                this.Grid_Utility.Visibility = Visibility.Visible;
                this.UC_Commands.IsPressed = false;
                this.UC_Utility.IsPressed = true;
            }
        }

        private void UpdateInfo_Tick(object sender, EventArgs e)
        {
            ControlTimer.UpdateTime();
        }

        private void USC_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationLib.ChangeBackgroundColor(sender as Menu, Color.FromRgb(0x00, 0x4c, 0x65), 0.2);
            AnimationLib.MoveToTargetXwoMargin(sender as Control, 10, 0, .2);
        }

        // Events for SubCommands
        private void USC_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationLib.ChangeBackgroundColor(sender as Menu, Color.FromRgb(0x00, 0x77, 0x9F), 0.2);
            AnimationLib.MoveToTargetXwoMargin(sender as Control, 0, 10, .2);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (IsFocusOk == false)
            {
                this.DeBlur(); // Remove the Blur effect
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (IsFocusOk == false)
            {
                this.DoBlur(); // Start doing the Blur effect
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Properties.Resources.GUILoaded);
            this.DoAnimation();

            if (ConfUCS.AutoStartServer)
                if (!ConfUCS.IsServerOnline)
                {
                    this.BTN_LaunchServer.IsEnabled = false;
                    AsyncUtils.DelayCall(1000, () => { this.LaunchConsoleThread(); });
                }
        }
    }

    internal static class AsyncUtils
    {
        public static void DelayCall(int msec, Action fn)
        {
            Dispatcher d = Dispatcher.CurrentDispatcher;
            new Task(
                () =>
                    {
                        Thread.Sleep(msec);
                        d.BeginInvoke(fn);
                    }).Start();
        }
    }
}