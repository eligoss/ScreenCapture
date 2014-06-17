using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture.ViewModels
{
    public class SettingsViewModel
    {
        public Dictionary<string, string> AudioDevices { get; set; }
        public Dictionary<string, string> Quality { get; set; }
        public Dictionary<string, string> FrameRate { get; set; }
        public Dictionary<string, string> Bitrate { get; set; }
        public bool CaptureMouseCursor { get; set; }
        public string DefaultVideoFolder { get; set; }
        public string DefaultScreensFolder { get; set; }
    }
}
