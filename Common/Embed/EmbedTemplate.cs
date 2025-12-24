using MeowBot.Common.System;
using MeowBot.Models.Entities;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;

namespace MeowBot.Common.Embed;

public static class EmbedTemplate
{
    public static EmbedProperties CreateConfigEmbed(GuildConfig config, Guild guild)
    {
        var timestamp = config.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss UTC");
        return new EmbedProperties()
        {
            Title = "🛠️ Server Configuration Dashboard",
            Description = "Manage your server settings by selecting channels from the menus below.",
            Color = new Color(0x3498DB),
            Fields =
            [
                new EmbedFieldProperties
                {
                    Name = "🔔 Notify Channel",
                    Value = config.NotifyChannel != null ? $"<#{config.NotifyChannel}>" : "❌ *Not Set*",
                    Inline = true
                },
                new EmbedFieldProperties
                {
                    Name = "📜 Log Channel",
                    Value = config.LogChannel != null ? $"<#{config.LogChannel}>" : "❌ *Not Set*",
                    Inline = true
                },
                new EmbedFieldProperties
                {
                    Name = "👤 Last Editor",
                    Value = config.UpdatedBy != 0 ? $"<@{config.UpdatedBy}>" : "*None*",
                    Inline = true
                },
                new EmbedFieldProperties
                {
                    Name = "⚙️ Technical Details",
                    Value = $"""
                             ```yaml
                             [ Server Information ]
                             Name: {guild.Name}
                             ID: {guild.Id}
                             Members: {guild.UserCount}
                             Boost_Tier: {guild.PremiumTier}
                             Last_Updated: {timestamp}

                             [ BOT Information ]
                             Status: Operational
                             OS: {SystemInfoHelper.GetOsPlatform()}
                             Runtime: {SystemInfoHelper.GetRuntime()}
                             Memory_Usage: {SystemInfoHelper.GetRamUsage()}
                             Uptime: {SystemInfoHelper.GetUptime()}
                             ```
                             """,
                }
            ],
            Footer = new EmbedFooterProperties()
            {
                Text = "MeowBot Management System"
            },
            Timestamp = DateTimeOffset.Now
        };
    }
}