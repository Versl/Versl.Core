using System;
using Newtonsoft.Json;

namespace Versl.Firebase.Data.DTO
{
    public class LinkDTO : DTOBase
    {
        public class Link
        {
            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("type")]
            public int Type { get; set; }
        }
    }
}
