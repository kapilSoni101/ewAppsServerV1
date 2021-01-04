using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {
    public class PlatformRepository:BaseRepository<Platform , AppPortalDbContext> ,IPlatformRepository {

        #region Local member
        #endregion


        #region Consttructor

        /// <summary>
        /// 
        /// </summary>
        public PlatformRepository(AppPortalDbContext appPortalDbContext, IUserSessionManager userSessionManager) : base(appPortalDbContext, userSessionManager) {
        }

        #endregion


        /// <summary>
        /// Get platform branding
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PlatformBrandingDTO> GetPlatformBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            //ToDo: Nitin: Review: spacing b/w code line is not proper.
            string query = @"SELECT p.ID,p.TenantId,p.Name,p.PoweredBy,p.Copyright,ts.ThemeId,t.ThemeKey FROM Platform p 
             INNER JOIN TenantSubscription ts ON ts.TenantId = p.TenantId and ts.AppId = @AppId
             INNER JOIN Theme t ON t.ID = ts.ThemeId
             WHERE p.TenantId = @TenantId AND ts.AppId = @AppId";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramAppId = new SqlParameter("@AppId", appId);

            // return await GetQueryEntityAsync<PlatformBrandingDQ>(query, new SqlParameter[] { paramTenantId, paramAppId }, token);
            return new PlatformBrandingDTO();

        }


       


    }

}
