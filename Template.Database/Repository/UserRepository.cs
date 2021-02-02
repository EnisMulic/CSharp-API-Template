using Template.Core.Interfaces.Repository;
using Template.Domain;

namespace Template.Database.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TemplateContext context) : base(context)
        {
        }
    }
}
