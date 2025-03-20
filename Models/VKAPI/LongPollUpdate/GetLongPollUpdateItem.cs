using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VkToDiscordReplication.Models.VKAPI.LongPollUpdate
{
    internal class GetLongPollUpdateItem
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("object")]
        public GetLongPollUpdateItemObject? Object { get; set; }
    }
}
