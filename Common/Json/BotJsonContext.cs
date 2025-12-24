using System.Text.Json.Serialization;
using MeowBot.Models.Entities;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;

namespace MeowBot.Common.Json;

[JsonSourceGenerationOptions(
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified
)]
[JsonSerializable(typeof(Interaction))]
[JsonSerializable(typeof(InteractionMessageProperties))]
[JsonSerializable(typeof(MessageReactionAddEventArgs))]
[JsonSerializable(typeof(GuildConfig))]
internal partial class BotJsonContext : JsonSerializerContext
{
}