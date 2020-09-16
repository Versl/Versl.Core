using System;
using Newtonsoft.Json;

namespace Versl.Models
{
    public class Category : DataModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
