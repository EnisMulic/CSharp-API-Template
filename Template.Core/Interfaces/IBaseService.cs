using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;

namespace Template.WebAPI.Interfaces
{
    public interface IBaseService<T, TSearch>
    {
        Task<PagedResponse<T>> Get(TSearch search, PaginationQuery pagination);
        Task<T> GetById(string id);
    }
}
