using UniAPI.UniAPI;

namespace UniAPI.PortocolService
{
    public class RestRequest<TRequest> : UniRequest<TRequest>
    {
        public required HttpMethod Method { get; set; }
    }

    public class RestRequest : UniRequest<object>
    {
        public required HttpMethod Method { get; set; }
    }
}
