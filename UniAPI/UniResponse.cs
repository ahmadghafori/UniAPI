namespace UniAPI
{
    /// <summary>
    /// Unified response for all protocols (REST, GraphQL, gRPC)
    /// 
    /// Fields:
    /// - StatusCode: HTTP status code or its equivalent
    /// - Success: whether the request was successful
    /// - Data: main response payload
    /// - Error: error message or failure details
    /// - Metadata: optional additional information
    ///     - REST: headers
    ///     - GraphQL: extensions
    ///     - gRPC: trailers / metadata
    /// </summary>
    public class UniResponse<TResponse>
    {
        /// <summary>
        /// Status code (HTTP or mapped equivalent)
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Indicates whether the request succeeded
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Response payload
        /// </summary>
        public TResponse? Data { get; set; }

        /// <summary>
        /// Error message or failure details
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// Optional metadata for responses
        /// - REST: headers
        /// - GraphQL: extensions
        /// - gRPC: trailers / metadata
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }
    }
}
