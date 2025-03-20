using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.GetById
{
    internal class GetByIdResponse
    {
        [JsonPropertyName("response")]
        public GetByIdItem? Response { get; set; }
    }
}
