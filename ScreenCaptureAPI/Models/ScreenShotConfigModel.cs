using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ScreenCaptureAPI.Log;

namespace ScreenCaptureAPI.Models
{
    public class ScreenshotConfigModel
    {
        private readonly IConfigManager configManager;

        public ScreenshotConfigModel()
        {
            this.configManager = ContainerManager.Resolve<IConfigManager>();
            this.ImageFormat = configManager.DefaultScreenshotFormat;
            this.FileName = GetFileName();
            this.FileFullPath = CreateFileFullPath(configManager.PathToScreenshotDirectory);

        }

        public ScreenshotConfigModel(Point upperLeftSource, Size blockRegionSize, ImageFormat imageFormat, string specificFolderToSave = null)
            : this()
        {
            this.UpperLeftSource = upperLeftSource;
            this.BlockRegionSize = blockRegionSize;
            this.ImageFormat = imageFormat;
            this.FileFullPath = CreateFileFullPath(specificFolderToSave);
        }

        public Point UpperLeftSource { get; set; }
        public Size BlockRegionSize { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public Bitmap Image { get; set; }
        public string FileFullPath { get; set; }
        public string FileName { get; set; }

        private string CreateFileFullPath(string specificFolderToSave = null)
        {
            string pathToDirectory = configManager.PathToScreenshotDirectory;

            if (specificFolderToSave != null)
            {
                if (Directory.Exists(specificFolderToSave))
                {
                    pathToDirectory = specificFolderToSave;
                }
                else
                {
                    Logging.Info("SpecificFolderToSave isn't exists!");
                    Logging.Info("Default folderToSave folder will be used: {0}", pathToDirectory);
                }
            }

            var tmpFileName = FileName == string.Empty ? GetFileName() : FileName;
            var fileType = ImageFormat.ToString();

            return string.Format("{0}{1}.{2}", pathToDirectory, tmpFileName, fileType);
        }


        private string GetFileName()
        {
            return DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
        }
    }
}
