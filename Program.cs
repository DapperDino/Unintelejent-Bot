using DSharpPlus;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UnintelejentBot;
using UnintelejentBot.Events;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{
    Config config;

    // Read the config file data
    using (StreamReader r = new StreamReader("Config.json"))
    {
        string json = r.ReadToEnd();
        config = JsonConvert.DeserializeObject<Config>(json);
    }

    // Create the bot instance
    var discord = new DiscordClient(new DiscordConfiguration
    {
        Token = config.Token
    });

    // Subscribe to the necessary events
    discord.GuildMemberAdded += GuildMemberAddedEvent.Handle;
    discord.MessageReactionAdded += MessageReactionAddedEvent.Handle;
    discord.MessageReactionRemoved += MessageReactionRemovedEvent.Handle;

    // Start up the bot
    await discord.ConnectAsync();

    // Make sure the application keeps running until interrupted
    await Task.Delay(-1);
}
