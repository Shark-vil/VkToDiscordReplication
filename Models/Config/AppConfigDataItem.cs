using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.Config
{
    internal class AppConfigDataItem
    {
        /// <summary>
        /// Community access token with "community management, wall" rights
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// LongPoll version, recommended - 5.150 and above
        /// </summary>
        [JsonPropertyName("longpoll_version")]
        public string LongpollVersion { get; set; } = "5.150";

        /// <summary>
        /// Numeric community ID WITHOUT MINUS SIGN
        /// </summary>
        [JsonPropertyName("group_id")]
        public string GroupId { get; set; } = string.Empty;

        /// <summary>
        /// Version for connecting to Long Poll. Actual version: 3
        /// </summary>
        [JsonPropertyName("lp_version")]
        public string LpVersion { get; set; } = "3";

        /// <summary>
        /// 1 - return the pts field, which is required for the messages.getLongPollHistory method to work
        /// </summary>
        [JsonPropertyName("need_pts")]
        public string NeedPts { get; set; } = "0";

        /// <summary>
        /// Channel webhook for sending replicated posts
        /// </summary>
        [JsonPropertyName("discord_webhook")]
        public string DiscordWebhook { get; set; } = string.Empty;

        /// <summary>
        /// Color of the backlight line of the embedding unit
        /// </summary>
        [JsonPropertyName("embed_color")]
        public string EmbedColor { get; set; } = string.Empty;

        /// <summary>
        /// Custom community name if unable to get from API or you want to use another
        /// </summary>
        [JsonPropertyName("custom_name")]
        public string CustomName { get; set; } = string.Empty;

        /// <summary>
        /// Link to a custom community avatar if you couldn't get it from the API or you want to use another one
        /// </summary>
        [JsonPropertyName("custom_avatar")]
        public string CustomAvatar { get; set; } = string.Empty;

        [JsonIgnore]
        public string CustomNameDefault { get; private set; } = "VK to Discord";

        [JsonIgnore]
        public string CustomAvatarDefault { get; private set; } = "https://i.ibb.co/35rLkX7S/bot.jpg";
    }
}
