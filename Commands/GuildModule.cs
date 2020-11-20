using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnintelejentBot.Clients;
using UnintelejentBot.Models;

namespace UnintelejentBot.Commands
{
    public class GuildModule : BaseCommandModule
    {
        public WoWClient WoWClient { private get; set; }

        [Command("Profile")]
        public async Task Test(CommandContext ctx, string characterName)
        {
            CharacterProfile characterProfile = await WoWClient.GetCharacterProfileSummary(characterName);

            CharacterMedia characterMedia = await WoWClient.GetCharacterMediaSummary(characterName);

            var profileEmbed = new DiscordEmbedBuilder
            {
                Title = characterProfile.Name,
                Description = $"{characterProfile.Gender.Name} {characterProfile.Race.Name}, {characterProfile.Spec.Name} {characterProfile.Class.Name}",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = characterMedia.Assets?.FirstOrDefault(x => x.Key == "avatar").Value ?? characterMedia.AvatarUrl,
                },
                ImageUrl = characterMedia.Assets?.FirstOrDefault(x => x.Key == "main").Value ?? characterMedia.RenderUrl,
                Color = Constants.Classes.FirstOrDefault(x => x.Name == characterProfile.Class.Name).Colour
            };

            await ctx.Channel.SendMessageAsync(embed: profileEmbed);
        }

        [Command("GuildData")]
        [RequireRoles(RoleCheckMode.Any, "Moderator")]
        public async Task GenerateGuildDataCommand(CommandContext ctx)
        {
            using var p = new ExcelPackage();

            var rolesSheet = p.Workbook.Worksheets.Add("Roles");

            var members = await ctx.Guild.GetAllMembersAsync();

            for (int i = 0; i < Constants.RoleNames.Length; i++)
            {
                string roleName = Constants.RoleNames[i];

                rolesSheet.Cells[1, i + 1].Value = roleName;

                var membersWithRole = members.Where
                    (x => x.Roles.Any
                    (x => x.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase)))
                    .ToArray();

                for (int j = 0; j < membersWithRole.Length; j++)
                {
                    rolesSheet.Cells[j + 2, i + 1].Value = membersWithRole[j].DisplayName;
                }
            }

            using (var range = rolesSheet.Cells[1, 1, 1, Constants.RoleNames.Length])
            {
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            rolesSheet.Cells.AutoFitColumns();

            var classesSheet = p.Workbook.Worksheets.Add("Classes");

            for (int i = 0; i < Constants.Classes.Length; i++)
            {
                string className = Constants.Classes[i].Name;

                classesSheet.Cells[1, i + 1].Value = className;

                var membersWithRole = members.Where
                    (x => x.Roles.Any
                    (x => x.Name.Equals(className, StringComparison.OrdinalIgnoreCase)))
                    .ToArray();

                for (int j = 0; j < membersWithRole.Length; j++)
                {
                    classesSheet.Cells[j + 2, i + 1].Value = membersWithRole[j].DisplayName;
                }
            }

            using (var range = classesSheet.Cells[1, 1, 1, Constants.Classes.Length])
            {
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            classesSheet.Cells.AutoFitColumns();

            await ctx.Channel.SendFileAsync("GuildData.xlsx", new MemoryStream(await p.GetAsByteArrayAsync()));
        }
    }
}
