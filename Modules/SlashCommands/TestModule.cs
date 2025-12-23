using NetCord.Services.ApplicationCommands;

namespace MeowBot.Modules.SlashCommands;

public class TestModule : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("pong", "Pong!")]
    public static string Pong() => "Ping!";
}