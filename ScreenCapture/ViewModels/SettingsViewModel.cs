using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ScreenCaptureAPI;

namespace ScreenCapture.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private List<string> audioDevices;
        private List<int> quality;
        private bool captureMouseCursor;
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel(IConfigManager ConfigManager)
        {
            this.AudioDevices = ConfigManager.AudioDevices.Select(q => q.Name).ToList();

            var AudioDevice = ConfigManager.AudioDevices.Where(q => q.Name == ConfigManager.EncoderDeviceName).ToList();

            if (AudioDevice.Count == 1)
            {
                List<string> a = ConfigManager.AudioDevices.Select(q => q.Name).ToList();
                this.audioDeviceIndex = a.IndexOf(ConfigManager.EncoderDeviceName);
            }
            this.Quality = GetQulityList().ToList();
            this.CaptureMouseCursor = ConfigManager.CaptureMouseCursor;
            this.ScreenshotDirectory = ConfigManager.PathToScreenshotDirectory;
            this.MovieDirectory = ConfigManager.PathToMovieDirectory;
        }

        public string MovieDirectory { get; set; }
        public string ScreenshotDirectory { get; set; }
        public int audioDeviceIndex { get; set; }


        public List<string> AudioDevices
        {
            get { return audioDevices; }
            set
            {
                audioDevices = value;
                NotifyPropertyChanged("AudioDevices");
            }
        }

        public List<int> Quality
        {
            get { return quality; }
            set
            {
                quality = value;
                NotifyPropertyChanged("Quality");
            }
        }

        public bool CaptureMouseCursor
        {
            get { return captureMouseCursor; }
            set
            {
                captureMouseCursor = value;
                NotifyPropertyChanged("CaptureMouseCursor");
            }
        }

        private IEnumerable<int> GetQulityList()
        {
            for (int i = 100; i > 0; i -= 10)
            {
                yield return i;
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class BoolToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value == true) ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value == 0) ? true : false;
        }
    }
}

