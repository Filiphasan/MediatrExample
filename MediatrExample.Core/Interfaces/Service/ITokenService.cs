using MediatrExample.Shared.DataModels.Auth;

namespace MediatrExample.Core.Interfaces.Service
{
    public interface ITokenService : IService
    {
        Task<string> CreateTokenAsync(TokenUserModel model);
    }
}
