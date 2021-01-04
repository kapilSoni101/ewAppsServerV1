/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Entity;
using ewApps.AppMgmt.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using Microsoft.EntityFrameworkCore;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// Provide add/update/delete business tenant methods.
    /// Provide get methods to get short information as DTO by mapping related entities.
    /// </summary>
    public class TenantSubscriptionRepository:BaseRepository<TenantSubscription, AppMgmtDbContext>, ITenantSubscriptionRepository {

        #region Constructor
        public TenantSubscriptionRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion

        #region Get

        /// <summary>
        /// Get primary user of a application by tenantid and appid.
        /// </summary>
        /// <param name="tenantId">Id of Business Tenant</param>
        /// <param name="appId"></param>
        /// <param name="uType"></param>
        /// <param name="token"></param>
        /// <returns>return usershort info.</returns>
        public async Task<UserShortInfoDQ> GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(Guid tenantId, Guid appId, UserTypeEnum uType, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT u.ID,u.FirstName, u.LastName, u.Email,  ul.AppId, ul.JoinedDate as UserActivationDate 
                    FROM am.TenantUser u
                    INNER JOIN am.UserTenantLinking ut on ut.TenantUserId = u.ID and ut.TenantId = @tenantId And ut.IsPrimary = 1 And ut.UserType = @uType
                    INNER JOIN  am.TenantUserAppLinking ul on ul.Appid = @appId  And ul.TenantId = @tenantId And u.Deleted = 0 And u.ID = ul.TenantUserId  ";
            SqlParameter paramAppId = new SqlParameter("@appId", appId);
            SqlParameter paramtenantId = new SqlParameter("@tenantId", tenantId);
            SqlParameter paramUType = new SqlParameter("@uType", (int)uType);
            return await GetQueryEntityAsync<UserShortInfoDQ>(sql, new SqlParameter[] { paramAppId, paramtenantId, paramUType }, token);
        }

        /// <summary>
        /// return tenant application subscription list from tenant id.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns>return list of tenant subscription.</returns>
        public async Task<List<TenantSubscription>> GetTenantSubscriptionListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await GetTenantSubscriptionListAsync(tenantId, token);
        }

        /// <summary>
        /// return subscription list by tenant(business) id.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        async Task<List<TenantSubscription>> GetTenantSubscriptionListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {          
            return await _context.TenantSubscription.Where(ts => ts.TenantId == tenantId && ts.Deleted == false).ToListAsync(token);
        }

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="token"></param>
        /// <returns>return list of application.</returns>
        public async Task<List<UserShortInfoDQ>> GetBusinessPrimaryUsersByBusinessIdAsync(Guid businessId, CancellationToken token = default(CancellationToken)) {           
            string sql = @"SELECT u.ID,u.FirstName, u.LastName, u.Email , ul.AppId,  
                            ul.JoinedDate as UserActivationDate FROM AM.TenantUser u 
                            INNER JOIN AM.UserTenantLinking utl ON utl.TenantUserId = u.ID AND 
                            utl.IsPrimary = 1 AND utl.Deleted = 0 AND UserType = {0} AND 
                            utl.TenantId= '{1}'  
                            INNER JOIN AM.TenantUserAppLinking ul ON ul.TenantUserId = utl.TenantUserId 
                            and ul.TenantId= utl.TenantId ";
            sql = string.Format(sql, (int)UserTypeEnum.Business, businessId.ToString());
            return await GetQueryEntityListAsync<UserShortInfoDQ>(sql, null, token);
        }

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="publisherTenantId">Publisher tenantId.</param>
        /// <returns>return list of application.</returns>
        public async Task<List<TenantAppSubscriptionDQ>> GetBusinessSubscriptionByBusinessIdAsync(Guid businessId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            /*appSetting.ID as AppSettingId, appSetting.Currency, appSetting.DateTimeFormat, appSetting.Language, 
                    appSetting.TimeZone, appSetting.ThemeId*/
            string sql = @"SELECT ap.Name, ts.* FROM am.TenantSubscription ts 
                    INNER JOIN am.App ap on ap.ID = ts.AppId 
                    AND ts.TenantId= '{0}'  ";
            sql = string.Format(sql + " And ts.Deleted = 0 ", businessId.ToString());
            return await GetQueryEntityListAsync<TenantAppSubscriptionDQ>(sql, null, token);
        }

        #endregion Get

        #region CRUD

        /// <summary>
        /// Mark application as deleted.
        /// </summary>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="appId">Application id</param>
        /// <returns></returns>
        public async Task DeleteApplicationAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {            
            SqlParameter tId = new SqlParameter("@TenantId", tenantId);
            SqlParameter appidParam = new SqlParameter("@AppId", appId);
            SqlParameter[] param = new SqlParameter[2] { tId, appidParam };
            await _context.Database.ExecuteSqlCommandAsync("[stpr_DeleteApplication] @TenantId, @AppId", param, token);
        }

        #endregion CRUD
    }
}
