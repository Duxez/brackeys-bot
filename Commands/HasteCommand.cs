﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Linq;

namespace BrackeysBot.Commands
{
    public class HasteCommand : ModuleBase
    {
        private readonly CommandService commands;
        
        private static readonly Regex _HasteKeyRegex = new Regex(@"{""key"":""(?<key>[a-z].*)""}", RegexOptions.Compiled);
        private const string DEFAULT_URL = "https://hastebin.com";
        private const string CODEBLOCK_IDENTIFIER = "```";
        private const int MASSIVE_THRESHOLD = 300;

        public HasteCommand(CommandService commands)
        {
            this.commands = commands;
        }

        [Command("modhaste")]
        public async Task ModHasteMessage(ulong messageId)
        {
            StaffCommandHelper.EnsureStaff(Context.User as IGuildUser);

            var message = await Context.Channel.GetMessageAsync(messageId);
            string content = RemoveCodeblockFormat(message.Content);
            string url = await HasteMessage(content);

            await ReplyAsync($"Message by { message.Author.Mention } was hasted to { url }.");
            await message.DeleteAsync();
        }

        public static async Task<string> HasteMessage(string message)
        {
            using (WebClient client = new WebClient())
            {
                var response = await client.UploadStringTaskAsync(DEFAULT_URL + "/documents", message);
                var match = _HasteKeyRegex.Match(response);

                if (!match.Success)
                {
                    Console.WriteLine(response);
                    return string.Empty;
                }

                string hasteUrl = String.Concat(DEFAULT_URL, "/", match.Groups["key"]);
                return hasteUrl;
            }
        }

        public static async Task HasteIfMassiveCodeblock (IMessage message)
        {
            string content = message.Content;
            if (content.StartsWith(CODEBLOCK_IDENTIFIER) 
                && content.EndsWith(CODEBLOCK_IDENTIFIER)
                && content.Length >= MASSIVE_THRESHOLD)
            {
                string code = RemoveCodeblockFormat(content);
                string url = await HasteMessage(code);

                await message.Channel.SendMessageAsync($"Hastebin created in place of massive codeblock by { message.Author.Mention }: { url }");
                await message.DeleteAsync();
            }
        }

        /// <summary>
        /// Removes the ``` formatting from a string.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string RemoveCodeblockFormat (string message)
        {
            if (message.StartsWith(CODEBLOCK_IDENTIFIER)
                   && message.EndsWith(CODEBLOCK_IDENTIFIER))
            {
                int startingIndex = message.IndexOf('\n') + 1; // +1 because of newline

                int endingIndex = message.Length - CODEBLOCK_IDENTIFIER.Length - 1; // same here
                int length = endingIndex - startingIndex;

                return message.Substring(startingIndex, length);
            }
            else return message;
        }
    }
}
