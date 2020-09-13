using System;
using Newtonsoft.Json;

namespace Versl.Models
{
    public abstract class LinkBase
    {
        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
