using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.UserSessionService {
    public class RemoteUserSessionManager:IUserSessionManager {

        private string[] _exceptionalSwaggerUrlSkippedInHeaderValueValidation = { "/favicon.ico", "/favicon-16x16.png", "/favicon-32x32.png", "/swagger-ui-standalone-preset.js", "/swagger-ui-bundle.js", "/swagger-ui-standalone-preset.js", "/swagger/v1/swagger.json", "/index.html", "/", "/swagger-ui.css" };
        private string[] _exceptionalApiUrl = {     "/api/businesssetting/list", "/api/publishersetting/list", "/api/authenticate/tokeninfo/subdomain", "/api/authenticate/tokeninfo",
                                                    "api/businessappuser/subdomain", "/api/businessappuser/tokeninfo", "/api/tokeninfo/check/token","/api/customer/customerlist/add", "/api/customer/customerlist/update",
                                                    "/api/customer/customerlist/delete", "/api/business/addbusiness", "/api/quickpayment/invoice/payment", "/api/quickpayment/quickpaydetail",
                                                    "businesssetting/list", "/api/payment/paymentdetail", "/api/payment/invoicelist","/api/webhook/add/subscription","/api/webhook/receiveevents",
                                                    "/webhook/receiveevents", "/api/webhook/servershutdown","/api/recurring/schedulePay", "/api/appuser/tokeninfochangepass",
                                                    "/api/recurring/schedulepay", "/api/businesssetting/checkappstatus/","/paymenthub", "/paymenthub/negotiate" ,"/api/shipmenttenant/validatesubdomain",
                                                    "/api/shipmenttenant/tokeninfo","/api/shipmenttenant/checkappstatus/","/api/busauthenticate/tokeninfo","/api/busauthenticate/validatesubdomain",
                                                    "/api/pubauthenticate/validatesubdomain","/api/pubauthenticate/tokeninfo", "/api/paymenttenant/validatesubdomain","/api/paymenttenant/tokeninfo","/api/customertenant/validatesubdomain","/api/customertenant/tokeninfo" };

        private string[] _sessionApiUrl = { "/api/platformusersession/set", "/api/publisherusersession/set", "/api/businessusersession/set", "/api/custusersession/set" };

        private readonly HttpContext _httpContext;
        // private readonly UserSessionDBContext _userSessionDbContext;
        private readonly RequestDelegate _next;
        private readonly string _clientSessionIdKey = "clientsessionid";
        private readonly string _appUserIdKey = "appuserid";
        private readonly string _appTenantIdKey = "tenantid";
        private readonly string _appIdKey = "appid";
        private readonly string _userType = "usertype";
        private readonly string _authorizationKey = "Authorization";
        private readonly string _subdomainKey = "subdomain";
        private readonly string _tenantNameKey = "tenantName";
        private readonly string _userNameKey = "userName";
        private readonly string _identityServerIdKey = "identityServerId";

        private UserSession _userSession;

        private DateTime _initTime;
        private ILogger<UserSessionManager> _loggerSerivce;
        UserSessionAppSettings _appSetting;

        // Constructor
        public RemoteUserSessionManager(IHttpContextAccessor accessor, ILogger<UserSessionManager> loggerSerivce, IOptions<UserSessionAppSettings> appSetting) {
            _httpContext = accessor.HttpContext;
            // _userSessionDbContext = userSessionDbContext;
            _initTime = DateTime.UtcNow;
            _loggerSerivce = loggerSerivce;
            _appSetting = appSetting.Value;
            if(_appSetting.AnonymousUrls != null && _appSetting.AnonymousUrls.Length > 0)
                _exceptionalApiUrl = _appSetting.AnonymousUrls.Union(_exceptionalApiUrl).ToArray();
        }

        /// <summary>
        /// Method responsible to information for user
        /// </summary>
        /// <returns></returns>
        public UserSession GetSession() {
            return _userSession;
        }


        public void ClearSession(UserSession entity) {
            try {

                // TODO: Service call to app managment (Clear Session)

                string baseuri = _appSetting.AppManagmentURL;
                string requesturl = "usersession/clearsession";

                // HttpClient client = new HttpClient();
                //HttpRequestProcessor httpRequestProcessor = new HttpRequestProcessor(client);
                //List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
                //headers.Add(new KeyValuePair<string, string>("clientsessionid", entity.ID.ToString()));
                //httpRequestProcessor.ExecutePUTRequest(baseuri, requesturl, AcceptMediaType.JSON, headers, null, null, entity);

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.HeaderParameters = new List<KeyValuePair<string, string>>();
                requestOptions.Method = requesturl;
                requestOptions.MethodData = entity;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Put;

                ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
                httpRequestProcessor.Execute<bool>(requestOptions, false);
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task DeletedByAppUserAndAppId(Guid appUserId, Guid appId) {
            try {
                // TODO: Service call to app managment (DeletedByAppUserAndAppId)
                string baseuri = _appSetting.AppManagmentURL;
                string requesturl = "usersession/deletesession/" + appUserId.ToString() + "/" + appId.ToString();

                //HttpClient client = new HttpClient();
                //HttpRequestProcessor httpRequestProcessor = new HttpRequestProcessor(client);
                //List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
                //// headers.Add(new KeyValuePair<string, string>("clientsessionid", entity.ID.ToString()));
                //httpRequestProcessor.ExecutePUTRequest<string, string>(baseuri, requesturl, AcceptMediaType.JSON, headers, null, null, null);

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = requesturl;
                requestOptions.MethodData = null;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Delete;

                ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
                httpRequestProcessor.Execute<string>(requestOptions, false);
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task DeletedByClientSessionId(Guid clientSessionId, string token) {
            try {
                // TODO: Service call to app managment (DeletedByClientSessionId)
                string baseuri = _appSetting.AppManagmentURL;
                string requesturl = "usersession/deletesessionby/" + clientSessionId.ToString() + "/" + token;

                //HttpClient client = new HttpClient();
                //HttpRequestProcessor httpRequestProcessor = new HttpRequestProcessor(client);
                //List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
                //// headers.Add(new KeyValuePair<string, string>("clientsessionid", entity.ID.ToString()));
                //httpRequestProcessor.ExecutePUTRequest<string, string>(baseuri, requesturl, AcceptMediaType.JSON, headers, null, null, null);

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = requesturl;
                requestOptions.MethodData = null;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Delete;

                ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
                httpRequestProcessor.Execute<string>(requestOptions, false);

            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        public void SetSession(UserSessionOptions options) {
            try {
                // IF current api is anonymous, return;
                if(_httpContext.Request.Path != null && AnonymousApi(_httpContext.Request.Path.Value.ToLower())) {
                    return;
                }

                // If current api is non-anonymous, header should contains client session id.
                else if(_httpContext.Request.Headers.ContainsKey(_clientSessionIdKey) && SessionExist()) {
                    return;
                }
                // Check if current request is to set light session
                else if(options.LightSession) {
                    Guid tenantId = GetTenantId();
                    Guid userId = GetAppUserId();
                    Guid appId = GetAppId();
                    _userSession = new UserSession() {
                        ID = Guid.NewGuid(),
                        TenantUserId = userId,
                        TenantId = tenantId,
                        UserName = "",
                        TenantName = "",
                        AppId = appId,
                        IdentityToken = "",
                        UserType = 0,
                        IdentityServerId = "",
                    };
                    return;
                }
                // Check if current request is to set server session
                else if(SessionApi(_httpContext.Request.Path.Value.ToLower())) {

                    // If client session id is missing in request header, raise exception.
                    if(_httpContext.Request.Headers.ContainsKey(_clientSessionIdKey) == false) {
                        // Raise exception.
                        StringBuilder errorMessageBuilder = new StringBuilder();
                        errorMessageBuilder.AppendLine("UserSessionManager.SetSession-Header doesn't contains ClientSesionId.");
                        errorMessageBuilder.AppendLine("UserSessionManager.SetSession-Url:" + _httpContext.Request.Path.Value);
                        throw new Exception(errorMessageBuilder.ToString());
                    }
                    // If session is already exist with same client session id, don't add any new session.
                    else if(SessionExist() == false) {
                        Guid tenantId = GetTenantId();
                        Guid userId = GetAppUserId();
                        Guid appId = GetAppId();
                        int userType = GetUserType();
                        string authorization = GetAuthorization();
                        string subDoamin = GetSubdomain();
                        string tenantName = GetTenantName();
                        string userName = GetUserName();
                        string identityServerId = GetIdentityServerId();

                        _userSession = new UserSession() {
                            ID = GetClientSessionId(),
                            TenantUserId = userId,
                            TenantId = tenantId,
                            UserName = userName,
                            TenantName = tenantName,
                            AppId = appId,
                            IdentityToken = authorization,
                            UserType = userType,
                            Subdomain = subDoamin,
                            IdentityServerId = identityServerId,
                        };

                        //   _loggingService.LogInfo("SetSession-" + "-" + GetAbsoluteUri() + "-" + _userSession.ID.ToString());

                        // TODO: add user session API (AddSession)
                        string baseuri = _appSetting.AppManagmentURL;
                        string requesturl = "usersession/add";

                        //   HttpClient client = new HttpClient();
                        //HttpRequestProcessor httpRequestProcessor = new HttpRequestProcessor(client);
                        //List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
                        //// headers.Add(new KeyValuePair<string, string>("clientsessionid", entity.ID.ToString()));
                        //httpRequestProcessor.ExecutePOSTRequest<UserSession, UserSession>(baseuri, requesturl, AcceptMediaType.JSON, headers, null, null, _userSession);

                        RequestOptions requestOptions = new RequestOptions();
                        requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                        requestOptions.Method = requesturl;
                        requestOptions.MethodData = _userSession;
                        requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                        requestOptions.ServiceRequestType = RequestTypeEnum.Post;

                        ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
                        httpRequestProcessor.Execute<UserSession>(requestOptions, false);
                    }
                    return;
                }
                else {
                    StringBuilder errorMessageBuilder = new StringBuilder();
                    errorMessageBuilder.AppendLine("UserSessionManager.SetSession-Invalid request.");
                    errorMessageBuilder.AppendLine("UserSessionManager.SetSession-Client Session Id-" + _clientSessionIdKey);
                    errorMessageBuilder.AppendLine("UserSessionManager.SetSession-Url:" + _httpContext.Request.Path.Value);
                    //System.Diagnostics.Debug.WriteLine(errorMessageBuilder.ToString());
                    throw new EwpInvalidLoginTokenException(errorMessageBuilder.ToString());
                    //throw new Exception(errorMessage);
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private Guid GetClientSessionId() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_clientSessionIdKey)) {
                    return Guid.Parse(_httpContext.Request.Headers[_clientSessionIdKey]);
                }
                else {
                    return Guid.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private Guid GetAppId() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_appIdKey)) {
                    Guid appId = new Guid();
                    if(Guid.TryParse(_httpContext.Request.Headers[_appIdKey], out appId)) {
                        return appId;
                    }
                    else {
                        return Guid.Empty;
                    }
                }
                else {
                    return Guid.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private Guid GetAppUserId() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_appUserIdKey)) {
                    Guid userId = new Guid();
                    if(Guid.TryParse(_httpContext.Request.Headers[_appUserIdKey], out userId)) {
                        return userId;
                    }
                    else {
                        return Guid.Empty;
                    }
                }
                else {
                    return Guid.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private Guid GetTenantId() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_appTenantIdKey)) {
                    Guid tenantId = new Guid();
                    if(Guid.TryParse(_httpContext.Request.Headers[_appTenantIdKey], out tenantId)) {
                        return tenantId;
                    }
                    else {
                        return Guid.Empty;
                    }
                }
                else {
                    return Guid.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private int GetUserType() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_userType)) {
                    int userType;
                    if(int.TryParse(_httpContext.Request.Headers[_userType], out userType)) {
                        return userType;
                    }
                    else {
                        return 0;
                    }
                }
                else {
                    return 0;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private string GetAuthorization() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_authorizationKey)) {
                    string authorization = Convert.ToString(_httpContext.Request.Headers[_authorizationKey]);
                    return authorization;
                }
                else {
                    return string.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private string GetSubdomain() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_subdomainKey)) {
                    string subdomain = Convert.ToString(_httpContext.Request.Headers[_subdomainKey]);
                    return subdomain;
                }
                else {
                    return string.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private string GetTenantName() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_tenantNameKey)) {
                    string tenantName = Convert.ToString(_httpContext.Request.Headers[_tenantNameKey]);
                    return tenantName;
                }
                else {
                    return string.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private string GetUserName() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_userNameKey)) {
                    string userName = Convert.ToString(_httpContext.Request.Headers[_userNameKey]);
                    return userName;
                }
                else {
                    return string.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private string GetIdentityServerId() {
            try {
                if(_httpContext.Request.Headers.ContainsKey(_identityServerIdKey)) {
                    string identityServerId = Convert.ToString(_httpContext.Request.Headers[_identityServerIdKey]);
                    return identityServerId;
                }
                else {
                    return string.Empty;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        private bool SessionExist() {
            bool exist = false;
            try {
                Guid clientsessionId = GetClientSessionId();
                // TODO: GetSession api (GetSession)
                //_userSession = _userSessionDbContext.GetSession(clientsessionId);

                string baseuri = _appSetting.AppManagmentURL;
                string requesturl = "usersession/get/" + clientsessionId.ToString();

                //HttpClient client = new HttpClient();
                //HttpRequestProcessor httpRequestProcessor = new HttpRequestProcessor(client);
                //List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
                //headers.Add(new KeyValuePair<string, string>("clientsessionid", clientsessionId.ToString()));
                //_userSession = httpRequestProcessor.ExecuteGetRequest<UserSession>(baseuri, requesturl, AcceptMediaType.JSON, headers, null, null);

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = requesturl;
                requestOptions.MethodData = null;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Get;

                ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
                _userSession = httpRequestProcessor.Execute<UserSession>(requestOptions, false);

                if(_userSession != null) {
                    //string currentIdentityToken = GetAuthorization();
                    //// If Token is diffrent then update Session
                    //if(_userSession.IdentityToken != currentIdentityToken) {
                    //    _userSession.IdentityToken = currentIdentityToken;
                    //    // TODO: Update user session api (UpdateSession)

                    //    requesturl = "usersession/update";
                    //    //httpRequestProcessor.ExecutePUTRequest<UserSession, UserSession>(baseuri, requesturl, AcceptMediaType.JSON, headers, null, null, _userSession);

                    //    requestOptions = new RequestOptions();
                    //    requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                    //    requestOptions.Method = requesturl;
                    //    requestOptions.MethodData = _userSession;
                    //    requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                    //    requestOptions.ServiceRequestType = RequestTypeEnum.Put;
                    //    httpRequestProcessor.Execute<UserSession>(requestOptions, false);
                    //}
                    exist = true;
                }
                else {
                    exist = false;
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
            // _loggingService.LogInfo("SesssionExists-" + "-" + GetAbsoluteUri() + "-" + clientsessionId.ToString() + "-" + exist);
            return exist;
        }

        private Uri GetAbsoluteUri() {
            var request = _httpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }

        private bool AnonymousApi(string apiUrl) {
            bool anonymous = false;
            anonymous = _exceptionalSwaggerUrlSkippedInHeaderValueValidation.Contains(apiUrl.ToLower());

            if(anonymous == false && _exceptionalApiUrl.Any(i => apiUrl.ToLower().StartsWith(i))) {
                anonymous = true;
            }
            return anonymous;
        }

        private bool SessionApi(string apiUrl) {
            bool sessionUrl = false;
            // _loggingService.LogInfo(apiUrl);
            if(_sessionApiUrl.Any(i => apiUrl.ToLower().StartsWith(i))) {
                sessionUrl = true;
            }
            // _loggingService.LogInfo(" session url:" + sessionUrl);
            return sessionUrl;
        }

        public async Task<UserSession> AddUserSessionAsync(UserSession entity) {
            throw new NotImplementedException();
        }

        public async Task<UserSession> GetUserSessionAsync(Guid userSessionId) {
            throw new NotImplementedException();
        }

        public async Task<UserSession> UpdateUserSessionAsync(UserSession entity) {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdentityServerId(string identityServerId) {
            throw new NotImplementedException();
        }
    public async Task DeletedByUserIdAndTenantId(Guid userID, Guid tenantId) {
      throw new NotImplementedException();
    }
    }
}
