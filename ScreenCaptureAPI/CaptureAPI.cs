﻿using System;
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

        public CaptureAPI(IScreenManager screenWorker, IConfigManager configManager, IFileManager fileManager)
        {
            this.screenHelper = screenWorker;
            this.configManager = configManager;
            this.fileManager = fileManager;
        }

        public void StartRecording(ScreenCaptureConfigModel screenCaptureConfigModel = null)
        {
            if (screenCaptureConfigModel == null)
                screenCaptureConfigModel = new ScreenCaptureConfigModel();

            screenHelper.StartCaptureScreenVideo(screenCaptureConfigModel);
        }

        public void StopRecording()
        {
            screenHelper.StopCaptureScreenVideo();
        }


        public void TakeScreenShot(ScreenshotConfigModel screenshotConfigModel, string specificFolderToSave = null)
        {
            Bitmap image = screenHelper.TakeScreenshot(screenshotConfigModel);
            fileManager.SaveScreenShot(image, screenshotConfigModel.ImageFormat, screenshotConfigModel.FileFullPath);

        }

        public void TakeScreenShot()
        {
            var screenshotConfigModel = new ScreenshotConfigModel(
                new Point(0, 0),
                new Point(0, 0),
                new Size((int)configManager.ScreenWidth, (int)configManager.ScreenHeight),
                configManager.DefaultScreenshotFormat);

            Bitmap image = screenHelper.TakeScreenshot(screenshotConfigModel);
            fileManager.SaveScreenShot(image, screenshotConfigModel.ImageFormat, screenshotConfigModel.FileFullPath);
        }


        public void Dispose()
        {

        }
    }
}