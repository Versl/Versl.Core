using System;
using Newtonsoft.Json;

namespace Versl.Models
{
    public class DataModelBase
    {
        public DataModelBase()
        {
        }

        [JsonIgnore]
        public string Id { get; set; }
    }
}
