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

namespace uVoice
{
    /// <summary>
    /// Lógica de interacción para Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            BgCol.AvailableColors.Clear();
            BgCol.AvailableColors.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.White, "Blanco"));
            BgCol.AvailableColors.Add(new Xceed.Wpf.Toolkit.ColorItem(Colors.Black, "Negro"));
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow ok = (MainWindow)Application.Current.MainWindow;
            ok.TitleBar();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((Label)((TabItem)e.AddedItems[0]).Header).Foreground = (SolidColorBrush)App.Current.FindResource("AccentColorBrush");
            ((Label)((TabItem)e.AddedItems[0]).Header).FontWeight = FontWeight.FromOpenTypeWeight(400);
            try
            {
                ((Label)((TabItem)e.RemovedItems[0]).Header).Foreground = (SolidColorBrush)App.Current.FindResource("BlackBrush");
                ((Label)((TabItem)e.RemovedItems[0]).Header).FontWeight = FontWeight.FromOpenTypeWeight(100);
            }
            catch { }
        }

        private void AcCol_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            App.Current.Resources["AccentColor"] = e.NewValue;
            App.Current.Resources["AccentColorBrush"] = new SolidColorBrush(e.NewValue);

        }
    }
}
