using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.LongPollUpdate
{
    internal class GetLongPollUpdateResponse
    {
        [JsonPropertyName("ts")]
        public string Ts { get; set; } = string.Empty;

        [JsonPropertyName("failed")]
        public short? Failed { get; set; }

        [JsonPropertyName("updates")]
        public List<GetLongPollUpdateItem> Updates { get; set; } = new();
    }
}
