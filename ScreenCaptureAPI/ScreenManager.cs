using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using Microsoft.Expression.Encoder.ScreenCapture;
using ScreenCaptureAPI.Models;

namespace ScreenCaptureAPI
{
    public class ScreenManager : IScreenManager
    {
        private ScreenCaptureJob screenCaptureJob;

        private readonly IConfigManager configManager;

        public ScreenManager(IConfigManager configManager, ScreenCaptureConfigModel screenCaptureConfigModel)
        {
            this.configManager = configManager;
        }

        public void ConfigureJob(ScreenCaptureConfigModel screenCaptureConfigModel)
        {
            CreateScreenCaptureJob(screenCaptureConfigModel);
        }

        public void Start(ScreenCaptureConfigModel screenCaptureConfigModel = null)
        {
            ConfigureJob(screenCaptureConfigModel);

            screenCaptureJob.Start();
        }

        public void Stop()
        {
            screenCaptureJob.Stop();
        }

        public void Resume()
        {
            screenCaptureJob.Resume();
        }

        public void Pause()
        {
            screenCaptureJob.Pause();
        }


        public ScreenshotConfigModel TakeScreenshot(ScreenshotConfigModel screenshotConfigModel)
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap((int)screenshotConfigModel.BlockRegionSize.Width,
                                           (int)screenshotConfigModel.BlockRegionSize.Height,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(screenshotConfigModel.UpperLeftSource, new Point(0, 0), screenshotConfigModel.BlockRegionSize, CopyPixelOperation.SourceCopy);
            screenshotConfigModel.Image = bmpScreenshot;

            return screenshotConfigModel;
        }

        #region private



        private void CreateScreenCaptureJob(ScreenCaptureConfigModel screenCaptureConfigModel)
        {
            screenCaptureJob = new ScreenCaptureJob();

            screenCaptureJob.CaptureRectangle = screenCaptureConfigModel.screenRectangle;
            screenCaptureJob.ShowFlashingBoundary = true;
            screenCaptureJob.ScreenCaptureVideoProfile.Quality = screenCaptureConfigModel.Quality;
            screenCaptureJob.ScreenCaptureVideoProfile.FrameRate = screenCaptureConfigModel.FrameRate;
            // screenCaptureJob.ScreenCaptureVideoProfile.Bitrate = screenCaptureConfigModel.Bitrate;
            screenCaptureJob.CaptureMouseCursor = screenCaptureConfigModel.CaptureMouseCursor;
            screenCaptureJob.AddAudioDeviceSource(screenCaptureConfigModel.AudioDevice);

            screenCaptureJob.OutputScreenCaptureFileName = screenCaptureConfigModel.OutputScreenCaptureFullPath;
        }

        #endregion


        public void Dispose()
        {
            if (screenCaptureJob != null)
                screenCaptureJob.Dispose();
        }
    }
}
