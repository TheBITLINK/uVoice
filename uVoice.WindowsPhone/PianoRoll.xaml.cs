using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace uVoice.WindowsPhone
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

        private void sv_Loaded(object sender, RoutedEventArgs e)
        {
            sv.ScrollToVerticalOffset(sv.ScrollableHeight / 2);
        }
    }
}
