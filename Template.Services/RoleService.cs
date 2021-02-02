using AutoMapper;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Database;
using Template.Domain;

namespace Template.Services
{
    public class RoleService : BaseService<RoleResponse, RoleSearchRequest, Role>
    {
        public RoleService(TemplateContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
