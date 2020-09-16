using System.Threading.Tasks;
using Versl.Models;

namespace Versl.Services.Data
{
    public interface ILayoutConfigurationDatabaseService : IDatabaseService<LayoutConfiguration>
    {
        Task<LayoutConfiguration> GetLayoutConfigurationAsync();
    }
}
