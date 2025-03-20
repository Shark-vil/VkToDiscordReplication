using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.GetById
{
    internal class GetByIdItem
    {
        [JsonPropertyName("groups")]
        public List<GetByIdGroupItem> Groups { get; set; } = new();
    }
}
