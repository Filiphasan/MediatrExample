using MediatrExample.Shared.DataModels.Auth;

namespace MediatrExample.Core.Interfaces.Service
{
    public interface ITokenService : IServiceDisposable
    {
        /// <summary>
        /// Create JWT
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> CreateTokenAsync(TokenUserModel model);
    }
}
