using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Data;

namespace Template.Services
{
    public interface IBaseService<T, TSearch>
    {
        Task<List<T>> Get(TSearch search, PaginationFilter pagination);
        Task<T> GetById(int id);
    }
}
