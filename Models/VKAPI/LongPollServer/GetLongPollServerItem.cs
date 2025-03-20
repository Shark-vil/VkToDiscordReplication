using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.LongPollServer
{
    internal class GetLongPollServerItem
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("server")]
        public string Server { get; set; } = string.Empty;

        [JsonPropertyName("ts")]
        public string Ts { get; set; } = string.Empty;
    }
}
