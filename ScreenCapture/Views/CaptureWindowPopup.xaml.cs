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

        }

        private void MovieCapture_Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Capture video
        }

        private void BackSelection_Button_Click(object sender, RoutedEventArgs e)
        {
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
