using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
//AshaSharda - Added class for making the HTTP requests

namespace  ewApps.ServiceRegistery.Common
{

  /// <summary>
  /// AcceptMediaType header value for making HTTP request
  /// </summary>
  public enum AcceptMediaType
  {
    /// <summary>
    /// Media type JSON =1
    /// </summary>
    JSON = 1,
    /// <summary>
    /// Media type files =2
    /// </summary>
    MultipartFormData = 2,
    /// <summary>
    /// Media type XML =3
    /// </summary>
    XML = 3,
  }

  /// <summary>
  /// Processor to make any HTTP request - Post/Get/Put/Delete
  /// </summary>
  public class HttpRequestProcessor
  {

    #region Constructor

    //Takes instance of HTTPClient
    /// <summary>
    /// Constructor for requestprocessor
    /// </summary>
    /// <param name="httpClient">httpClient instance from the caller </param>
    public HttpRequestProcessor(HttpClient httpClient)
    {
      HttpClientInstance = httpClient;
      // Configure HttpClient instance.
      HttpClientInstance.Timeout = RequestTimeOut;
    }

    #endregion
    /// <summary>
    /// Proprty HttpClientInstance only getter;
    /// </summary>
    protected HttpClient HttpClientInstance
    {
      get; private set;
    }

    // Defaut value is 3 minutes.
    /// <summary>
    /// Default timeout period of a request. 
    /// </summary>
    protected virtual TimeSpan RequestTimeOut
    {
      get;
      set;
    } = new TimeSpan(0, 3, 0);

