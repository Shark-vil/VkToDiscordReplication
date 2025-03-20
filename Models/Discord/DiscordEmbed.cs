using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.Discord
{
    internal class DiscordEmbed
    {
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("embeds")]
        public List<DiscordEmbedItem>? Embeds { get; set; }
    }

    //internal class DiscordEmbedItemField
    //{
    //    [JsonPropertyName("name")]
    //    public string Name { get; set; } = string.Empty;

    //    [JsonPropertyName("value")]
    //    public string Value { get; set; } = string.Empty;

    //    [JsonPropertyName("inline")]
    //    public bool Inline { get; set; }

    //    [JsonPropertyName("image")]
    //    public DiscordEmbedImage? Image { get; set; }
    //}
}
