using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace UniAPI.PortocolService
{
    public class GraphQLService
    {
        private readonly HttpClient _httpClient;

        public GraphQLService(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<UniResponse<TResult>> Send<TRequest, TResult>(GraphQLRequest<TRequest> request)
        {

            string graphqlFieldName = ToGraphQLFieldName((typeof(TRequest).Name));

            // ساخت query و variables
            var (query, variables) = BuildQuery(request.Body, "query", graphqlFieldName);

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, request.BaseUrl);

            // Headers
            if (request.Headers != null)
            {
                foreach (var h in request.Headers)
                    httpRequest.Headers.TryAddWithoutValidation(h.Key, h.Value);
            }

            // Body: فایل یا JSON
            if (request.Files != null && request.Files.Any())
            {
                var content = new MultipartFormDataContent();

                // Step 1: operations
                var operations = new { query, variables };
                content.Add(new StringContent(JsonSerializer.Serialize(operations), Encoding.UTF8, "application/json"), "operations");

                // Step 2: map
                var map = new Dictionary<string, string[]>();
                for (int i = 0; i < request.Files.Count; i++)
                {
                    var variableName = Path.GetFileNameWithoutExtension(request.Files[i].FileName).Replace(" ", "_");
                    map[i.ToString()] = new[] { $"variables.{variableName}" };
                }
                content.Add(new StringContent(JsonSerializer.Serialize(map), Encoding.UTF8, "application/json"), "map");

                // Step 3: actual files
                for (int i = 0; i < request.Files.Count; i++)
                    content.Add(request.Files[i].GetStreamContent, i.ToString(), request.Files[i].FileName);

                httpRequest.Content = content;
            }
            else
            {
                // فقط JSON
                var body = new { query, variables };
                httpRequest.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            }

            using var cts = new CancellationTokenSource(request.Config?.Timeout ?? TimeSpan.FromSeconds(30));
            var response = await _httpClient.SendAsync(httpRequest, cts.Token);
            var responseContent = await response.Content.ReadAsStringAsync(cts.Token);

            if (!response.IsSuccessStatusCode)
            {
                return new UniResponse<TResult>
                {
                    StatusCode = (int)response.StatusCode,
                    Success = false,
                    Error = $"GraphQL request failed: {responseContent}"
                };
            }

            var graphQLResponse = JsonSerializer.Deserialize<GraphQLRawResponse<TResult>>(responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var meta = new Dictionary<string, object>();
            foreach (var item in response.Headers)
                meta.Add(item.Key, item.Value);

            return new UniResponse<TResult>
            {
                StatusCode = (int)response.StatusCode,
                Success = graphQLResponse?.Errors == null || !graphQLResponse.Errors.Any(),
                Error = graphQLResponse?.Errors != null ? string.Join("; ", graphQLResponse.Errors.Select(e => e.Message)) : "",
                Data = graphQLResponse.Data!,
                Metadata = meta
            };
        }

        // --------------------------
        // Private helper: Query builder
        // --------------------------
        private static (string query, object variables) BuildQuery<T>(T? model, string operationType = "query", string graphqlFieldName = "")
        {
            var props = model != null ? typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance) : Array.Empty<PropertyInfo>();
            var variables = new Dictionary<string, object?>();
            foreach (var prop in props)
                variables[prop.Name] = prop.GetValue(model);

            string query;

            if (props.Length > 0)
            {
                var fields = string.Join(" ", props.Select(p => p.Name));
                query = $@"{operationType} {graphqlFieldName}(" +
                        string.Join(", ", props.Select(p => $"${p.Name}: String")) + ") {{ " +
                        $"{graphqlFieldName}(" +
                        string.Join(", ", props.Select(p => $"{p.Name}: ${p.Name}")) +
                        $") {{ {fields} }} }}";
            }
            else
            {
                // مدل خالی → حداقل query استاندارد
                query = $@"{operationType} {graphqlFieldName} {{ {graphqlFieldName}(input: {{}}) {{ data {{ items {{ name }} }} message }} }}";
            }

            return (query, variables);
        }

        private static string ToGraphQLFieldName(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return "default_field";

            var sb = new StringBuilder();
            for (int i = 0; i < className.Length; i++)
            {
                char c = className[i];
                if (char.IsUpper(c))
                {
                    if (i != 0)
                        sb.Append('_');
                    sb.Append(char.ToLower(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }


    }

    // Raw response GraphQL
    public class GraphQLRawResponse<T>
    {
        public T? Data { get; set; }
        public GraphQLError[]? Errors { get; set; }
    }

    public class GraphQLError
    {
        public string Message { get; set; } = "";
        public object? Extensions { get; set; }
    }
}
