using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.LongPollServer
{
    internal class GetLongPollServerResponse
    {
        [JsonPropertyName("response")]
        public GetLongPollServerItem Response { get; set; } = new();
    }
}
