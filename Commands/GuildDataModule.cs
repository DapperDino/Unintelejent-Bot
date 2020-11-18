using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UnintelejentBot.Commands
{
    public class GuildDataModule : BaseCommandModule
    {
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

            for (int i = 0; i < Constants.ClassNames.Length; i++)
            {
                string className = Constants.ClassNames[i];

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

            using (var range = classesSheet.Cells[1, 1, 1, Constants.ClassNames.Length])
            {
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            classesSheet.Cells.AutoFitColumns();

            await ctx.Channel.SendFileAsync("GuildData.xlsx", new MemoryStream(await p.GetAsByteArrayAsync()));
        }
    }
}
