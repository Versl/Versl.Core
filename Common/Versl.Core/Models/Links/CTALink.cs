using System;
using Newtonsoft.Json;
using Versl.Enums;

namespace Versl.Models
{
    public class CTALink : LinkBase
    {
        [JsonProperty("type")]
        public CTALinkType Type { get; set; }
    }
}
