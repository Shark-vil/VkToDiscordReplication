using System.Text.Json;
using VkToDiscordReplication.Helpers;
using VkToDiscordReplication.Models;
using VkToDiscordReplication.Models.Config;
using VkToDiscordReplication.Models.VKAPI.GetById;
using VkToDiscordReplication.Models.VKAPI.LongPollServer;
using VkToDiscordReplication.Models.VKAPI.LongPollUpdate;

namespace VkToDiscordReplication.Service
{
    internal static class VkApiService
    {
        public static async Task<GetLongPollServerItem> GetLongPollServerAsync(BotProfile bot)
        {
            string apiUrl = UrlHelper.SetQueryFromUrl(
                "https://api.vk.com/method/groups.getLongPollServer",
                new System.Collections.Specialized.NameValueCollection
                {
                    { "access_token", bot.Config.AccessToken },
                    { "group_id", bot.Config.GroupId },
                    { "v", string.IsNullOrWhiteSpace(bot.Config.LongpollVersion) ? "5.150" : bot.Config.LongpollVersion },
                    { "lp_version", string.IsNullOrWhiteSpace(bot.Config.LpVersion) ? "3" : bot.Config.LpVersion },
                    { "need_pts", string.IsNullOrWhiteSpace(bot.Config.NeedPts) ? "1" : bot.Config.NeedPts }
                }
            );

            string responseJson = await HttpService.GetAsync(apiUrl);
            var responseObject = JsonSerializer.Deserialize<GetLongPollServerResponse>(responseJson);

            if (responseObject == null)
                throw new NullReferenceException(nameof(responseObject));

            if (responseObject.Response == null)
                throw new NullReferenceException(nameof(responseObject.Response));

            if (string.IsNullOrEmpty(responseObject.Response.Key))
                throw new NullReferenceException(nameof(responseObject.Response.Key));

            if (string.IsNullOrEmpty(responseObject.Response.Server))
                throw new NullReferenceException(nameof(responseObject.Response.Server));

            if (string.IsNullOrEmpty(responseObject.Response.Ts))
                throw new NullReferenceException(nameof(responseObject.Response.Ts));

            return responseObject.Response;
        }

        public static async Task<GetLongPollUpdateResponse?> GetLongPollUpdateAsync(GetLongPollServerItem longPollData)
        {
            string apiUrl = UrlHelper.SetQueryFromUrl(
                longPollData.Server,
                new System.Collections.Specialized.NameValueCollection
                {
                    { "act", "a_check" },
                    { "wait", "90" },
                    { "key", longPollData.Key },
                    { "ts", longPollData.Ts },
                    { "mode", "8" },
                    { "version", "3" },
                }
            );

            string? responseJson = null;
            
            try
            {
                responseJson = await HttpService.GetAsync(apiUrl, TimeSpan.FromSeconds(90));
            }
            catch (TaskCanceledException)
            {
                return null;
            }
            //catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested) { }

            if (string.IsNullOrEmpty(responseJson))
                throw new NullReferenceException(nameof(responseJson));

            var responseObject = JsonSerializer.Deserialize<GetLongPollUpdateResponse>(responseJson);
            if (responseObject == null)
                throw new NullReferenceException(nameof(responseObject));

            if (responseObject.Updates == null || responseObject.Updates.Count == 0)
                return null;

            return responseObject;
        }

        public static async Task<GetByIdGroupItem?> GetByIdAsync(BotProfile bot)
        {
            string apiUrl = UrlHelper.SetQueryFromUrl(
                "https://api.vk.com/method/groups.getById",
                new System.Collections.Specialized.NameValueCollection
                {
                    { "access_token", bot.Config.AccessToken },
                    { "group_id", bot.Config.GroupId },
                    { "v", string.IsNullOrWhiteSpace(bot.Config.LongpollVersion) ? "5.150" : bot.Config.LongpollVersion }
                }
            );

            string responseJson = await HttpService.GetAsync(apiUrl, TimeSpan.FromSeconds(90));
            var responseObject = JsonSerializer.Deserialize<GetByIdResponse>(responseJson);

            if (responseObject == null)
                throw new NullReferenceException(nameof(responseObject));

            if (responseObject.Response == null)
                throw new NullReferenceException(nameof(responseObject.Response));

            if (responseObject.Response.Groups.Count == 0)
                throw new NullReferenceException(nameof(responseObject.Response.Groups));

            return responseObject.Response.Groups[0];
        }
    }
}
