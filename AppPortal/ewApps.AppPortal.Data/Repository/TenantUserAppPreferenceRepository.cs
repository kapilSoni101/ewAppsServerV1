using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {
    public class TenantUserAppPreferenceRepository:BaseRepository<TenantUserAppPreference, AppPortalDbContext> , ITenantUserAppPreferenceRepository{

        public TenantUserAppPreferenceRepository(AppPortalDbContext appPortalDbContext, IUserSessionManager userSessionManager) : base(appPortalDbContext, userSessionManager) {

        }
    }
}
