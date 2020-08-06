using Newtonsoft.Json;
using System;

namespace Common.DTOs
{
    public class TeamDTO 
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
