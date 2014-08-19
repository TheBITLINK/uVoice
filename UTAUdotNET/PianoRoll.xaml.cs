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
    /// Lógica de interacción para PianoRoll.xaml
    /// </summary>
    public partial class PianoRoll : UserControl
    {
        public PianoRoll()
        {
            InitializeComponent();
            
        }

        private void ge_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle sendr = (Rectangle)sender;
            sendr.Opacity = 0.8;
        }

        private void ge_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ge_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle sendr = (Rectangle)sender;
            sendr.Opacity = 1.0;
        }

        private void sv_Loaded(object sender, RoutedEventArgs e)
        {
            sv.ScrollToVerticalOffset(sv.ScrollableHeight / 2);
        }
    }
}
