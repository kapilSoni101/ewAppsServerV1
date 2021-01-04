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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ewApps.Core.ExceptionService;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ewApps.Core.ServiceProcessor {
    public class HttpRequestProcessor:IHttpRequestProcessor {

        #region Constructor

        public HttpRequestProcessor(HttpClient httpClient) {
            HttpClientInstance = httpClient;
            // Configure HttpClient instance.
            HttpClientInstance.Timeout = RequestTimeOut;
        }

        #endregion

        protected HttpClient HttpClientInstance {
            get; private set;
        }

        // Defaut value is 3 minutes.
        protected virtual TimeSpan RequestTimeOut {
            get;
            set;
        } = new TimeSpan(0, 3, 0);

        #region Http API Async Executor Methods

        /// <summary>
        /// Executes the get request with given uri parameters and header values.
        /// </summary>
        /// <typeparam name="T">Expected response result type.</typeparam>
        /// <param name="requestUri">The base URI.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="responseType">Requested response format type.</param>
        /// <param name="headerParameters">The header parameters.</param>
        /// <param name="pathParameters">The path parameters.</param>
        /// <param name="uriParameters">The URI parameters.</param>
        /// <returns>
        /// Returns response result in requested format.
        /// </returns>
        public async Task<T> ExecuteGETRequestAsync<T>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters) {
            string uriSuffix = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uriSuffix = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uriSuffix = AddQueryStringParameters(uriSuffix, uriParameters);

            uriSuffix = ConcatenateUri(baseUri, uriSuffix);

            // Executes Get request synchornously.
            HttpResponseMessage response = await HttpClientInstance.GetAsync(uriSuffix);

            // Reads and returns result from response is status is 200.      
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return await response.Content.ReadAsAsync<T>();
            }
            // Throw exception if response is other then 200.
            else {
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}

                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return await RaiseServiceException<T>(response);

                //    return default(T);
            }
        }

        public async Task<T> ExecutePOSTRequestAsync<T, U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);
            HttpResponseMessage response;
            if(responseType == AcceptMediaTypeEnum.MultipartFormData) {
                /*using(var mpContent =
                        new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))) {
                    //content.Add(new StreamContent(new MemoryStream(image)), "bilddatei", "upload.jpg");
                    mpContent.Add(bodyParam as HttpContent);
                    response = await HttpClientInstance.PostAsync(uri, mpContent);
                }*/
                string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
                HttpClientInstance.DefaultRequestHeaders.Add("ContentType", "multipart/form-data; boundary=" + boundary);
                response = await HttpClientInstance.PostAsync(uri, bodyParam as HttpContent);
            }
            else {

                HttpContent content = CreateJsonObjectContent<U>(bodyParam);
                // Executes Get request synchronously.
                response = await HttpClientInstance.PostAsync(uri, content);
            }

            // Reads and returns result from response is status is 200.      
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return await response.Content.ReadAsAsync<T>();
            }
            // Throw exception if response is other then 200.
            else {
                //string responseError = await response.Content.ReadAsStringAsync();
                //throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return await RaiseServiceException<T>(response);
            };
        }

        public async Task ExecutePOSTRequestAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes Get request synchornously.
            HttpResponseMessage response = await HttpClientInstance.PostAsync(uri, content);

            // Throw exception if response is other then 200.
            if(response.IsSuccessStatusCode == false) {
                ////string responseError = await response.Content.ReadAsStringAsync();
                ////throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                await RaiseServiceException<object>(response);
            }
        }

        public async Task<T> ExecutePUTRequestAsync<T, U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam, bool executeAsync = false) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes PUT request asynchornously.
            HttpResponseMessage response = await HttpClientInstance.PutAsync(uri, content);


            // Reads and returns result from response is status is 200.      
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return await response.Content.ReadAsAsync<T>();
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = await response.Content.ReadAsStringAsync();
                ////throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return await RaiseServiceException<T>(response);
            }
        }

        public async Task ExecutePUTRequestAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes PUT request synchornously.
            HttpResponseMessage response = await HttpClientInstance.PutAsync(uri, content);

            // Throw exception if response is other then 200.
            if(response.IsSuccessStatusCode == false) {
                ////string responseError = await response.Content.ReadAsStringAsync();
                ////throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                await RaiseServiceException<object>(response);
            }
        }

        public async Task ExecuteDELETERequestAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes DELETE request synchornously.
            HttpResponseMessage response = await HttpClientInstance.DeleteAsync(uri);

            // Throw exception if response is other then 200.
            if(!response.IsSuccessStatusCode) {
                ////string responseError = await response.Content.ReadAsStringAsync();
                ////throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                await RaiseServiceException<object>(response);
            }
        }

        public async Task<string> ExecuteGetRequestAsStringAsync(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            // Executes Get request synchornously.
            HttpResponseMessage response = await HttpClientInstance.GetAsync(uri);

            // Reads and returns result from response is status is 200.      
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return await response.Content.ReadAsStringAsync();
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = await response.Content.ReadAsStringAsync();
                ////throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return await RaiseServiceException<string>(response);

            };
        }

        /// <summary>
        /// Executes the post request with given uri parameters and header values.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="responseType">Requested response format type.</param>
        /// <param name="headerParameters">The header parameters.</param>
        /// <param name="pathParameters">The path parameters.</param>
        /// <param name="uriParameters">The URI parameters.</param>
        /// <param name="bodyParam">The body parameter.</param>
        /// <returns>
        /// Returns response result in requested format.
        /// </returns>
        public async Task<string> ExecutePOSTRequestAndReturnAsStringAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes Get request synchornously.
            HttpResponseMessage response = await HttpClientInstance.PostAsync(uri, content);

            // Reads and returns result from response is status is 200.     
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return await response.Content.ReadAsStringAsync();
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = await response.Content.ReadAsStringAsync();
                ////throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return await RaiseServiceException<string>(response);
            }
        }

        /// <summary>
        /// Executes the post request with given uri parameters and header values.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="responseType">Requested response format type.</param>
        /// <param name="headerParameters">The header parameters.</param>
        /// <param name="pathParameters">The path parameters.</param>
        /// <param name="uriParameters">The URI parameters.</param>
        /// <returns>
        /// Returns response result in requested format.
        /// </returns>
        public async Task<string> ExecutePOSTRequestAndReturnAsStringAsync(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            // Executes Get request synchornously.
            HttpResponseMessage response = await HttpClientInstance.PostAsync(uri, null);

            // Reads and returns result from response is status is 200.     
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return await response.Content.ReadAsStringAsync();
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = await response.Content.ReadAsStringAsync();
                ////throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return await RaiseServiceException<string>(response);

            }
        }

        /// <summary>
        /// Executes the post request with given uri parameters and header values.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="responseType">Requested response format type.</param>
        /// <param name="headerParameters">The header parameters.</param>
        /// <param name="pathParameters">The path parameters.</param>
        /// <param name="uriParameters">The URI parameters.</param>
        /// <param name="bodyParam">The body parameter.</param>
        /// <returns>
        /// Returns response result in requested format.
        /// </returns>
        public async Task<string> ExecutePUTRequestAndReturnAsStringAsync<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            // Gets HttpClient instance.
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes Get request synchornously.
            HttpResponseMessage response = await HttpClientInstance.PutAsync(uri, content);

            // Reads and returns result from response is status is 200.     
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return await response.Content.ReadAsStringAsync();
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = await response.Content.ReadAsStringAsync();
                ////throw new Exception(responseError);
                //string responseError = await response.Content.ReadAsStringAsync();
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return await RaiseServiceException<string>(response);

            }
        }

        public async Task<T> PostMultipartAsync<T>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, string fileName, byte[] fileData, string contentType) {
            using(var requestContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))) {
                // Gets HttpClient instance.
                string uri = "";

                // Add request headers
                AddRequestHeaders(responseType, headerParameters);

                // Append path parameters in request uri.
                uri = AddPathParameters(requestUri, pathParameters);

                // Append query string parameters in request uri.
                uri = AddQueryStringParameters(uri, uriParameters);

                uri = ConcatenateUri(HttpClientInstance.BaseAddress.AbsoluteUri, uri);
                uri = ConcatenateUri(baseUri, uri);

                HttpContent fileContent = new StreamContent(new MemoryStream(fileData));
                //fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
                requestContent.Add(fileContent, fileName, fileName);

                HttpResponseMessage response = await HttpClientInstance.PostAsync(uri, requestContent);

                // Reads and returns result from response is status is 200.     
                if(response.IsSuccessStatusCode) {
                    // Reads and returns result from response.       
                    return await response.Content.ReadAsAsync<T>();
                }
                // Throw exception if response is other then 200.
                else {
                    ////string responseError = await response.Content.ReadAsStringAsync();
                    ////throw new Exception(responseError);
                    //string responseError = await response.Content.ReadAsStringAsync();
                    //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                    //string message = "";
                    //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                    //    message = string.Join(", ", ewpError.MessageList);
                    //}
                    //else {
                    //    message = "Error in " + response.RequestMessage.RequestUri;
                    //}
                    //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                    return await RaiseServiceException<T>(response);

                }
            }
        }

        #endregion

        #region Http API Sync Executor Methods

        /// <summary>
        /// Executes the get request with given uri parameters and header values.
        /// </summary>
        /// <typeparam name="T">Expected response result type.</typeparam>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="responseType">Requested response format type.</param>
        /// <param name="headerParameters">The header parameters.</param>
        /// <param name="pathParameters">The path parameters.</param>
        /// <param name="uriParameters">The URI parameters.</param>
        /// <returns>
        /// Returns response result in requested format.
        /// </returns>
        public T ExecuteGetRequest<T>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters) {
            string uriSuffix = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uriSuffix = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uriSuffix = AddQueryStringParameters(uriSuffix, uriParameters);

            uriSuffix = ConcatenateUri(baseUri, uriSuffix);

            // Executes Get request synchornously.
            HttpResponseMessage response = HttpClientInstance.GetAsync(uriSuffix).Result;

            // Reads and returns result from response is status is 200.      
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return response.Content.ReadAsAsync<T>().Result;
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return RaiseServiceException<T>(response).Result;

            }
        }

        public T ExecutePOSTRequest<T, U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes Get request synchronously.
            HttpResponseMessage response = HttpClientInstance.PostAsync(uri, content).Result;

            // Reads and returns result from response is status is 200.      
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return response.Content.ReadAsAsync<T>().Result;
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);
                return RaiseServiceException<T>(response).Result;

            };
        }

        public void ExecutePOSTRequest<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes Get request synchornously.
            HttpResponseMessage response = HttpClientInstance.PostAsync(uri, content).Result;

            // Throw exception if response is other then 200.
            if(!response.IsSuccessStatusCode) {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                RaiseServiceException<object>(response);
            }
        }

        public T ExecutePUTRequest<T, U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam, bool executeAsync = false) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes PUT request synchornously.
            HttpResponseMessage response = HttpClientInstance.PutAsync(uri, content).Result;

            // Reads and returns result from response is status is 200.      
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return response.Content.ReadAsAsync<T>().Result;
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                ////// throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return RaiseServiceException<T>(response).Result;

            };
        }

        public void ExecutePUTRequest<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes PUT request synchornously.
            HttpResponseMessage response = HttpClientInstance.PutAsync(uri, content).Result;

            // Throw exception if response is other then 200.
            if(!response.IsSuccessStatusCode) {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                //////throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                RaiseServiceException<object>(response);

            }
        }

        public void ExecuteDELETERequest<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes DELETE request synchornously.
            HttpResponseMessage response = HttpClientInstance.DeleteAsync(uri).Result;

            // Throw exception if response is other then 200.
            if(!response.IsSuccessStatusCode) {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                //////throw new EwpHttpResponseException(string.Format("An error occured to execute DELETE {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                RaiseServiceException<object>(response);

            }
        }

        public void ExecuteDELETERequest(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            // Executes DELETE request synchornously.
            HttpResponseMessage response = HttpClientInstance.DeleteAsync(uri).Result;

            // Throw exception if response is other then 200.
            if(!response.IsSuccessStatusCode) {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                //////throw new EwpHttpResponseException(string.Format("An error occured to execute DELETE {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                RaiseServiceException<object>(response);

            }
        }

        public async Task ExecuteDELETERequestAsync(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            // Executes DELETE request synchornously.
            HttpResponseMessage response = HttpClientInstance.DeleteAsync(uri).Result;

            // Throw exception if response is other then 200.
            if(!response.IsSuccessStatusCode) {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                //////throw new EwpHttpResponseException(string.Format("An error occured to execute DELETE {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                await RaiseServiceException<object>(response);

            }
        }

        public string ExecuteGetRequestAsString(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            // Executes Get request synchornously.
            HttpResponseMessage response = HttpClientInstance.GetAsync(uri).Result;

            // Reads and returns result from response is status is 200.      
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return response.Content.ReadAsStringAsync().Result;
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return RaiseServiceException<string>(response).Result;

            };
        }

        /// <summary>
        /// Executes the post request with given uri parameters and header values.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="responseType">Requested response format type.</param>
        /// <param name="headerParameters">The header parameters.</param>
        /// <param name="pathParameters">The path parameters.</param>
        /// <param name="uriParameters">The URI parameters.</param>
        /// <param name="bodyParam">The body parameter.</param>
        /// <returns>
        /// Returns response result in requested format.
        /// </returns>
        public string ExecutePOSTRequestAndReturnAsString<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes Get request synchornously.
            HttpResponseMessage response = HttpClientInstance.PostAsync(uri, content).Result;

            // Reads and returns result from response is status is 200.     
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return response.Content.ReadAsStringAsync().Result;
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return RaiseServiceException<string>(response).Result;

            };
        }

        /// <summary>
        /// Executes the post request with given uri parameters and header values.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="responseType">Requested response format type.</param>
        /// <param name="headerParameters">The header parameters.</param>
        /// <param name="pathParameters">The path parameters.</param>
        /// <param name="uriParameters">The URI parameters.</param>
        /// <param name="bodyParam">The body parameter.</param>
        /// <returns>
        /// Returns response result in requested format.
        /// </returns>
        public string ExecutePUTRequestAndReturnAsString<U>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam) {
            // Gets HttpClient instance.
            string uri = "";

            // Add request headers
            AddRequestHeaders(responseType, headerParameters);

            // Append path parameters in request uri.
            uri = AddPathParameters(requestUri, pathParameters);

            // Append query string parameters in request uri.
            uri = AddQueryStringParameters(uri, uriParameters);

            uri = ConcatenateUri(baseUri, uri);

            HttpContent content = CreateJsonObjectContent<U>(bodyParam);

            // Executes Get request synchornously.
            HttpResponseMessage response = HttpClientInstance.PutAsync(uri, content).Result;

            // Reads and returns result from response is status is 200.     
            if(response.IsSuccessStatusCode) {
                // Reads and returns result from response.       
                return response.Content.ReadAsStringAsync().Result;
            }
            // Throw exception if response is other then 200.
            else {
                ////string responseError = response.Content.ReadAsStringAsync().Result;
                ////throw new Exception(responseError);
                //string responseError = response.Content.ReadAsStringAsync().Result;
                //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);

                //string message = "";
                //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                //    message = string.Join(", ", ewpError.MessageList);
                //}
                //else {
                //    message = "Error in " + response.RequestMessage.RequestUri;
                //}
                //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                return RaiseServiceException<string>(response).Result;

            }
        }

        public T PostMultipart<T>(string baseUri, string requestUri, AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, string fileName, byte[] fileData, string contentType) {
            using(var requestContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))) {
                // Gets HttpClient instance.
                string uri = "";

                // Add request headers
                AddRequestHeaders(responseType, headerParameters);

                // Append path parameters in request uri.
                uri = AddPathParameters(requestUri, pathParameters);

                // Append query string parameters in request uri.
                uri = AddQueryStringParameters(uri, uriParameters);

                uri = ConcatenateUri(HttpClientInstance.BaseAddress.AbsoluteUri, uri);
                uri = ConcatenateUri(baseUri, uri);

                HttpContent fileContent = new StreamContent(new MemoryStream(fileData));
                //fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
                requestContent.Add(fileContent, fileName, fileName);

                HttpResponseMessage response = HttpClientInstance.PostAsync(uri, requestContent).Result;

                // Reads and returns result from response is status is 200.     
                if(response.IsSuccessStatusCode) {
                    // Reads and returns result from response.       
                    return response.Content.ReadAsAsync<T>().Result;
                }
                // Throw exception if response is other then 200.
                else {
                    ////string responseError = response.Content.ReadAsStringAsync().Result;
                    ////throw new Exception(responseError);
                    //string responseError = response.Content.ReadAsStringAsync().Result;
                    //EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
                    //string message = "";
                    //if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                    //    message = string.Join(", ", ewpError.MessageList);
                    //}
                    //else {
                    //    message = "Error in " + response.RequestMessage.RequestUri;
                    //}
                    //throw new EwpServiceException(message, response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);

                    return RaiseServiceException<T>(response).Result;

                }
            }
        }

        public async Task<string> GetBearerTokenAsync(string clientId, string authServiceUrl, string authScope) {
            // Use this code to call connector with app to app communication
            // discover endpoints from metadata
            //var disco = await DiscoveryClient.GetAsync("https://localhost:44300");
            var disco = await DiscoveryClient.GetAsync(authServiceUrl);
            if(disco.IsError) {
                Console.WriteLine(disco.Error);
                return "";
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, clientId, "secret");
            // string[] str = new string[] { "conn-vcapi", "authapi" };
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("authapi payapi connvcapi");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(authScope);

            if(tokenResponse.IsError) {
                // ToDo: Add Log here.
                Console.WriteLine(tokenResponse.Error);
                return "";
            }
            return tokenResponse.AccessToken.ToString();
        }

        #endregion

        #region Private Methods

        private HttpContent CreateJsonObjectContent<T>(T model) {
            MediaTypeFormatter mediaTypeFormatter = new JsonMediaTypeFormatter();
            MediaTypeHeaderValue mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(GetMediaTypeString(AcceptMediaTypeEnum.JSON));

            HttpContent content = new ObjectContent<T>(model, mediaTypeFormatter, mediaTypeHeaderValue);
            return content;
        }

        private string AddRequestHeaders(AcceptMediaTypeEnum responseType, List<KeyValuePair<string, string>> headerParameters) {
            string responseMediaType;
            if(headerParameters != null && headerParameters.Count > 0) {
                // Add Header Items in client.
                foreach(KeyValuePair<string, string> headerItem in headerParameters) {
                    if(HttpClientInstance.DefaultRequestHeaders.Any(i => i.Key.ToLower().Equals(headerItem.Key.ToLower())) == false) {
                        HttpClientInstance.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                    }
                }
            }

            // Clear Accept Header Values.
            HttpClientInstance.DefaultRequestHeaders.Accept.Clear();

            responseMediaType = GetMediaTypeString(responseType);

            // Add Accept Header Entry.
            HttpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(responseMediaType));

            return responseMediaType;
        }

        private string ConcatenateUri(string baseUri, string suffixUri) {
            Uri uriPartOne = new Uri(baseUri);
            Uri uriPartTwo = new Uri(uriPartOne, suffixUri);
            return uriPartTwo.AbsoluteUri;
        }


        // Append given string parameters in request uri as a path parameters
        private string AddPathParameters(string requestUri, List<KeyValuePair<string, string>> pathParameters) {
            if(pathParameters != null && pathParameters.Count > 0) {
                for(int i = 0; i < pathParameters.Count; i++) {
                    requestUri = requestUri.Replace("{" + pathParameters[i].Key + "}", pathParameters[i].Value);
                }
                return requestUri;
            }
            else {
                return requestUri;
            }
        }

        // Append query string parameters in request uri as name value pair.
        private static string AddQueryStringParameters(string requestUri, List<KeyValuePair<string, string>> queryStringParameters) {
            if(queryStringParameters != null && queryStringParameters.Count > 0) {
                return string.Concat(requestUri + "?", string.Join("&", queryStringParameters.Select(k => string.Format("{0}={1}", k.Key, k.Value))));
            }
            else {
                return requestUri;
            }
        }

        #endregion

        #region Protected Methods

        protected string GetMediaTypeString(AcceptMediaTypeEnum responseType) {
            string responseMediaType;
            switch(responseType) {
                case AcceptMediaTypeEnum.JSON:
                    responseMediaType = "application/json";
                    break;
                case AcceptMediaTypeEnum.XML:
                    responseMediaType = "application/xml";
                    break;
                case AcceptMediaTypeEnum.MultipartFormData:
                    responseMediaType = "multipart/form-data";
                    break;
                default:
                    responseMediaType = "application/json";
                    break;
            }

            return responseMediaType;
        }

        protected virtual void AddDefaultHeaders() {
            HttpClientInstance.DefaultRequestHeaders.Add("AccessToken", Guid.NewGuid().ToString());
        }

        private async Task<T> RaiseServiceException<T>(HttpResponseMessage response) {
            string responseError = await response.Content.ReadAsStringAsync();
            EwpError ewpError = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(responseError);
            StringBuilder message = new StringBuilder();
            if(ewpError != null && ewpError.MessageList != null && ewpError.MessageList.Count > 0) {
                message.AppendJoin(", ", ewpError.MessageList);
            }
            else {
                message.AppendFormat("Error in {0} \r\n HttpStatusCode: {1}", response.RequestMessage.RequestUri, response.StatusCode);
            }

            message.AppendLine();
            //if(response.RequestMessage.Headers.Contains("Authorization")) {
            //    message.AppendLine("Token:" + response.RequestMessage.Headers.GetValues("Authorization").FirstOrDefault());
            //}


            if(ewpError != null) {
                throw new EwpServiceException(message.ToString(), response.StatusCode, ewpError.ErrorType, ewpError.EwpErrorDataList);
            }
            else {
                throw new EwpServiceException(message.ToString(), response.StatusCode, ErrorType.System, null);
            }
        }

        #endregion

    }

    public static class HttpResponseMessageExtension {
        public static async Task<ExceptionResponse> ExceptionResponse(this HttpResponseMessage httpResponseMessage) {
            string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
            return exceptionResponse;
        }
    }

    public class ExceptionResponse {
        public string Message {
            get; set;
        }
        public string ExceptionMessage {
            get; set;
        }
        public string ExceptionType {
            get; set;
        }
        public string StackTrace {
            get; set;
        }
        public ExceptionResponse InnerException {
            get; set;
        }
    }
}

