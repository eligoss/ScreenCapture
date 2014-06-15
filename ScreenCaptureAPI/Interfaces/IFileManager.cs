using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace ScreenCaptureAPI.Interfaces
{
    public interface IFileManager
    {
        /// <summary>
        /// Method that save something to file.
        /// </summary>
        /// <param name="input"></param>
        void SaveToFile(Stream inputStream, string destPath);        

        void SaveScreenShot(Bitmap image, ImageFormat imageFormat, string fullPath);

        /// <summary>
        /// Method that save something to file.
        /// </summary>
        /// <param name="input"></param>
        void GetInfoFromFile(string filePath);
    }
}
