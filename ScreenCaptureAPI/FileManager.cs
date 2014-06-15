using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ScreenCaptureAPI.Interfaces;
using ScreenCaptureAPI.Log;

namespace ScreenCaptureAPI
{
    public class FileManager : IFileManager
    {
        public void SaveToFile(Stream inputStream, string destPath)
        {
            try
            {
                using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                {
                    inputStream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                Logging.Info("FileManager Exception");
                Logging.LogError(ex, ex.Message);
            }
        }

        public void SaveScreenShot(Bitmap image, ImageFormat imageFormat, string fullPath)
        {
            try
            {
                image.Save(fullPath, imageFormat);
            }
            catch (Exception ex)
            {
                Logging.Info("FileManager Exception");
                Logging.LogError(ex, ex.Message);
            }
        }

        public void GetInfoFromFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
