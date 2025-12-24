using System.Data;
using System.Text.Json;
using MeowBot.Abstractions.Repositories;
using MeowBot.Common.Json;
using MeowBot.Models.Entities;
using Npgsql;
using NpgsqlTypes;

namespace MeowBot.Infrastructure.Repositories;

public class GuildRepository(NpgsqlDataSource dataSource) : IGuildRepository
{
    public async Task<GuildConfig?> GetConfigAsync(ulong guildId)
    {
        const string sql = "SELECT config, last_updated FROM guilds WHERE guild_id = @guildId";
        await using var cmd = dataSource.CreateCommand(sql);
        cmd.Parameters.AddWithValue("guildId", (long)guildId);

        await using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess);

        if (!await reader.ReadAsync()) return null;
        var json  =  reader.GetString(0);
        var config = JsonSerializer.Deserialize(json, BotJsonContext.Default.GuildConfig);

        if (config == null) return null;
        
        config.GuildId = guildId;
        config.LastUpdated = reader.GetDateTime(1);
        return config;
    }

    public async Task SaveConfigAsync(GuildConfig config)
    {
        const string sql = """
                                   INSERT INTO guilds (guild_id, config) 
                                   VALUES (@guildId, @config) 
                                   ON CONFLICT (guild_id) 
                                   DO UPDATE SET config = EXCLUDED.config;
                           """;
        await using var cmd = dataSource.CreateCommand(sql);
        cmd.Parameters.AddWithValue("guildId", (long)config.GuildId);

        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(config, BotJsonContext.Default.GuildConfig);
        cmd.Parameters.AddWithValue("config", NpgsqlDbType.Jsonb, jsonBytes);

        await cmd.ExecuteNonQueryAsync();
    }
}