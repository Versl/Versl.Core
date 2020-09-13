using Versl.Firebase.Data.DTO;
using Versl.Models;
using Versl.Services.Data;

namespace Versl.Firebase.Data
{
    public class CategoryDatabaseService : FirebaseDatabaseService<Category, CategoryDTO>, ICategoryDatabaseService
    {
        public CategoryDatabaseService()
        {
        }

        public override string BasePath => "entities/categories";
    }
}
