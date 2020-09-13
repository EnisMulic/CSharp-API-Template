using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template.Contracts.Requests;
using Template.Contracts.Responses;
using Template.Core.Interfaces;

namespace Template.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T, TSearch> : ControllerBase
    {
        private readonly IBaseService<T, TSearch> _service;
        private readonly IMapper _mapper;

        public BaseController(IBaseService<T, TSearch> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<T>>> Get([FromQuery]TSearch search, [FromQuery]PaginationQuery pagination)
        {
            var response = await _service.Get(search, pagination);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetById(string id)
        {
            var response = await _service.GetById(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
