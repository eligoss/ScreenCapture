using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Expression.Encoder.Profiles;

namespace ScreenCaptureAPI
{
    public interface IConfigManager
    {
        double ScreenHeight { get; }
        double ScreenWidth { get; }
        Rectangle ScreenRectangle { get; }

        string PathToVideoDirectory { get; }
        string PathToScreenshotDirectory { get; }

        int FrameRate { get; }
        int Quality { get; }
        ConstantBitrate Bitrate { get; }

        ImageFormat DefaultScreenshotFormat { get; }
    }
}
