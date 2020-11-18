using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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

            rolesSheet.Cells["A1"].Value = "Tank";
            rolesSheet.Cells["B1"].Value = "Healer";
            rolesSheet.Cells["C1"].Value = "DPS";

            using (var range = rolesSheet.Cells[1, 1, 1, 3])
            {
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            int tanksCount = 1;
            int healersCount = 1;
            int dpsCount = 1;

            var members = await ctx.Guild.GetAllMembersAsync();

            foreach (var member in members)
            {
                if (member.Roles.Any(x => x.Name.Equals("Tank", StringComparison.OrdinalIgnoreCase)))
                {
                    tanksCount++;
                    rolesSheet.Cells[$"A{tanksCount}"].Value = member.DisplayName;
                }

                if (member.Roles.Any(x => x.Name.Equals("Healer", StringComparison.OrdinalIgnoreCase)))
                {
                    healersCount++;
                    rolesSheet.Cells[$"B{healersCount}"].Value = member.DisplayName;
                }

                if (member.Roles.Any(x => x.Name.Equals("DPS", StringComparison.OrdinalIgnoreCase)))
                {
                    dpsCount++;
                    rolesSheet.Cells[$"C{dpsCount}"].Value = member.DisplayName;
                }
            }

            rolesSheet.Cells.AutoFitColumns();

            var classesSheet = p.Workbook.Worksheets.Add("Classes");

            classesSheet.Cells["A1"].Value = "Warrior";
            classesSheet.Cells["B1"].Value = "Paladin";
            classesSheet.Cells["C1"].Value = "Hunter";
            classesSheet.Cells["D1"].Value = "Rogue";
            classesSheet.Cells["E1"].Value = "Priest";
            classesSheet.Cells["F1"].Value = "Shaman";
            classesSheet.Cells["G1"].Value = "Mage";
            classesSheet.Cells["H1"].Value = "Warlock";
            classesSheet.Cells["I1"].Value = "Monk";
            classesSheet.Cells["J1"].Value = "Druid";
            classesSheet.Cells["K1"].Value = "Demon Hunter";
            classesSheet.Cells["L1"].Value = "Death Knight";

            using (var range = classesSheet.Cells[1, 1, 1, 12])
            {
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            classesSheet.Cells.AutoFitColumns();

            await ctx.Channel.SendFileAsync("GuildData.xlsx", new MemoryStream(await p.GetAsByteArrayAsync()));
        }
    }
}
