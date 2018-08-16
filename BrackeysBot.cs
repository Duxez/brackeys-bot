using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace BrackeysBot 
{
    public sealed class BrackeysBot 
    {
        public static IConfiguration Configuration { get; set; }

        public static KarmaTable Karma { get; set; }
        public static SettingsTable Settings { get; set; }
        public static RuleTable Rules { get; set; }
        public static HelpTable Help { get; set; }

        private IServiceProvider _services;
        private DiscordSocketClient _client;
        private CommandService _commandService;

        public BrackeysBot ()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public async Task Start () 
        {
            _client = new DiscordSocketClient();

            _commandService = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_commandService)
                .BuildServiceProvider();

            await InstallCommands();
            RegisterMassiveCodeblockHandle();

            await _client.LoginAsync(TokenType.Bot, Configuration["token"]);
            await _client.SetGameAsync($"{ Configuration["prefix"] }help");
            await _client.StartAsync();

            Karma = new KarmaTable();
            Settings = new SettingsTable();
            Rules = new RuleTable();
            Help = new HelpTable();
        }

        private async Task InstallCommands ()
        {
            _client.MessageReceived += HandleCommand;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly());
        }
        private async Task HandleCommand(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return;

            int argPos = 0;
            if (!msg.HasStringPrefix(Configuration["prefix"], ref argPos)) return;

            CommandContext context = new CommandContext(_client, msg);

            IResult result = await _commandService.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
            {
                EmbedBuilder builder = new EmbedBuilder()
                    .WithTitle("Error")
                    .WithDescription(result.ErrorReason)
                    .WithColor(Color.Red);

                await context.Channel.SendMessageAsync(string.Empty, false, builder);
            }
        }

        private void RegisterMassiveCodeblockHandle ()
        {
            _client.MessageReceived += HandleMassiveCodeblock;
        }
        private async Task HandleMassiveCodeblock (SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return;

            await Commands.HasteCommand.HasteIfMassiveCodeblock(s);
        }
    }
}