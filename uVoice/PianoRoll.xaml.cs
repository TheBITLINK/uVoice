using NAudio.Midi;
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
    /// Lógica de interacción para PianoRoll.xaml
    /// </summary>
    public partial class PianoRoll : UserControl
    {
        MidiEventCollection midiEvents;
        double xScale = 1.0 / 10;
        double yScale = 15;
        long lastPosition = 0;

        public MidiEventCollection MidiEvents
        {
            get
            {
                return midiEvents;
            }
            set
            {
                // a quarter note is 20 units wide
                xScale = (20.0 / value.DeltaTicksPerQuarterNote);
                midiEvents = value;
                lastPosition = 0;
                NoteCanvas.Children.Clear();
                for (int track = 0; track < midiEvents.Tracks; track++)
                {
                    foreach (MidiEvent midiEvent in value[track])
                    {
                        if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
                        {
                            NoteOnEvent noteOn = (NoteOnEvent)midiEvent;
                            if (noteOn.OffEvent != null)
                            {
                                Rectangle rectangle = MakeNoteRectangle(noteOn.NoteNumber, noteOn.AbsoluteTime, noteOn.NoteLength, noteOn.Channel);
                                NoteCanvas.Children.Add(rectangle);
                                lastPosition = Math.Max(lastPosition, noteOn.AbsoluteTime + noteOn.NoteLength);
                            }
                        }
                    }
                }
                this.Width = lastPosition * xScale;
                this.Height = 128 * yScale;
            }
        }

        private Rectangle MakeNoteRectangle(int noteNumber, long startTime, int duration, int channel)
        {
            Rectangle rect = new Rectangle();
            if (channel == 10)
            {
                rect.Stroke = new SolidColorBrush(Colors.DarkGreen);
                rect.Fill = new SolidColorBrush(Colors.LightGreen);
                duration = midiEvents.DeltaTicksPerQuarterNote / 4;
            }
            else
            {
                rect.Stroke = new SolidColorBrush(Colors.DarkBlue);
                rect.Fill = new SolidColorBrush(Colors.LightBlue);
            }
            rect.Width = (double)duration * xScale;
            rect.Height = yScale;
            rect.SetValue(Canvas.TopProperty, (double)(127 - noteNumber) * yScale);
            rect.SetValue(Canvas.LeftProperty, (double)startTime * xScale);
            return rect;
        }

        private void DrawGrid()
        {
            GridCanvas.Children.Clear();
            int beat = 0;
            for (long n = 0; n < lastPosition; n += midiEvents.DeltaTicksPerQuarterNote)
            {
                Line line = new Line();
                line.X1 = n * xScale;
                line.X2 = n * xScale;
                line.Y1 = 0;
                line.Y2 = 128 * yScale;
                if (beat % 4 == 0)
                {
                    line.Stroke = Brushes.White;
                }
                else
                {
                    line.Stroke = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
                }
                GridCanvas.Children.Add(line);
                beat++;
            }
        }

        private void DrawNotes()
{
    NoteCanvas.Children.Clear();
 
    NoteCanvas.Children.Add(MakeNoteRectangle(0, 0, midiEvents.DeltaTicksPerQuarterNote, 0));
    for (int track = 0; track < midiEvents.Tracks; track++)
    {
        foreach (MidiEvent midiEvent in midiEvents[track])
        {
            if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
            {
                NoteOnEvent noteOn = (NoteOnEvent)midiEvent;
                if (noteOn.OffEvent != null)
                {
                    Rectangle rectangle = MakeNoteRectangle(noteOn.NoteNumber, noteOn.AbsoluteTime, noteOn.NoteLength, noteOn.Channel);
                    NoteCanvas.Children.Add(rectangle);
                    lastPosition = Math.Max(lastPosition, noteOn.AbsoluteTime + noteOn.NoteLength);
                }
            }
        }
    }
    RootCanvas.Width = lastPosition * xScale;
    RootCanvas.Height = 128 * yScale;
    NoteBackgroundRenderScaleTransform.ScaleX = RootCanvas.Width;
}

        private void CreateBackgroundCanvas()
        {
            for (int note = 0; note < 128; note++)
            {
                if ((note % 12 == 1) // C#
                 || (note % 12 == 3) // Eb
                 || (note % 12 == 6) // F#
                 || (note % 12 == 8) // Ab
                 || (note % 12 == 10)) // Bb
                {
                    Rectangle rect = new Rectangle();
                    rect.Height = yScale;
                    rect.Width = 1;
                    rect.Fill = new SolidColorBrush(Color.FromArgb(64, 255, 255, 255));
                    rect.SetValue(Canvas.TopProperty, GetNoteYPosition(note));
                    NoteBackgroundCanvas.Children.Add(rect);
                }
            }
            for (int note = 0; note < 128; note++)
            {
                Line line = new Line();
                line.X1 = 0;
                line.X2 = 1;
                line.Y1 = GetNoteYPosition(note);
                line.Y2 = GetNoteYPosition(note);
                line.Stroke = Brushes.Transparent;
                NoteBackgroundCanvas.Children.Add(line);
            }
        }

        private double GetNoteYPosition(int note)
        {
            return yScale * note;
        }


        public PianoRoll()
        {
            InitializeComponent();
        }

        public PianoRoll(string pah)
        {
            InitializeComponent();
            MidiFile midiFile = new MidiFile(pah);
            midiEvents = midiFile.Events;
        }

        private void sv_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer1.ScrollToVerticalOffset(scrollViewer1.ScrollableHeight / 2);
        }
    }
}
