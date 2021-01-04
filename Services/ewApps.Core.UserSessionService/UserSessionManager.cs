using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ewApps.Core.ExceptionService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.UserSessionService {

    public class UserSessionManager:IUserSessionManager {

        private string[] _exceptionalSwaggerUrlSkippedInHeaderValueValidation = { "/favicon.ico", "/favicon-16x16.png", "/favicon-32x32.png", "/swagger-ui-standalone-preset.js", "/swagger-ui-bundle.js", "/swagger-ui-standalone-preset.js", "/swagger/v1/swagger.json", "/index.html", "/", "/swagger-ui.css" };
        private string[] _exceptionalApiUrl = {     "/api/businesssetting/list", "/api/publishersetting/list", "/api/authenticate/tokeninfo/subdomain", "/api/authenticate/tokeninfo",
                                                    "api/businessappuser/subdomain", "/api/businessappuser/tokeninfo", "/api/tokeninfo/check/token","/api/customer/customerlist/add", "/api/customer/customerlist/update",
                                                    "/api/customer/customerlist/delete", "/api/business/addbusiness", "/api/quickpayment/invoice/payment", "/api/quickpayment/quickpaydetail",
                                                    "businesssetting/list", "/api/payment/paymentdetail", "/api/payment/invoicelist","/api/webhook/add/subscription","/api/webhook/receiveevents",
                                                    "/webhook/receiveevents", "/api/webhook/servershutdown","/api/recurring/schedulePay", "/api/appuser/tokeninfochangepass",
                                                    "/api/recurring/schedulepay", "/api/businesssetting/checkappstatus/","/paymenthub", "/paymenthub/negotiate" ,"/api/shipmenttenant/validatesubdomain",
                                                    "/api/shipmenttenant/tokeninfo","/api/shipmenttenant/checkappstatus/","/api/busauthenticate/tokeninfo","/api/busauthenticate/validatesubdomain",
                                                    "/api/pubauthenticate/validatesubdomain","/api/pubauthenticate/tokeninfo", "/api/paymenttenant/validatesubdomain","/api/paymenttenant/tokeninfo","/api/customertenant/validatesubdomain","/api/customertenant/tokeninfo","/api/tenant/gettenantlinkinginfo" };



        private string[] _sessionApiUrl = { "/api/platformusersession/set", "/api/publisherusersession/set", "/api/businessusersession/set" , "/api/custusersession/set", "/api/vendusersession/set" };

        private readonly HttpContext _httpContext;
        private readonly UserSessionDBContext _userSessionDbContext;
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
        public UserSessionManager(IHttpContextAccessor accessor, UserSessionDBContext userSessionDbContext, ILogger<UserSessionManager> loggerSerivce, IOptions<UserSessionAppSettings> appSetting) {
            _httpContext = accessor.HttpContext;
            _userSessionDbContext = userSessionDbContext;
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


        /// <summary>
        /// Method responsible to gather information for user
        /// </summary>
        /// <param name="lightSession">if set to <c>true</c> [light session].</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="EwpInvalidLoginTokenException"></exception>
        public void SetSession(UserSessionOptions options) {
            // _loggingService.LogInfo("UserSessionManager.SetSession-" + _initTime.ToString() + " " + "Url-" + _httpContext.Request.Path.Value.ToString());

            try {
                // IF current api is anonymous, return;
                if(_httpContext.Request.Path != null && AnonymousApi(_httpContext.Request.Path.Value.ToLower())) {
                    return;
                }

                // If current api is non-anonymous, header should contains client session id.
                else if(_httpContext.Request.Headers.ContainsKey(_clientSessionIdKey) && SessionExists()) {
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
                    else if(SessionExists() == false) {
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

                        // Save the object tothe database		
                        _userSessionDbContext.Add(_userSession);
                        _userSessionDbContext.SaveChanges();
                    }
                    return;
                }
                else {
                    StringBuilder errorMessageBuilder = new StringBuilder();
                    errorMessageBuilder.AppendLine("UserSessionManager.SetSession-Invalid request.");
                    errorMessageBuilder.AppendLine("UserSessionManager.SetSession-Client Session Id-" + _httpContext.Request.Headers[_clientSessionIdKey]);
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

        /// <summary>
        /// Method responsible to user information from session 
        /// </summary>
        /// <param name="entity"></param>
        public void ClearSession(UserSession entity) {
            try {
                _userSessionDbContext.Set<UserSession>().Remove(entity);
                _userSessionDbContext.SaveChanges();
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

        private bool SessionExists() {
            bool exist = false;
            try {
                Guid clientsessionId = GetClientSessionId();
                _userSession = _userSessionDbContext.GetSession(clientsessionId);
                if(_userSession != null) {
                    string currentIdentityToken = GetAuthorization();
                    // If Token is diffrent then update Session
                    //if(_userSession.IdentityToken != currentIdentityToken) {
                    //    _userSession.IdentityToken = currentIdentityToken;
                    //    _userSessionDbContext.Update(_userSession);
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
            _userSessionDbContext.Add(entity);
            _userSessionDbContext.SaveChanges();
            return entity;
        }

        public async Task<UserSession> GetUserSessionAsync(Guid userSessionId) {
            return _userSessionDbContext.GetSession(userSessionId);
        }

        public async Task<UserSession> UpdateUserSessionAsync(UserSession entity) {
            _userSessionDbContext.Update(entity);
            _userSessionDbContext.SaveChanges();
            return entity;
        }

        public async Task DeleteByIdentityServerId(string identityServerId) {
            List<UserSession> userSessionList = _userSessionDbContext.GetUserSessionByIdentityServerId(identityServerId);
            if(userSessionList != null) {
                _userSessionDbContext.Set<UserSession>().RemoveRange(userSessionList);
                _userSessionDbContext.SaveChanges();
            }
        }

        #region Ishwar

        public async Task DeletedByAppUserAndAppId(Guid appUserId, Guid appId) {
            try {
                List<UserSession> userSessions = await _userSessionDbContext.UserSessions.Where(s => s.TenantUserId == appUserId && s.AppId == appId).ToListAsync<UserSession>();
                if(userSessions != null && userSessions.Count > 0) {
                    _userSessionDbContext.Set<UserSession>().RemoveRange(userSessions);
                    _userSessionDbContext.SaveChanges();
                }
            }
            catch(Exception ex) {
                _loggerSerivce.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task DeletedByClientSessionId(Guid clientSessionId, string token) {
            if(!string.IsNullOrEmpty(token)) {
                List<UserSession> us = await _userSessionDbContext.UserSessions.Where(s => s.IdentityServerId == token).ToListAsync();
                if(us != null && us.Count > 0) {
                    foreach(UserSession item in us) {
                        _userSessionDbContext.Set<UserSession>().Remove(item);
                        _userSessionDbContext.SaveChanges();
                    }
                }
            }
            else {
                UserSession userSession = await _userSessionDbContext.UserSessions.Where(s => s.ID == clientSessionId).FirstOrDefaultAsync<UserSession>();
                if(userSession != null) {
                    _userSessionDbContext.Set<UserSession>().Remove(userSession);
                    _userSessionDbContext.SaveChanges();
                }
            }
        }

    public async Task DeletedByUserIdAndTenantId(Guid userID, Guid tenantId) {       
       
      List<UserSession> us = await _userSessionDbContext.UserSessions.Where(s => s.TenantUserId == userID && s.TenantId == tenantId).ToListAsync();
      if(us != null && us.Count > 0) {
        foreach(UserSession item in us) {
          _userSessionDbContext.Set<UserSession>().Remove(item);
          _userSessionDbContext.SaveChanges();
        }
      }

    }

    #endregion Ishwar

  }
}
