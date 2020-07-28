using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Template.Contracts.Requests;
using Template.Contracts.Responses;
using Template.Domain;
using Template.Services;
using Template.WebAPI.Services.Interfaces;

namespace Template.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CRUDController<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest>
    {
        public UserController(ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest> service, IMapper mapper) 
            : base(service, mapper)
        {
        }
    }
}