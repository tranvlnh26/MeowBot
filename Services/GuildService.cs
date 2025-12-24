using System.Collections.Concurrent;
using MeowBot.Abstractions.Repositories;
using MeowBot.Abstractions.Services;
using MeowBot.Common.Cache;
using MeowBot.Models.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MeowBot.Services;

public class GuildService(IGuildRepository repository, IMemoryCache cache, ILogger<GuildService> logger) : IGuildService
{
    public async Task<GuildConfig> GetConfigAsync(ulong guildId)
    {
        var cacheKey = $"guild_config_{guildId}";
        if (cache.TryGetValue(cacheKey, out GuildConfig? config) && config != null)
        {
            return config;
        }

        config = await repository.GetConfigAsync(guildId);
        config ??= new GuildConfig { GuildId = guildId };
        
        cache.SetWithDefault(cacheKey, config);
        
        return config;
    }

    public void SaveConfigAsync(GuildConfig config)
    {
        cache.SetWithDefault($"guild_config_{config.GuildId}", config);
        
        _ = Task.Run(async () => 
        {
            try 
            {
                await repository.SaveConfigAsync(config);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Không thể lưu config cho guild {GuildId}", config.GuildId);
            }
        });
    }
}