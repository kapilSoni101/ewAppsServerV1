/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ewApps.Core.ServiceProcessor {
    public interface IHttpRequestProcessor {
        Task ExecuteDELETERequestAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        Task<string> ExecuteGetRequestAsStringAsync(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters);

        Task<T> ExecuteGETRequestAsync<T>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters);

        Task<string> ExecutePOSTRequestAndReturnAsStringAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        Task<T> ExecutePOSTRequestAsync<T, U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        Task ExecutePOSTRequestAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        Task<T> ExecutePUTRequestAsync<T, U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam, bool executeAsync);

        Task ExecutePUTRequestAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        Task<string> ExecutePUTRequestAndReturnAsStringAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        Task<T> PostMultipartAsync<T>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, string fileName, byte[] fileData, string contentType);

        Task ExecuteDELETERequestAsync(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters);

        // ========================================Sync Methods========================================//
        void ExecuteDELETERequest<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        string ExecuteGetRequestAsString(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters);

        T ExecuteGetRequest<T>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters);

        string ExecutePOSTRequestAndReturnAsString<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        T ExecutePOSTRequest<T, U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        void ExecutePOSTRequest<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        T ExecutePUTRequest<T, U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam, bool executeAsync);

        void ExecutePUTRequest<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        string ExecutePUTRequestAndReturnAsString<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam);

        T PostMultipart<T>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, string fileName, byte[] fileData, string contentType);

        Task<string> GetBearerTokenAsync(string clientId, string authServiceUrl, string scope);

    }
}