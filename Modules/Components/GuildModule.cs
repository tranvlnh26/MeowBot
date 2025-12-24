using MeowBot.Abstractions.Services;
using MeowBot.Common.Embed;
using MeowBot.Models.Entities;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ComponentInteractions;

namespace MeowBot.Modules.Components;

public class GuildModule(IGuildService guildService) : ComponentInteractionModule<ComponentInteractionContext>
{
    
    [ComponentInteraction("config")] 
    public async Task HandleConfigAsync(string type)
    {
        await RespondAsync(InteractionCallback.DeferredModifyMessage);
        var interaction = (ChannelMenuInteraction)Context.Interaction;
        var channelId = interaction.Data.SelectedValues[0].Id;
        var guildId = Context.Interaction.GuildId!.Value;

        var config = await guildService.GetConfigAsync(guildId);

        var responseLabel = type switch
        {
            "log" => SetLogChannel(config, channelId),
            "notify" => SetNotifyChannel(config, channelId),
            _ => throw new InvalidOperationException($"Loại cấu hình {type} không hợp lệ.")
        };
        
        config.UpdatedBy = Context.User.Id;

       guildService.SaveConfigAsync(config);
       var updatedEmbed = EmbedTemplate.CreateConfigEmbed(config, Context.Guild!);
       await interaction.ModifyResponseAsync(m =>
       {
           m.Content = $"{responseLabel} Channel đã được cập nhật thành <#{channelId}>";
           m.Embeds = [updatedEmbed];
       });
    }

    private static string SetLogChannel(Models.Entities.GuildConfig config, ulong channelId)
    {
        config.LogChannel = channelId;
        return "Log";
    }

    private static string SetNotifyChannel(Models.Entities.GuildConfig config, ulong channelId)
    {
        config.NotifyChannel = channelId;
        return "Notify";
    }
}