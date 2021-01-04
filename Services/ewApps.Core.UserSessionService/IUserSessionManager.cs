using System;
using System.Threading.Tasks;

namespace ewApps.Core.UserSessionService {

    public interface IUserSessionManager {

        UserSession GetSession();

        void SetSession(UserSessionOptions options);

        void ClearSession(UserSession entity);

        Task DeletedByAppUserAndAppId(Guid appUserId, Guid appId);

        Task DeletedByClientSessionId(Guid clientSessionId, string token);

        Task<UserSession> AddUserSessionAsync(UserSession entity);

        Task<UserSession> GetUserSessionAsync(Guid userSessionId);

        Task<UserSession> UpdateUserSessionAsync(UserSession entity);

        Task DeleteByIdentityServerId(string identityServerId);

    Task DeletedByUserIdAndTenantId(Guid userID, Guid tenantId);
    }
}
//1) (Clear Session) -- Method call --
//2) (DeletedByAppUserAndAppId) -- Method call --
//3) (DeletedByClientSessionId) -- Method call --
//4) (AddSession) -- DS usesession.add() -- 
//5) (GetSession) -- DS usesession.get() --
//6) (UpdateSession) -- DS usesession.update()