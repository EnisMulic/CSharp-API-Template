using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Data;
using Template.Services;
using Template.WebAPI.Services.Interfaces;

namespace Template.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CRUDController<IdentityUser, IdentityUser, object, object>
    {
        public UserController(ICRUDService<IdentityUser, IdentityUser, object, object> service, IUriService uriService, IMapper mapper) 
            : base(service, uriService, mapper)
        {
        }
    }
}