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
using System.Windows.Shapes;
using ScreenCapture.ViewModels;
using ScreenCaptureAPI;

namespace ScreenCapture.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow(this);
            w.Close();
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            var settingsViewModel = (DataContext as SettingsViewModel);
            var configManager = ContainerManager.Resolve<IConfigManager>();
            configManager.EncoderDeviceName = settingsViewModel.AudioDevices.ElementAt(this.AudioDevicesValue.SelectedIndex);
            configManager.Quality = settingsViewModel.Quality.ElementAt(this.QualityValue.SelectedIndex);
            configManager.PathToMovieDirectory = this.MovieDirectoryValue.Text;
            configManager.PathToScreenshotDirectory = this.ScreenshotDirectoryValue.Text;
            configManager.CaptureMouseCursor = this.IsCaptureMouseCursorValue.IsChecked.Value;

            Window w = Window.GetWindow(this);
            w.Close();
        }
    }
}
