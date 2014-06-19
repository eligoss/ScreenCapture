using Microsoft.Practices.Unity;
using ScreenCaptureAPI.Interfaces;
using ScreenCaptureAPI.Log;
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
            container.RegisterType<IFileManager, FileManager>();    
            container.RegisterType<ScreenCaptureConfigModel, ScreenCaptureConfigModel>();

            //Must be only one instance.
            IConfigManager configManager = new ConfigManager();
            container.RegisterInstance<IConfigManager>(configManager);
                 
        }

        public static void Initialise()
        {
        }
    }
}
