using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.Discord
{
    internal class DiscordEmbedAuthor
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
