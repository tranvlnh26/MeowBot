using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MeowBot.Modules.ContextMenus;

public class UserMenuModule : ApplicationCommandModule<ApplicationCommandContext>
{
    [UserCommand("Avatar")]
    public InteractionCallbackProperties<InteractionMessageProperties> GetAvatar()
    {
        var interaction = (UserCommandInteraction)Context.Interaction;
        var targetUser = interaction.Data.TargetUser;
        var avatarUrl = targetUser.GetAvatarUrl(ImageFormat.Png)?.ToString(1024);

        var embed = new EmbedProperties()
        {
            Title = avatarUrl,
            Image = avatarUrl,
            Color = new Color(0x2F3136)
        };
        var message = new InteractionMessageProperties()
        {
            Embeds = [embed]
        };


        return InteractionCallback.Message(message);
    }
}