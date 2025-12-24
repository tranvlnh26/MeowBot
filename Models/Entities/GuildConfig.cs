using System.Text.Json.Serialization;

namespace MeowBot.Models.Entities;

public class GuildConfig
{
    [JsonIgnore] public ulong GuildId { get; set; }
    [JsonPropertyName("log_channel")] public ulong? LogChannel { get; set; }
    [JsonPropertyName("notify_channel")] public ulong? NotifyChannel { get; set; }
    [JsonIgnore] public DateTime LastUpdated { get; set; }
    [JsonPropertyName("updated_by")] public ulong UpdatedBy { get; set; } 
}