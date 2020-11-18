using DSharpPlus;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UnintelejentBot;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{
    Config config;

    using (StreamReader r = new StreamReader("Config.json"))
    {
        string json = r.ReadToEnd();
        config = JsonConvert.DeserializeObject<Config>(json);
    }

    var discord = new DiscordClient(new DiscordConfiguration
    {
        Token = config.Token
    });

    discord.MessageCreated += async (c, e) =>
    {
        if (e.Message.Content.StartsWith("ping"))
            await e.Message.RespondAsync("pong!");
    };

    await discord.ConnectAsync();
    await Task.Delay(-1);
}
