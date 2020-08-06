using Newtonsoft.Json;
using System;
using BSA_EF.DAL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BSA_EF.DAL.Models
{
    public class User : IEntity
    {
        [JsonProperty("id")]
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [EmailAddressAttribute]
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [Required]
        [JsonProperty("registeredAt")]
        public DateTime RegisteredAt { get; set; }

        [JsonProperty("teamId")]
        public int? TeamId { get; set; }
    }
}
