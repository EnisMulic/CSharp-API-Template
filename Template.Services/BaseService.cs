using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.WebAPI.Helpers;
using System.Linq.Expressions;
using System;
using Template.Core.Interfaces.Services;
using Template.Core.Interfaces.Repository;

namespace Template.Services
{
    [Authorize]
    public class BaseService<TModel, TSearch, TDatabase> : IBaseService<TModel, TSearch> where TDatabase: class
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<TDatabase> _repository;
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TDatabase>();
        }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TDatabase>();
            _mapper = mapper;
            _uriService = uriService;
        }

        public virtual async Task<PagedResponse<TModel>> Get(TSearch search, PaginationQuery pagination)
        {
            var query = (await _repository.GetAllAsync()).AsQueryable();

            query = ApplySort(query, search);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            int count = await query.CountAsync();

            var list = await query.ToListAsync();
            var response = _mapper.Map<List<TModel>>(list);

            var pagedResponse = PaginationHelper.CreatePaginatedResponse(_uriService, pagination, response, count);
            return pagedResponse;
        }



        protected IQueryable<TDatabase> ApplySort(IQueryable<TDatabase> query, TSearch search)
        {
            var sort = GetSortExpression(search);
            if(sort != null)
            {
                return query.OrderBy(sort);
            }

            return query;
        }

        protected virtual Expression<Func<TDatabase, object>> GetSortExpression(TSearch sortRequest)
        {
            return null;
        }
        

        
    }
}
