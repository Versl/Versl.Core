using System.Threading.Tasks;
using Versl.Models;

namespace Versl.Services.Data
{
    public interface ILayoutConfigurationDataService : IDataService<LayoutConfiguration>
    {
        Task<LayoutConfiguration> GetLayoutConfigurationAsync();
    }
}
