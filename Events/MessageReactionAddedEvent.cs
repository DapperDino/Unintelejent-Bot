using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UnintelejentBot.Events
{
    public static class MessageReactionAddedEvent
    {
        private static readonly ulong RolesReactMessageId;
        private static readonly Dictionary<ulong, ulong> EmojiRoles;

        static MessageReactionAddedEvent()
        {
            // Read the ServerSettings file data
            using StreamReader r = new StreamReader("ServerSettings.json");
            string json = r.ReadToEnd();

            // Deserialize the json data into a ServerSettings object
            var serverSettings = JsonConvert.DeserializeObject<ServerSettings>(json);

            // Cache the data
            EmojiRoles = serverSettings.EmojiRoles;
            RolesReactMessageId = serverSettings.RolesReactMessageId;
        }

        public static async Task Handle(DiscordClient s, MessageReactionAddEventArgs e)
        {
            // Make sure the member is not a bot
            if (e.User.IsBot) { return; }

            // Make sure the reaction is on the correct message
            if (e.Message.Id != RolesReactMessageId) { return; }

            // Make sure the reaction is valid and get the associated role id
            if (!EmojiRoles.TryGetValue(e.Emoji.Id, out ulong roleId)) { return; }

            // Cast the DiscordUser object to the DiscordMember type as they are in a guild
            var member = (DiscordMember)e.User;

            // Get the role from the server
            DiscordRole roleToGrant = e.Guild.GetRole(roleId);

            // Grant the role to the member
            await member.GrantRoleAsync(roleToGrant);
        }
    }
}
