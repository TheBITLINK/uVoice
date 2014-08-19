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

namespace UTAUdotNET
{
    /// <summary>
    /// Lógica de interacción para SideMenu.xaml
    /// </summary>
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
        }

        private void ListBoxItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window1 VentanadeLocura = new Window1();
            VentanadeLocura.Show();
        }

        private void ListBoxItem_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainlb.SelectedIndex = -1;
        }
    }
}
