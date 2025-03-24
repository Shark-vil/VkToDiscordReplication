namespace VkToDiscordReplication.Service
{
    internal static class HttpService
    {
        private static TimeSpan _defaultTimeOut { get { return TimeSpan.FromSeconds(60); } }

        private static HttpClient _httpClient
        {
            get
            {
                if (_httpClientValue == null)
                {
                    _httpClientValue = new HttpClient();
                    _httpClientValue.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
                    _httpClientValue.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    _httpClientValue.Timeout = _defaultTimeOut;
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
            using (CancellationTokenSource cts = new CancellationTokenSource(requestTimeout ?? _defaultTimeOut))
            using (HttpResponseMessage response = await _httpClient.SendAsync(request, cts.Token))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        internal static async Task<string> GetAsync(string url, TimeSpan? requestTimeout = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (CancellationTokenSource cts = new CancellationTokenSource(requestTimeout ?? _defaultTimeOut))
            using (HttpResponseMessage response = await _httpClient.SendAsync(request, cts.Token))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        internal static async Task<bool> DownloadFileAsync(string url, string filePath)
        {
            try
            {
                using (Stream s = await _httpClient.GetStreamAsync(url))
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                    await s.CopyToAsync(fs);

                return File.Exists(filePath);
            }
            catch
            {
                return false;
            }
        }

        internal static async Task<bool> CheckSuccessAsync(string url, TimeSpan? requestTimeout = null)
        {
            try
            {
                using var cts = new CancellationTokenSource(requestTimeout ?? _defaultTimeOut);
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
