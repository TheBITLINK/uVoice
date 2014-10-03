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

namespace uVoice.VoiceRecorder
{
    /// <summary>
    /// Lógica de interacción para MainUI.xaml
    /// </summary>
    public partial class MainUI : UserControl
    {
        public MainUI()
        {
            InitializeComponent();
            RecListHelper rlh = new RecListHelper(RLB);
            rlh.LoadRLArray(DefRL.CV.JapanaseR);
            JPrCV.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        MainWindow mwnd = (MainWindow)App.Current.MainWindow;

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow ok = (MainWindow)Application.Current.MainWindow;
            ok.TitleBar();
        }

        Exception Memory { get; set; }

        void error(object sender, RoutedEventArgs e)
        {
            Rect DialBnd = new Rect();
            DialBnd.Width = 320;
            DialBnd.Height = 240;
            mwnd.dialogLayer.ShowWindow(new Meta.ErrDialog(Memory, false), DialBnd, "Detalles del error", true);
        }
        private void All_Click(object sender, RoutedEventArgs e)
        {
            RecListHelper rlh = new RecListHelper(RLB);
            JPrCV.IsChecked = false;
            JPCV.IsChecked = false;
            ENCV.IsChecked = false;
            ESCV.IsChecked = false;
            JPrCV.IsChecked = false;
            JPCV.IsChecked = false;
            ENCV.IsChecked = false;
            ESCV.IsChecked = false;
            CUSTOMRL.IsChecked = false;
            MenuItem mI = (MenuItem)sender;
            switch (mI.Name)
            {
                case "JPrCV":
                    JPrCV.IsChecked = true;
                    rlh.LoadRLArray(DefRL.CV.JapanaseR);
                    break;
                case "JPCV":
                    JPCV.IsChecked = true;
                    rlh.LoadRLArray(DefRL.CV.Japanase);
                    break;
                case "ENCV":
                    ENCV.IsChecked = true;
                    rlh.LoadRLArray(DefRL.CV.English);
                    break;
                case "ESCV":
                    ESCV.IsChecked = true;
                    rlh.LoadRLArray(DefRL.CV.Spanish);
                    break;
                case "JPrVCV":
                    JPrCV.IsChecked = true;
                    rlh.LoadRLArray(DefRL.VCV.JapanaseR);
                    break;
                case "JPVCV":
                    JPVCV.IsChecked = true;
                    rlh.LoadRLArray(DefRL.VCV.Japanase);
                    break;
                case "ENVCV":
                    ENVCV.IsChecked = true;
                    rlh.LoadRLArray(DefRL.VCV.English);
                    break;
                case "ESVCV":
                    ESVCV.IsChecked = true;
                    rlh.LoadRLArray(DefRL.VCV.Spanish);
                    break;
                case "CUSTOMRL":
                    System.Windows.Forms.OpenFileDialog opf = new System.Windows.Forms.OpenFileDialog();
                    opf.Filter = "Archivos RecList|*.txt|Todo|*.*";
                    opf.Multiselect = false;
                    opf.ShowDialog();
                    try
                    {
                        rlh.LoadRLFile(opf.FileName);
                    }
                    catch(Exception ex)
                    {
                        Memory = ex;
                        mwnd.dialogLayer.ShowError("Se ha producido un error al intentar abrir el archivo.", new RoutedEventHandler(error));
                    };
                    break;
            }
        }
    }
}
