using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Contracts.V1;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Core.Interfaces;

namespace Template.WebAPI.Controllers.V1
{
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userAccountService;
        public AuthController(IAuthService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<IActionResult> Register([FromBody] UserAccountRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(i => i.Errors.Select(j => j.ErrorMessage))
                });
            }

            var authResponse = await _userAccountService.RegisterAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest
                (
                    new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    }
                );
            }

            return Ok
            (
                new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }

        [HttpPost(ApiRoutes.Auth.Authenticate)]
        public async Task<IActionResult> Authenticate([FromBody] UserAccountAuthenticationRequest request)
        {
            var authResponse = await _userAccountService.AuthenticateAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest
                (
                    new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    }
                );
            }

            return Ok
            (
                new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _userAccountService.RefreshTokenAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest
                (
                    new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    }
                );
            }

            return Ok
            (
                new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }
    }
}