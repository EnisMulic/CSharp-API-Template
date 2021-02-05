using Microsoft.AspNetCore.Mvc;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Core.Interfaces;

namespace Template.WebAPI.Controllers.V1
{

    [ApiController]
    public class UserController : CRUDController<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest>
    {
        public UserController(ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest> service)
            : base(service)
        {
        }
    }
}