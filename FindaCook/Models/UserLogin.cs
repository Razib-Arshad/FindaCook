using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FindaCook.Maui.Models
{
    public class UserLogin
    {
        [JsonPropertyName("id")]
        public string? UserId { get; set; }

        [JsonPropertyName("userName")]
        public string? UserName { get; set; }

        [JsonPropertyName("userEmail")]
        public string? UserEmail { get; set; }

    }

}
