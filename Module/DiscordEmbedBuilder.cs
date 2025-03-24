using VkToDiscordReplication.Models.Discord;

namespace VkToDiscordReplication.Module
{
    internal class DiscordEmbedBuilder
    {
        private DiscordEmbed _embed { get; set; }
        private DiscordEmbedItem _firstEmbedItem
        {
            get
            {
                if (_embed.Embeds == null)
                    throw new NullReferenceException(nameof(_embed.Embeds));
                return _embed.Embeds[0];
            }
        }
        private string _embedUrl { get; set; }

        public DiscordEmbedBuilder(string embedUrl)
        {
            _embed = new DiscordEmbed
            {
               Embeds = new List<DiscordEmbedItem>
               {
                   new DiscordEmbedItem
                   {
                       Url = embedUrl,
                   }
               }
            };
            _embedUrl = embedUrl;
        }

        public void SetContent(string content) => _embed.Content = content;

        public void SetColor(int color) => _firstEmbedItem.Color = color;

        public void SetAuthor(string name, string iconUrl, string url)
        {
            _firstEmbedItem.Author = new DiscordEmbedAuthor
            {
                Name = name,
                IconUrl = iconUrl,
                Url = url
            };
        }

        public void AddText(string text)
        {
            if (_firstEmbedItem.Description == null)
                _firstEmbedItem.Description = text;
            else
                _firstEmbedItem.Description += text;
        }

        public void AddImage(string imageUrl)
        {
            if (_embed.Embeds == null)
                throw new NullReferenceException(nameof(_embed.Embeds));

            _embed.Embeds.Add(new DiscordEmbedItem
            {
                Url = _embedUrl,
                Image = new DiscordEmbedImage
                {
                    Url = imageUrl
                }
            });
        }

        //public void AddVideo(string videoUrl)
        //{
        //    if (_embed.Embeds == null)
        //        throw new NullReferenceException(nameof(_embed.Embeds));

        //    _embed.Embeds.Add(new DiscordEmbedItem
        //    {
        //        Url = _embedUrl,
        //        Video = new DiscordEmbedVideo
        //        {
        //            Url = videoUrl
        //        }
        //    });
        //}

        public DiscordEmbed Build() => _embed;
    }
}
