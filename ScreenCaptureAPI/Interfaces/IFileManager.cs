using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ScreenCaptureAPI.Models;

namespace ScreenCaptureAPI.Interfaces
{
    public interface IFileManager
    {
        void SaveScreenShot(Bitmap image, ImageFormat imageFormat, string fullPath);

        void SaveSettingInConfig(SettingsModel settingsModel);

        SettingsModel LoadSettingFromConfig();
    }
}
