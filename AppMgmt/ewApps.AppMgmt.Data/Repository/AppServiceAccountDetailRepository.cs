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
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This is the repository responsible to perform CRUD operations on service entity.
    /// </summary>
    public class AppServiceAccountDetailRepository:BaseRepository<AppServiceAccountDetail, AppMgmtDbContext>, IAppServiceAccountDetailRepository {

        #region Constructor

        /// <summary>
        /// Parameter conatins the DBContent and SessionManager, It will be used by a parent class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        /// <param name="connectionManager"></param>
        public AppServiceAccountDetailRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        public async Task UpdateAccountDetailJsonAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid entityId, string accountJson) {
            string sql = "UPDATE AppServiceAccountDetail SET AccountJson = @accountJson WHERE AppId = @appId And EntityId=@entityId AND ServiceId=@appServiceId AND ServiceAttributeId=@attrbuteId ";
            SqlParameter accountJsonParam = new SqlParameter("@accountJson", accountJson);
            SqlParameter appParam = new SqlParameter("@appId", appId);
            SqlParameter enParam = new SqlParameter("@entityId", entityId);
            SqlParameter appServiceIdParam = new SqlParameter("@appServiceId", appServiceId);
            SqlParameter attrbuteIdParam = new SqlParameter("@attrbuteId", attrbuteId);


            await _context.Database.ExecuteSqlCommandAsync(sql, new SqlParameter[] { appParam, appServiceIdParam, attrbuteIdParam, accountJsonParam, enParam });
        }

        /// <summary>
        /// Delete appservice account detail.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appServiceId"></param>
        /// <param name="attrbuteId"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task DeleteAppServiceAccountDetailAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid entityId, Guid tenantId) {
            string sql = "UPDATE AppServiceAccountDetail SET Deleted = 1  WHERE AppId = @appId And EntityId=@entityId AND ServiceId=@appServiceId AND ServiceAttributeId=@attrbuteId AND TenantId=@tenantId ";
            SqlParameter appParam = new SqlParameter("@appId", appId);
            SqlParameter appServiceIdParam = new SqlParameter("@appServiceId", appServiceId);
            SqlParameter attrbuteIdParam = new SqlParameter("@attrbuteId", attrbuteId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter enParam = new SqlParameter("@entityId", entityId);

            await _context.Database.ExecuteSqlCommandAsync(sql, new SqlParameter[] { appParam, appServiceIdParam, attrbuteIdParam, tenantIdParam, enParam });
        }

        /// <summary>
        /// Delete appservice account detail.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appServiceId"></param>        
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task DeleteAppServiceAccountDetailAsync(Guid appId, Guid appServiceId, Guid entityId, Guid tenantId) {
            string sql = "UPDATE AppServiceAccountDetail SET Deleted = 1  WHERE AppId = @appId And EntityId=@entityId AND ServiceId=@appServiceId AND TenantId=@tenantId ";
            SqlParameter appParam = new SqlParameter("@appId", appId);
            SqlParameter appServiceIdParam = new SqlParameter("@appServiceId", appServiceId);
            // SqlParameter attrbuteIdParam = new SqlParameter("@attrbuteId", attrbuteId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter enParam = new SqlParameter("@entityId", entityId);

            await _context.Database.ExecuteSqlCommandAsync(sql, new SqlParameter[] { appParam, appServiceIdParam, tenantIdParam, enParam });
        }

        #endregion Constructor

        #region Add/Update/Delete

        /// <summary>
        /// Update app service account detail.
        /// </summary>
        /// <param name="appId">Application id</param>
        /// <param name="appServiceId">AppService unique id like (VeriCheck, UPS/FedEx GUID)</param>
        /// <param name="attrbuteId">attribute id, like Ground, FedEx Ground etc.</param>
        /// <param name="entityId">For which entity data is being save</param>
        /// <param name="accountJson">Json object contains account information.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateAccountDetailJsonAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid entityId, string accountJson, CancellationToken token = default(CancellationToken)) {
            string sql = "UPDATE AppServiceAccountDetail SET AccountJson = @accountJson WHERE AppId = @appId And EntityId=@entityId AND ServiceId=@appServiceId AND ServiceAttributeId=@attrbuteId ";
            SqlParameter accountJsonParam = new SqlParameter("@accountJson", accountJson);
            SqlParameter appParam = new SqlParameter("@appId", appId);
            SqlParameter enParam = new SqlParameter("@entityId", entityId);
            SqlParameter appServiceIdParam = new SqlParameter("@appServiceId", appServiceId);
            SqlParameter attrbuteIdParam = new SqlParameter("@attrbuteId", attrbuteId);


            await _context.Database.ExecuteSqlCommandAsync(sql, new SqlParameter[] { appParam, appServiceIdParam, attrbuteIdParam, accountJsonParam, enParam }, token);
        }

        /// <summary>
        /// Delete appservice account detail.
        /// </summary>
        /// <param name="appId">Application id</param>
        /// <param name="appServiceId">AppService unique id like (VeriCheck, UPS/FedEx GUID)</param>
        /// <param name="attrbuteId">attribute id, like Ground, FedEx Ground etc.</param>
        /// <param name="entityId">For which entity data is being save</param>
        /// <param name="tenantId">unique tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteAppServiceAccountDetailAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid entityId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "UPDATE AppServiceAccountDetail SET Deleted = 1  WHERE AppId = @appId And EntityId=@entityId AND ServiceId=@appServiceId AND ServiceAttributeId=@attrbuteId AND TenantId=@tenantId ";
            SqlParameter appParam = new SqlParameter("@appId", appId);
            SqlParameter appServiceIdParam = new SqlParameter("@appServiceId", appServiceId);
            SqlParameter attrbuteIdParam = new SqlParameter("@attrbuteId", attrbuteId);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter enParam = new SqlParameter("@entityId", entityId);

            await _context.Database.ExecuteSqlCommandAsync(sql, new SqlParameter[] { appParam, appServiceIdParam, attrbuteIdParam, tenantIdParam, enParam }, token);
        }

        #endregion Add/Update/Delete

        public async Task<List<AppServiceAccountDetail>> GetEntityByEntityTypeAndEntityAndServiceAndAttributeId(int entityType, Guid entityId, Guid appServiceId, Guid appServiceAttributeId, bool includeDeleted) {
            return _context.AppServiceAccountDetail.Where(i => i.EntityType == entityType && i.EntityId == entityId && i.ServiceId == appServiceId && i.ServiceAttributeId == appServiceAttributeId && i.Deleted == includeDeleted).ToList();
        }

    }
}
