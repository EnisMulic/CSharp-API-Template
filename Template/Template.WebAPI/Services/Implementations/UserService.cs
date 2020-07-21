using AutoMapper;
using Lyra.WebAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Template.Contracts.Requests;
using Template.Data;
using Template.Services;

namespace Template.WebAPI.Services.Implementations
{
    public class UserService : CRUDService<IdentityUser, UserSearchRequest, IdentityUser, UserInsertRequest, object>
    {
        private readonly TemplateContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(TemplateContext context, UserManager<IdentityUser> userManager, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async override Task<List<IdentityUser>> Get(UserSearchRequest search, PaginationFilter pagination)
        {
            var query = _context.Set<IdentityUser>().AsQueryable();

            query = ApplyFilterToQuery(query, search);

            if (pagination != null)
            {
                var skip = (pagination.PageNumber - 1) * pagination.PageSize;
                query = query.Skip(skip).Take(pagination.PageSize);
            }


            var list = await query.ToListAsync();
            return _mapper.Map<List<IdentityUser>>(list);
        }

        public async override Task<IdentityUser> Insert(UserInsertRequest request)
        {
            var user = new IdentityUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            var newUser = await _userManager.CreateAsync(user, request.Password);

            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return user;
        }

        private IQueryable<IdentityUser> ApplyFilterToQuery(IQueryable<IdentityUser> query, UserSearchRequest filter)
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
