using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Versl.Models
{
    public class MenuConfiguration
    {
        public MenuConfiguration()
        {
            Links = new List<MenuLink>();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("links")]
        public List<MenuLink> Links { get; set; }
    }
}
