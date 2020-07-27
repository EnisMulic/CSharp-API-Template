using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Contracts.Responses;
using Template.Database;
using Template.WebAPI.Helpers;
using Template.WebAPI.Services.Interfaces;

namespace Template.Services
{
    [Authorize]
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch> where TDatabase: class
    {
        private readonly TemplateContext _context;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public BaseService(TemplateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseService(TemplateContext context, IMapper mapper, IUriService uriService)
        {
            _context = context;
            _mapper = mapper;
            _uriService = uriService;
        }

        public virtual async Task<PagedResponse<TModel>> Get(TSearch search, PaginationFilter pagination)
        {
            var query = _context.Set<TDatabase>()
                .AsNoTracking()
                .AsQueryable();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            var list = await query.ToListAsync();
            var pagedResponse = await GetPagedResponse(_mapper.Map<List<TModel>>(list), pagination);
            return pagedResponse;
        }

        public virtual async Task<TModel> GetById(string id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);
            return _mapper.Map<TModel>(entity);
        }

        protected async Task<PagedResponse<TModel>> GetPagedResponse(List<TModel> list, PaginationFilter pagination)
        {
            int count = await _context.Set<TDatabase>()
                .AsNoTracking()
                .CountAsync();

            return PaginationHelper.CreatePaginatedResponse(_uriService, pagination, list, count);
        }
            
        
    }
}
