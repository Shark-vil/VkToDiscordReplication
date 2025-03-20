using System.Text.Json.Serialization;
using VkToDiscordReplication.Models.VKAPI.Attachments.Photo;

namespace VkToDiscordReplication.Models.VKAPI.Attachments
{
    internal class VkAttachment
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("photo")]
        public VkAttachmentPhoto? Photo { get; set; }

        [JsonPropertyName("audio")]
        public VkAttachmentAudio? Audio { get; set; }

        [JsonPropertyName("doc")]
        public VkAttachmentDocument? Document { get; set; }

        [JsonPropertyName("poll")]
        public VkAttachmentPoll? Poll { get; set; }
    }
}
