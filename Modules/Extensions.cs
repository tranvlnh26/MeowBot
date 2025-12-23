using MeowBot.Modules.ContextMenus;
using MeowBot.Modules.SlashCommands;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Services.ApplicationCommands;

namespace MeowBot.Modules;

public static class Extensions
{
    public static IHost AddSlashCommands(this IHost host)
    {
        host.AddApplicationCommandModule<TestModule>();

        return host;
    }

    public static IHost AddContextMenus(this IHost host)
    {
        host.AddApplicationCommandModule<UserMenuModule>();

        return host;
    }
}