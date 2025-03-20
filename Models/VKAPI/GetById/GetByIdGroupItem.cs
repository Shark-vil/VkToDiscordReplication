using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.GetById
{
    internal class GetByIdGroupItem
    {
        [JsonPropertyName("photo_100")]
        public string? Photo100 { get; set; }

        //[JsonPropertyName("photo_200")]
        //public string? Photo200 {  get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
