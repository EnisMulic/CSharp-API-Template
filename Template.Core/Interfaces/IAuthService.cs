using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Domain;

namespace Template.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterAsync(UserAccountRegistrationRequest request);

        Task<AuthenticationResult> AuthenticateAsync(UserAccountAuthenticationRequest request);

        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
