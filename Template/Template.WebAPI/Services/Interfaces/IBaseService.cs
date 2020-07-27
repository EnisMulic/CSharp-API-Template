using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Contracts.Responses;
using Template.Database;

namespace Template.Services
{
    public interface IBaseService<T, TSearch>
    {
        Task<PagedResponse<T>> Get(TSearch search, PaginationFilter pagination);
        Task<T> GetById(string id);
    }
}
