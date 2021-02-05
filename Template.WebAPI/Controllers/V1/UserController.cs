using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Core.Interfaces;
using Template.WebAPI.Attributes;
using Template.WebAPI.Extensions;

namespace Template.WebAPI.Controllers.V1
{

    [ApiController]
    public class UserController : CRUDController<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest>
    {
        private readonly ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest> _service;
        public UserController(ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest> service)
            : base(service)
        {
            _service = service;
        }

        [HttpGet("@me")]
        [Cached(6000)]
        public async Task<ActionResult<UserResponse>> Me()
        {
            var id = HttpContext.GetUserId();

            if (id == null)
            {
                return BadRequest();
            }

            var response = await _service.GetById((int)id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}