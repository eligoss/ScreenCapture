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

namespace ScreenCapture.Views
{
    /// <summary>
    /// Interaction logic for PreviewCaptureButtonsPanel.xaml
    /// </summary>
    public partial class PreviewCaptureButtonsPanel : UserControl
    {
        public PreviewCaptureButtonsPanel()
        {
            InitializeComponent();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var preViewCaptureModel = (DataContext as PreViewCaptureWindowViewModel);
            using(var screenManager = ContainerManager.Resolve<CaptureAPI>())
            {
                screenManager.SaveScreenShot(preViewCaptureModel.ScreenshotConfigModel);
            }
            Window w = Window.GetWindow(this);
            w.Close();
        }

        private void Copy_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetImage((DataContext as PreViewCaptureWindowViewModel).ScreenshotConfigModel.Image);
            Window.GetWindow(this).Close();
            System.Windows.Forms.MessageBox.Show("Picture copied to Clipboard!");
            Window w = Window.GetWindow(this);
            w.Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow(this);
            w.Close();
        }
    }
}
