using AutoMapper;
using Template.Contracts.Requests;
using Template.Domain;

namespace Template.WebAPI.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Template.Contracts.Requests.PaginationQuery, Template.Data.PaginationFilter>();
            CreateMap<User, UserInsertRequest>().ReverseMap();
            CreateMap<User, UserUpdateRequest>().ReverseMap();
        }
    }
}
