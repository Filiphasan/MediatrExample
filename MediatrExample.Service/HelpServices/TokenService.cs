using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Shared.DataModels.Auth;
using MediatrExample.Shared.OptionModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediatrExample.Service.HelpServices
{
    public class TokenService : BaseService<TokenService>, ITokenService
    {
        private readonly MyTokenOptions _options;

        public TokenService(IOptions<MyTokenOptions> options, ILogger<TokenService> logger) : base(logger)
        {
            _options = options.Value;
        }


        public Task<string> CreateTokenAsync(TokenUserModel model)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claimList = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, model.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, $"{model.Name} {model.LastName}"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Ticks.ToString())
                };

                var jwtToken = new JwtSecurityToken(claims: claimList, expires: DateTime.Now.AddMinutes(_options.TokenExpireTimeMinute), signingCredentials: credentials);
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return Task.FromResult(token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ValueTask DisposeAsync()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return ValueTask.CompletedTask;
        }
    }
}
