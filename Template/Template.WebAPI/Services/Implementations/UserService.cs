using AutoMapper;
using Lyra.WebAPI.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Data;
using Template.Services;

namespace Template.WebAPI.Services.Implementations
{
    public class UserService : CRUDService<IdentityUser, IdentityUser, IdentityUser, object, object>
    {
        public UserService(TemplateContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
