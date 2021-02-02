using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Template.Core.Interfaces.Repository;
using Template.Core.Interfaces.Search;

namespace Template.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TemplateContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(TemplateContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IEnumerable<T> GetPagedAsync(int page, int amount)
        {
            if (page <= 0 || amount <= 0)
            {
                return null;
            }

            return _dbSet.Skip((page - 1) * amount).Take(amount);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public virtual IEnumerable<T> FindPaged(Expression<Func<T, bool>> expression, int page, int amount)
        {
            if(page <= 0 || amount <= 0)
            {
                return null;
            }

            return _dbSet.Where(expression).Skip((page - 1) * amount).Take(amount);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        protected virtual IQueryable<T> ApplySorting(IQueryable<T> query, ISearchRequest searchRequest)
        {
            var sort = GetSortExpression(searchRequest);
            if (sort != null)
            {
                return query.OrderBy(sort);
            }

            return query;
        }

        protected virtual Expression<Func<T, object>> GetSortExpression(ISearchRequest search)
        {
            throw new NotImplementedException();
        }
    }
}
