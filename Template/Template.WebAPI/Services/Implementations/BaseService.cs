using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Data;

namespace Template.Services
{
    [Authorize]
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch> where TDatabase: class
    {
        private readonly TemplateContext _context;
        private readonly IMapper _mapper;

        public BaseService(TemplateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<List<TModel>> Get(TSearch search, PaginationFilter pagination)
        {
            var query = _context.Set<TDatabase>().AsQueryable();

            if(pagination != null)
            {
                var skip = (pagination.PageNumber - 1) * pagination.PageSize;
                query = query.Skip(skip).Take(pagination.PageSize);
            }


            var list = await query.ToListAsync();
            return _mapper.Map<List<TModel>>(list);
        }

        public virtual async Task<TModel> GetById(int id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);
            return _mapper.Map<TModel>(entity);
        }
        
    }
}
