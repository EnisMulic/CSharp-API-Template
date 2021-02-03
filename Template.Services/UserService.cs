using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Core.Interfaces;
using Template.Database;
using Template.Domain;
using Template.WebAPI.Helpers;

namespace Template.Services
{
    public class UserService : CRUDService<UserResponse, UserSearchRequest, User, UserInsertRequest, UserUpdateRequest>
    {
        private readonly UserManager<User> _userManager;

        public UserService(TemplateContext context, UserManager<User> userManager, IMapper mapper, IUriService uriService) 
            : base(context, mapper, uriService)
        {
            _userManager = userManager;
        }

        public async override Task<PagedResponse<UserResponse>> Get(UserSearchRequest search, PaginationQuery pagination)
        {
            var query = _context.Set<User>().AsQueryable()
                .Include(i => i.Roles).AsQueryable();
                

            query = ApplyFilter(query, search);
            query = ApplySorting(query, search);

            int count = await query.CountAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            query = query.Skip(skip).Take(pagination.PageSize);

            
            var list = await query.ToListAsync();
            var response = _mapper.Map<List<UserResponse>>(list);

            var pagedResponse = PaginationHelper.CreatePaginatedResponse(_uriService, pagination, response, count);
            return pagedResponse;
        }

        public override async Task<UserResponse> GetById(int id)
        {
            var entity = await _context.Set<User>()
                .Include(i => i.Roles)
                .SingleOrDefaultAsync(i => i.Id == id);
            return _mapper.Map<UserResponse>(entity);
        }

        public async override Task<UserResponse> Insert(UserInsertRequest request)
        {
            var user = _mapper.Map<User>(request);
            await _userManager.CreateAsync(user, request.Password);

            //await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(user);
        }

        protected override IQueryable<User> ApplyFilter(IQueryable<User> query, UserSearchRequest filter)
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

        protected override Expression<System.Func<User, object>> GetSortExpression(UserSearchRequest search)
        {
            switch (search.OrderBy)
            {
                case nameof(User.FirstName):
                    return i => i.FirstName;
                case nameof(User.LastName):
                    return i => i.LastName;
                case nameof(User.Email):
                    return i => i.Email;
                case nameof(User.UserName):
                    return i => i.UserName;
                default:
                    return i => i.Id;
            }
        }
    }
}
