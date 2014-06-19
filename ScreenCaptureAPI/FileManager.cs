using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ScreenCaptureAPI.Interfaces;
using ScreenCaptureAPI.Log;
using ScreenCaptureAPI.Models;
using System.Xml.Serialization;
using System.Xml;

namespace ScreenCaptureAPI
{
    public class FileManager : IFileManager
    {
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

        public void SaveSettingInConfig(SettingsModel settingsModel)
        {
            var serializer = new XmlSerializer(settingsModel.GetType());

            using (var writer = XmlWriter.Create("settings.xml"))
            {
                serializer.Serialize(writer, settingsModel);
            }
        }

        public SettingsModel LoadSettingFromConfig()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(SettingsModel));
                if (File.Exists("settings.xml"))
                {

                    using (var reader = XmlReader.Create("settings.xml"))
                    {
                        return (SettingsModel)serializer.Deserialize(reader);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
