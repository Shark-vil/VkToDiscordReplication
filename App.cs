using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using VkToDiscordReplication.Helpers;
using VkToDiscordReplication.Models;
using VkToDiscordReplication.Models.Config;
using VkToDiscordReplication.Models.Discord;
using VkToDiscordReplication.Models.VKAPI.Attachments;
using VkToDiscordReplication.Models.VKAPI.GetById;
using VkToDiscordReplication.Models.VKAPI.LongPollUpdate;
using VkToDiscordReplication.Module;
using VkToDiscordReplication.Service;

namespace VkToDiscordReplication
{
    internal class App
    {
        private static List<Task> _botsTasks = new List<Task>();
        private ILogger<App> _logger;

        public App()
        {
            _logger = LoggerService.GetLogger<App>();
        }

        internal async Task RunAsync()
        {
            AppConfigData configGroups;
            try
            {
                configGroups = await ConfigService.GetConfigAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error initializing config: {0}", ex);
                return;
            }

            _logger.LogInformation("Initialization of bots");

            foreach (AppConfigDataItem config in configGroups.Groups)
                _botsTasks.Add(Task.Run(() => RunBot(config)));

            await Task.WhenAll(_botsTasks);
        }

        internal async Task RunBot(AppConfigDataItem config)
        {
            var bot = new BotProfile(config);
            bot.EmbedColor = ColorHelper.ConvertHexToInt(config.EmbedColor);

            if (string.IsNullOrWhiteSpace(config.AccessToken))
            {
                _logger.LogError("AccessToken not set");
                return;
            }

            if (string.IsNullOrWhiteSpace(config.GroupId))
            {
                _logger.LogError("GroupId not set");
                return;
            }

            _logger.LogInformation("Starting a group bot - {0}", config.GroupId);

            await InitBotInfoAsync(bot);

            while (true)
            {
                if (bot.ServerUpdateRequired && !await UpdateLongPollServerAsync(bot))
                {
                    await Task.Delay(TimeSpan.FromSeconds(30));
                    continue;
                }

                if (bot.LongPollServer == null)
                    throw new NullReferenceException(nameof(bot.LongPollServer));

                GetLongPollUpdateResponse? updateData = await CheckUpdatesAsync(bot);
                if (updateData == null) continue;

                switch (updateData.Failed)
                {
                    case 1:
                        bot.LongPollServer.Ts = updateData.Ts;
                        continue;
                    case 2:
                    case 3:
                        bot.ServerUpdateRequired = true;
                        continue;
                    case 4:
                        _logger.LogError("[{0}] Version error", config.GroupId);
                        return;
                }

                bot.LongPollServer.Ts = updateData.Ts;

                foreach (GetLongPollUpdateItem update in updateData.Updates)
                {
                    if (update.Object == null
                    || Math.Abs(update.Object.FromId).ToString() != config.GroupId
                    || update.Type != "wall_post_new"
                    || (string.IsNullOrWhiteSpace(update.Object.Text) && update.Object.Attachments.Count == 0))
                        continue;

                    DiscordEmbed embed = DiscordService.MakeEmbeded(bot, update);
                    if (await DiscordService.SendWebhookAsync(bot, embed) && embed.Embeds != null && embed.Embeds.Count != 0)
                        _logger.LogInformation("[{0}] Successful replication of post - {1}", config.GroupId, embed.Embeds[0].Url);
                }
            }
        }

        private async Task InitBotInfoAsync(BotProfile bot)
        {
            AppConfigDataItem config = bot.Config;

            if (string.IsNullOrWhiteSpace(config.CustomName) || string.IsNullOrWhiteSpace(config.CustomAvatar))
            {
                try
                {
                    GetByIdGroupItem? groupInfo = await VkApiService.GetByIdAsync(bot);
                    if (groupInfo != null && groupInfo.Photo100 != null && groupInfo.Name != null)
                    {
                        if (string.IsNullOrWhiteSpace(config.CustomName))
                            bot.GroupName = groupInfo.Name;

                        if (string.IsNullOrWhiteSpace(config.CustomAvatar))
                            bot.GroupAvatarUrl = groupInfo.Photo100;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("[{0}] Error retrieving group data: {1}", config.GroupId, ex);
                }
            }

            if (string.IsNullOrWhiteSpace(bot.GroupName))
                bot.GroupName = string.IsNullOrWhiteSpace(config.CustomName) ? config.CustomNameDefault : config.CustomName;

            if (string.IsNullOrWhiteSpace(bot.GroupAvatarUrl))
                bot.GroupAvatarUrl = string.IsNullOrWhiteSpace(config.CustomAvatar) ? config.CustomAvatarDefault : config.CustomAvatar;
        }

        private async Task<bool> UpdateLongPollServerAsync(BotProfile bot)
        {
            try
            {
                bot.LongPollServer = await VkApiService.GetLongPollServerAsync(bot);
                bot.ServerUpdateRequired = false;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0}] Error getting LongPoll server: {1}", bot.Config.GroupId, ex);
                return false;
            }
        }

        private async Task<GetLongPollUpdateResponse?> CheckUpdatesAsync(BotProfile bot)
        {
            if (bot.LongPollServer == null)
                throw new NullReferenceException(nameof(bot.LongPollServer));

            try
            {
                return await VkApiService.GetLongPollUpdateAsync(bot.LongPollServer);
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0}] Error receiving updates: {1}", bot.Config.GroupId, ex);
            }

            return null;
        }
    }
}
