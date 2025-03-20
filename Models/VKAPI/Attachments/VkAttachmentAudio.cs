using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.Attachments
{
    internal class VkAttachmentAudio
    {
        [JsonPropertyName("artist")]
        public string Artist { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("release_audio_id")]
        public string ReleaseAudioId { get; set; } = string.Empty;
    }
}
