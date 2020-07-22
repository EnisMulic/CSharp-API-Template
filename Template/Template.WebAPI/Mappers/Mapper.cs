using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Contracts.Requests;

namespace Template.WebAPI.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Template.Contracts.Requests.PaginationQuery, Template.Data.PaginationFilter>();
            CreateMap<IdentityUser, UserInsertRequest>().ReverseMap();
            CreateMap<IdentityUser, UserUpdateRequest>().ReverseMap();
        }
    }
}
