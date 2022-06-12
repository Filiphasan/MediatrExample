namespace MediatrExample.Core.Interfaces.Service
{
    public interface ILogHelper<T>
    {
        void LogError(Exception exception);
        void LogError(string message, Exception exception);
        void LogInfo(string message);
        void LogInfo(string message, object obje);
    }
}
