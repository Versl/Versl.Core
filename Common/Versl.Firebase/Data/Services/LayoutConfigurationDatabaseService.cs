using System;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Versl.Firebase.Data.DTO;
using Versl.Models;
using Versl.Services.Data;

namespace Versl.Firebase.Data
{
    public class LayoutConfigurationDatabaseService : FirebaseDatabaseService<LayoutConfiguration, LayoutConfigurationDTO>, ILayoutConfigurationDatabaseService
    {
        public LayoutConfigurationDatabaseService()
        {

        }

        public override string BasePath => "configuration/layout";

        public async Task<LayoutConfiguration> GetLayoutConfigurationAsync()
        {
            var layoutConfig = await _client.Child(BasePath).OnceSingleAsync<LayoutConfiguration>();

            return layoutConfig;            
        }

        public override async Task UpdateAsync(LayoutConfiguration item)
        {
            await _client.Child($"{BasePath}").PutAsync(item);
        }

        public override Task<LayoutConfiguration> InsertAsync(LayoutConfiguration item)
        {
            throw new NotImplementedException();
        }
    }
}
