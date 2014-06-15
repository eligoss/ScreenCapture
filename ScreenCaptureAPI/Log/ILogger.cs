namespace ScreenCaptureAPI.Log
{
    public interface ILogger
    {
        void Error(string message);
        void Info(string message);
        void Warning(string message);
    }
}
