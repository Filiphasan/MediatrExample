﻿using FluentValidation;
using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrExample.CQRS.Auth.Login
{
    public class LoginHandler : BaseHandler<LoginRequest, GenericResponse<LoginResponse>, LoginHandler>, IRequestHandler<LoginRequest, GenericResponse<LoginResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;

        public LoginHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<IValidator<LoginRequest>> validators, ILogHelper<LoginHandler> logHelper, IUserRepository userRepository, ITokenService tokenService, IHashService hashService) : base(httpContextAccessor, validators, logHelper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _hashService = hashService;
        }

        public async Task<GenericResponse<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            await CheckValidate(request);
            try
            {
                var response = new LoginResponse();

                var user = await _userRepository.Where(x => x.Mail == request.Mail).FirstOrDefaultAsync();
                
                if (user is null)
                {
                    return GenericResponse<LoginResponse>.Error(400, "Mail Not Found!");
                }

                if (!await _hashService.CheckSHA256HashAsync(user.PasswordHash, request.Password))
                {
                    return GenericResponse<LoginResponse>.Error(400, "EMail or Password is Wrong!");
                }

                response.AccessToken = await _tokenService.CreateTokenAsync(new Shared.DataModels.Auth.TokenUserModel
                {
                    Id = user.Id,
                    Name = user.FirstName,
                    LastName = user.LastName,
                });
                response.Id = user.Id;
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.Mail = user.Mail;
                response.Gsm = user.Gsm;
                
                _logHelper.LogInfo($"UserId:{user.Id} olan {user.FirstName} {user.LastName} Giriş Yaptı", response);

                return GenericResponse<LoginResponse>.Success(response);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                return GenericResponse<LoginResponse>.Error(500,ex.Message);
            }
        }
    }
}