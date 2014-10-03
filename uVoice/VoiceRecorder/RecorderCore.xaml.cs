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
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Threading;

namespace uVoice.VoiceRecorder
{
    /// <summary>
    /// Lógica de interacción para RecorderCore.xaml
    /// </summary>
    public partial class RecorderCore : UserControl
    {
        private MMDevice selectedDevice;
        private int sampleRate;
        private int bitDepth;
        private int channelCount;
        private int sampleTypeIndex;
        private WasapiCapture capture;
        private WaveFileWriter writer;
        private string currentFileName;
        private string message;
        private float peak;
        private readonly SynchronizationContext synchronizationContext;
        private float recordLevel;
        private int shareModeIndex;

        public IEnumerable<MMDevice> CaptureDevices { get; private set; }

        public MMDevice SelectedDevice
        {
            get
            {
                return this.selectedDevice;
            }
            set
            {
                if (this.selectedDevice == value)
                    return;
                this.selectedDevice = value;
            }
        }

        public RecorderCore()
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            this.CaptureDevices = (IEnumerable<MMDevice>)Enumerable.ToArray<MMDevice>((IEnumerable<MMDevice>)deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active));
            MMDevice defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            this.SelectedDevice = Enumerable.FirstOrDefault<MMDevice>(this.CaptureDevices, (Func<MMDevice, bool>)(c => c.ID == defaultDevice.ID));
            //cm.ItemsSource = CaptureDevices;
            //cm.SelectedItem = defaultDevice;
            InitializeComponent();
        }

        private void helpbtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //bnol.Background = Brushes.White;
            //kbhelp.Visibility = System.Windows.Visibility.Visible;
        }

        private void helpbtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //bnol.Background = Brushes.Transparent;
            //kbhelp.Visibility = System.Windows.Visibility.Hidden;
        }

        private void helpbtn_MouseLeave(object sender, MouseEventArgs e)
        {
            bnol.Background = Brushes.Transparent;
            kbhelp.Visibility = System.Windows.Visibility.Hidden;
            viewer.Visibility = System.Windows.Visibility.Visible;
        }

        private void bnol_MouseEnter(object sender, MouseEventArgs e)
        {
            bnol.Background = Brushes.White;
            kbhelp.Visibility = System.Windows.Visibility.Visible;
            viewer.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
