using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using VkToDiscordReplication.Helpers;
using VkToDiscordReplication.Models;
using VkToDiscordReplication.Models.Discord;
using VkToDiscordReplication.Models.VKAPI.Attachments;
using VkToDiscordReplication.Models.VKAPI.Attachments.Photo;
using VkToDiscordReplication.Models.VKAPI.LongPollUpdate;
using VkToDiscordReplication.Module;

namespace VkToDiscordReplication.Service
{
    internal static class DiscordService
    {
        private static ILogger _logger = LoggerService.GetLogger(nameof(DiscordService));

        internal static DiscordEmbed MakeEmbeded(BotProfile bot, GetLongPollUpdateItem update)
        {
            if (update.Object == null)
                throw new NullReferenceException(nameof(update.Object));

            string postUrl = $"https://vk.com/wall{update.Object.FromId}_{update.Object.Id}";
            string text = update.Object.Text;
            text = TextHelper.VkDomainsToDiscord(text);
            text = TextHelper.VkTagsToDiscord(text);

            var embedBuilder = new DiscordEmbedBuilder(postUrl);
            embedBuilder.SetContent(bot.Config.AlertText);
            embedBuilder.AddText(text);
            if (update.Object.Attachments.Count != 0 && update.Object.Attachments.Any(x => Array.Exists(["photo", "audio", "doc", "poll"], val => val == x.Type)))
                embedBuilder.AddText("\n\n**Вложения**");
            if (bot.EmbedColor != null)
                embedBuilder.SetColor((int)bot.EmbedColor);
            embedBuilder.SetAuthor(bot.GroupName, bot.GroupAvatarUrl, postUrl);

            foreach (VkAttachment attachment in update.Object.Attachments)
            {
                switch (attachment.Type)
                {
                    case "photo":
                        if (attachment.Photo != null && attachment.Photo.OrigPhoto != null && attachment.Photo.Sizes.Count != 0)
                        {
                            //var photo = attachment.Photo.Sizes
                            //    .Where(x => x.Type == "x")
                            //    .FirstOrDefault();

                            //if (photo == null)
                            //{
                            //    double avWidth = attachment.Photo.Sizes.Average(x => x.Width);
                            //    double avHeight = attachment.Photo.Sizes.Average(x => x.Height);
                            //    photo = attachment.Photo.Sizes
                            //        .OrderBy(s => Math.Abs(s.Width - avWidth) + Math.Abs(s.Height - avHeight))
                            //        .First();
                            //}

                            //if (photo == null)
                            //    photo = attachment.Photo.OrigPhoto;

                            VkAttachmentPhotoItem? photo = attachment.Photo.OrigPhoto;
                            if (photo != null)
                                embedBuilder.AddImage(photo.Url);
                        };
                        break;

                    case "audio":
                        if (attachment.Audio != null)
                            embedBuilder.AddText($"\n- 🎵 Музыка: [{attachment.Audio.Artist} - {attachment.Audio.Title}](https://vk.com/audio{attachment.Audio.ReleaseAudioId})");
                        break;

                    case "doc":
                        if (attachment.Document != null)
                            embedBuilder.AddText($"\n- 📄 Документ: [{attachment.Document.Title}]({attachment.Document.Url})");
                        break;

                    case "poll":
                        if (attachment.Poll != null)
                            embedBuilder.AddText($"\n- 📊 Опрос: [{attachment.Poll.Question}](https://vk.com/poll{update.Object.FromId}_{attachment.Poll.Id})");
                        break;
                }
            }

            return embedBuilder.Build();
        }

        internal static async Task<bool> SendWebhookAsync(BotProfile bot, DiscordEmbed embed)
        {
            try
            {
                await HttpService.PostAsync(
                    bot.Config.DiscordWebhook,
                    new StringContent(JsonSerializer.Serialize(embed), Encoding.UTF8, "application/json"),
                    TimeSpan.FromSeconds(10)
                );

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0}] Error sending to Discord: {1}", bot.Config.GroupId, ex);
            }

            return false;
        }
    }
}
