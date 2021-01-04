/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 31 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 31 Oct 2019
 */

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
    public class ViewSettingsRepository:BaseRepository<ViewSettings, AppPortalDbContext>, IViewSettingsRepository {

        #region Local Variable

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewSettingsRepository"/> class.
        /// </summary>
        public ViewSettingsRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion


        #region Get view settings

        
        public async Task<List<ViewSettingDTO>> GetViewSettingsListAsync(string screenkey, string appkey, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {

            string sql = @"SELECT vs.ID,vs.ViewName,vs.ScreenId,vs.IsDefault,vs.AppKey,vs.ViewSettingJson
                           FROM ap.ViewSettings vs
                           WHERE vs.ScreenId = @Screenkey AND vs.Appkey = @Appkey AND vs.TenantId = @TenantId AND vs.TenantUserId = @TenantUserId AND Deleted = 0";

            SqlParameter paramId = new SqlParameter("@Screenkey", screenkey);
            SqlParameter paramAppkey = new SqlParameter("@Appkey", appkey);
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramTenantUserId = new SqlParameter("@TenantUserId", tenantUserId);
            return await GetQueryEntityListAsync<ViewSettingDTO>(sql, new SqlParameter[] { paramId, paramAppkey, paramTenantId, paramTenantUserId }, token);
        }



        #endregion Get  view settings
    }
}
