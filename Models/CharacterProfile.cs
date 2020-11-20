using Newtonsoft.Json;

namespace UnintelejentBot.Models
{
    [JsonObject]
    public class CharacterProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("gender")]
        public Gender Gender { get; set; }
        [JsonProperty("race")]
        public Race Race { get; set; }
        [JsonProperty("character_class")]
        public CharacterClass Class { get; set; }
        [JsonProperty("active_spec")]
        public ActiveSpec Spec { get; set; }
        [JsonProperty("media")]
        public Media Media { get; set; }
    }

    [JsonObject]
    public class Gender
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject]
    public class Race
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject]
    public class CharacterClass
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject]
    public class ActiveSpec
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject]
    public class Media
    {
        [JsonProperty("name")]
        public string Href { get; set; }
    }
}
