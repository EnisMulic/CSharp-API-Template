using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Model.Requests;
using Template.Model.Responses;

namespace Template.WebAPI.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<AuthenticationResult> RegisterAsync(UserAccountRegistrationRequest request);

        Task<AuthenticationResult> AuthenticateAsync(UserAccountAuthenticationRequest request);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
