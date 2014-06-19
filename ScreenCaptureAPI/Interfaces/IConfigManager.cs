using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Profiles;

namespace ScreenCaptureAPI
{
    public interface IConfigManager
    {
        double ScreenHeight { get; }
        double ScreenWidth { get; }
        Rectangle ScreenRectangle { get; }

        string PathToMovieDirectory { get; }
        string PathToScreenshotDirectory { get; }
        bool CaptureMouseCursor { get; }
        string EncoderDeviceName { get; }

        IEnumerable<EncoderDevice> AudioDevices { get; }
        string AudioDevice { get; }

        int FrameRate { get; }
        int Quality { get; }
        ConstantBitrate Bitrate { get; }

        ImageFormat DefaultScreenshotFormat { get; }
    }
}
