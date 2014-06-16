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

        }

        private void Copy_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CaptureWindowViewModel).SelectedRect = new Rect(0, 0, 1, 1);
            this.Visibility = System.Windows.Visibility.Collapsed;
 
        }
    }
}
