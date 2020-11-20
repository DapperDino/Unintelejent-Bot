using Newtonsoft.Json;

namespace UnintelejentBot.Models
{
    [JsonObject]
    public class CharacterProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("faction")]
        public Faction Faction { get; set; }
        [JsonProperty("race")]
        public Race Race { get; set; }
        [JsonProperty("character_class")]
        public CharacterClass Class { get; set; }
        [JsonProperty("active_spec")]
        public ActiveSpec ActiveSpec { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("achievement_points")]
        public int AchievementPoints { get; set; }
        [JsonProperty("average_item_level")]
        public int ItemLevel { get; set; }
        [JsonProperty("active_title")]
        public ActiveTitle ActiveTitle { get; set; }
    }

    [JsonObject]
    public class Faction
    {
        [JsonProperty("name")]
        public string Name;
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
    public class ActiveTitle
    {
        [JsonProperty("display_string")]
        public string DisplayString { get; set; }
    }
}
