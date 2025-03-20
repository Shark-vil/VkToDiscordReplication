using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.Config
{
    internal class AppConfigData
    {
        [JsonPropertyName("groups")]
        public AppConfigDataItem[] Groups { get; set; } = [ new AppConfigDataItem() ];
    }
}
