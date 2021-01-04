using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;

namespace ewApps.AppMgmt.DS {

    //    1) (Clear Session) -- Method call -- 
    //2) (DeletedByAppUserAndAppId) -- Method call -- 
    //3) (DeletedByClientSessionId) -- Method call --
    //4) (AddSession) -- DS usesession.add()
    //5) (GetSession) -- DS usesession.get()
    //6) (UpdateSession) -- DS usesession.update()

    public class SessionMgmtDS:ISessionMgmtDS {

        IUserSessionManager _userSessionManager;
        ILogger<SessionMgmtDS> _loggerService;

        public SessionMgmtDS(IUserSessionManager userSessionManager, ILogger<SessionMgmtDS> loggerService) {
            _userSessionManager = userSessionManager;
            _loggerService = loggerService;
        }

        public async Task ClearSessionAsync(UserSession userSession) {
            try {
                _userSessionManager.ClearSession(userSession);
            }
            catch(Exception ex) {
                _loggerService.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task DeletedByAppUserAndAppIdAsync(Guid appUserId, Guid appId) {
            try {
                await _userSessionManager.DeletedByAppUserAndAppId(appUserId, appId);
            }
            catch(Exception ex) {
                _loggerService.LogError(ex, ex.Message);
                throw;
            }
        }


        public async Task DeletedByClientSessionIdAsync(Guid clientSessionId, string token) {
            try {
                await _userSessionManager.DeletedByClientSessionId(clientSessionId, token);
            }
            catch(Exception ex) {
                _loggerService.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<UserSession> AddUserSessionAsync(UserSession userSession) {
            try {
                return await _userSessionManager.AddUserSessionAsync(userSession);
            }
            catch(Exception ex) {
                _loggerService.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<UserSession> GetUserSessionAsync(Guid userSessionId) {
            try {
                return await _userSessionManager.GetUserSessionAsync(userSessionId);
            }
            catch(Exception ex) {
                _loggerService.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<UserSession> UpdateUserSessionAsync(UserSession userSession) {
            try {
                return await _userSessionManager.UpdateUserSessionAsync(userSession);
            }
            catch(Exception ex) {
                _loggerService.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task LogOut(string identityServerId) {
            try {
                await _userSessionManager.DeleteByIdentityServerId(identityServerId);
            }
            catch(Exception ex) {
                _loggerService.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}
