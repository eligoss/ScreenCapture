using System;
using System.Drawing;
using ScreenCaptureAPI.Models;
namespace ScreenCaptureAPI
{
    public interface IScreenManager : IDisposable
    {
        /// <summary>
        /// Configure Screen Capture Job
        /// </summary>
        /// <param name="screenCaptureConfigModel">All input params that's are needed for configuration job.</param>
        void ConfigureScreenCaptureJob(ScreenCaptureConfigModel screenCaptureConfigModel);

        /// <summary>
        /// Method for start capture Screen Video.
        /// </summary>
        /// <param name="inputParams">Input config params.</param>        
        void StartCaptureScreenVideo(ScreenCaptureConfigModel inputParams);

        /// <summary>
        /// Method for stop capture Screen Video.
        /// </summary>
        /// <param name="inputParams">Input config params.</param>        
        void StopCaptureScreenVideo();

        /// <summary>
        /// Method thats take a screenshot.
        /// </summary>
        /// <param name="inputParams">Input config params.</param>
        /// <returns>Bitmap Image.</returns>
        ScreenshotConfigModel TakeScreenshot(ScreenshotConfigModel inputParams);

        void Dispose();
    }
}
