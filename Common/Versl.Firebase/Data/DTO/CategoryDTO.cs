using System;
using Newtonsoft.Json;

namespace Versl.Firebase.Data.DTO
{
    public class CategoryDTO : DTOBase
    {        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
