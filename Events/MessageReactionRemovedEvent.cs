using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace UnintelejentBot.Events
{
    public static class MessageReactionRemovedEvent
    {
        public static async Task Handle(DiscordClient s, MessageReactionRemoveEventArgs e)
        {
            // Make sure the member is not a bot
            if (e.User.IsBot) { return; }
        }
    }
}
