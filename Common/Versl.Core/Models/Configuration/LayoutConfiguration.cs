using System.Collections.Generic;
using Newtonsoft.Json;

namespace Versl.Models
{
    public interface ILayoutConfiguration
    {
        string Title { get; }

        List<Tab> Tabs { get; }
        MenuConfiguration Menu { get; }
        ColorConfiguration Colors { get; set; }
    }

    public class LayoutConfiguration : DataModelBase, ILayoutConfiguration
    {
        public LayoutConfiguration()
        {
            Colors = new ColorConfiguration();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("tabs")]
        public List<Tab> Tabs { get; set; }

        [JsonProperty("menu")]
        public MenuConfiguration Menu { get; set; }

        [JsonProperty("colors")]
        public ColorConfiguration Colors { get; set; }
    }
}
