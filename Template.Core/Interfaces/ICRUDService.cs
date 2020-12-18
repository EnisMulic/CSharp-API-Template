using System.Threading.Tasks;

namespace Template.WebAPI.Interfaces
{
    public interface ICRUDService<T, TSearch, TInsert, TUpdate> : IBaseService<T, TSearch>
    {
        Task<T> Insert(TInsert request);
        Task<T> Update(int id, TUpdate request);
        Task<bool> Delete(int id);
    }
}
