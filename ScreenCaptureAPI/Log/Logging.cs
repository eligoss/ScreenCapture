using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace ScreenCaptureAPI.Log
{
    public static class Logging
    {
        private static ILogger[] Loggers { get; set; }

        static Logging()
        {
            Loggers = ContainerManager.Container.ResolveAll<ILogger>().ToArray();
        }

        public static void LogError(Exception ex, string message = null)
        {
            if (ex != null)
            {
                string error = "";
                if (!string.IsNullOrWhiteSpace(message))
                {
                    error = message + ": ";
                }

                error += ex.ToString();

                foreach (ILogger logger in Loggers)
                {
                    logger.Error(error);
                }
            }
        }

        public static void Info(string message, params object[] parameters)
        {
            if (message != null)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Info(string.Format(message, parameters));
                }
            }
        }

        public static void Warning(string message, params object[] parameters)
        {
            if (message != null)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Warning(string.Format(message, parameters));
                }
            }
        }
    }
}
