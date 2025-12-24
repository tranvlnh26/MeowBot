using System.Text.Json.Serialization;

namespace MeowBot.Models.Entities;

public class GuildConfig
{
    [JsonIgnore] public ulong GuildId { get; set; }

    [JsonPropertyName("language")] public string? Language { get; set; } = "vi";
}