    #region Http API Async Executor Methods

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
    public async Task<T> ExecuteGetRequestAsync<T>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return await response.Content.ReadAsAsync<T>();
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = await response.Content.ReadAsStringAsync();
        throw new Exception(responseError);
        
      }
    }

    /// <summary>
    /// Execute the Post type request
    /// </summary>
    /// <typeparam name="T">Expected return type from the call</typeparam>
    /// <typeparam name="U">Input object type.</typeparam>
    /// <param name="baseUri">Base URI of the calling API</param>
    /// <param name="requestUri">Specific Request URI</param>
    /// <param name="responseType">Response type , XML/Json</param>
    /// <param name="headerParameters">header paraments in keyvalue pair</param>
    /// <param name="pathParameters">Path parameters in Key value pair</param>
    /// <param name="uriParameters">URI parameters</param>
    /// <param name="bodyParam">Body Parameters</param>
    /// <returns>Object of type T</returns>
    public async Task<T> ExecutePOSTRequestAsync<T, U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      HttpResponseMessage response = await HttpClientInstance.PostAsync(uri, content);

      // Reads and returns result from response is status is 200.      
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return await response.Content.ReadAsAsync<T>();
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = await response.Content.ReadAsStringAsync();
        // throw new EwpHttpResponseException(string.Format("An error occured to execute POST {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      };
    }

    /// <summary>
    /// Executes the Post request with only Input type, no output is expected.
    /// </summary>
    /// <typeparam name="U">Input object type.</typeparam>
    /// <param name="baseUri">Base URI of the calling API</param>
    /// <param name="requestUri">Specific Request URI</param>
    /// <param name="responseType">Response type , XML/Json</param>
    /// <param name="headerParameters">header paraments in keyvalue pair</param>
    /// <param name="pathParameters">Path parameters in Key value pair</param>
    /// <param name="uriParameters">URI parameters</param>
    /// <param name="bodyParam">Body Parameters</param>
    /// <returns>NO Content</returns>
    public async Task ExecutePOSTRequestAsync<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (!response.IsSuccessStatusCode)
      {
        string responseError = await response.Content.ReadAsStringAsync();
        // throw new EwpHttpResponseException(string.Format("An error occured to execute POST {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }
    /// <summary>
    /// Put request http call
    /// </summary>
    /// <typeparam name="T">Expected Output typr</typeparam>
    /// <typeparam name="U">Input object type.</typeparam>
    /// <param name="baseUri">Base URI of the calling API</param>
    /// <param name="requestUri">Specific Request URI</param>
    /// <param name="responseType">Response type , XML/Json</param>
    /// <param name="headerParameters">header paraments in keyvalue pair</param>
    /// <param name="pathParameters">Path parameters in Key value pair</param>
    /// <param name="uriParameters">URI parameters</param>
    /// <param name="bodyParam">Body Parameters</param>
    /// <param name="executeAsync"></param>
    /// <returns>Object of type T</returns>
    public async Task<T> ExecutePUTRequestAsync<T, U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam, bool executeAsync = false)
    {
      string uri = "";

      // Add request headers
      AddRequestHeaders(responseType, headerParameters);

      // Append path parameters in request uri.
      uri = AddPathParameters(requestUri, pathParameters);

      // Append query string parameters in request uri.
      uri = AddQueryStringParameters(uri, uriParameters);

      uri = ConcatenateUri(baseUri, uri);

      HttpContent content = CreateJsonObjectContent<U>(bodyParam);

      //// Executes PUT request synchornously.
      //HttpResponseMessage response = await HttpClientInstance.PutAsync(uri, content);

      HttpResponseMessage response;

      if (executeAsync)
      {
        // Executes PUT request asynchornously.
        response = await HttpClientInstance.PutAsync(uri, content);
      }
      else
      {
        // Executes PUT request synchornously.
        response = HttpClientInstance.PutAsync(uri, content).Result;
      }

      // Reads and returns result from response is status is 200.      
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return await response.Content.ReadAsAsync<T>();
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = await response.Content.ReadAsStringAsync();
        // throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      };
    }
    /// <summary>
    /// Executes the Put request without any return
    /// </summary>
    /// <typeparam name="U">Input object type.</typeparam>
    /// <param name="baseUri">Base URI of the calling API</param>
    /// <param name="requestUri">Specific Request URI</param>
    /// <param name="responseType">Response type , XML/Json</param>
    /// <param name="headerParameters">header paraments in keyvalue pair</param>
    /// <param name="pathParameters">Path parameters in Key value pair</param>
    /// <param name="uriParameters">URI parameters</param>
    /// <param name="bodyParam">Body Parameters</param>
    /// <returns>No Content</returns>
    public async Task ExecutePUTRequestAsync<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (!response.IsSuccessStatusCode)
      {
        string responseError = await response.Content.ReadAsStringAsync();
        //throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }
    /// <summary>
    /// Delete type Http call
    /// </summary>
    /// <typeparam name="U">Input object type.</typeparam>
    /// <param name="baseUri">Base URI of the calling API</param>
    /// <param name="requestUri">Specific Request URI</param>
    /// <param name="responseType">Response type , XML/Json</param>
    /// <param name="headerParameters">header paraments in keyvalue pair</param>
    /// <param name="pathParameters">Path parameters in Key value pair</param>
    /// <param name="uriParameters">URI parameters</param>
    /// <param name="bodyParam">Body Parameters</param>
    /// <returns></returns>
    public async Task ExecuteDELETERequestAsync<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (!response.IsSuccessStatusCode)
      {
        string responseError = await response.Content.ReadAsStringAsync();
        //throw new EwpHttpResponseException(string.Format("An error occured to execute DELETE {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception();
      }
    }
    /// <summary>
    /// Get request 
    /// </summary>
    /// <param name="baseUri">Base URI of the calling API</param>
    /// <param name="requestUri">Specific Request URI</param>
    /// <param name="responseType">Response type , XML/Json</param>
    /// <param name="headerParameters">header paraments in keyvalue pair</param>
    /// <param name="pathParameters">Path parameters in Key value pair</param>
    /// <param name="uriParameters">URI parameters</param>
    /// <returns>string</returns>
    public async Task<string> ExecuteGetRequestAsStringAsync(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return await response.Content.ReadAsStringAsync();
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = await response.Content.ReadAsStringAsync();
        //throw new EwpHttpResponseException(string.Format("An error occured to execute GET {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
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
    public async Task<string> ExecutePOSTRequestAndReturnAsStringAsync<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return await response.Content.ReadAsStringAsync();
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = await response.Content.ReadAsStringAsync();
        ;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute POST {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
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
    /// <returns>
    /// Returns response result in requested format.
    /// </returns>
    public async Task<string> ExecutePOSTRequestAndReturnAsStringAsync(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return await response.Content.ReadAsStringAsync();
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = await response.Content.ReadAsStringAsync();
        ;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute POST {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
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
    public async Task<string> ExecutePUTRequestAndReturnAsStringAsync<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return await response.Content.ReadAsStringAsync();
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = await response.Content.ReadAsStringAsync();
        //throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }
    /// <summary>
    /// HTTP Post request
    /// </summary>
    /// <typeparam name="T">Return Type of Post request</typeparam>
    /// <param name="baseUri">Base URI tobe called</param>
    /// <param name="requestUri">Specific call URI</param>
    /// <param name="responseType">Response Type -JSON or XML</param>
    /// <param name="headerParameters">Header parameters for the Post call</param>
    /// <param name="pathParameters">Path parameter array for the call</param>
    /// <param name="uriParameters">Query parameters for the call</param>
    /// <param name="fileName">Name of the file for file upload requests</param>
    /// <param name="fileData">Binary File Data for upload</param>
    /// <param name="contentType">HTTP Content Type</param>
    /// <returns>Object of type T</returns>

    public async Task<T> PostMultipartAsync<T>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, string fileName, byte[] fileData, string contentType)
    {
      using (var requestContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
      {
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
        if (response.IsSuccessStatusCode)
        {
          // Reads and returns result from response.       
          return await response.Content.ReadAsAsync<T>();
        }
        // Throw exception if response is other then 200.
        else
        {
          string responseError = await response.Content.ReadAsStringAsync();
          //throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
          throw new Exception(responseError);
        }


      }
    }

    /// <summary>
    /// Validates the request.
    /// </summary>
    /// <param name="request">The request object</param>
    /// <returns>bool value for validation</returns>
    /// <remarks>Exception if argument is null</remarks>
    public bool ValidateRequest(object request)
    {
      if (request == null)
      {
        //throw new EwpNullRequestArgumentException();
        throw new Exception();
      }
      return true;
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
    public T ExecuteGetRequest<T>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return response.Content.ReadAsAsync<T>().Result;
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute GET {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }
    /// <summary>
    /// Executes HTTP Post request
    /// </summary>
    /// <typeparam name="T">Return Type</typeparam>
    /// <typeparam name="U">Inpute object type</typeparam>
    /// <param name="baseUri">Base Url for HTTP call</param>
    /// <param name="requestUri">Method specific URL</param>
    /// <param name="responseType">Resposne type - JSON/XML</param>
    /// <param name="headerParameters">List of header parameters for HTTP call</param>
    /// <param name="pathParameters">List of Path parameters</param>
    /// <param name="uriParameters">List of QueryString parameters</param>
    /// <param name="bodyParam">Body param of type U</param>
    /// <returns>Object of type T</returns>
    public T ExecutePOSTRequest<T, U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return response.Content.ReadAsAsync<T>().Result;
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        // throw new EwpHttpResponseException(string.Format("An error occured to execute POST {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      };
    }
    /// <summary>
    /// Executes HTTP Post request
    /// </summary>
    /// <typeparam name="U">Input object type</typeparam>
    /// <param name="baseUri">Base Url for HTTP call</param>
    /// <param name="requestUri">Method specific URL</param>
    /// <param name="responseType">Resposne type - JSON/XML</param>
    /// <param name="headerParameters">List of header parameters for HTTP call</param>
    /// <param name="pathParameters">List of Path parameters</param>
    /// <param name="uriParameters">List of QueryString parameters</param>
    /// <param name="bodyParam">Body param of type U</param>
    /// <returns>void</returns>
    public void ExecutePOSTRequest<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (!response.IsSuccessStatusCode)
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        // throw new EwpHttpResponseException(string.Format("An error occured to execute POST {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }
    /// <summary>
    /// Executes HTTP Put request
    /// </summary>
    /// <typeparam name="T">Return Type</typeparam>
    /// <typeparam name="U">Inpute object type</typeparam>
    /// <param name="baseUri">Base Url for HTTP call</param>
    /// <param name="requestUri">Method specific URL</param>
    /// <param name="responseType">Resposne type - JSON/XML</param>
    /// <param name="headerParameters">List of header parameters for HTTP call</param>
    /// <param name="pathParameters">List of Path parameters</param>
    /// <param name="uriParameters">List of QueryString parameters</param>
    /// <param name="bodyParam">Body param of type U</param>
    /// <param name="executeAsync">Http call should be async or sync</param>
    /// <returns>Object of type T</returns>
    public T ExecutePUTRequest<T, U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam, bool executeAsync = false)
    {
      string uri = "";

      // Add request headers
      AddRequestHeaders(responseType, headerParameters);

      // Append path parameters in request uri.
      uri = AddPathParameters(requestUri, pathParameters);

      // Append query string parameters in request uri.
      uri = AddQueryStringParameters(uri, uriParameters);

      uri = ConcatenateUri(baseUri, uri);

      HttpContent content = CreateJsonObjectContent<U>(bodyParam);

      //// Executes PUT request synchornously.
      //HttpResponseMessage response = await HttpClientInstance.PutAsync(uri, content);

      HttpResponseMessage response = HttpClientInstance.PutAsync(uri, content).Result;

      // Reads and returns result from response is status is 200.      
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return response.Content.ReadAsAsync<T>().Result;
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        // throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      };
    }

    /// <summary>
    /// Executes HTTP Put request
    /// </summary>
    /// <typeparam name="U">Inpute object type</typeparam>
    /// <param name="baseUri">Base Url for HTTP call</param>
    /// <param name="requestUri">Method specific URL</param>
    /// <param name="responseType">Resposne type - JSON/XML</param>
    /// <param name="headerParameters">List of header parameters for HTTP call</param>
    /// <param name="pathParameters">List of Path parameters</param>
    /// <param name="uriParameters">List of QueryString parameters</param>
    /// <param name="bodyParam">Body param of type U</param>
    /// <returns>void</returns>
    public void ExecutePUTRequest<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (!response.IsSuccessStatusCode)
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }
    /// <summary>
    /// Executes HTTP Delete request
    /// </summary>
    /// <param name="baseUri">Base Url for HTTP call</param>
    /// <param name="requestUri">Method specific URL</param>
    /// <param name="responseType">Resposne type - JSON/XML</param>
    /// <param name="headerParameters">List of header parameters for HTTP call</param>
    /// <param name="pathParameters">List of Path parameters</param>
    /// <param name="uriParameters">List of QueryString parameters</param>
    /// <param name="bodyParam">Body param of type U</param>
    /// <returns>void</returns>
    public void ExecuteDELETERequest<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (!response.IsSuccessStatusCode)
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute DELETE {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }

    /// <summary>
    /// Executes HTTP Delete request
    /// </summary>
    /// <param name="baseUri">Base Url for HTTP call</param>
    /// <param name="requestUri">Method specific URL</param>
    /// <param name="responseType">Resposne type - JSON/XML</param>
    /// <param name="headerParameters">List of header parameters for HTTP call</param>
    /// <param name="pathParameters">List of Path parameters</param>
    /// <param name="uriParameters">List of QueryString parameters</param>
    /// <returns>void</returns>
    public void ExecuteDELETERequest(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters)
    {
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
      if (!response.IsSuccessStatusCode)
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute DELETE {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }
    /// <summary>
    /// Executes HTTP Get request
    /// </summary>
    /// <param name="baseUri">Base Url for HTTP call</param>
    /// <param name="requestUri">Method specific URL</param>
    /// <param name="responseType">Resposne type - JSON/XML</param>
    /// <param name="headerParameters">List of header parameters for HTTP call</param>
    /// <param name="pathParameters">List of Path parameters</param>
    /// <param name="uriParameters">List of QueryString parameters</param>
    /// <returns>json string</returns>
    public string ExecuteGetRequestAsString(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return response.Content.ReadAsStringAsync().Result;
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute GET {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
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
    public string ExecutePOSTRequestAndReturnAsString<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return response.Content.ReadAsStringAsync().Result;
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute POST {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
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
    public string ExecutePUTRequestAndReturnAsString<U>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, U bodyParam)
    {
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
      if (response.IsSuccessStatusCode)
      {
        // Reads and returns result from response.       
        return response.Content.ReadAsStringAsync().Result;
      }
      // Throw exception if response is other then 200.
      else
      {
        string responseError = response.Content.ReadAsStringAsync().Result;
        //throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
        throw new Exception(responseError);
      }
    }
    /// <summary>
    /// Executes Post request with file content
    /// </summary>
    /// <typeparam name="T">Output Type</typeparam>
    /// <param name="baseUri">Base URL of the Http call</param>
    /// <param name="requestUri">Methos specific URL of HTTP call</param>
    /// <param name="responseType">Response type - JSON/XML</param>
    /// <param name="headerParameters">List of Header parameters (key,Value) </param>
    /// <param name="pathParameters">List of Path Parameters</param>
    /// <param name="uriParameters">List of QueryString</param>
    /// <param name="fileName">File Name </param>
    /// <param name="fileData">File Data</param>
    /// <param name="contentType">HTTP Content type</param>
    /// <returns>Object of type T</returns>

    public T PostMultipart<T>(string baseUri, string requestUri, AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> uriParameters, string fileName, byte[] fileData, string contentType)
    {
      using (var requestContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
      {
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
        if (response.IsSuccessStatusCode)
        {
          // Reads and returns result from response.       
          return response.Content.ReadAsAsync<T>().Result;
        }
        // Throw exception if response is other then 200.
        else
        {
          string responseError = response.Content.ReadAsStringAsync().Result;
          //throw new EwpHttpResponseException(string.Format("An error occured to execute PUT {0} method.\r\n Error Details:{1}", response.RequestMessage.RequestUri, responseError), responseError, (int)response.StatusCode);
          throw new Exception(responseError);
        }


      }
    }

    #endregion

    #region Private Methods

    private HttpContent CreateJsonObjectContent<T>(T model)
    {
      MediaTypeFormatter mediaTypeFormatter = new JsonMediaTypeFormatter();
      MediaTypeHeaderValue mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(GetMediaTypeString(AcceptMediaType.JSON));

      HttpContent content = new ObjectContent<T>(model, mediaTypeFormatter, mediaTypeHeaderValue);
      return content;
    }

    private string AddRequestHeaders(AcceptMediaType responseType, List<KeyValuePair<string, string>> headerParameters)
    {
      string responseMediaType;
      if (headerParameters != null && headerParameters.Count > 0)
      {
        // Add Header Items in client.
        foreach (KeyValuePair<string, string> headerItem in headerParameters)
        {
          if (HttpClientInstance.DefaultRequestHeaders.Any(i => i.Key.ToLower().Equals(headerItem.Key.ToLower())) == false)
          {
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

    private string ConcatenateUri(string baseUri, string suffixUri)
    {
      Uri uriPartOne = new Uri(baseUri);
      Uri uriPartTwo = new Uri(uriPartOne, suffixUri);
      return uriPartTwo.AbsoluteUri;
    }

    //// Append path parameters and query string parameters in request uri.
    //// Remarks: This path parameter sequence should be match with request uri sequence.
    //private string AppendUriParameters(string requestUri, List<KeyValuePair<string, string>> pathParameters, List<KeyValuePair<string, string>> queryStringParameters)
    //{
    //  // Append path parameters in request uri.
    //  string updatedUri = AddPathParameters(requestUri, pathParameters);
    //  // Append query string parameters in request uri.
    //  updatedUri = AppendUriQueryStringParameters(updatedUri, queryStringParameters);
    //  return updatedUri;
    //}

    // Append given string parameters in request uri as a path parameters
    private string AddPathParameters(string requestUri, List<KeyValuePair<string, string>> pathParameters)
    {
      if (pathParameters != null && pathParameters.Count > 0)
      {
        for (int i = 0; i < pathParameters.Count; i++)
        {
          requestUri = requestUri.Replace("{" + pathParameters[i].Key + "}", pathParameters[i].Value);
        }
        return requestUri;
      }
      else
      {
        return requestUri;
      }
    }

    // Append query string parameters in request uri as name value pair.
    private static string AddQueryStringParameters(string requestUri, List<KeyValuePair<string, string>> queryStringParameters)
    {
      if (queryStringParameters != null && queryStringParameters.Count > 0)
      {
        return string.Concat(requestUri + "?", string.Join("&", queryStringParameters.Select(k => string.Format("{0}={1}", k.Key, k.Value))));
      }
      else
      {
        return requestUri;
      }
    }

    #endregion

    #region Protected Methods
    /// <summary>
    /// GEts the media type for HTTP call
    /// </summary>
    /// <param name="responseType">Http response type</param>
    /// <returns></returns>
    protected string GetMediaTypeString(AcceptMediaType responseType)
    {
      string responseMediaType;
      switch (responseType)
      {
        case AcceptMediaType.JSON:
          responseMediaType = "application/json";
          break;
        case AcceptMediaType.XML:
          responseMediaType = "application/xml";
          break;
        case AcceptMediaType.MultipartFormData:
          responseMediaType = "multipart/form-data";
          break;
        default:
          responseMediaType = "application/json";
          break;
      }

      return responseMediaType;
    }

    /// <summary>
    /// Adds default header fields like AccessToken
    /// </summary>
    protected virtual void AddDefaultHeaders()
    {
      HttpClientInstance.DefaultRequestHeaders.Add("AccessToken", Guid.NewGuid().ToString());
    }

    #endregion

    public async Task<string> GetBearerTokenAsync(string clientId, string authServiceUrl)
    {
      // Use this code to call connector with app to app communication
      // discover endpoints from metadata
      //var disco = await DiscoveryClient.GetAsync("https://localhost:44300");
      var disco = await DiscoveryClient.GetAsync(authServiceUrl);
      if (disco.IsError)
      {
        Console.WriteLine(disco.Error);
        return "";
      }

      // request token
      var tokenClient = new TokenClient(disco.TokenEndpoint, clientId, "secret");
      //string[] str = new string[] { "conn-vcapi", "authapi" ,"payapi"};
      var tokenResponse = await tokenClient.RequestClientCredentialsAsync("sapb1api payapi");

      if (tokenResponse.IsError)
      {
        Console.WriteLine(tokenResponse.Error);
        return "";
      }
      return tokenResponse.AccessToken.ToString();
    }


  }
}