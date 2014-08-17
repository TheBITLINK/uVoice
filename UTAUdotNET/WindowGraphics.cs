using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace UTAUdotNET
{
    public class WindowRendering
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;      // width of left border that retains its size 
            public int cxRightWidth;     // width of right border that retains its size 
            public int cyTopHeight;      // height of top border that retains its size 
            public int cyBottomHeight;   // height of bottom border that retains its size
        };


        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(
            IntPtr hwnd,
            ref MARGINS pMarInset);

        public void ExtendDwm(Window wnd)
        {
            try
            {
                // Obtain the window handle for WPF application
                IntPtr mainWindowPtr = new WindowInteropHelper(wnd).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

                // Get System Dpi
                System.Drawing.Graphics desktop = System.Drawing.Graphics.FromHwnd(mainWindowPtr);
                float DesktopDpiX = desktop.DpiX;
                float DesktopDpiY = desktop.DpiY;

                // Set Margins
                WindowRendering.MARGINS margins = new WindowRendering.MARGINS();

                // Extend glass frame into client area 
                // Note that the default desktop Dpi is 96dpi. The  margins are 
                // adjusted for the system Dpi.
                margins.cxLeftWidth = 5;
                margins.cxRightWidth = 5;
                margins.cyTopHeight = Convert.ToInt32((72 + 5) * (DesktopDpiX / 96));
                margins.cyBottomHeight = 5;

                int hr = WindowRendering.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                // 
                if (hr < 0)
                {
                    //DwmExtendFrameIntoClientArea Failed
                }
            }
            // If not Vista, paint background white. 
            catch (DllNotFoundException)
            {
                Application.Current.MainWindow.Background = Brushes.Black;
            }
        }
    }
}
