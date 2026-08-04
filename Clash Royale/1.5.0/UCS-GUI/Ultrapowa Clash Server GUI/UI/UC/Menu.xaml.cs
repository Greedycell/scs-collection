using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CRS.UI.UC
{
    /// <summary>
    /// Logica di interazione per Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public RoutedEvent ClickEvent;
        public RoutedEvent OverEvent;
        public RoutedEvent RetireEvent;

        public Menu()
        {
            this.InitializeComponent();
            this.ClickEvent = ButtonBase.ClickEvent.AddOwner(typeof(Menu));
            this.OverEvent = MouseEnterEvent.AddOwner(typeof(Menu));
            this.RetireEvent = MouseLeaveEvent.AddOwner(typeof(Menu));

            this.Background = new SolidColorBrush(Color.FromRgb(0x00, 0x77, 0x9F));
            var RT = new RotateTransform(90);
            this.Arrow.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Arrow.RenderTransform = RT;
        }

        #region Events

        public event RoutedEventHandler Retire
        {
            add {
                this.AddHandler(this.OverEvent, value); }

            remove {
                this.RemoveHandler(this.OverEvent, value); }
        }

        public event RoutedEventHandler Over
        {
            add {
                this.AddHandler(this.OverEvent, value); }

            remove {
                this.RemoveHandler(this.OverEvent, value); }
        }

        public event RoutedEventHandler Click
        {

            add {
                this.AddHandler(this.ClickEvent, value); }

            remove {
                this.RemoveHandler(this.ClickEvent, value); }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            this.CaptureMouse();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();
                if (this.IsMouseOver) this.RaiseEvent(new RoutedEventArgs(this.ClickEvent, this));
            }
        }

        #endregion

        public string NameLabel
        {
            get
            {
                return this.Name.Content.ToString();
            }

            set
            {
                this.Name.Content = value;
                if (this.IsArrowEnabled)
                {
                    this.Name.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    this.Arrow.Margin = new Thickness(0, 0, -this.Name.DesiredSize.Width - 16, 0);
                    this.ACAB.Margin = new Thickness(-24, 0, 0, 0);
                }  
            }
        }

        public ImageSource ImageLink
        {
            get
            {
                return this.Icon.Source;
            }

            set
            {
                this.Icon.Source = value;
            }
        }

        public ImageSource ImageArrow
        {
            get
            {
                return this.Arrow.Source;
            }

            set
            {
                this.Arrow.Source = value;
                this.IsArrowEnabled = true;
            }
        }

        private bool _IsArrowEnabled = false;
        public bool IsArrowEnabled
        {
            get
            {
                return this._IsArrowEnabled;
            }

            set
            {
                this._IsArrowEnabled = value;
            }
        }

        private bool _IsPressed = false;
        public bool IsPressed
        {
            get
            {
                return this._IsPressed;
            }

            set
            {
                this._IsPressed = value;
                if (value)
                {
                    AnimationLib.RotateImage(this.Arrow, 180, 0.25);
                    AnimationLib.ChangeBackgroundColor(this, Color.FromRgb(0x00, 0x4c, 0x65), 0.2);
                }
                else
                {
                    AnimationLib.RotateImage(this.Arrow, 90, 0.25);
                    AnimationLib.ChangeBackgroundColor(this, Color.FromRgb(0x00, 0x77, 0x9F), 0.2);
                }
            }
        }
    }
}
