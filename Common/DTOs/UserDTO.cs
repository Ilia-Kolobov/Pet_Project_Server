using Newtonsoft.Json;
using System;

namespace Common.DTOs
{
    public class UserDTO 
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("registeredAt")]
        public DateTime RegisteredAt { get; set; }

        [JsonProperty("teamId")]
        public int? TeamId { get; set; }
    }
}
