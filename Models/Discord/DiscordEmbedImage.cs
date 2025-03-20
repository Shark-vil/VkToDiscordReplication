using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.Discord
{
    internal class DiscordEmbedImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}
