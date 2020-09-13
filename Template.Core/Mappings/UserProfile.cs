using AutoMapper;
using Template.Contracts.Requests;
using Template.Contracts.Responses;
using Template.Domain;

namespace Template.Core.Mappings
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
