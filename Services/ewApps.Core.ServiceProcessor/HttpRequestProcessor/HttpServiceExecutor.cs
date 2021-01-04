using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ewApps.Core.ServiceProcessor {
    /// <summary>
    /// This class executes service request as HTTP request.
    /// </summary>
    public class HttpServiceExecutor {
        public string _rootUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpServiceExecutor"/> class and root url.
        /// </summary>
        /// <param name="rootUrl">The root URL.</param>
        public HttpServiceExecutor(string rootUrl) {
            _rootUrl = rootUrl;
        }

        /// <summary>
        /// Executes the Http request asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of return object.</typeparam>
        /// <param name="requestOptions">The request options to execute Http request.</param>
        /// <param name="useCache">if set to <c>true</c> [cache] will be use to get data.</param>
        /// <returns>Returns service response of type [T].</returns>
        /// <exception cref="InvalidOperationException">Raise <see cref="InvalidOperationException"/> if requested service type is not supported.</exception>
        public async Task<T> ExecuteAsync<T>(RequestOptions requestOptions, bool useCache) {
            HttpClient httpClient = new HttpClient();
            IHttpRequestProcessor httpRequestProcessor = new HttpRequestProcessor(httpClient);

            // Generates Bearer token based for requested AppClient from authentication server.
            if(requestOptions.BearerTokenInfo != null) {
                string bearerToken = await httpRequestProcessor.GetBearerTokenAsync(requestOptions.BearerTokenInfo.AppClientName, requestOptions.BearerTokenInfo.AuthServiceUrl, requestOptions.BearerTokenInfo.AuthScope);
                httpClient.SetBearerToken(bearerToken);
            }

            // Executes Rest API request based on request type.
            switch(requestOptions.ServiceRequestType) {
                case RequestTypeEnum.Get:
                    return await httpRequestProcessor.ExecuteGETRequestAsync<T>(_rootUrl, requestOptions.Method, requestOptions.AcceptType, requestOptions.HeaderParameters, requestOptions.PathParameters, requestOptions.UriParamerters);
                case RequestTypeEnum.Put:
                    return await httpRequestProcessor.ExecutePUTRequestAsync<T, object>(_rootUrl, requestOptions.Method, requestOptions.AcceptType, requestOptions.HeaderParameters, requestOptions.PathParameters, requestOptions.UriParamerters, requestOptions.MethodData, true);
                case RequestTypeEnum.Post:
                    return await httpRequestProcessor.ExecutePOSTRequestAsync<T, object>(_rootUrl, requestOptions.Method, requestOptions.AcceptType, requestOptions.HeaderParameters, requestOptions.PathParameters, requestOptions.UriParamerters, requestOptions.MethodData);
                case RequestTypeEnum.Delete:
                    await httpRequestProcessor.ExecuteDELETERequestAsync<object>(_rootUrl, requestOptions.Method, requestOptions.AcceptType, requestOptions.HeaderParameters, requestOptions.PathParameters, requestOptions.UriParamerters, requestOptions.MethodData);
                    return default(T);
                default:
                    throw new InvalidOperationException("Invalid Http Request Type");
            }
        }

        /// <summary>
        /// Executes the Http request.
        /// </summary>
        /// <typeparam name="T">The type of return object.</typeparam>
        /// <param name="requestOptions">The request options to execute Http request.</param>
        /// <param name="useCache">if set to <c>true</c> [cache] will be use to get data.</param>
        /// <returns>Returns service response of type [T].</returns>
        /// <exception cref="InvalidOperationException">Raise <see cref="InvalidOperationException"/> if requested service type is not supported.</exception>
        public T Execute<T>(RequestOptions requestOptions, bool useCache) {
            HttpClient httpClient = new HttpClient();
            IHttpRequestProcessor httpRequestProcessor = new HttpRequestProcessor(httpClient);

            // Generates Bearer token based for requested AppClient from authentication server.
            if(requestOptions.BearerTokenInfo != null) {
                string bearerToken = httpRequestProcessor.GetBearerTokenAsync(requestOptions.BearerTokenInfo.AppClientName, requestOptions.BearerTokenInfo.AuthServiceUrl, requestOptions.BearerTokenInfo.AuthScope).Result;
                httpClient.SetBearerToken(bearerToken);
            }

            // Executes Rest API request based on request type.
            switch(requestOptions.ServiceRequestType) {
                case RequestTypeEnum.Get:
                    return httpRequestProcessor.ExecuteGetRequest<T>(_rootUrl, requestOptions.Method, requestOptions.AcceptType, requestOptions.HeaderParameters, requestOptions.PathParameters, requestOptions.UriParamerters);
                case RequestTypeEnum.Put:
                    return httpRequestProcessor.ExecutePUTRequest<T, object>(_rootUrl, requestOptions.Method, requestOptions.AcceptType, requestOptions.HeaderParameters, requestOptions.PathParameters, requestOptions.UriParamerters, requestOptions.MethodData, true);
                case RequestTypeEnum.Post:
                    return httpRequestProcessor.ExecutePOSTRequest<T, object>(_rootUrl, requestOptions.Method, requestOptions.AcceptType, requestOptions.HeaderParameters, requestOptions.PathParameters, requestOptions.UriParamerters, requestOptions.MethodData);
                case RequestTypeEnum.Delete:
                    httpRequestProcessor.ExecuteDELETERequest<object>(_rootUrl, requestOptions.Method, requestOptions.AcceptType, requestOptions.HeaderParameters, requestOptions.PathParameters, requestOptions.UriParamerters, requestOptions.MethodData);
                    return default(T);
                default:
                    throw new InvalidOperationException("Invalid Http Request Type");
            }
        }
    }
}
