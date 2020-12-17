﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.WebAPI.Interfaces;

namespace Template.WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController<RoleResponse, RoleSearchRequest>
    {
        public RoleController(IBaseService<RoleResponse, RoleSearchRequest> service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}