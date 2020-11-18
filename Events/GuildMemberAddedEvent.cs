using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace UnintelejentBot.Events
{
    public static class GuildMemberAddedEvent
    {
        public static async Task Handle(DiscordClient s, GuildMemberAddEventArgs e)
        {
            // Make sure the member is not a bot
            if (e.Member.IsBot) { return; }

            // Get the welcome channel
            DiscordChannel welcomeChannel = e.Guild.GetDefaultChannel();

            // Create the welcome embed with some initial data
            var welcomeEmbed = new DiscordEmbedBuilder
            {
                Title = $"Welcome {e.Member.DisplayName}",
                ImageUrl = e.Member.AvatarUrl,
                Color = DiscordColor.Green,
            };

            // Add extra fields
            welcomeEmbed
                .AddField("Roles", "Head over to the #roles channel to claim the roles for the classes and roles that you play.");

            // Send the embed to the welcome channel
            await welcomeChannel.SendMessageAsync(embed: welcomeEmbed);
        }
    }
}
