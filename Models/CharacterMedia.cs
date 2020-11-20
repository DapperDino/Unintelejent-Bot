using Newtonsoft.Json;

namespace UnintelejentBot.Models
{
    public class CharacterMedia
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("render_url")]
        public string RenderUrl { get; set; }
        [JsonProperty("assets")]
        public Asset[] Assets;
    }

    [JsonObject]
    public class Asset
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
