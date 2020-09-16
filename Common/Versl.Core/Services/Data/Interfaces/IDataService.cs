using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Versl.Services.Data
{
    public interface IDataService<M>
    {
        Task<M> GetAsync(string id);
        Task<List<M>> GetListAsync();
        Task<M> InsertAsync(M item);
        Task UpdateAsync(M item);
        Task DeleteAsync(string id);
    }
}
