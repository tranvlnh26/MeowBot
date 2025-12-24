using System.Collections.Concurrent;
using MeowBot.Abstractions.Repositories;
using MeowBot.Abstractions.Services;
using MeowBot.Models.Entities;

namespace MeowBot.Services;

public class GuildService(IGuildRepository repository) : IGuildService
{
    private readonly ConcurrentDictionary<ulong, GuildConfig> _configs = [];

    public async Task<GuildConfig> GetConfigAsync(ulong guildId)
    {
        if (_configs.TryGetValue(guildId, out var config))
            return config;

        config = await repository.GetConfigAsync(guildId);
        config ??= new GuildConfig { GuildId = guildId };
        _configs[guildId] = config;

        return config;
    }

    public async Task SaveConfigAsync(GuildConfig config)
    {
        await repository.SaveConfigAsync(config);

        _configs[config.GuildId] = config;
    }
}