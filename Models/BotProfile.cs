using VkToDiscordReplication.Models.Config;
using VkToDiscordReplication.Models.VKAPI.LongPollServer;

namespace VkToDiscordReplication.Models
{
    internal class BotProfile
    {
        internal string GroupName { get; set; } = string.Empty;

        internal string GroupAvatarUrl { get; set; } = string.Empty;

        internal GetLongPollServerItem? LongPollServer { get; set; }

        internal bool ServerUpdateRequired { get; set; } = true;

        internal int? EmbedColor { get; set; }

        internal AppConfigDataItem Config { get; set; }

        public BotProfile(AppConfigDataItem config)
        {
            Config = config;
        }
    }
}
