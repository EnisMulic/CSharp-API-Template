using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.WebAPI.Interfaces;
using Template.Database;
using Template.Domain;
using Template.WebAPI.Helpers;

namespace Template.Services
{
    public class UserService : CRUDService<UserResponse, UserSearchRequest, User, UserInsertRequest, UserUpdateRequest>
    {
        private readonly TemplateContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserService(TemplateContext context, UserManager<User> userManager, IMapper mapper, IUriService uriService) 
            : base(context, mapper, uriService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _uriService = uriService;
        }

        public async override Task<PagedResponse<UserResponse>> Get(UserSearchRequest search, PaginationQuery pagination)
        {
            var query = _context.Set<User>()
                .Include(i => i.Roles)
                .AsQueryable();

            query = ApplyFilterToQuery(query, search);


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
