using System;
using System.Threading.Tasks;
using Template.Core.Interfaces.Repository;
using Template.Database.Repository;
using Template.Domain;

namespace Template.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TemplateContext _context;

        public UnitOfWork(TemplateContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync(true);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            switch(type)
            {
                case nameof(User):
                    return (IRepository<T>)new UserRepository(_context);
                case nameof(Role):
                    return (IRepository<T>)new RoleRepository(_context);
                default:
                    return null;
            }
        }
    }
}
