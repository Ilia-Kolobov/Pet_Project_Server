using BSA_EF.DAL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSA_EF.DAL.Models
{
    public class Project : IEntity
    {
        [JsonProperty("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        [Required]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("deadline")]
        public DateTime Deadline { get; set; }
        [Required]
        [JsonProperty("authorId")]
        public int AuthorId { get; set; }

        [Required]
        [JsonProperty("teamId")]
        public int TeamId { get; set; }
       
    }
}
