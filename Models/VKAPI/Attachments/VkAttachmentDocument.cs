using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.Attachments
{
    internal class VkAttachmentDocument
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
