using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {
   public class TenantAppLinkingRepository:BaseRepository<TenantAppLinking, AppPortalDbContext>, ITenantAppLinkingRepository {

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenInfoRepository"/> class.
        /// </summary>
        public TenantAppLinkingRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
    }
}
