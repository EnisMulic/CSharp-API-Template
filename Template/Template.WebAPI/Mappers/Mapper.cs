using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.WebAPI.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Template.Contracts.Requests.PaginationQuery, Template.Data.PaginationFilter>();
        }
    }
}
