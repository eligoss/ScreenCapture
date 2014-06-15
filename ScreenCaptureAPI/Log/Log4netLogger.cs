using log4net;

namespace ScreenCaptureAPI.Log
{
    public class Log4netLogger : ILogger
    {
        private static readonly ILog logger;

        static Log4netLogger()
        {
            logger = LogManager.GetLogger("log4net");
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Debug(message); //Need to double check this.
        }
    }
}
