using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnintelejentBot.Events
{
    public static class MessageReactionAddedEvent
    {
        private static readonly Dictionary<ulong, ulong> EmojiRoles = new Dictionary<ulong, ulong>
        {
            { 778439262207082506, 778246343012384778 }, // Tank
            { 778440111905046538, 778246700749684786 }, // Healer
            { 778440123569930242, 778246938197229578 }, // DPS
            { 778440267501010975, 778239467587895296 }, // Warrior
            { 778440505440469002, 778239532477972481 }, // Paladin
            { 778440582497828874, 778239779585523752 }, // Hunter
            { 778440762811088947, 778239889308647445 }, // Rogue
            { 778440828909518849, 778239985315872768 }, // Priest
            { 778440884592705557, 778240116451049481 }, // Shaman
            { 778440941090504734, 778240264115453953 }, // Mage
            { 778440984022876190, 778239013126930444 }, // Warlock
            { 778441057917599805, 778240367203844106 }, // Monk
            { 778441118357389342, 778240503602610216 }, // Druid
            { 778441197161152522, 778240589480591360 }, // Demon Hunter
            { 778441287690747915, 778240666735083540 }, // Death Knight
        };

        public static async Task Handle(DiscordClient s, MessageReactionAddEventArgs e)
        {
            // Make sure the member is not a bot
            if (e.User.IsBot) { return; }

            // Make sure the reaction is on the correct message
            if (e.Message.Id != 778454021417336862) { return; }

            // Make sure the reaction is valid and get the associated role id
            if (!EmojiRoles.TryGetValue(e.Emoji.Id, out ulong roleId)) { return; }

            // Cast the User object to the DiscordMember type as they are in a guild
            var member = (DiscordMember)e.User;

            // Make sure the member doesn't already have the role
            if (member.Roles.Any(x => x.Id == roleId)) { return; }

            // Get the role from the server
            DiscordRole roleToGrant = e.Guild.GetRole(roleId);

            // Grant the role to the member
            await member.GrantRoleAsync(roleToGrant);
        }
    }
}
