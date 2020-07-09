using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Model.Requests;
using Template.Model.Responses;
using Template.WebAPI.Services.Interfaces;

namespace Template.WebAPI.Services.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        public Task<AuthenticationResult> AuthenticateAsync(UserAccountAuthenticationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RegisterAsync(UserAccountRegistrationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
