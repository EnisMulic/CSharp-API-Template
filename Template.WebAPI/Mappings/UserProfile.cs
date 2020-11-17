using AutoMapper;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Domain;

namespace Template.WebAPI.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserInsertRequest>().ReverseMap();
            CreateMap<User, UserUpdateRequest>().ReverseMap();
            CreateMap<User, UserResponse>();
        }
    }
}
