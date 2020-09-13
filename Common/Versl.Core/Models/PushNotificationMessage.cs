using System;
using Newtonsoft.Json;

namespace Versl.Models
{
    public class PushNotificationMessage
    {
        public PushNotificationMessage()
        {
            Topic = "general";
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }
    }
}
