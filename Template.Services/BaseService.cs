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
using System.Linq.Expressions;
using System;

namespace Template.Services
{
    [Authorize]
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch>
        where TDatabase : class
        where TSearch : SortQuery
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

            query = ApplyFilter(query, search);
            query = ApplySorting(query, search);

            int count = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);


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

        protected virtual IQueryable<TDatabase> ApplyFilter(IQueryable<TDatabase> query, TSearch search)
        {
            return query;
        }

        protected virtual IQueryable<TDatabase> ApplySorting(IQueryable<TDatabase> query, TSearch search)
        {
            var expression = GetSortExpression(search);

            if (expression != null)
            {
                if (search.SortOrder == SortOrder.ASC)
                {
                    query = query.OrderBy(expression);
                }
                else
                {
                    query = query.OrderByDescending(expression);
                }
            }

            return query;
        }

        protected virtual Expression<Func<TDatabase, object>> GetSortExpression(TSearch search)
        {
            return null;
        }
    }
}
