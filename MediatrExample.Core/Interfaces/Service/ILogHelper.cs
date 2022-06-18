namespace MediatrExample.Core.Interfaces.Service
{
    public interface ILogHelper<T>
    {
        void LogError(Exception exception);
        void LogError(string message, Exception exception);
        void LogInfo(string message);
        void LogInfo<TObj>(string message, TObj obje) where TObj : class, new();
    }
}
