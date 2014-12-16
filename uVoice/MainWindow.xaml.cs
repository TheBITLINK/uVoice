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
using System.IO;

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
            sideMenu.op_open.MouseUp += op_open_MouseUp;
            DirectoryInfo cache = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CoreData\\cache");
            try
            {
                cache.Delete(true);
            }
            catch { }
            MenuHide = (Storyboard)this.Resources["SidemenuHide"];
            MenuHide.Completed += MenuHide_Completed;
        }

        void MenuHide_Completed(object sender, EventArgs e)
        {
            if(Transparent)
            {
                projectname.Foreground = Brushes.Black;
            }
            sideMenu.Visibility = System.Windows.Visibility.Hidden;
        }

        System.Windows.Forms.OpenFileDialog opf = new System.Windows.Forms.OpenFileDialog();

        void op_open_MouseUp(object sender, MouseButtonEventArgs e)
        {
            opf.ShowDialog();
            controlarea.Children.Remove(mainpiano);
            mainpiano = new PianoRoll();
            mainpiano.Margin = new Thickness(0, 46, 0, 0);
            mainpiano.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF333333"));
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
                //titlebar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#111111"));
                controlarea.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                mainmenu.BorderBrush = Brushes.Transparent;
                projectnameblur.Opacity = 0;
                projectname.Foreground = Brushes.White;
                time_glow.Opacity = 0;
                time.Foreground = Brushes.White;
                username.Foreground = Brushes.White;
                usernameblur.Opacity = 0;
            }
            if (this.WindowState != System.Windows.WindowState.Maximized)
            {
                Transparent = true;
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
                    //titlebar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#111111"));
                    controlarea.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                    mainmenu.BorderBrush = Brushes.Transparent;
                    projectnameblur.Opacity = 0;
                    projectname.Foreground = Brushes.White;
                    time_glow.Opacity = 0;
                    time.Foreground = Brushes.White;
                    username.Foreground = Brushes.White;
                    usernameblur.Opacity = 0;
                }
                else
                {
                    //titlebar.Background = Brushes.Transparent;
                    controlarea.Background = null;
                    mainmenu.BorderBrush = Brushes.White;
                    projectnameblur.Opacity = 1;
                    projectname.Foreground = Brushes.Black;
                    time_glow.Opacity = 1;
                    time.Foreground = Brushes.Black;
                    username.Foreground = Brushes.Black;
                    usernameblur.Opacity = 1;
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
            sideMenu.Visibility = System.Windows.Visibility.Visible;
            Storyboard MenuShow = (Storyboard)this.Resources["SidemenuShow"];
            MenuShow.Begin();
            projectname.Foreground = Brushes.White;
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

        Storyboard MenuHide { get; set; }

        private void grid1_MouseUp(object sender, MouseButtonEventArgs e)
        {
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

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                LayoutRoot.Margin = new Thickness(4);
            }
            else
            {
                LayoutRoot.Margin = new Thickness(4,0,4,4);
            }
        }
    }
}
