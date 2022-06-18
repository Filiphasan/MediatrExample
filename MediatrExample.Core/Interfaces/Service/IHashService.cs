namespace MediatrExample.Core.Interfaces.Service
{
    public interface IHashService : IServiceDisposable
    {
        /// <summary>
        /// MD5 Hash for string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Hash String</returns>
        Task<string> SetMD5HashAsync(string value);
        /// <summary>
        /// SHA256 Hash for string value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Hash String</returns>
        Task<string> SetSHA256HashAsync(string value);
        /// <summary>
        /// Compare hashValue and value. True if hashValue and value is equal.
        /// </summary>
        /// <param name="hashValue"></param>
        /// <param name="value"></param>
        /// <returns>True if value and hashValue are equal, False other</returns>
        Task<bool> CheckMD5HashAsync(string hashValue, string value);
        /// <summary>
        /// Compare hashValue and value. True if hashValue and value is equal.
        /// </summary>
        /// <param name="hashValue"></param>
        /// <param name="value"></param>
        /// <returns>True if value and hashValue are equal, False other</returns>
        Task<bool> CheckSHA256HashAsync(string hashValue, string value);
    }
}
