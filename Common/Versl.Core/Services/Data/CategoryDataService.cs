using Versl.Models;

namespace Versl.Services.Data
{
    public class CategoryDataService : DataServiceBase<Category>, ICategoryDataService
    {
        public CategoryDataService(ICategoryDatabaseService dataService) : base(dataService)
        {
            
        }
    }
}
