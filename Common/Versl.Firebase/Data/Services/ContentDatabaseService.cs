using System.Collections.Generic;
using System.Threading.Tasks;
using Versl.Firebase.Data.DTO;
using Versl.Models;
using Versl.Services.Data;
using System.Linq;

namespace Versl.Firebase.Data
{
    public class ContentDatabaseService : FirebaseDatabaseService<ContentItem, ContentItemDTO>, IContentDatabaseService
    {
        public ContentDatabaseService()
        {
        }

        public override string BasePath => "entities/content";

        public override async Task<List<ContentItem>> GetListAsync()
        {
            var list = await base.GetListAsync();

            return list.OrderByDescending(c => c.Date).ToList();
        }
    }
}
