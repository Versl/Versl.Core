using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Versl.Firebase.Data.DTO;
using Versl.Models;
using Versl.Services.Data;

namespace Versl.Firebase.Data
{
    public abstract class FirebaseDatabaseService<M, D> : IDatabaseService<M> where M : DataModelBase where D : DTOBase
    {
        protected FirebaseClient _client;

        public FirebaseDatabaseService()
        {
            _client = DatabaseClientProvider.DefaultInstance.GetClient();
        }

        public abstract string BasePath { get; }

        //crud operations
        public virtual async Task<M> GetAsync(string id)
        {
            var item = await _client.Child($"{BasePath}\\{id}").OnceSingleAsync<M>();

            item.Id = id;

            return item;
        }

        public virtual async Task<List<M>> GetListAsync()
        {
            var list = await _client.Child(BasePath).OnceAsync<M>();
            
            var retval = new List<M>();
            foreach (var item in list)
            {
                item.Object.Id = item.Key;
                retval.Add(item.Object);
            }

            return retval;
        }

        public virtual async Task<M> InsertAsync(M item)
        {
            var obj = await _client.Child(BasePath).PostAsync(item);

            var model = obj.Object;
            model.Id = obj.Key;

            return model;
        }

        public virtual async Task UpdateAsync(M item)
        {
            if (string.IsNullOrEmpty(item.Id))
                throw new NullReferenceException("ID is null");

            await _client.Child($"{BasePath}\\{item.Id}").PutAsync(item);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _client.Child(BasePath).Child(id).DeleteAsync();
        }
    }
}
