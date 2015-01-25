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
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Threading;
using System.IO;

namespace uVoice.VoiceRecorder
{
    /// <summary>
    /// Lógica de interacción para RecorderCore.xaml
    /// </summary>
    public partial class RecorderCore : UserControl
    {
        int sampleRate = 22000;
        int channels = 1;
        WaveIn waveIn;
        string currentFileName;
        public FileInfo currentfile;
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;
        private MMDevice selectedDevice;
        WaveFileWriter writer;
        DirectoryInfo cachefld;
        string cachefldn;

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

        public void Play()
        {
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(cachefldn);
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.PlaybackStopped += waveOutDevice_PlaybackStopped;
            waveOutDevice.Play();
        }

        void waveOutDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
            }
            if (waveOutDevice != null)
            {
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
        }


        public RecorderCore(string fni)
        {
            currentFileName = fni;
            currentfile = new FileInfo(fni);
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            this.CaptureDevices = (IEnumerable<MMDevice>)Enumerable.ToArray<MMDevice>((IEnumerable<MMDevice>)deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active));
            MMDevice defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            this.SelectedDevice = Enumerable.FirstOrDefault<MMDevice>(this.CaptureDevices, (Func<MMDevice, bool>)(c => c.ID == defaultDevice.ID));
            InitializeComponent();
            CheckExistence();
        }

        void CheckExistence()
        {
            if (currentfile.Exists)
            {
                Random rd = new Random();
                cachefld = new DirectoryInfo(currentfile.Directory.Parent.Parent.Parent.FullName + "\\cache\\" + currentfile.Directory.Name);
                if (!cachefld.Exists)
                {
                    cachefld.Create();
                }
                cachefldn = currentfile.Directory.Parent.Parent.Parent.FullName + "\\cache\\" + currentfile.Directory.Name + "\\uvb" + rd.Next(10000);
                currentfile.CopyTo(cachefldn, true);
                waveView.WaveStream = new NAudio.Wave.WaveFileReader(cachefldn);
                waveView.Update();
                playfl.IsEnabled = true;
            }       
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

        bool recording = false;

        void TryRecord()
        {
            if (currentfile.Exists)
            {
                currentfile.Delete();
            }
            if (!recording)
            {
                waveIn = new WaveIn();
                waveIn.WaveFormat = new WaveFormat(sampleRate, channels);
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(
                    waveIn_DataAvailable);
                writer = new WaveFileWriter(currentfile.FullName, waveIn.WaveFormat);
                waveIn.StartRecording();
                recording = true;
            }
        }

        void TryStop()
        {
            if(recording)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                writer.Close();
                recording = false;
            }
            CheckExistence();
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TryRecord();
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TryStop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }
    }
}
