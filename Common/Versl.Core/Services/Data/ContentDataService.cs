using Versl.Models;

namespace Versl.Services.Data
{
    public class ContentDataService : DataServiceBase<ContentItem> , IContentDataService
    {        
        public ContentDataService(IContentDatabaseService dataService) : base(dataService)
        {            
        }
    }
}
