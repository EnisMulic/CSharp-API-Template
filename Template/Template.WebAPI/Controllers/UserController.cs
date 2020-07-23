using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Contracts.Requests;
using Template.Data;
using Template.Services;
using Template.WebAPI.Services.Interfaces;

namespace Template.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CRUDController<User, UserSearchRequest, UserInsertRequest, UserUpdateRequest>
    {
        public UserController(ICRUDService<User, UserSearchRequest, UserInsertRequest, UserUpdateRequest> service, IUriService uriService, IMapper mapper) 
            : base(service, uriService, mapper)
        {
        }
    }
}