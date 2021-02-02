using System.Threading.Tasks;
using Template.Domain;

namespace Template.Core.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        public IUserRepository Users => Repository<User>() as IUserRepository;
        public IRoleRepository Roles => Repository<Role>() as IRoleRepository;

        IRepository<T> Repository<T>() where T : class;
        Task<int> SaveAsync();
    }
}
