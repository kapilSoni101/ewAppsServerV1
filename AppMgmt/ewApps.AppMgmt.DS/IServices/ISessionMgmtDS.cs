using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {

    //    1) (Clear Session) -- Method call
    //2) (DeletedByAppUserAndAppId) -- Method call
    //3) (DeletedByClientSessionId) -- Method call
    //4) (AddSession) -- DS usesession.add()
    //5) (GetSession) -- DS usesession.get()
    //6) (UpdateSession) -- DS usesession.update()

    public interface ISessionMgmtDS {

        /// <summary>
        /// Clear session that is delete the session.
        /// </summary>
        /// <param name="userSessionId">usersession unique identifier.</param>
        /// <returns></returns>
        Task ClearSessionAsync(UserSession userSession);

        Task DeletedByAppUserAndAppIdAsync(Guid appUserId, Guid appId);

        Task DeletedByClientSessionIdAsync(Guid clientSessionId, string token);

        Task<UserSession> AddUserSessionAsync(UserSession userSession);

        Task<UserSession> GetUserSessionAsync(Guid userSessionId);

        Task<UserSession> UpdateUserSessionAsync(UserSession userSession);

        Task LogOut(string identityServerId);
    }
}
