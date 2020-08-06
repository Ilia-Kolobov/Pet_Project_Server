using BSA_EF.DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSA_EF.DAL.Models
{
    public class ProjectTask : IEntity
    {
        [JsonProperty("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }


        [JsonProperty("finishedAt")]
        public DateTime FinishedAt { get; set; }

        [Required]
        [JsonProperty("state")]
        [Range(0,3)]
        public int State { get; set; }

        [Required]
        [JsonProperty("projectId")]
        public int ProjectId { get; set; }

        [Required]
        [JsonProperty("performerId")]
        public int PerformerId { get; set; }

    }
}
