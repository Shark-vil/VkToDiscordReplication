using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VkToDiscordReplication.Models.VKAPI.Attachments
{
    internal class VkAttachmentPoll
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;
    }
}
