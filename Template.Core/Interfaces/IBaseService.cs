using System.Threading.Tasks;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;

namespace Template.Core.Interfaces
{
    public interface IBaseService<T, TSearch>
    {
        Task<PagedResponse<T>> Get(TSearch search, PaginationQuery pagination);
        Task<T> GetById(int id);
    }
}
