using System.Collections.Generic;

namespace ewApps.Core.ServiceProcessor {
    /// <summary>
    /// This class contains request options to execute service.
    /// </summary>
    public class RequestOptions {

        #region Discussion Notes
        //       protocol: REST, MessageBroker, ...
        //reqtype: GEt/Post/Put
        //headers: key-value pairs, include ObjNameAbbv
        //method: Use this to create HTTP Url from ServiceBaseUrl + method
        //methodData: JSON - PK, ... 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestOptions"/> class and it's member.
        /// </summary>
        public RequestOptions() {
            HeaderParameters = new List<KeyValuePair<string, string>>();
            PathParameters = new List<KeyValuePair<string, string>>();
            UriParamerters = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// The Service request protocol type.
        /// </summary>
        /// <seealso cref="ServiceProtocolTypeEnum"/>
        public ServiceProtocolTypeEnum ProtocolType {
            get; set;
        }

        /// <summary>
        /// The service request type.
        /// </summary>
        /// <seealso cref="RequestTypeEnum"/>
        public RequestTypeEnum ServiceRequestType {
            get; set;
        }

        /// <summary>
        /// The response accept type.
        /// </summary>
        /// <seealso cref="AcceptMediaTypeEnum"/>
        public AcceptMediaTypeEnum AcceptType {
            get; set;
        }

        /// <summary>
        /// The list header parameters.
        /// </summary>
        public List<KeyValuePair<string, string>> HeaderParameters {
            get; set;
        }

        /// <summary>
        /// The list of path parameters.
        /// </summary>
        public List<KeyValuePair<string, string>> PathParameters {
            get; set;
        }

        /// <summary>
        /// The list of uri parameters.
        /// </summary>
        public List<KeyValuePair<string, string>> UriParamerters {
            get; set;
        }

        /// <summary>
        /// The service method name.
        /// </summary>
        public string Method {
            get; set;
        }

        /// <summary>
        /// The method data.
        /// </summary>
        public object MethodData {
            get; set;
        }

        /// <summary>
        /// The information to generate Bearer token.
        /// </summary>
        /// <remarks>If null bearer token will not generate.</remarks>
        public BearerTokenOption BearerTokenInfo {
            get; set;
        }

        public static RequestOptions CreateInstance(ServiceProtocolTypeEnum protocol, RequestTypeEnum requestType, AcceptMediaTypeEnum acceptType, string method, object methodData, string clientName = "", string authServerUrl = "", List<KeyValuePair<string, string>> headerParams = null, List<KeyValuePair<string, string>> pathParams = null, List<KeyValuePair<string, string>> uriParams = null) {
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = acceptType;
            requestOptions.BearerTokenInfo = new BearerTokenOption();
            requestOptions.BearerTokenInfo.AppClientName = clientName;
            requestOptions.BearerTokenInfo.AuthServiceUrl = authServerUrl;
            if(headerParams != null) {
                requestOptions.HeaderParameters = headerParams;
            }
            requestOptions.Method = method;
            requestOptions.MethodData = methodData;
            if(pathParams != null) {
                requestOptions.PathParameters = pathParams;
            }

            requestOptions.ProtocolType = protocol;
            requestOptions.ServiceRequestType = requestType;
            // Request params
            if(uriParams != null) {
                requestOptions.UriParamerters = uriParams;
            }
            return requestOptions;
        }

    }
}