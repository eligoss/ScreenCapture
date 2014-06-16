using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenCaptureAPI.Log;
using ScreenCaptureAPI.Interfaces;
using ScreenCaptureAPI.Models;

namespace ScreenCaptureAPI
{
    public static class Bootstrapper
    {
        static Bootstrapper()
        {
            var container = ContainerManager.Container;
            RegisterTypes(container);
        }

        public static void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<CaptureAPI, CaptureAPI>();            
            container.RegisterType<ILogger, Log4netLogger>("log4net");
            container.RegisterType<IScreenManager, ScreenManager>();
            container.RegisterType<ScreenCaptureConfigModel, ScreenCaptureConfigModel>();

            //Must be only one instance.
            IConfigManager configManager = new ConfigManager();
            container.RegisterInstance<IConfigManager>(configManager);

            container.RegisterType<IFileManager, FileManager>();

            //container.RegisterType<IDateTimeProvider, CurrentDateTimeProvider>();
        }

        public static void Initialise()
        {
        }
    }
}
