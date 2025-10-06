using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UniAPI.UniAPI;

namespace PortocolService
{
    public class RestService
    {
        private HttpClient httpClient = new HttpClient();

        public async Task<UniRequest<TRequest>> Send<TResult, TRequest>(UniRequest<TRequest> request) where TResult : class 
        {
            try
            {
                var uri = BuildUri(request.Endpoint, request.Paramet);
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
                        if (file.FileContent != null)
                        {
                            content.Add(new ByteArrayContent(file.FileContent), file.FileName);
                        }
                    }

                    httpRequest.Content = content;
                }
                else if (request.Body != null)
                {
                    var json = JsonSerializer.Serialize(request.Body);
                    httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                using var cts = new CancellationTokenSource(request.Timeout ?? TimeSpan.FromMinutes(3));
                var response = await httpClient.SendAsync(httpRequest, cts.Token);
                var responseContent = await response.Content.ReadAsStringAsync(cts.Token);

                if (!response.IsSuccessStatusCode)
                    return ResultFunctionHelper.Error<TResult>(null,$"Request failed: {responseContent}");

                var data = JsonSerializer.Deserialize<TResult>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return ResultFunctionHelper.Success(data);
            }
            catch (Exception ex)
            {
                return ResultFunctionHelper.Error<TResult>(null,$"Exception: {ex.Message}");
            }
        }

        public async Task<ResultFunction<byte[]>> DownloadData<TRequest>(RestRequest<TRequest> request) where TRequest : class
        {
            try
            {
                var uri = BuildUri(request.Endpoint, request.Paramet);
                using var cts = new CancellationTokenSource(request.Timeout ?? TimeSpan.FromMinutes(3));
                var data = await httpClient.GetByteArrayAsync(uri, cts.Token);
                return ResultFunctionHelper.Success(data);
            }
            catch (Exception ex)
            {
                return ResultFunctionHelper.Error<byte[]>(null,$"Exception: {ex.Message}");
            }
        }


        private static string BuildUri(string baseUrl, IReadOnlyDictionary<string, string> queryParams)
        {
            if (queryParams == null || !queryParams.Any())
                return baseUrl;

            var query = string.Join("&", queryParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value.ToString()!)}"));
            return $"{baseUrl}?{query}";
        }

    }

}
