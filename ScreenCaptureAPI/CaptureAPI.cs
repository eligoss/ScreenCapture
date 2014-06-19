using System;
using System.Drawing;
using System.Drawing.Imaging;
using ScreenCaptureAPI.Interfaces;
using ScreenCaptureAPI.Models;

namespace ScreenCaptureAPI
{
    public class CaptureAPI : IDisposable
    {
        private readonly IScreenManager screenHelper;
        private readonly IConfigManager configManager;
        private readonly IFileManager fileManager;        

        public CaptureAPI()
        {
            this.screenHelper = ContainerManager.Resolve<IScreenManager>();
            this.configManager = ContainerManager.Resolve<IConfigManager>();
            this.fileManager = ContainerManager.Resolve<IFileManager>();
        }

        public void StartRecording(ScreenCaptureConfigModel screenCaptureConfigModel = null)
        {
            if (screenCaptureConfigModel == null)
                screenCaptureConfigModel = new ScreenCaptureConfigModel();

            screenHelper.Start(screenCaptureConfigModel);
        }

        public void StopRecording()
        {
            screenHelper.Stop();
        }

        public void ResumeRecording()
        {
            screenHelper.Resume();
        }

        public void PauseRecording()
        {
            screenHelper.Pause();
        }


        public ScreenshotConfigModel TakeScreenShot(ScreenshotConfigModel screenshotConfigModel, string specificFolderToSave = null)
        {
            return screenHelper.TakeScreenshot(screenshotConfigModel);            
        }     

        public void TakeScreenShot()
        {
            var screenshotConfigModel = new ScreenshotConfigModel(
                new Point(0, 0),
                new Size((int)configManager.ScreenWidth, (int)configManager.ScreenHeight),
                configManager.DefaultScreenshotFormat);

            screenshotConfigModel = screenHelper.TakeScreenshot(screenshotConfigModel);
            fileManager.SaveScreenShot(screenshotConfigModel.Image, screenshotConfigModel.ImageFormat, screenshotConfigModel.FileFullPath);
        }

        public void SaveScreenShot(ScreenshotConfigModel screenshotConfigModel)
        {
            fileManager.SaveScreenShot(screenshotConfigModel.Image, screenshotConfigModel.ImageFormat, screenshotConfigModel.FileFullPath);
        }

        public void Dispose()
        {
            screenHelper.Dispose();
        }
    }
}
