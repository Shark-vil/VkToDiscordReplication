using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.Discord
{
    internal class DiscordEmbedItem
    {
        [JsonPropertyName("author")]
        public DiscordEmbedAuthor? Author { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("color")]
        public int? Color { get; set; } // 0x00ff00

        [JsonPropertyName("image")]
        public DiscordEmbedImage? Image { get; set; }

        //[JsonPropertyName("video")]
        //public DiscordEmbedVideo? Video { get; set; }

        //[JsonPropertyName("fields")]
        //public List<DiscordEmbedItemField> Fields { get; set; } = new();
    }
}
