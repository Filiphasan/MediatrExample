namespace MediatrExample.Core.Interfaces.Service
{
    public interface ILogHelper<T>
    {
        /// <summary>
        /// Error Log with CorrelationId and Exception
        /// </summary>
        /// <param name="exception"></param>
        void LogError(Exception exception);
        /// <summary>
        /// Error Log with CorrelationId, Exception and Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void LogError(string message, Exception exception);
        /// <summary>
        /// Info Log with CorrelationId and Message
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);
        /// <summary>
        /// Info Log with CorrelationId, Message and Object
        /// </summary>
        /// <typeparam name="TObj"></typeparam>
        /// <param name="message"></param>
        /// <param name="obje"></param>
        void LogInfo<TObj>(string message, TObj obje) where TObj : class, new();
    }
}
