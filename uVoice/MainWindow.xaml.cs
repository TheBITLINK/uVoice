using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace uVoice
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Un truquito para que la ventana se vea bonita C:
        WindowRendering wdr = new WindowRendering();
        DispatcherTimer DoubleClickCount = new DispatcherTimer();
        bool Clicked { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Clicked = false;
            DoubleClickCount.Interval = TimeSpan.FromMilliseconds(200);
            DoubleClickCount.Tick += DoubleClickCount_Tick;
        }

        void DoubleClickCount_Tick(object sender, EventArgs e)
        {
            Clicked = false;
            DoubleClickCount.Stop();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Version win8version = new Version(6, 2, 9200, 0);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
                Environment.OSVersion.Version >= win8version)
            {
                if (System.IO.Directory.Exists(Environment.GetEnvironmentVariable("HOMEDRIVE") + "\\AeroGlass\\"))
                {
                    TransparencyEnabled = wdr.ExtendDwm(this);
                }
                else
                {
                    TransparencyEnabled = false;
                }
            }
            else
            {
                TransparencyEnabled = wdr.ExtendDwm(this);
            }
            if (!TransparencyEnabled)
            {
                titlebar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#111111"));
                controlarea.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                mainmenu.BorderBrush = Brushes.Transparent;
                projectnameblur.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        bool Transparent { get; set; }
        bool TransparencyEnabled { get; set; }

        public void Transparency(bool status)
        {
            if (TransparencyEnabled)
            {
                Transparent = status;
                if (!status)
                {
                    titlebar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#111111"));
                    controlarea.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                    mainmenu.BorderBrush = Brushes.Transparent;
                    projectnameblur.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    titlebar.Background = Brushes.Transparent;
                    controlarea.Background = null;
                    mainmenu.BorderBrush = Brushes.White;
                    projectnameblur.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TitleBar();
        }

        private void MaximizeOrRestore()
        {
            if (WindowState == System.Windows.WindowState.Maximized)
            {
                MaxHeight = double.PositiveInfinity;
                MaxWidth = double.PositiveInfinity;
                this.WindowState = System.Windows.WindowState.Normal;
                Clicked = false;
                adr.Visibility = System.Windows.Visibility.Hidden;
                Transparency(true);
                return;
            }
            if (WindowState == System.Windows.WindowState.Normal)
            {
                System.Drawing.Point pt =
                        System.Windows.Forms.Cursor.Position;
                System.Windows.Forms.Screen currentScreen;
                currentScreen =
                        System.Windows.Forms.Screen.FromPoint(pt);
                if (currentScreen.Primary)
                {
                    MaxHeight =
                        SystemParameters.MaximizedPrimaryScreenHeight;
                    MaxWidth =
                        SystemParameters.MaximizedPrimaryScreenWidth;
                }
                else
                {
                    MaxHeight = double.PositiveInfinity;
                    MaxWidth = double.PositiveInfinity;
                }
                this.WindowState = System.Windows.WindowState.Maximized;
                adr.Visibility = System.Windows.Visibility.Visible;
                Transparency(false);
                Clicked = false;
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard MenuShow = (Storyboard)this.Resources["SidemenuShow"];
            MenuShow.Begin();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maxres_Click(object sender, RoutedEventArgs e)
        {
            this.MaximizeOrRestore();
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void grid1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Storyboard MenuHide = (Storyboard)this.Resources["SidemenuHide"];
            MenuHide.Begin();
        }

        private void projectname_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TitleBar();
        }

        public void TitleBar()
        {
            if (!Clicked)
            {
                DragMove();
                Clicked = true;
                DoubleClickCount.Start();
                return;
            }
            else
            {
                this.MaximizeOrRestore();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            settings.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
