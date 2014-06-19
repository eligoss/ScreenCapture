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
using ScreenCaptureAPI.Interfaces;
using ScreenCaptureAPI.Models;

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
            var fileManager = ContainerManager.Resolve<IFileManager>();

            var settings = new SettingsModel
            {
                EncoderDeviceName = settingsViewModel.AudioDevices.ElementAt(this.AudioDevicesValue.SelectedIndex),
                Quality = settingsViewModel.Quality.ElementAt(this.QualityValue.SelectedIndex),
                PathToMovieDirectory = this.MovieDirectoryValue.Text,
                PathToScreenshotDirectory = this.ScreenshotDirectoryValue.Text,
                CaptureMouseCursor = this.IsCaptureMouseCursorValue.IsChecked.Value
            };
            fileManager.SaveSettingInConfig(settings);

            Window w = Window.GetWindow(this);
            w.Close();
        }
    }
}
