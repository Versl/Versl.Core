using System;
using Newtonsoft.Json;

namespace Versl.Models
{
    public class Icon
    {
        [JsonProperty("glyph")]
        public string Glyph { get; set; }

        [JsonProperty("family")]
        public string Family { get; set; }
    }
}
