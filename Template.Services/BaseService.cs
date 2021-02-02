using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.WebAPI.Helpers;
using Template.Database;
using Template.Core.Interfaces;

namespace Template.Services
{
    [Authorize]
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch> where TDatabase: class
    {
        protected readonly TemplateContext _context;
        protected readonly IMapper _mapper;
        protected readonly IUriService _uriService;
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

        public virtual async Task<PagedResponse<TModel>> Get(TSearch search, PaginationQuery pagination)
        {
            var query = _context.Set<TDatabase>().AsQueryable();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            int count = await query.CountAsync();

            var list = await query.ToListAsync();
            var response = _mapper.Map<List<TModel>>(list);

            var pagedResponse = PaginationHelper.CreatePaginatedResponse(_uriService, pagination, response, count);
            return pagedResponse;
        }

        public virtual async Task<TModel> GetById(int id)
        {
            var entity = await _context.Set<TDatabase>().FindAsync(id);
            return _mapper.Map<TModel>(entity);
        }
    }
}
