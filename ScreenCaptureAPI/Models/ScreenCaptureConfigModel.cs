using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Profiles;

namespace ScreenCaptureAPI.Models
{
    public class ScreenCaptureConfigModel
    {
        private readonly IConfigManager configManager;

        public ScreenCaptureConfigModel()
        {
            this.configManager = ContainerManager.Resolve<IConfigManager>();

            //Initialize with default values.
            this.Quality = this.configManager.Quality;
            this.FrameRate = this.configManager.FrameRate;
            this.Bitrate = this.configManager.Bitrate;
            this.screenRectangle = this.configManager.ScreenRectangle;
            this.OutputScreenCaptureFullPath = CreateFileFullPath();
        }

        private int pQuality;

        public bool CaptureMouseCursor { get; set; }

        private IEnumerable<EncoderDevice> pAudioDevices;

        private EncoderDevice pAudioDevice;

        public FileType FileType { get; set; }

        public Rectangle screenRectangle { get; set; }

        public double FrameRate { get; set; }

        public ConstantBitrate Bitrate { get; set; }

        public string OutputScreenCaptureFullPath { get; set; }

        /// <summary>
        /// Value must in range 0<=x<=100
        /// </summary>   
        public int Quality
        {
            get { return pQuality; }
            set
            {
                if (value >= 0 && value <= 101)
                    pQuality = value;
            }
        }

        public EncoderDevice AudioDevice
        {
            get
            {
                if (pAudioDevice == null)
                    return AudioDevices.First(item => item.Name.Contains(@"Speakers"));
                else
                    return pAudioDevice;

            }
            set { pAudioDevice = value; }
        }

        public IEnumerable<EncoderDevice> AudioDevices
        {
            get
            {
                if (pAudioDevices == null)
                    return EncoderDevices.FindDevices(EncoderDeviceType.Audio);
                else
                    return pAudioDevices;

            }
            set { pAudioDevices = value; }
        }

        private string CreateFileFullPath()
        {
            var pathToDirectory = configManager.PathToVideoDirectory;
            var tmpFileName = CreateFileName();
            var fileType = FileType.Avi.ToString();

            return string.Format("{0}{1}.{2}", pathToDirectory, tmpFileName, fileType);
        }

        private string CreateFileName()
        {
            var date = DateTime.Now;
            return (date.Year.ToString() + "." + date.Month.ToString() + "." + date.Day.ToString() + " " + date.TimeOfDay.ToString()).Replace(":", "-");
        }
    }
}
