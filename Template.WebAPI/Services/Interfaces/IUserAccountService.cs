using System.Threading.Tasks;
using Template.Contracts.Requests;
using Template.Domain;

namespace Template.WebAPI.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<AuthenticationResult> RegisterAsync(UserAccountRegistrationRequest request);

        Task<AuthenticationResult> AuthenticateAsync(UserAccountAuthenticationRequest request);

        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
