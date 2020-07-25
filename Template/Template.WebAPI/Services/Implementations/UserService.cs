using AutoMapper;
using Lyra.WebAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Contracts.Requests;
using Template.Contracts.Responses;
using Template.Database;
using Template.Domain;

namespace Template.WebAPI.Services.Implementations
{
    public class UserService : CRUDService<UserResponse, UserSearchRequest, User, UserInsertRequest, UserUpdateRequest>
    {
        private readonly TemplateContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService(TemplateContext context, UserManager<User> userManager, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async override Task<List<UserResponse>> Get(UserSearchRequest search, PaginationFilter pagination)
        {
            var query = _context.Set<User>().AsQueryable();

            query = ApplyFilterToQuery(query, search);

            if (pagination != null)
            {
                var skip = (pagination.PageNumber - 1) * pagination.PageSize;
                query = query.Skip(skip).Take(pagination.PageSize);
            }


            var list = await query.ToListAsync();
            return _mapper.Map<List<UserResponse>>(list);
        }

        public async override Task<UserResponse> Insert(UserInsertRequest request)
        {
            var user = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            await _userManager.CreateAsync(user, request.Password);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(user);
        }

        private IQueryable<User> ApplyFilterToQuery(IQueryable<User> query, UserSearchRequest filter)
        {
            if(!string.IsNullOrEmpty(filter?.Id))
            {
                query = query.Where(i => i.Id == filter.Id);
            }

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

            return query;
        }
    }
}
