using MeowBot.Modules;
using MeowBot.Modules.ContextMenus;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Hosting.Services.ComponentInteractions;

namespace MeowBot.Modules;

public static class Extensions
{
    public static IHost AddComponentInteractions(this IHost host)
    {
        host.AddComponentInteractionModule<Components.GuildModule>();

        return host;
    }
    public static IHost AddSlashCommands(this IHost host)
    {
        host.AddApplicationCommandModule<SlashCommands.GuildModule>();

        return host;
    }

    public static IHost AddContextMenus(this IHost host)
    {
        host.AddApplicationCommandModule<UserMenuModule>();

        return host;
    }
}