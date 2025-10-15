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
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch(Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch(Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch(Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig();
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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig();
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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig();
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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig();
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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                    case ApiProtocol.GraphQL:
                        {
                            var restservice = new GraphQLService();
                            var result = await restservice.Send<TResponse, TRequest>(new GraphQLRequest<TResponse,TResponse>
                            {
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Protocol = request.Protocol,
                                Config = Config,
                                Body = request.Body,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }



        public async Task<UniResponse<TResponse>> Post<TResponse>(UniRequest<TResponse> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig();
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
                            var result = await restservice.Send(new RestRequest<TResponse>
                            {
                                Method = HttpMethod.Post,
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Protocol = request.Protocol,
                                Config = Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }

                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        public async Task<UniResponse<TResponse>> Put<TResponse>(UniRequest<TResponse> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig();
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
                            var result = await restservice.Send(new RestRequest<TResponse>
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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = ex.Message,
                };
            }
        }

        public async Task<UniResponse<TResponse>> Delete<TResponse>(UniRequest<TResponse> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig();
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
                            var result = await restservice.Send(new RestRequest<TResponse>
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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
        public async Task<UniResponse<TResponse>> Get<TResponse>(UniRequest<TResponse> request)
        {
            try
            {
                var baseUrl = request.BaseUrl ?? request.Config?.BaseUrl;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    if (request?.OnError is not null)
                        request.OnError(request, 500, $"Request failed: BaseUrl is null");

                    return new UniResponse<TResponse>
                    {
                        StatusCode = 500,
                        Success = false,
                        Error = $"Request failed: BaseUrl is null"
                    };
                }

                var fullPath = CombineUrl(baseUrl, request.PathOrQuery);

                var Config = new UniRequestConfig();
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
                            var result = await restservice.Send(new RestRequest<TResponse>
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

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                    case ApiProtocol.GraphQL:
                        {
                            var restservice = new GraphQLService();
                            var result = await restservice.Send<TResponse,TResponse>(new GraphQLRequest<TResponse>
                            {
                                BaseUrl = baseUrl + request.PathOrQuery,
                                Protocol = request.Protocol,
                                Config = Config,
                                Files = request.Files,
                                Headers = request.Headers,
                                Parameters = request.Parameters,
                                PathOrQuery = request.PathOrQuery
                            });

                            if (request?.OnError is not null)
                            {
                                if (!result.Success)
                                {
                                    request.OnError(request, result.StatusCode, result.Error);
                                }
                            }

                            if (request?.OnResult is not null)
                            {
                                if (result.Success)
                                {
                                    request.OnResult(result, result.StatusCode, result.Error);
                                }
                            }

                            return result;
                        }
                }


                if (request?.OnError is not null)
                    request.OnError(request, 500, "Request failed: Not finde ApiProtocol");

                return new UniResponse<TResponse>
                {
                    StatusCode = 500,
                    Success = false,
                    Error = $"Request failed: Not finde ApiProtocol"
                };
            }
            catch (Exception ex)
            {
                if (request?.OnError is not null)
                    request.OnError(request, 500, ex.Message);

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
