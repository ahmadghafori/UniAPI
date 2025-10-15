using PortocolService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAPI.PortocolService;
using UniAPI.UniAPI;

namespace UniAPI
{
    public class UniApiClient
    {
        private string CombineUrl(string baseUrl, string? path)
        {
            if (path is not null)
                return $"{baseUrl.TrimEnd('/')}/{path.TrimStart('/')}";

            return baseUrl ;
        }


        public async Task<UniResponse<TResponse>> Post<TResponse, TRequest>(UniRequest<TRequest> request)
        {
           try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request.Config?.OnError is not null)
                        request.Config.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                //ApiProtocol

                switch (request.Protocol)
                {
                    case ApiProtocol.Rest:
                        {
                            var restservice = new RestService();
                            var result = await restservice.Send<TResponse, TRequest>(new RestRequest<TRequest>
                            {
                                Method = HttpMethod.Post,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Body = request.Body,
                                Protocol = request.Protocol,
                                Config = request.Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request.Config?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.Config.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch(Exception ex)
            {
                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        public async Task<UniResponse<TResponse>> Put<TResponse, TRequest>(UniRequest<TRequest> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request.Config?.OnError is not null)
                        request.Config.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                //ApiProtocol

                switch (request.Protocol)
                {
                    case ApiProtocol.Rest:
                        {
                            var restservice = new RestService();
                            var result = await restservice.Send<TResponse, TRequest>(new RestRequest<TRequest>
                            {
                                Method = HttpMethod.Put,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Body = request.Body,
                                Protocol = request.Protocol,
                                Config = request.Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request.Config?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.Config.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch(Exception ex)
            {
                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        public async Task<UniResponse<TResponse>> Delete<TResponse, TRequest>(UniRequest<TRequest> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request.Config?.OnError is not null)
                        request.Config.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                //ApiProtocol

                switch (request.Protocol)
                {
                    case ApiProtocol.Rest:
                        {
                            var restservice = new RestService();
                            var result = await restservice.Send<TResponse, TRequest>(new RestRequest<TRequest>
                            {
                                Method = HttpMethod.Delete,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Body = request.Body,
                                Protocol = request.Protocol,
                                Config = request.Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request.Config?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.Config.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch(Exception ex)
            {
                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        /// <summary>
        /// REST protocol does not support sending a body. Even if one is provided, it will be ignored.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UniResponse<TResponse>> Get<TResponse,TRequest>(UniRequest<TRequest> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request.Config?.OnError is not null)
                        request.Config.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                switch (request.Protocol)
                {
                    case ApiProtocol.Rest:
                        {
                            var restservice = new RestService();
                            var result = await restservice.Send<TResponse, TRequest>(new RestRequest<TRequest>
                            {
                                Method = HttpMethod.Get,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Protocol = request.Protocol,
                                Config = request.Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request.Config?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.Config.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }



        public async Task<UniResponse<TResponse>> Post<TResponse, TRequest>(UniRequest<TResponse, TRequest> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request.Config?.OnError is not null)
                        request.Config.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig<TRequest>();
                if (request.Config is not null)
                {
                    Config.EnableLogging = request.Config.EnableLogging;
                    Config.RetryDelay = request.Config.RetryDelay;
                    Config.Timeout = request.Config.Timeout;
                    Config.RetryCount = request.Config.RetryCount;
                }

                //ApiProtocol

                switch (request.Protocol)
                {
                    case ApiProtocol.Rest:
                        {
                            var restservice = new RestService();
                            var result = await restservice.Send<TResponse, TRequest>(new RestRequest<TRequest>
                            {
                                Method = HttpMethod.Post,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Body = request.Body,
                                Protocol = request.Protocol,
                                Config = Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request.Config?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.Config.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request.Config?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.Config.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        public async Task<UniResponse<TResponse>> Put<TResponse, TRequest>(UniRequest<TResponse,TRequest> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request.Config?.OnError is not null)
                        request.Config.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig<TRequest>();
                if (request.Config is not null)
                {
                    Config.EnableLogging = request.Config.EnableLogging;
                    Config.RetryDelay = request.Config.RetryDelay;
                    Config.Timeout = request.Config.Timeout;
                    Config.RetryCount = request.Config.RetryCount;
                }

                //ApiProtocol

                switch (request.Protocol)
                {
                    case ApiProtocol.Rest:
                        {
                            var restservice = new RestService();
                            var result = await restservice.Send<TResponse, TRequest>(new RestRequest<TRequest>
                            {
                                Method = HttpMethod.Put,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Body = request.Body,
                                Protocol = request.Protocol,
                                Config = Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request.Config?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.Config.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request.Config?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.Config.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        public async Task<UniResponse<TResponse>> Delete<TResponse, TRequest>(UniRequest<TResponse, TRequest> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request.Config?.OnError is not null)
                        request.Config.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig<TRequest>();
                if (request.Config is not null)
                {
                    Config.EnableLogging = request.Config.EnableLogging;
                    Config.RetryDelay = request.Config.RetryDelay;
                    Config.Timeout = request.Config.Timeout;
                    Config.RetryCount = request.Config.RetryCount;
                }

                //ApiProtocol

                switch (request.Protocol)
                {
                    case ApiProtocol.Rest:
                        {
                            var restservice = new RestService();
                            var result = await restservice.Send<TResponse, TRequest>(new RestRequest<TRequest>
                            {
                                Method = HttpMethod.Delete,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Body = request.Body,
                                Protocol = request.Protocol,
                                Config = Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request.Config?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.Config.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request.Config?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.Config.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        /// <summary>
        /// REST protocol does not support sending a body. Even if one is provided, it will be ignored.
        /// </summary>
        public async Task<UniResponse<TResponse>> Get<TResponse, TRequest>(UniRequest<TResponse, TRequest> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request.Config?.OnError is not null)
                        request.Config.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig<TRequest>();
                if (request.Config is not null)
                {
                    Config.EnableLogging = request.Config.EnableLogging;
                    Config.RetryDelay = request.Config.RetryDelay;
                    Config.Timeout = request.Config.Timeout;
                    Config.RetryCount = request.Config.RetryCount;
                }

                //ApiProtocol

                switch (request.Protocol)
                {
                    case ApiProtocol.Rest:
                        {
                            var restservice = new RestService();
                            var result = await restservice.Send<TResponse, TRequest>(new RestRequest<TRequest>
                            {
                                Method = HttpMethod.Get,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Protocol = request.Protocol,
                                Config = Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request.Config?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.Config.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request.Config?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.Config.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request.Config?.OnError is not null)
                    request.Config.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

    }
}
