using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.Attachments.Photo
{
    internal class VkAttachmentPhoto
    {
        [JsonPropertyName("sizes")]
        public List<VkAttachmentPhotoItem> Sizes { get; set; } = new();

        [JsonPropertyName("orig_photo")]
        public VkAttachmentPhotoItem? OrigPhoto { get; set; }
    }
}
