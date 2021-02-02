using Template.Core.Interfaces.Repository;
using Template.Domain;

namespace Template.Database.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(TemplateContext context) : base(context)
        {
        }
    }
}
