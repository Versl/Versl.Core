using System.Threading.Tasks;
using MvvmCross;
using Versl.Models;
using Versl.Utility;

namespace Versl.Services.Data
{
    public class LayoutConfigurationDataService : DataServiceBase<LayoutConfiguration>, ILayoutConfigurationDataService
    {
        private ILayoutConfigurationDatabaseService _layoutConfigurationDatabaseService;

        public LayoutConfigurationDataService(ILayoutConfigurationDatabaseService dataService) : base(dataService)
        {
            _layoutConfigurationDatabaseService = dataService;
        }
        
        public async Task<LayoutConfiguration> GetLayoutConfigurationAsync()
        {
            return await _layoutConfigurationDatabaseService.GetLayoutConfigurationAsync();
        }
    }
}
