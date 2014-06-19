using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Profiles;
using ScreenCaptureAPI.Interfaces;
using ScreenCaptureAPI.Log;
using ScreenCaptureAPI.Models;

namespace ScreenCaptureAPI
{
    public class ConfigManager : IConfigManager
    {
        private readonly SettingsModel settings;

        public ConfigManager()
        {
            settings = ContainerManager.Resolve<IFileManager>().LoadSettingFromConfig();

            Logging.Info("*************************");
            Logging.Info("ConfigManager");
            Logging.Info("ScreenHeight: {0}", ScreenHeight);
            Logging.Info("ScreenWidth: {0}", ScreenWidth);

            Logging.Info("PathToVideoDirectory: {0}", PathToMovieDirectory);
            Logging.Info("FrameRate: {0}", FrameRate);
            Logging.Info("Quality: {0}", Quality);
            Logging.Info("Bitrate: {0}", Bitrate);
            Logging.Info("DefaultScreenshotFormat: {0}", DefaultScreenshotFormat);

            Logging.Info("*************************");
        }

        public Rectangle ScreenRectangle
        {
            get { return new Rectangle(0, 0, (int)ScreenWidth, (int)ScreenHeight); }
        }

        public double ScreenHeight
        {
            get { return System.Windows.SystemParameters.PrimaryScreenHeight; }
        }

        public double ScreenWidth
        {
            get { return System.Windows.SystemParameters.PrimaryScreenWidth; }
        }

        public bool CaptureMouseCursor
        {
            get
            {
                bool result = false;
                bool.TryParse(ConfigurationManager.AppSettings["CaptureMouseCursor"], out result);
                return result;
            }
        }

        public ImageFormat DefaultScreenshotFormat
        {
            get
            {
                string defaultScreenshotFormat = ConfigurationManager.AppSettings["DefaultScreenshotFormat"];
                return ParseScreenshotFormat(defaultScreenshotFormat);
            }
        }

        public string PathToMovieDirectory
        {
            get
            {
                string pathToDirectory = "";

                if (string.IsNullOrEmpty(settings == null ? "" : settings.PathToMovieDirectory))
                    pathToDirectory = ConfigurationManager.AppSettings["PathToVideoDirectory"];
                else
                    pathToDirectory = settings.PathToMovieDirectory;

                if (Directory.Exists(pathToDirectory))
                    return pathToDirectory;

                var result = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos) + @"\";
                Logging.Info("PathToVideoDirectory is empty or isn't valid.");
                Logging.Info("Default Video folder will be used: {0}", result);
                return result;
            }
        }

        public string PathToScreenshotDirectory
        {
            get
            {
                string pathToDirectory = "";
                if (string.IsNullOrEmpty(settings == null ? "" : settings.PathToScreenshotDirectory))
                    pathToDirectory = ConfigurationManager.AppSettings["PathToScreenshotDirectory"];
                else
                    pathToDirectory = settings.PathToScreenshotDirectory;

                if (Directory.Exists(pathToDirectory))
                    return pathToDirectory;

                var result = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures) + @"\";
                Logging.Info("PathToScreenshotDirectory is empty or isn't valid.");
                Logging.Info("Default Screenshot folder will be used: {0}", result);

                return result;
            }
        }

        public int FrameRate
        {
            get
            {
                var frameRateString = ConfigurationManager.AppSettings["FrameRate"];
                int frame = default(int);

                int.TryParse(frameRateString, out frame);

                return frame;
            }
        }

        public int Quality
        {
            get
            {
                if (settings != null)
                {
                    if (settings.Quality != 0)
                        return settings.Quality;
                }

                var qualityString = ConfigurationManager.AppSettings["Quality"];
                int quality = default(int);

                int.TryParse(qualityString, out quality);

                return quality;
            }
        }

        public ConstantBitrate Bitrate
        {
            get
            {
                var bitrateString = ConfigurationManager.AppSettings["Bitrate"];
                int bitrate = default(int);

                int.TryParse(bitrateString, out bitrate);

                return new ConstantBitrate(bitrate);
            }
        }

        public IEnumerable<EncoderDevice> AudioDevices
        {
            get { return EncoderDevices.FindDevices(EncoderDeviceType.Audio); }
        }

        public string AudioDevice
        {
            get
            {
                if (!string.IsNullOrEmpty(settings == null ? "" : settings.EncoderDeviceName))
                    return settings.EncoderDeviceName;
                return string.Empty;
            }
        }

        public string EncoderDeviceName
        {
            get
            {
                return settings.EncoderDeviceName;
            }
        }

        #region private

        private double DpiWidthFactor;
        private double DpiHeightFactor;

        private void CalculateDpiFactors()
        {
            Window MainWindow = Application.Current.MainWindow;
            PresentationSource MainWindowPresentationSource = PresentationSource.FromVisual(MainWindow);
            Matrix m = MainWindowPresentationSource.CompositionTarget.TransformToDevice;
            this.DpiWidthFactor = m.M11;
            this.DpiHeightFactor = m.M22;
        }

        private ImageFormat ParseScreenshotFormat(string defaultScreenshotFormat)
        {
            switch (defaultScreenshotFormat)
            {
                case "Bmp":
                    return ImageFormat.Bmp;

                case "Emf":
                    return ImageFormat.Emf;

                case "Exif":
                    return ImageFormat.Exif;

                case "Gif":
                    return ImageFormat.Gif;

                case "Icon":
                    return ImageFormat.Icon;

                case "Jpeg":
                    return ImageFormat.Jpeg;

                case "MemoryBmp":
                    return ImageFormat.MemoryBmp;

                case "Png":
                    return ImageFormat.Png;

                case "Tiff":
                    return ImageFormat.Tiff;

                case "Wmf":
                    return ImageFormat.Wmf;
                default:
                    {
                        Logging.Warning("DefaultScreenshotFormat parse error");
                        Logging.Info("DefaultScreenshotFormat will be used: Png");
                        return ImageFormat.Png;
                    }
            }
        }

        #endregion
    }
}
