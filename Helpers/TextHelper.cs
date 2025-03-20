using System.Text.RegularExpressions;
using System.Web;

namespace VkToDiscordReplication.Helpers
{
    internal static class TextHelper
    {
        private static MatchEvaluator _tagsConvertMatchEvaluator = new MatchEvaluator(match =>
        {
            string tag = match.Groups[1].Value;
            return $"[#{tag}](https://vk.com/feed?q=%23{HttpUtility.UrlEncode(tag)}&section=search)";
        });

        internal static string VkDomainsToDiscord(string text) => Regex.Replace(text, @"\[#(?:[^\|\]]+)\|([^\|\]]+)\|([^\|\]]+)\]", "[$1]($2)");

        internal static string VkTagsToDiscord(string text) => Regex.Replace(text, @"#([a-zA-Zа-яА-Я0-9_]+)", _tagsConvertMatchEvaluator);
    }
}
