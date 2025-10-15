using System.Text;
using System.Text.Json;
using UniAPI;
using UniAPI.PortocolService;

namespace PortocolService
{
    public class RestService
    {
        private HttpClient httpClient = new HttpClient();

        public async Task<UniResponse<TResult>> Send<TResult, TRequest>(RestRequest<TRequest> request)
        {
            var uri = BuildUri(request.BaseUrl, request.Parameters);
            using var httpRequest = new HttpRequestMessage(request.Method, uri);

            if (request.Headers != null)
            {
                foreach (var h in request.Headers)
                {
                    if (!httpRequest.Headers.TryAddWithoutValidation(h.Key, h.Value.ToString()))
                    {
                        if (httpRequest.Content != null)
                            httpRequest.Content.Headers.TryAddWithoutValidation(h.Key, h.Value.ToString());
                    }
                }
            }

            if (request.Method != HttpMethod.Get)
            {
                if (request.Files != null && request.Files.Any())
                {
                    var content = new MultipartFormDataContent();

                    // JSON body

                    if (request.Body != null)
                    {
                        var json = JsonSerializer.Serialize(request.Body);
                        content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "body");
                    }

                    // Files
                    foreach (var file in request.Files)
                    {
                        content.Add(file.GetStreamContent, "file", file.FileName);
                    }

                    httpRequest.Content = content;
                }
                else if (request.Body != null)
                {
                    var json = JsonSerializer.Serialize(request.Body);
                    httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            using var cts = new CancellationTokenSource(request.Config.Timeout);
            var response = await httpClient.SendAsync(httpRequest, cts.Token);
            var responseContent = await response.Content.ReadAsStringAsync(cts.Token);

            if (!response.IsSuccessStatusCode)
            {
                return new UniResponse<TResult>
                {
                    StatusCode = int.Parse(response.StatusCode.ToString()),
                    Success = false,
                    Error = $"Request failed: {responseContent}"
                };
            }

            var data = JsonSerializer.Deserialize<TResult>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var Meta = new Dictionary<string, object>();
            foreach (var item in response.Headers)
            {
                Meta.Add(item.Key, item.Value);
            }

            return new UniResponse<TResult>
            {
                StatusCode = (int)response.StatusCode,
                Success = true,
                Error = "",
                Data = data,
                Metadata = Meta
            };
        }

        public async Task<UniResponse<TResult>> Send<TResult>(RestRequest<TResult> request)
        {
            var uri = BuildUri(request.BaseUrl, request.Parameters);
            using var httpRequest = new HttpRequestMessage(request.Method, uri);

            if (request.Headers != null)
            {
                foreach (var h in request.Headers)
                {
                    if (!httpRequest.Headers.TryAddWithoutValidation(h.Key, h.Value.ToString()))
                    {
                        if (httpRequest.Content != null)
                            httpRequest.Content.Headers.TryAddWithoutValidation(h.Key, h.Value.ToString());
                    }
                }
            }

            if (request.Method != HttpMethod.Get)
            {
                if (request.Files != null && request.Files.Any())
                {
                    var content = new MultipartFormDataContent();

                    // JSON body

                    if (request.Body != null)
                    {
                        var json = JsonSerializer.Serialize(request.Body);
                        content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "body");
                    }

                    // Files
                    foreach (var file in request.Files)
                    {
                        content.Add(file.GetStreamContent, "file", file.FileName);
                    }

                    httpRequest.Content = content;
                }
                else if (request.Body != null)
                {
                    var json = JsonSerializer.Serialize(request.Body);
                    httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            using var cts = new CancellationTokenSource(request.Config.Timeout);
            var response = await httpClient.SendAsync(httpRequest, cts.Token);
            var responseContent = await response.Content.ReadAsStringAsync(cts.Token);

            if (!response.IsSuccessStatusCode)
            {
                return new UniResponse<TResult>
                {
                    StatusCode = int.Parse(response.StatusCode.ToString()),
                    Success = false,
                    Error = $"Request failed: {responseContent}"
                };
            }

            var data = JsonSerializer.Deserialize<TResult>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var Meta = new Dictionary<string, object>();
            foreach (var item in response.Headers)
            {
                Meta.Add(item.Key, item.Value);
            }

            return new UniResponse<TResult>
            {
                StatusCode = (int)response.StatusCode,
                Success = true,
                Error = "",
                Data = data,
                Metadata = Meta
            };
        }

        public async Task<UniResponse<byte[]>> DownloadData<TRequest>(RestRequest<TRequest> request) where TRequest : class
        {
            var uri = BuildUri(request.BaseUrl, request.Parameters);
            using var cts = new CancellationTokenSource(request.Config.Timeout);
            var data = await httpClient.GetByteArrayAsync(uri, cts.Token);

            return new UniResponse<byte[]>
            {
                StatusCode = 200,
                Success = true,
                Error = "",
                Data = data,
            };
        }

        private static string BuildUri(string baseUrl, IReadOnlyDictionary<string, object>? queryParams)
        {
            if (queryParams == null || !queryParams.Any())
                return baseUrl;

            var query = string.Join("&", queryParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value.ToString()!)}"));
            return $"{baseUrl}?{query}";
        }
    }

}
