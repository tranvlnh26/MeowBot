using MeowBot.Abstractions.Repositories;
using MeowBot.Abstractions.Services;
using MeowBot.Infrastructure.Repositories;
using MeowBot.Modules;
using MeowBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services.ApplicationCommands;


var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;
services.AddDiscordGateway()
        .AddApplicationCommands()
        .AddNpgsqlDataSource(builder.Configuration["PostgresConnection"]!)
        .AddSingleton<IGuildRepository, GuildRepository>()
        .AddSingleton<IGuildService, GuildService>();


var host = builder.Build();

host.AddSlashCommands()
    .AddContextMenus();

await host.RunAsync();