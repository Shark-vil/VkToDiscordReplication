using System.Text.Json.Serialization;
using VkToDiscordReplication.Models.VKAPI.Attachments;

namespace VkToDiscordReplication.Models.VKAPI.LongPollUpdate
{
    internal class GetLongPollUpdateItemObject
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("from_id")]
        public long FromId { get; set; }

        [JsonPropertyName("inner_type")]
        public string InnerType { get; set; } = string.Empty;

        [JsonPropertyName("post_type")]
        public string PostType { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("hash")]
        public string Hash { get; set; } = string.Empty;

        [JsonPropertyName("attachments")]
        public List<VkAttachment> Attachments { get; set; } = new();
    }
}
