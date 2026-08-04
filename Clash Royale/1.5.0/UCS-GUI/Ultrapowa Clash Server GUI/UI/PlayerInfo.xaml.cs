using System;
using System.Windows;
using System.Windows.Media.Animation;
//using CRS.Helpers;
namespace CRS.UI
{
    using System.ComponentModel;

    /// <summary>
    /// Logica di interazione per Popup.xaml
    /// </summary>
    public partial class PlayerInfo : Window
    {

        int DeltaVariation = 100;

        public PlayerInfo()
        {
            this.InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.OpInW();

            MainWindow.RemoteWindow.UpdateTheListPlayers();
            this.CB_Player.ItemsSource = MainWindow.RemoteWindow.Players;

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
            OpOut.Completed += (s, _) => { this.Close(); MainWindow.IsFocusOk = true; };
            this.BeginAnimation(OpacityProperty, OpOut);
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
