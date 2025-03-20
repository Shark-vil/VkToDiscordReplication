namespace VkToDiscordReplication.Service
{
    internal static class HttpService
    {
        private static HttpClient _httpClient
        {
            get
            {
                if (_httpClientValue == null)
                {
                    _httpClientValue = new HttpClient();
                    _httpClientValue.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
                    _httpClientValue.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    _httpClientValue.Timeout = TimeSpan.FromSeconds(100);
                }
                return _httpClientValue;
            }
        }
        private static HttpClient? _httpClientValue;

        internal static async Task<string> PostAsync(string url, HttpContent? content = null, TimeSpan? requestTimeout = null)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = content
            };

            using (request)
            using (CancellationTokenSource cts = new CancellationTokenSource(requestTimeout ?? TimeSpan.FromSeconds(10)))
            using (HttpResponseMessage response = await _httpClient.SendAsync(request, cts.Token))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        internal static async Task<string> GetAsync(string url, TimeSpan? requestTimeout = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (CancellationTokenSource cts = new CancellationTokenSource(requestTimeout ?? TimeSpan.FromSeconds(10)))
            using (HttpResponseMessage response = await _httpClient.SendAsync(request, cts.Token))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        internal static async Task<bool> CheckSuccessAsync(string url, TimeSpan? requestTimeout = null)
        {
            try
            {
                using var cts = new CancellationTokenSource(requestTimeout ?? TimeSpan.FromSeconds(10));
                using var response = await _httpClient.GetAsync(url, cts.Token);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
