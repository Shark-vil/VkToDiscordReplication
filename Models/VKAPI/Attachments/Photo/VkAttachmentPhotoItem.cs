using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.Attachments.Photo
{
    internal class VkAttachmentPhotoItem
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
