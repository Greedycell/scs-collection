using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace CRS.UI
{
    using System.ComponentModel;

    /// <summary>
    /// Logica di interazione per PopupUpdater.xaml
    /// </summary>
    public partial class PopupUpdater : Window
    {
        private bool IsGoingPage = false;

        public PopupUpdater()
        {
            this.Opacity = 0;
            this.InitializeComponent();
            this.RTB_Console.Document.Blocks.Clear();
            this.RTB_Console.AppendText(Sys.ConfUCS.Changelog);
            Version thisAppVer = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.lbl_CurVer.Content = "Current CRS version: " + thisAppVer.Major + "." + thisAppVer.Minor + "." + thisAppVer.Build + "." + thisAppVer.MinorRevision;
            this.lbl_NewVer.Content = "New CRS version: " + Sys.ConfUCS.NewVer.Major + "." + Sys.ConfUCS.NewVer.Minor + "." + Sys.ConfUCS.NewVer.Build + "." + Sys.ConfUCS.NewVer.MinorRevision;

        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_GoPage_Click(object sender, RoutedEventArgs e)
        {
            this.IsGoingPage = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.OpInW();
           
            int DeltaVariation = 100;
            AnimationLib.MoveToTargetY(this.btn_Cancel, DeltaVariation, 0.25);
            AnimationLib.MoveToTargetY(this.btn_GoPage, DeltaVariation, 0.25, 50);
            AnimationLib.MoveToTargetY(this.RTB_Console, DeltaVariation, 0.25, 100);
            AnimationLib.MoveToTargetY(this.lbl_Changelog, DeltaVariation, 0.25, 150);
            AnimationLib.MoveToTargetY(this.lbl_CurVer, DeltaVariation, 0.25, 200);
            AnimationLib.MoveToTargetY(this.lbl_NewVer, DeltaVariation, 0.25, 250);
            AnimationLib.MoveToTargetY(this.lbl_Title, DeltaVariation, 0.25, 300);

            AnimationLib.MoveWindowToTargetY(this, DeltaVariation, this.Top, 0.25);

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.OpOutW(sender, e);
        }

        private void OpInW()
        {
            var OpIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
            this.BeginAnimation(OpacityProperty, OpIn);

        }

        private void OpOutW(object sender, CancelEventArgs e)
        {
            this.Closing -= this.Window_Closing;
            e.Cancel = true;
            var OpOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.125));
            OpOut.Completed += (s, _) => { this.Close(); MainWindow.IsFocusOk = true; if (this.IsGoingPage) System.Diagnostics.Process.Start(Sys.ConfUCS.UrlPage);
                this.IsGoingPage = false; };
            this.BeginAnimation(OpacityProperty, OpOut);
        }


        

    }
}
