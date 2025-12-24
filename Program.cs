using MeowBot.Abstractions.Repositories;
using MeowBot.Abstractions.Services;
using MeowBot.Infrastructure.Repositories;
using MeowBot.Modules;
using MeowBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Hosting.Services.ComponentInteractions;


var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;

// --- INFRASTRUCTURE ---
services.AddMemoryCache();
services.AddNpgsqlDataSource(builder.Configuration["PostgresConnection"]!);
services.AddSingleton<IGuildRepository, GuildRepository>();

// --- SERVICES ---
services.AddSingleton<IGuildService, GuildService>();


// --- DISCORD ---
services.AddDiscordGateway();
services.AddApplicationCommands();
services.AddComponentInteractions();

var host = builder.Build();

host.AddSlashCommands()
    .AddContextMenus()
    .AddComponentInteractions();

await host.RunAsync();