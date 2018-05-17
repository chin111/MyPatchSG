using Newtonsoft.Json;

namespace MyPatchSG.SAL.DTO
{
    public class AccessTokenDto
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string username { get; set; }
        public string expires_in { get; set; }

        [JsonProperty("as:client_id")]
        public string client_id { get; set; }

        [JsonProperty(".issued")]
        public string issued { get; set; }

        [JsonProperty(".expires")]
        public string expires { get; set; }

        [JsonProperty("appinfo")]
        public string appinfo { get; set; }
    }
}
