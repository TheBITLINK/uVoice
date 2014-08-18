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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UTAUdotNET
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
            wdr.ExtendDwm(this);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void MaximizeOrRestore()
        {
            if (WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                Clicked = false;
                adr.Visibility = System.Windows.Visibility.Hidden;
                return;
            }
            if (WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                adr.Visibility = System.Windows.Visibility.Visible;
                Clicked = false;
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 VentanadeLocura = new Window1();
            VentanadeLocura.Show();
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
    }
}
