﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna<skhanna@eworkplaceapps.com>
 * Date: 14 January 2019
 * 
 */

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.UserSessionService {

    /// <summary>
    /// Middleware extention to mange session
    /// </summary>
    public class UserSessionMiddleware {

        private readonly RequestDelegate _next;
        private readonly UserSessionOptions _options;
        private ILogger<UserSessionMiddleware> _loggerService;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserSessionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public UserSessionMiddleware(RequestDelegate next, UserSessionOptions option, ILogger<UserSessionMiddleware> loggerService) {
            _next = next;
            _options = option;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Invokes the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext) {
            IUserSessionManager userSessionManager = (IUserSessionManager)httpContext.RequestServices.GetService(typeof(IUserSessionManager));
            try {
                //_loggingService.LogInfo("UserSessionMiddleware-Start");
                //_loggingService.LogInfo("Url-" + httpContext.Request.Path);
                // Session will be set whenever middleware is called in the requestpipeline, if same sessionid is passed the session will be taken from DB        

                userSessionManager.SetSession(_options);

                httpContext.Response.OnStarting((state) => {
                    UserSession session = userSessionManager.GetSession();
                    if(session != null) {
                        httpContext.Response.Headers.Add("TenantName", session.TenantName);
                        httpContext.Response.Headers.Add("TenantId", session.TenantId.ToString());
                        httpContext.Response.Headers.Add("UserId", session.TenantUserId.ToString());
                        httpContext.Response.Headers.Add("UserName", session.UserName);
                    }
                    return Task.CompletedTask;
                }, null);


                await _next(httpContext);
                //  AddUserSessionInfomationInResponse(userSessionManager, httpContext);
                //_loggingService.LogInfo("UserSessionMiddleware-End");
            }
            catch(System.Exception ex) {
                AddUserSessionInfomation(userSessionManager, ex);
                _loggerService.LogError(ex, ex.Message);
                throw;
            }
        }

        private static void AddUserSessionInfomation(IUserSessionManager userSessionManager, System.Exception ex) {
            UserSession session = userSessionManager.GetSession();
            if(session != null) {
                if(ex.GetBaseException().Data.Contains("TenantName") == false) {
                    ex.GetBaseException().Data.Add("TenantName", session.TenantName);
                }
                if(ex.GetBaseException().Data.Contains("TenantId") == false) {
                    ex.GetBaseException().Data.Add("TenantId", session.TenantId);
                }
                if(ex.GetBaseException().Data.Contains("UserId") == false) {
                    ex.GetBaseException().Data.Add("UserId", session.TenantUserId);
                }
                if(ex.GetBaseException().Data.Contains("UserName") == false) {
                    ex.GetBaseException().Data.Add("UserName", session.UserName);
                }
            }
            //return Task.CompletedTask;
        }

        //private Task AddUserSessionInfomationInResponse(IUserSessionManager userSessionManager) {
        //    UserSession session = userSessionManager.GetSession();
        //    if(session != null) {
        //        httpContext.Response.Headers.Add("TenantName", session.TenantName);
        //        httpContext.Response.Headers.Add("TenantId", session.TenantId.ToString());
        //        httpContext.Response.Headers.Add("UserId", session.AppUserId.ToString());
        //        httpContext.Response.Headers.Add("UserName", session.UserName);
        //    }
        //}
    }
}
