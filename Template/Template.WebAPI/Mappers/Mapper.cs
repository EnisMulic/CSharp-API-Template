using AutoMapper;
using Template.Contracts.Requests;
using Template.Contracts.Responses;
using Template.Domain;

namespace Template.WebAPI.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserInsertRequest>().ReverseMap();
            CreateMap<User, UserUpdateRequest>().ReverseMap();
            CreateMap<User, UserResponse>();
        }
    }
}
