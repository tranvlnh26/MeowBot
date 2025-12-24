using MeowBot.Models.Entities;

namespace MeowBot.Abstractions.Repositories;

public interface IGuildRepository
{
    Task<GuildConfig?> GetConfigAsync(ulong guildId);
    Task SaveConfigAsync(GuildConfig config);
}