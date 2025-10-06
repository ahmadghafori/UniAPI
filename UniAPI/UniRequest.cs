namespace UniAPI
{
    namespace UniAPI
    {
        /// <summary>
        /// Represents a unified request for multiple API protocols (REST, GraphQL, gRPC).
        /// 
        /// This class allows you to send requests using a single model, while internally mapping
        /// the request to the appropriate protocol format:
        /// - REST: uses HttpMethod directly, parameters go to query string, body is request payload
        /// - GraphQL: GET maps to Query, other HTTP methods map to Mutation; variables come from Parameters
        /// - gRPC: method name and message object are used; Metadata can include headers or parameters
        /// 
        /// You can configure per-request settings such as Timeout, Retry, Logging, and handle errors
        /// using the optional OnError callback.
        /// </summary>
        /// <typeparam name="TRequest">Type of the request payload</typeparam>
        public class UniRequest<TRequest>
        {
            /// <summary>
            /// Select the protocol to use for this request.
            /// </summary>
            public required ApiProtocol Protocol { get; set; } = ApiProtocol.Rest;


            /// <summary>
            /// Base URL of the server (e.g., https://api.example.com)
            /// Required if Config.BaseUrl is not provided.
            /// </summary>
            public required string BaseUrl { get; set; }




            /// <summary>
            /// Path or Query string
            /// - REST: endpoint path, e.g., "/users"
            /// - GraphQL: Query or Mutation text
            /// - gRPC: method name, e.g., "UserService/GetUser"
            /// </summary>
            public string? PathOrQuery { get; set; } = "";

            /// <summary>
            /// Main payload
            /// - REST: request body
            /// - GraphQL: variables map
            /// - gRPC: request message object
            /// </summary>
            public TRequest? Body { get; set; }

            /// <summary>
            /// Parameters
            /// - REST: added to query string
            /// - GraphQL: mapped as variables
            /// - gRPC: can be used as metadata or input
            /// </summary>
            public Dictionary<string, object>? Parameters { get; set; }

            /// <summary>
            /// Headers
            /// - REST: added to HttpClient headers
            /// - GraphQL: added to HTTP headers
            /// - gRPC: added to Metadata
            /// </summary>
            public Dictionary<string, string>? Headers { get; set; }

            /// <summary>
            /// Request configuration (Timeout, Retry, Logging, etc.)
            /// </summary>
            public UniRequestConfig<TRequest>? Config { get; set; }
        }
    }
}
