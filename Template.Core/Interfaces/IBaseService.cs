using System.Threading.Tasks;
using Template.Contracts.Requests;
using Template.Contracts.Responses;

namespace Template.Core.Interfaces
{
    public interface IBaseService<T, TSearch>
    {
        Task<PagedResponse<T>> Get(TSearch search, PaginationQuery pagination);
        Task<T> GetById(string id);
    }
}
