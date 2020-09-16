using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;

namespace Versl.Models
{    
    public class ContentItem : DataModelBase
    {
        public ContentItem()
        {
            CTA = new CTALink();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("shortTitle")]
        public string ShortTitle { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

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
        public CTALink CTA { get; set; }
    }
}
