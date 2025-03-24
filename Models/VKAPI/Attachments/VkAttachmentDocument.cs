using System.Text.Json.Serialization;

namespace VkToDiscordReplication.Models.VKAPI.Attachments
{
    internal class VkAttachmentDocument
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("ext")]
        public string Ext { get; set; } = string.Empty;

        //[JsonPropertyName("preview")]
        //public VkAttachmentPreview? Preview { get; set; }
    }

    //internal class VkAttachmentPreview
    //{
    //    [JsonPropertyName("video")]
    //    public VkAttachmentPreviewVideo? Video { get; set; }
    //}

    //internal class VkAttachmentPreviewVideo
    //{
    //    [JsonPropertyName("src")]
    //    public string Src { get; set; } = string.Empty;
    //}
}
