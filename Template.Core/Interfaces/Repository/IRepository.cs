using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Template.Core.Interfaces.Repository
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetPagedAsync(int page, int amount);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindPaged(Expression<Func<T, bool>> expression, int page, int amount);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T newEntity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
