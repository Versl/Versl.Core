using System;
using Newtonsoft.Json;

namespace Versl.Firebase.Data.DTO
{
    public class ContentItemDTO : DTOBase
    {        
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("shortTitle")]
        public string ShortTitle { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        [JsonProperty("isProtected")]
        public bool IsProtected { get; set; }

        [JsonProperty("isPublished")]
        public bool IsPublished { get; set; }

        [JsonProperty("isSubscription")]
        public bool IsSubscription { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("cta")]
        public LinkDTO CTA { get; set; }
    }
}
