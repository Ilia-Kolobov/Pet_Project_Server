using Newtonsoft.Json;
using System;
using BSA_EF.DAL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BSA_EF.DAL.Models
{
    public class Team : IEntity
    {
        [JsonProperty("id")]
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        public string Discription { get; set; }
    }
}
