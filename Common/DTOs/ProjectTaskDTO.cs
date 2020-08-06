
using Newtonsoft.Json;
using System;


namespace Common.DTOs
{
    public class ProjectTaskDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("finishedAt")]
        public DateTime FinishedAt { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("projectId")]
        public int ProjectId { get; set; }

        [JsonProperty("performerId")]
        public int PerformerId { get; set; }

    }
}
