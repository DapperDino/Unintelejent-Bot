using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using UnintelejentBot.Clients;
using UnintelejentBot.Commands;
using UnintelejentBot.Events;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{
    // Load the config data
    var config = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("config.json")
        .Build();

    // Create the bot instance
    var discord = new DiscordClient(new DiscordConfiguration
    {
        Token = config["Token"]
    });

    // Set up dependency injection
    var services = new ServiceCollection()
        .AddSingleton(config)
        .AddHttpClient()
        .AddSingleton<WoWClient>()
        .BuildServiceProvider();

    // Set up command settings
    var commands = discord.UseCommandsNext(new CommandsNextConfiguration
    {
        StringPrefixes = new[] { "!" },
        Services = services
    });

    commands.RegisterCommands<GuildModule>();

    // Subscribe to the necessary events
    discord.GuildMemberAdded += GuildMemberAddedEvent.Handle;
    discord.MessageReactionAdded += MessageReactionAddedEvent.Handle;
    discord.MessageReactionRemoved += MessageReactionRemovedEvent.Handle;

    // Start up the bot
    await discord.ConnectAsync();

    // Make sure the application keeps running until interrupted
    await Task.Delay(-1);
}
