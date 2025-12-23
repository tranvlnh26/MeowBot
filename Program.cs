using MeowBot.Modules;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services.ApplicationCommands;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddDiscordGateway()
    .AddApplicationCommands();

var host = builder.Build();

host.AddSlashCommands()
    .AddContextMenus();

await host.RunAsync();