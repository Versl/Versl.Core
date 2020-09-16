using System;
using Newtonsoft.Json;

namespace Versl.Models
{
    public class Tab : DataModelBase
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
