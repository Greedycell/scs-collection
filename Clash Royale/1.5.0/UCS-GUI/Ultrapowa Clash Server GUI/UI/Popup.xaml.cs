namespace CRS.UI
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media.Animation;

    using CRS.Helpers;

    #endregion

    /// <summary>
    ///     Logica di interazione per Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        bool IsRequiredSecPage;

#pragma warning disable CS0414 // El campo 'Popup.WasSecPage' está asignado pero su valor nunca se usa
        bool WasSecPage = false;
#pragma warning restore CS0414 // El campo 'Popup.WasSecPage' está asignado pero su valor nunca se usa

        int DeltaVariation = 100;

        bool IsErrorHappens;

        public class cause
        {
            public const int BAN = 0;

            public const int BANIP = 1;

            public const int TEMPBAN = 2;

            public const int TEMPBANIP = 3;

            public const int UNBAN = 4;

            public const int UNBANIP = 5;

            public const int MUTE = 6;

            public const int UNMUTE = 7;

            public const int KICK = 8;
        }

        public int CC = -1;

        public Popup(int Slc_cause = -1)
        {
            this.Opacity = 0;

            this.InitializeComponent();

            this.LB_Main.Content = Slc_cause == cause.BAN
                                       ? "Select a player to ban"
                                       : Slc_cause == cause.BANIP
                                           ? "Select a player to ban ip"
                                           : Slc_cause == cause.TEMPBAN
                                               ? "Select a player to ban temporarily"
                                               : Slc_cause == cause.TEMPBANIP
                                                   ? "Select a player to ban ip"
                                                   : Slc_cause == cause.UNBAN
                                                       ? "Select a player to unban"
                                                       : Slc_cause == cause.UNBANIP
                                                           ? "Select a player to unban ip"
                                                           : Slc_cause == cause.MUTE
                                                               ? "Select a player to mute"
                                                               : Slc_cause == cause.UNMUTE
                                                                   ? "Select a player to unmute"
                                                                   : Slc_cause == cause.KICK
                                                                       ? "Select a player to kick"
                                                                       : "Error";

            if (Slc_cause == cause.UNBAN || Slc_cause == cause.UNBANIP || Slc_cause == cause.MUTE
                || Slc_cause == cause.UNMUTE || Slc_cause == cause.KICK || Slc_cause == cause.BAN)
            {
                this.btn_ok.Content = Properties.Resources.OK;
                this.IsRequiredSecPage = false;
            }
            else if (Slc_cause == -1)
            {
                this.btn_ok.Content = Properties.Resources.Exit;
                this.IsErrorHappens = false;
                this.IsRequiredSecPage = false;
            }
            else
            {
                this.btn_ok.Content = Properties.Resources.Continue;
                this.IsRequiredSecPage = true;
            }

            this.CC = Slc_cause;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.OpInW();

            MainWindow.RemoteWindow.UpdateTheListPlayers();
            this.CB_Player.ItemsSource = MainWindow.RemoteWindow.Players;

            AnimationLib.MoveToTargetY(this.btn_cancel, this.DeltaVariation, 0.25);
            AnimationLib.MoveToTargetY(this.btn_ok, this.DeltaVariation, 0.25, 50);
            AnimationLib.MoveToTargetY(this.CB_Player, this.DeltaVariation, 0.25, 100);
            AnimationLib.MoveToTargetY(this.LB_Main, this.DeltaVariation, 0.25, 150);
            AnimationLib.MoveToTargetY(this.img_Commands, this.DeltaVariation, 0.25, 200);

            AnimationLib.MoveWindowToTargetY(this, this.DeltaVariation, this.Top, 0.25);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.OpOutW(sender, e);
        }

        private void OpInW()
        {
            var OpIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.125));
            this.BeginAnimation(OpacityProperty, OpIn);
        }

        private void OpOutW(object sender, CancelEventArgs e)
        {
            this.Closing -= this.Window_Closing;
            e.Cancel = true;
            var OpOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.125));
            OpOut.Completed += (s, _) =>
                {
                    this.Close();
                    MainWindow.IsFocusOk = true;
                };
            this.BeginAnimation(OpacityProperty, OpOut);
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsErrorHappens)
            {
                this.Close();
                return;
            }

            if (!this.IsRequiredSecPage)
            {
                if (this.CB_Player.SelectedIndex == -1) MessageBox.Show(Properties.Resources.SelectAPlayerFirst);
                else
                {
                    string[] SPLT = this.CB_Player.SelectedItem.ToString().Split(' ');

                    switch (this.CC)
                    {
                        case 0:
                            CommandParser.CommandRead("/ban " + SPLT[2]);
                            this.Close();
                            break;
                        case 4:
                            CommandParser.CommandRead("/unban " + SPLT[2]);
                            this.Close();
                            break;
                        case 8:
                            CommandParser.CommandRead("/kick " + SPLT[2]);
                            this.Close();
                            break;
                    }
                }
            }
        }
    }
}