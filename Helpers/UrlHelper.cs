using System.Collections.Specialized;
using System.Web;

namespace VkToDiscordReplication.Helpers
{
    internal static class UrlHelper
    {
        internal static string SetQueryFromUrl(string url, NameValueCollection keyValues)
        {
            var queryParams = new List<string>();
            string?[] keys = keyValues.AllKeys;
            foreach (string? key in keys)
            {
                if (key == null) continue;
                string[]? values = keyValues.GetValues(key);
                if (values != null)
                    foreach (string value in values)
                    {
                        if (string.IsNullOrEmpty(value)) continue;
                        string newParam = string.Format(
                            "{0}={1}",
                            HttpUtility.UrlEncode(key),
                            HttpUtility.UrlEncode(value)
                        );
                        queryParams.Add(newParam);
                    }
            }

            if (queryParams.Count == 0) return url;
            return url + "?" + string.Join("&", queryParams);
        }
    }
}
