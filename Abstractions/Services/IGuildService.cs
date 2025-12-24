using MeowBot.Models.Entities;

namespace MeowBot.Abstractions.Services;

public interface IGuildService
{
    Task<GuildConfig> GetConfigAsync(ulong guildId);
    Task SaveConfigAsync(GuildConfig config);
}