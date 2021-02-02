using AutoMapper;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Core.Interfaces.Repository;
using Template.Database;
using Template.Domain;

namespace Template.Services
{
    public class RoleService : BaseService<RoleResponse, RoleSearchRequest, Role>
    {
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
