using AutoMapper;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Core.Interfaces;
using Template.Database;

namespace Template.Services
{
    public class CRUDService<TModel, TSearch, TDatabase, TInsert, TUpdate> :
        BaseService<TModel, TSearch, TDatabase>, ICRUDService<TModel, TSearch, TInsert, TUpdate>
        where TDatabase : class
        where TSearch : SortQuery
    {
        public CRUDService(TemplateContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
        }
        public virtual async Task<TModel> Insert(TInsert request)
        {
            var entity = _mapper.Map<TDatabase>(request);

            await _context.Set<TDatabase>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> Update(int id, TUpdate request)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);
            
            _mapper.Map(request, entity);

            _context.Set<TDatabase>().Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);

            try
            {
                _context.Set<TDatabase>().Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}