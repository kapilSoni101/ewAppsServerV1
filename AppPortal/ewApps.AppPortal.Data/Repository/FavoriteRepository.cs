using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// favorite
    /// </summary>
    public class FavoriteRepository:BaseRepository<Favorite, AppPortalDbContext>, IFavoriteRepository {

        #region Local member
        IUserSessionManager userSessionManager;
        #endregion Local member

        #region Constructor
        public FavoriteRepository(AppPortalDbContext appPortalDbContext, IUserSessionManager userSessionManager) : base(appPortalDbContext, userSessionManager) {
        }

        #endregion Consturctor

        #region Get favorite

        /// <summary>
        /// Get favorite menu item list by Appkey, tenantId, TenantUserId
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="portalkey"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<FavoriteViewDTO>> GetFavoriteMenuList(Guid appid, string portalkey, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {

            //string sql = @"SELECT ap.Favorite.MenuKey,ap.Favorite.Url,ap.Favorite.AppId,ap.Favorite.PortalKey,ap.Favorite.System
            //               FROM ap.Favorite 
            //               WHERE ap.Favorite.AppId = @appid AND ap.Favorite.PortalKey = @portalkey AND ap.Favorite.TenantId = @TenantId AND ap.Favorite.TenantUserId = @TenantUserId AND Deleted = 0";

            string sql = @"SELECT ap.Favorite.MenuKey,ap.Favorite.Url,ap.Favorite.AppId,ap.Favorite.PortalKey,ap.Favorite.System
                           FROM ap.Favorite
                           WHERE(ap.Favorite.AppId = @appid) AND (ap.Favorite.PortalKey = @portalkey) AND
                           (ap.Favorite.TenantId = @TenantId OR ap.Favorite.TenantId = '00000000-0000-0000-0000-000000000000') AND
                           (ap.Favorite.TenantUserId = @TenantUserId OR ap.Favorite.TenantUserId = '00000000-0000-0000-0000-000000000000') AND Deleted = 0";

            SqlParameter paramAppId = new SqlParameter("@appid", appid);
            SqlParameter paramPortalkey = new SqlParameter("@portalkey", portalkey);
            SqlParameter paramTenantId = new SqlParameter("@tenantId", tenantId);
            SqlParameter paramTenantUserId = new SqlParameter("@tenantUserId", tenantUserId);
            return await GetQueryEntityListAsync<FavoriteViewDTO>(sql, new SqlParameter[] { paramAppId, paramPortalkey, paramTenantId, paramTenantUserId }, token);
        }



        #endregion Get favorite

    }
}
