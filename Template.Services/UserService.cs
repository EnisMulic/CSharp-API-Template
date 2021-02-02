using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Domain;
using Template.WebAPI.Helpers;
using System.Linq.Expressions;
using Template.Core.Interfaces.Services;
using Template.Core.Interfaces.Repository;

namespace Template.Services
{
    public class UserService : CRUDService<UserResponse, UserSearchRequest, User, UserInsertRequest, UserUpdateRequest>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, IUriService uriService) 
            : base(unitOfWork, mapper, uriService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _uriService = uriService;
        }

        public async override Task<PagedResponse<UserResponse>> Get(UserSearchRequest search, PaginationQuery pagination)
        {
            var query = (await _repository.GetAllAsync()).AsQueryable()
                .Include(i => i.Roles).AsQueryable();
                

            query = ApplyFilterToQuery(query, search);
            query = ApplySort(query, search);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            int count = await query.CountAsync();

            var list = await query.ToListAsync();
            var response = _mapper.Map<List<UserResponse>>(list);

            var pagedResponse = PaginationHelper.CreatePaginatedResponse(_uriService, pagination, response, count);
            return pagedResponse;
        }

        public override async Task<UserResponse> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<UserResponse>(entity);
        }

        public async override Task<UserResponse> Insert(UserInsertRequest request)
        {
            var user = _mapper.Map<User>(request);
            await _userManager.CreateAsync(user, request.Password);

            //await _context.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserResponse>(user);
        }

        protected override Expression<System.Func<User, object>> GetSortExpression(UserSearchRequest sortRequest)
        {
            switch(sortRequest.OrderBy)
            {
                case "FirstName": 
                    return x => x.FirstName;
                case "LastName":
                    return x => x.LastName;
                case "Email":
                    return x => x.Email;
                default:
                    return x => x.Id;
            }
        }

        private IQueryable<User> ApplyFilterToQuery(IQueryable<User> query, UserSearchRequest filter)
        {

            if(!string.IsNullOrEmpty(filter?.UserName))
            {
                query = query.Where(i => i.UserName == filter.UserName);
            }

            if (!string.IsNullOrEmpty(filter?.Email))
            {
                query = query.Where(i => i.Email == filter.Email);
            }

            if (!string.IsNullOrEmpty(filter?.PhoneNumber))
            {
                query = query.Where(i => i.PhoneNumber == filter.PhoneNumber);
            }

            if(filter.Roles?.Count() > 0)
            {
                query = query.Where(i => i.Roles.Any(j => filter.Roles.Contains(j.RoleId)));
            }

            return query;
        }
    }
}
