using DSharpPlus.Entities;

namespace UnintelejentBot
{
    public static class Constants
    {
        public static readonly string[] RoleNames = new string[]
        {
            "Tank",
            "Healer",
            "DPS"
        };

        public static readonly Characterclass[] Classes = new Characterclass[]
        {
            new Characterclass("Warrior", new DiscordColor("#c69b6d")),
            new Characterclass("Paladin", new DiscordColor("#f48cba")),
            new Characterclass("Hunter", new DiscordColor("#aad372")),
            new Characterclass("Rogue", new DiscordColor("#fff468")),
            new Characterclass("Priest", new DiscordColor("#eeeeee")),
            new Characterclass("Shaman", new DiscordColor("#0070dd")),
            new Characterclass("Mage", new DiscordColor("#3fc7eb")),
            new Characterclass("Warlock", new DiscordColor("#8788ee")),
            new Characterclass("Monk", new DiscordColor("#00ff98")),
            new Characterclass("Druid", new DiscordColor("#ff7c0a")),
            new Characterclass("Demon Hunter", new DiscordColor("#a330c9")),
            new Characterclass("Death Knight", new DiscordColor("#c41e3a")),
        };

        public record Characterclass(string Name, DiscordColor Colour);
    }
}
