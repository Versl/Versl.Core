using System.Collections.Generic;
using System.Threading.Tasks;
using Versl.Models;

namespace Versl.Services.Data
{
    public abstract class DataServiceBase<M> : IDataService<M> where M : DataModelBase
    {
        protected IDatabaseService<M> DataService;

        public DataServiceBase(IDatabaseService<M> dataService)
        {
            DataService = dataService;
        }

        public virtual Task DeleteAsync(string id)
        {
            return DataService.DeleteAsync(id);
        }

        public virtual async Task<M> GetAsync(string id)
        {
            return await DataService.GetAsync(id);
        }

        public virtual Task<List<M>> GetListAsync()
        {
            return DataService.GetListAsync();
        }

        public virtual Task<M> InsertAsync(M item)
        {
            return DataService.InsertAsync(item);
        }

        public virtual Task UpdateAsync(M item)
        {
            return DataService.UpdateAsync(item);
        }
    }
}
