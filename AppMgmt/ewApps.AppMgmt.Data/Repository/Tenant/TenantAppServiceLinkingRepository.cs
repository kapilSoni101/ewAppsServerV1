/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* 
* Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
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
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using Microsoft.EntityFrameworkCore;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This is the repository responsible to perform CRUD operations on service entity.
    /// </summary>
    public class TenantAppServiceLinkingRepository:BaseRepository<TenantAppServiceLinking, AppMgmtDbContext>, ITenantAppServiceLinkingRepository {

        #region Constructor

        /// <summary>
        /// Parameter conatins the DBContent and SessionManager, It will be used by a parent class.
        /// </summary>
        public TenantAppServiceLinkingRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get application subscribed services by TenantId.
        /// </summary>
        /// <param name="businessId">Unique business id.</param>
        /// <param name="appId">Application id</param>
        /// <param name="token"></param>
        /// <returns>return list of TenantAppService.</returns>
        public async Task<List<TenantAppServiceLinking>> GetAppServiceByTenantIdAndAppIdAsync(Guid businessId, Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _context.TenantAppServiceLinking.Where(tasl => tasl.Deleted == false && tasl.TenantId == businessId && tasl.AppId == appId).ToListAsync(token);
        }

        /// <summary>
        /// Get application sub services subscribed by a Tenant applications.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="entityTypeEnum">Entity type</param>
        /// <param name="token"></param>
        /// <returns>return Guid of application sub services and Application id.</returns>
        public async Task<List<TenantAppServiceDQ>> GetAppServiceByBusinessIdAsync(Guid businessId, int entityTypeEnum, CancellationToken token = default(CancellationToken)) {
            //string sql = "SELECT tas.ID, tas.AppServiceId, tas.AppId, as1.Name FROM TenantAppService tas inner join AppService as1 on as1.ID = tas.AppServiceId And tas.TenantId= '{0}' And tas.Deleted = 0 ";
            string sql = "SELECT DISTINCT tasl.ID,appS.Name, appS.ID as AppServiceId, appS.AppId, asAttr.Name AS  AttributeName,  ISNULL(tasl.ServiceAttributeId, 0x00) AS AttributeId, actDTl.AccountJson " +
                         "FROM am.TenantAppServiceLinking tasl INNER JOIN am.AppService appS ON tasl.ServiceId = appS.ID AND tasl.Deleted = 0  And tasl.TenantId = '{0}' " +
                         "LEFT JOIN am.AppServiceAttribute asAttr ON asAttr.AppServiceID = appS.ID AND asAttr.Deleted = 0 AND asAttr.ID = tasl.ServiceAttributeId " +
                         "LEFT JOIN am.AppServiceAccountDetail actDTl ON actDTl.ServiceID = appS.ID AND actDTl.Deleted = 0 AND actDTl.ServiceAttributeId = tasl.ServiceAttributeId And actDTl.EntityId = '{0}' And actDTl.EntityType = {1} ";
            sql = string.Format(sql, businessId.ToString(), entityTypeEnum);
            return await GetQueryEntityListAsync<TenantAppServiceDQ>(sql, null, token);
        }

        //public async Task<List<TenantAppServiceDQ>> GetAppServiceByTenantIdAsync(Guid businessId, CancellationToken token = default(CancellationToken)) {
        //    //string sql = "SELECT tas.ID, tas.AppServiceId, tas.AppId, as1.Name FROM TenantAppService tas inner join AppService as1 on as1.ID = tas.AppServiceId And tas.TenantId= '{0}' And tas.Deleted = 0 ";
        //    string sql = "SELECT tasl.ID,appS.Name, appS.ID as AppServiceId, appS.AppId, asAttr.Name AS  AttributeName,  tasl.ServiceAttributeId AS AttributeId, tasl.TenantId, actDTl.AccountJson " +
        //                 "FROM TenantAppServiceLinking tasl INNER JOIN AppService appS ON tasl.ServiceId = appS.ID And tasl.TenantId = '{0}' " +
        //                 "LEFT JOIN AppServiceAttribute asAttr ON asAttr.AppServiceID = appS.ID AND asAttr.ID = tasl.ServiceAttributeId " +
        //                 "LEFT JOIN AppServiceAccountDetail actDTl ON actDTl.ServiceID = appS.ID AND actDTl.Deleted = 0 AND actDTl.ServiceAttributeId = tasl.ServiceAttributeId And actDTl.EntityId = '{0}' ";
        //    sql = string.Format(sql, businessId.ToString());
        //    return await GetQueryEntityListAsync<TenantAppServiceDQ>(sql, null, token);
        //}

        public async Task<List<TenantAppServiceLinking>> GetEntityByAppAndTenantId(Guid appId, Guid tenantId, bool includeDeleted) {
            return _context.TenantAppServiceLinking.Where(i => i.AppId == appId && i.TenantId == tenantId && i.Deleted == includeDeleted).ToList();
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// Delete appservice linking and attribute.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appServiceId"></param>
        /// <param name="attrbuteId"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task DeleteAppServiceLinkingByAppServiceAndAttributeAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "DELETE FROM am.TenantAppServiceLinking WHERE AppId = @appId AND ServiceId=@appServiceId AND ServiceAttributeId=@attrbuteId AND TenantId=@tenantId ";
            SqlParameter appParam = new SqlParameter("@appId", appId);
            SqlParameter appServiceIdParam = new SqlParameter("@appServiceId", appServiceId);
            SqlParameter attrbuteIdParam = new SqlParameter("@attrbuteId", attrbuteId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);

            await _context.Database.ExecuteSqlCommandAsync(sql, new SqlParameter[] { appParam, appServiceIdParam, attrbuteIdParam, tenantIdParam }, token);
        }

        /// <summary>
        /// Delete appservice linking and attribute.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appServiceId"></param>        
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task DeleteAppServiceLinkingByAppServiceAndAttributeAsync(Guid appId, Guid appServiceId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "DELETE FROM am.TenantAppServiceLinking WHERE AppId = @appId AND ServiceId=@appServiceId AND TenantId=@tenantId ";
            SqlParameter appParam = new SqlParameter("@appId", appId);
            SqlParameter appServiceIdParam = new SqlParameter("@appServiceId", appServiceId);
            // SqlParameter attrbuteIdParam = new SqlParameter("@attrbuteId", attrbuteId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);

            await _context.Database.ExecuteSqlCommandAsync(sql, new SqlParameter[] { appParam, appServiceIdParam, tenantIdParam }, token);
        }

        #endregion Delete

    }
}
