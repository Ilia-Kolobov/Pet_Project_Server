
using Newtonsoft.Json;
using System;


namespace Common.DTOs
{
    public class ProjectDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }

        [JsonProperty("authorId")]
        public int AuthorId { get; set; }

        [JsonProperty("teamId")]
        public int TeamId { get; set; }



    }
}
