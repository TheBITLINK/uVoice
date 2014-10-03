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

namespace uVoice.Meta
{
    /// <summary>
    /// Lógica de interacción para ErrDialog.xaml
    /// </summary>
    public partial class ErrDialog : UserControl
    {
        public ErrDialog(Exception ex)
        {
            InitializeComponent();
            errmsg.Text = ex.Message;
            stacktrace.IsEnabled = false;
            stacktrace.Text = "Stack trace: \n\n" + ex.StackTrace;
        }

        public ErrDialog(Exception ex, bool reportable)
        {
            InitializeComponent();
            errmsg.Text = ex.Message;
            stacktrace.IsEnabled = false;
            stacktrace.Text = "Stack trace: \n\n" + ex.StackTrace;
            reportbtn.IsEnabled = reportable;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
