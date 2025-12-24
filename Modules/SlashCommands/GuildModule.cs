using MeowBot.Abstractions.Services;
using MeowBot.Common.Embed;
using MeowBot.Common.System;
using MeowBot.Models.Entities;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MeowBot.Modules.SlashCommands;

public class GuildModule(IGuildService guildService) : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("config", "Mở bảng cấu hình Server")]
    public async Task<InteractionCallbackProperties<InteractionMessageProperties>> OpenSettingsAsync()
    {
        var config = await guildService.GetConfigAsync(Context.Guild!.Id);
        var embed = EmbedTemplate.CreateConfigEmbed(config, Context.Guild!);
        var notifyMenu = new ChannelMenuProperties("config:notify")
        {
            Placeholder = "Choose Notify Channel...",
            ChannelTypes = [ChannelType.TextGuildChannel, ChannelType.AnnouncementGuildChannel]
        };
        var logMenu = new ChannelMenuProperties("config:log")
        {
            Placeholder = "Choose Log Channel...",
            ChannelTypes = [ChannelType.TextGuildChannel, ChannelType.AnnouncementGuildChannel]
        };
        return InteractionCallback.Message(new InteractionMessageProperties
        {
            Embeds = [embed],
            Components =
            [
                notifyMenu,
                logMenu
            ],
            Flags = MessageFlags.Ephemeral
        });
    }
}