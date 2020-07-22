using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template.Contracts.Requests;
using Template.Contracts.Responses;
using Template.Data;
using Template.Services;
using Template.WebAPI.Helpers;
using Template.WebAPI.Services.Interfaces;

namespace Template.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T, TSearch> : ControllerBase
    {
        private readonly IBaseService<T, TSearch> _service;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public BaseController(IBaseService<T, TSearch> service, IUriService uriService, IMapper mapper)
        {
            _service = service;
            _uriService = uriService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]TSearch search, [FromQuery]PaginationQuery paginationQuery)
        {
            //return await _service.Get(search);
            Microsoft.AspNetCore.Http.PathString path = HttpContext.Request.Path;
            
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var response = await _service.Get(search, pagination);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<T>(response));
            }

            var paginationResponse = PaginationHelper.CreatePaginatedResponse(_uriService, pagination, response);
            return Ok(paginationResponse);
        }

        [HttpGet("{id}")]
        public async Task<T> GetById(string id)
        {
            return await _service.GetById(id);
        }
    }
}
