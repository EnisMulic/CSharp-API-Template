using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Contracts;
using Template.Model.Requests;
using Template.Model.Responses;
using Template.WebAPI.Services.Interfaces;

namespace Template.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPost(ApiRoutes.UserAccount.Register)]
        public async Task<AuthenticationResult> Register([FromBody] UserAccountRegistrationRequest request)
        {
            return await _userAccountService.RegisterAsync(request);
        }

        [HttpPost(ApiRoutes.UserAccount.Authenticate)]
        public async Task<AuthenticationResult> Authenticate([FromBody] UserAccountAuthenticationRequest request)
        {
            return await _userAccountService.AuthenticateAsync(request);
        }
    }
}