namespace ScreenCaptureAPI.Models
{
    public class SettingsModel
    {
        public string EncoderDeviceName { get; set; }
        public int Quality { get; set; }
        public string PathToMovieDirectory { get; set; }
        public string PathToScreenshotDirectory { get; set; }
        public bool CaptureMouseCursor { get; set; }
    }
}
