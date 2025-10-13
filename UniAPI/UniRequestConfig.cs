using UniAPI.UniAPI;

namespace UniAPI
{
    /// <summary>
    /// Configuration settings for a UniRequest.
    /// 
    /// This class allows you to define per-request options such as:
    /// - Timeout: maximum waiting time for a response
    /// - RetryCount: number of retry attempts in case of failure
    /// - RetryDelay: delay between retries
    /// - EnableLogging: whether to log request and response details
    /// - OnError: callback to handle errors or capture response failures for this request
    /// 
    /// These settings can be applied individually to each request for fine-grained control.
    /// </summary>
    /// <typeparam name="TRequest">Type of the request payload</typeparam>
    /// <typeparam name="TResponse">Type of the response payload</typeparam>
    public class UniRequestConfig<TResponse,TRequest>
    {
        /// <summary>
        /// Base URL default for requests
        /// </summary>
        public string? BaseUrl { get; set; }

        /// <summary>
        /// Maximum time to wait for a response
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Number of retries in case of failure
        /// </summary>
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// Delay between retries
        /// </summary>
        public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>
        /// If true, request and response details will be logged
        /// </summary>
        public bool EnableLogging { get; set; } = false;

        /// <summary>
        /// Callback to handle errors or logging data
        /// - Parameter 1: the UniRequest object
        /// - Parameter 2: Status code (HTTP or mapped)
        /// - Parameter 3: Error message or response body
        /// </summary>
        public Action<UniRequest<TRequest>, int, string>? OnError { get; set; }

        public Action<UniResponse<TResponse>,int,string>? OnResult { get; set; }
    }


    /// <summary>
    /// Configuration settings for a UniRequest.
    /// 
    /// This class allows you to define per-request options such as:
    /// - Timeout: maximum waiting time for a response
    /// - RetryCount: number of retry attempts in case of failure
    /// - RetryDelay: delay between retries
    /// - EnableLogging: whether to log request and response details
    /// - OnError: callback to handle errors or capture response failures for this request
    /// 
    /// These settings can be applied individually to each request for fine-grained control.
    /// </summary>
    /// <typeparam name="TRequest">Type of the request payload</typeparam>
    public class UniRequestConfig<TRequest>
    {
        /// <summary>
        /// Base URL default for requests
        /// </summary>
        public string? BaseUrl { get; set; }

        /// <summary>
        /// Maximum time to wait for a response
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Number of retries in case of failure
        /// </summary>
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// Delay between retries
        /// </summary>
        public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>
        /// If true, request and response details will be logged
        /// </summary>
        public bool EnableLogging { get; set; } = false;

        /// <summary>
        /// Callback to handle errors or logging data
        /// - Parameter 1: the UniRequest object
        /// - Parameter 2: Status code (HTTP or mapped)
        /// - Parameter 3: Error message or response body
        /// </summary>
        public Action<UniRequest<TRequest>, int, string>? OnError { get; set; }
    }
}
