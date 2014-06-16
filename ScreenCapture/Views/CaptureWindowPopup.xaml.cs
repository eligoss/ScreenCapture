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
using ScreenCapture.ViewModels;
using ScreenCaptureAPI;
using ScreenCaptureAPI.Models;
using System.Drawing;
using System.Windows.Interop;

namespace ScreenCapture.Views
{
    /// <summary>
    /// Interaction logic for CaptureWindowPopup.xaml
    /// </summary>
    public partial class CaptureWindowPopup : UserControl
    {
        public CaptureWindowPopup()
        {
            InitializeComponent();
        }

        private void Screenshot_Button_Click(object sender, RoutedEventArgs e)
        {
            var captureAPI = ContainerManager.Resolve<CaptureAPI>();

            PreviewCaptureWindow pcw = new PreviewCaptureWindow();
            Window window = Window.GetWindow(this);
            CaptureWindowViewModel cwvm = window.DataContext as CaptureWindowViewModel;
            window.Close();

            Rect r = (DataContext as CaptureWindowViewModel).SelectedRect;
            r.Width += 1;
            r.Height += 1;

            System.Windows.Controls.Image s = new System.Windows.Controls.Image();

            ScreenshotConfigModel screenshot = captureAPI.TakeScreenShot(new ScreenshotConfigModel
            {
                UpperLeftSource = new System.Drawing.Point((int)r.Left, (int)r.Top),
                BlockRegionSize = new System.Drawing.Size((int)r.Width, (int)r.Height)
            });

            s.Source = Bitmap2BitmapImage(screenshot.Image, (int)r.Width, (int)r.Height);

            pcw.DataContext = new PreViewCaptureWindowViewModel(r.Width, r.Height, s.Source, cwvm, screenshot);
            pcw.ShowDialog();
        }

        private BitmapSource Bitmap2BitmapImage(Bitmap bitmap, int width, int height)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            bitmap.GetHbitmap(),
            IntPtr.Zero,
            System.Windows.Int32Rect.Empty,
            BitmapSizeOptions.FromWidthAndHeight(width, height));
        }


        private void MovieCapture_Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Capture video
        }

        private void BackSelection_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CaptureWindowViewModel).IsFullScreen = false;
            (DataContext as CaptureWindowViewModel).SelectedRect = new Rect(0, 0, 1, 1);
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}
