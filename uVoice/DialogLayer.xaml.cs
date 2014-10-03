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
using Xceed.Wpf.Toolkit;

namespace uVoice
{
    /// <summary>
    /// Lógica de interacción para DialogLayer.xaml
    /// </summary>
    public partial class DialogLayer : UserControl
    {
        public DialogLayer()
        {
            InitializeComponent();
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Memory = e.Exception;
            mwnd.dialogLayer.ShowError("Se ha producido un error inesperado.", new RoutedEventHandler(error));
        }

        MainWindow mwnd = (MainWindow)App.Current.MainWindow;
        Exception Memory { get; set; }
        void error(object sender, RoutedEventArgs e)
        {
            Rect DialBnd = new Rect();
            DialBnd.Width = 320;
            DialBnd.Height = 240;
            mwnd.dialogLayer.ShowWindow(new Meta.ErrDialog(Memory, false), DialBnd, "Detalles del error", true);
        }

        public void ShowWindow(UIElement content, Rect bounds, string caption, bool modal)
        {
            ChildWindow cw = new ChildWindow();
            cw.Width = bounds.Width;
            cw.Height = bounds.Height;
            cw.Left = bounds.Left;
            cw.Top = bounds.Top;
            cw.Caption = caption;
            cw.Content = content;
            cw.IsModal = modal;
            Wnx.Children.Add(cw);
            cw.Show();
        }

        public void ShowError(string Message)
        {
            errlabel.Content = Message;
            errinfobtn.Visibility = System.Windows.Visibility.Hidden;
            ErrorDial.Visibility = System.Windows.Visibility.Visible;
        }

        public RoutedEventHandlerInfo[] GetRoutedEventHandler(UIElement eL, RoutedEvent rE)
        {
            var eventHandlersStoreProperty = typeof(UIElement).GetProperty("EventHandlersStore", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            object eventHandlersStore = eventHandlersStoreProperty.GetValue(eL, null);
            var GetRoutedEventHandlers = eventHandlersStore.GetType().GetMethod("GetRoutedEventHandlers", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            var routedEventHendlers = (RoutedEventHandlerInfo[])GetRoutedEventHandlers.Invoke(eventHandlersStore, new object[] { rE });
            return routedEventHendlers;
        }

        public void ShowError(string Message, RoutedEventHandler InfoButtonClick)
        {
            try
            {
                var rEh = GetRoutedEventHandler(errinfobtn, Button.ClickEvent);
                foreach (var rE in rEh)
                {
                    errinfobtn.Click -= (RoutedEventHandler)rE.Handler;
                }
            }
            catch { };
            errinfobtn.Click += InfoButtonClick;
            errlabel.Content = Message;
            errinfobtn.Visibility = System.Windows.Visibility.Visible;
            ErrorDial.Visibility = System.Windows.Visibility.Visible;
        }

        private void errok_Click(object sender, RoutedEventArgs e)
        {
            errinfobtn.Visibility = System.Windows.Visibility.Hidden;
            ErrorDial.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
