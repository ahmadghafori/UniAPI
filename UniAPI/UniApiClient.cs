using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAPI.UniAPI;

namespace UniAPI
{
    public class UniApiClient
    {
        private string CombineUrl(string baseUrl, string? path)
        {
            if(path is not null)
                return baseUrl.TrimEnd('/') + "/" + path.TrimStart('/');

            return baseUrl;
        }

        /// <summary>
        /// Unified API client to handle REST, GraphQL, and gRPC requests.
        /// </summary>
        public async Task<UniResponse<TResponse>> SendAsync<TRequest,TResponse>(UniRequest<TRequest> request)
        {
            // Determine final BaseUrl
            var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new InvalidOperationException("BaseUrl must be specified in request or config.");

            var fullPath = CombineUrl(baseUrl,request.PathOrQuery);

            // TODO: Implement sending logic for REST / GraphQL / gRPC
            // Apply Config (Timeout, Retry, Logging)
            // Map response to UniResponse<TResponse>
            // Invoke OnError if failure

            throw new NotImplementedException();
        }

    }
}
