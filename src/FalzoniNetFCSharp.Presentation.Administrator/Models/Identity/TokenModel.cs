using Newtonsoft.Json;
using System;

namespace FalzoniNetFCSharp.Presentation.Administrator.Models.Identity
{
    public class TokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(".expires")]
        public DateTime? Expire { get; set; }

        [JsonProperty(".issued")]
        public DateTime? Issue { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("roleId")]
        public string RoleId { get; set; }
    }
}