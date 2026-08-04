namespace CRS.UI
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    using CRS.Core.Settings;
    using CRS.GameFiles;

    #endregion

    /// <summary>
    ///     Logica di interazione per GeneralPopup.xaml
    /// </summary>
    public partial class PopupConfiguration : Window
    {
        public PopupConfiguration()
        {
            this.Opacity = 0;
            this.InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadConfig();
            this.OpInW();

            int DeltaVariation = 300;
            AnimationLib.MoveToTargetY(this.CB_EnableMaintenance, -DeltaVariation / 2, 0.25);
            AnimationLib.MoveToTargetY(this.lbl_EnableMaintenance, -DeltaVariation / 2, 0.25, 50);
            AnimationLib.MoveToTargetY(this.BTN_Load, -DeltaVariation / 2, 0.25, 100);
            AnimationLib.MoveToTargetY(this.BTN_Save, -DeltaVariation / 2, 0.25, 150);
            AnimationLib.MoveToTargetY(this.BTN_Discard, -DeltaVariation / 2, 0.25, 200);
            AnimationLib.MoveToTargetX(this.TB_Gems, DeltaVariation, 0.25);
            AnimationLib.MoveToTargetX(this.lbl_Gems, DeltaVariation, 0.25, 25);
            AnimationLib.MoveToTargetX(this.TB_Gold, DeltaVariation, 0.25, 25);
            AnimationLib.MoveToTargetX(this.lbl_Gold, DeltaVariation, 0.25, 50);
            AnimationLib.MoveToTargetX(this.TB_Trophies, DeltaVariation, 0.25, 100);
            AnimationLib.MoveToTargetX(this.lbl_Trophies, DeltaVariation, 0.25, 125);
            AnimationLib.MoveToTargetX(this.TB_StartingLevel, DeltaVariation, 0.25, 150);
            AnimationLib.MoveToTargetX(this.lbl_StartingLevel, DeltaVariation, 0.25, 175);
            AnimationLib.MoveToTargetX(this.TB_Experience, DeltaVariation, 0.25, 175);
            AnimationLib.MoveToTargetX(this.lbl_Experience, DeltaVariation, 0.25, 200);

            AnimationLib.MoveToTargetX(this.lbl_PatchServer, -DeltaVariation, 0.25);
            AnimationLib.MoveToTargetX(this.TB_PatchServer, -DeltaVariation, 0.25, 25);
            AnimationLib.MoveToTargetX(this.lbl_Outdated, -DeltaVariation, 0.25, 25);
            AnimationLib.MoveToTargetX(this.TB_Outdated, -DeltaVariation, 0.25, 50);
            AnimationLib.MoveToTargetX(this.lbl_ConnName, -DeltaVariation, 0.25, 50);
            AnimationLib.MoveToTargetX(this.CB_ConnName, -DeltaVariation, 0.25, 75);
            AnimationLib.MoveToTargetX(this.lbl_ClientVer, -DeltaVariation, 0.25, 75);
            AnimationLib.MoveToTargetX(this.TB_ClientVer, -DeltaVariation, 0.25, 100);
            AnimationLib.MoveToTargetX(this.lbl_Maintenance, -DeltaVariation, 0.25, 100);
            AnimationLib.MoveToTargetX(this.TB_Maintenance, -DeltaVariation, 0.25, 125);
            AnimationLib.MoveToTargetX(this.lbl_Port, -DeltaVariation, 0.25, 125);
            AnimationLib.MoveToTargetX(this.TB_Port, -DeltaVariation, 0.25, 150);
            AnimationLib.MoveToTargetX(this.lbl_DebugPort, -DeltaVariation, 0.25, 150);
            AnimationLib.MoveToTargetX(this.TB_DebugPort, -DeltaVariation, 0.25, 175);
            AnimationLib.MoveToTargetX(this.lbl_EnableDebug, -DeltaVariation, 0.25, 175);
            AnimationLib.MoveToTargetX(this.CB_EnableDebug, -DeltaVariation, 0.25, 200);
            AnimationLib.MoveToTargetX(this.lbl_LogLevel, -DeltaVariation, 0.25, 200);
            AnimationLib.MoveToTargetX(this.TB_LogLevel, -DeltaVariation, 0.25, 225);
            AnimationLib.MoveToTargetX(this.lbl_CustomPatch, -DeltaVariation, 0.25, 225);
            AnimationLib.MoveToTargetX(this.CB_CustomPatch, -DeltaVariation, 0.25, 250);
            AnimationLib.MoveToTargetX(this.lbl_APIManager, -DeltaVariation, 0.25, 250);
            AnimationLib.MoveToTargetX(this.CB_APIManager, -DeltaVariation, 0.25, 275);

            AnimationLib.MoveToTargetY(this.lbl_Title, DeltaVariation, 0.25, 150);
            AnimationLib.MoveToTargetY(this.img_Utility, DeltaVariation, 0.25, 200);

            AnimationLib.MoveWindowToTargetY(this, 100, this.Top, 0.25);
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
            var OpOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.25));
            OpOut.Completed += (s, _) =>
                {
                    this.Close();
                    MainWindow.IsFocusOk = true;
                };
            this.BeginAnimation(OpacityProperty, OpOut);
        }

        private void BTN_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.BlockSaves)
                if (item)
                {
                    MessageBox.Show(
                        "There are some invalid values, fix it before saving.\nHelp: Starting items should not exceed the maximum value of 999999999 and the minum value of 0\nPort max value: 65535 and should be same of debug port\nStarting level max value: 9");
                    return;
                }

            this.SaveChanges();
        }

        private void BTN_ReloadConfig_Click(object sender, RoutedEventArgs e)
        {
            this.LoadConfig();
        }

        private void DiscardChanges()
        {
            var DG = MessageBox.Show("Are you sure?", "Warning", MessageBoxButton.YesNo);
            if (DG == MessageBoxResult.Yes) this.Close();
        }

        private void SaveChanges()
        {
            this.Close();
        }

        private void LoadConfig()
        {
            this.NeedToCheck = false;

            this.TB_Gold.Text = Settings.StartingGold.ToString();
            this.TB_Gems.Text = Settings.StartingGems.ToString();
            this.TB_StartingLevel.Text = Settings.StartingLevel.ToString();

            this.TB_Trophies.Text = Settings.StartingTrophies.ToString();
            this.TB_Experience.Text = Settings.StartingExperience.ToString();

            var versionData = FingerPrint.version.Split('.');
            this.TB_ClientVer.Text = versionData[0] + "." + versionData[1] + "." + versionData[2];
            this.TB_PatchServer.Text = Constants.Patching.ToString();
            this.TB_Maintenance.Text = Convert.ToString(Settings.MaintenanceDuration);
            this.TB_LogLevel.Text = ConfigurationManager.AppSettings["loggingLevel"];
            this.TB_Outdated.Text = ConfigurationManager.AppSettings["oldClientVersion"];
            this.TB_DebugPort.Text = ConfigurationManager.AppSettings["proDebugPort"];
            this.TB_Port.Text = "9339";

            var CN = ConfigurationManager.AppSettings["databaseConnectionName"];
            if (CN.ToLower() == "sqliteentities")
            {
                this.CN_T.IsSelected = true;
                this.CN_F.IsSelected = false;
            }
            else
            {
                this.CN_F.IsSelected = true;
                this.CN_T.IsSelected = false;
            }
            this.CN_F.IsSelected = true;
            this.CN_T.IsSelected = false;
            var CP = Convert.ToString(Constants.Patching);
            if (CP.ToLower() == "true")
            {
                this.CP_T.IsSelected = true;
                this.CP_F.IsSelected = false;
            }
            else
            {
                this.CP_F.IsSelected = true;
                this.CP_T.IsSelected = false;
            }

            var AM = Convert.ToString(Convert.ToBoolean(ConfigurationManager.AppSettings["apiManager"]));
            if (AM.ToLower() == "true")
            {
                this.AM_T.IsSelected = true;
                this.AM_F.IsSelected = false;
            }
            else
            {
                this.AM_F.IsSelected = true;
                this.AM_F.IsSelected = false;
            }

            var ED = Convert.ToString(Settings.Debug);
            if (ED.ToLower() == "true")
            {
                this.ED_T.IsSelected = true;
                this.ED_F.IsSelected = false;
            }
            else
            {
                this.ED_F.IsSelected = true;
                this.ED_T.IsSelected = false;
            }

            var EM = Convert.ToString(Settings.Maintenance);
            if (EM.ToLower() == "true")
            {
                this.EM_T.IsSelected = true;
                this.EM_F.IsSelected = false;
            }
            else
            {
                this.EM_F.IsSelected = true;
                this.EM_T.IsSelected = false;
            }

            this.NeedToCheck = true;
        }

        public bool[] BlockSaves = new bool[10];

        public bool NeedToCheck;

        private bool CheckValues(string X, int maxmax = 999999999, int minmin = 0)
        {
            int WOW;
            try
            {
                WOW = Convert.ToInt32(X);
            }
            catch (Exception)
            {
                return false;
            }

            if (WOW > maxmax || WOW < minmin) return false;

            return true;
        }

        SolidColorBrush GOOD = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x69, 0x7C)); // 0

        SolidColorBrush ERR = new SolidColorBrush(Color.FromArgb(0xFF, 0xB4, 0x2A, 0x0C)); // 2

        #region EVENTS

        private void TB_Gems_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsOk = this.CheckValues(this.TB_Gems.Text);
            if (IsOk)
            {
                this.TB_Gems.Background = this.GOOD;
                this.BlockSaves[0] = false;
            }
            else
            {
                this.TB_Gems.Background = this.ERR;
                this.BlockSaves[0] = true;
            }
        }

        private void TB_Port_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.NeedToCheck)
            {
                bool IsOk = this.CheckValues(this.TB_Port.Text, 65535);
                if (IsOk)
                {
                    this.TB_Port.Background = this.GOOD;
                    this.BlockSaves[8] = false;

                    int Con1 = 0, Con2 = 0;
                    try
                    {
                        Con1 = Convert.ToInt32(this.TB_DebugPort.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        Con2 = Convert.ToInt32(this.TB_Port.Text);
                    }
                    catch (Exception)
                    {
                    }

                    if (Con1 == Con2)
                    {
                        this.TB_Port.Background = this.ERR;
                        this.TB_DebugPort.Background = this.ERR;
                        this.BlockSaves[8] = true;
                        this.BlockSaves[9] = true;
                    }
                    else
                    {
                        bool IsOk1 = this.CheckValues(this.TB_DebugPort.Text, 65535);
                        if (IsOk1)
                        {
                            this.TB_DebugPort.Background = this.GOOD;
                            this.BlockSaves[9] = false;
                        }
                    }
                }
                else
                {
                    this.TB_Port.Background = this.ERR;
                    this.BlockSaves[8] = true;
                }
            }
        }

        private void TB_DebugPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.NeedToCheck)
            {
                bool IsOk = this.CheckValues(this.TB_DebugPort.Text, 65535);
                if (IsOk)
                {
                    this.TB_DebugPort.Background = this.GOOD;
                    this.BlockSaves[9] = false;
                    int Con1 = 0, Con2 = 0;
                    try
                    {
                        Con1 = Convert.ToInt32(this.TB_DebugPort.Text);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        Con2 = Convert.ToInt32(this.TB_Port.Text);
                    }
                    catch (Exception)
                    {
                    }

                    if (Con1 == Con2)
                    {
                        this.TB_Port.Background = this.ERR;
                        this.TB_DebugPort.Background = this.ERR;
                        this.BlockSaves[8] = true;
                        this.BlockSaves[9] = true;
                    }
                    else
                    {
                        bool IsOk1 = this.CheckValues(this.TB_Port.Text, 65535);
                        if (IsOk1)
                        {
                            this.TB_Port.Background = this.GOOD;
                            this.BlockSaves[8] = false;
                        }
                    }
                }
                else
                {
                    this.TB_DebugPort.Background = this.ERR;
                    this.BlockSaves[9] = true;
                }
            }
        }

        private void TB_Gold_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsOk = this.CheckValues(this.TB_Gold.Text);
            if (IsOk)
            {
                this.TB_Gold.Background = this.GOOD;
                this.BlockSaves[1] = false;
            }
            else
            {
                this.TB_Gold.Background = this.ERR;
                this.BlockSaves[1] = true;
            }
        }

        private void TB_Trophies_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsOk = this.CheckValues(this.TB_Trophies.Text, 99999, 0);
            if (IsOk)
            {
                this.TB_Trophies.Background = this.GOOD;
                this.BlockSaves[4] = false;
            }
            else
            {
                this.TB_Trophies.Background = this.ERR;
                this.BlockSaves[4] = true;
            }
        }

        private void TB_Experience_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsOk = this.CheckValues(this.TB_Experience.Text, 100000);
            if (IsOk)
            {
                this.TB_Experience.Background = this.GOOD;
                this.BlockSaves[5] = false;
            }
            else
            {
                this.TB_Experience.Background = this.ERR;
                this.BlockSaves[5] = true;
            }
        }

        private void TB_StartingLevel_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsOk = this.CheckValues(this.TB_StartingLevel.Text, 13);
            if (IsOk)
            {
                this.TB_StartingLevel.Background = this.GOOD;
                this.BlockSaves[7] = false;
            }
            else
            {
                this.TB_StartingLevel.Background = this.ERR;
                this.BlockSaves[7] = true;
            }
        }

        #endregion
    }
}