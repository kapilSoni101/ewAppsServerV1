/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Amit Mundra
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
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This class implements standard database logic and operations for AppUser entity.
    /// </summary>
    public class TenantUserRepository:BaseRepository<TenantUser, AppMgmtDbContext>, ITenantUserRepository {

        #region Constructor

    
        public TenantUserRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

        }

        #endregion Constructor

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

        #endregion Get

        #region Public Methods

        /// <inheritdoc />
        public async Task<Tuple<string, bool>> UserAlreadyJoinedAnyApplication(Guid tenantUserId) {
            List<TenantUserAppLinking> list = await _context.TenantUserAppLinking.Where(u => u.TenantUserId == tenantUserId).ToListAsync();
            if(list != null && list.Count > 0) {
                foreach(TenantUserAppLinking item in list) {
                    string name = string.Empty;
                    if(item.JoinedDate != null) {
                        App app = await _context.App.FindAsync(item.AppId);
                        Tenant tenant = await _context.Tenant.FindAsync(item.TenantId);
                        if(!(app.AppKey == AppKeyEnum.plat.ToString() || app.AppKey == AppKeyEnum.pub.ToString())) {
                            TenantLinking tenantLinking = _context.TenantLinking.FirstOrDefault(t => t.BusinessTenantId == item.TenantId && t.BusinessPartnerTenantId == null);
                            Tenant pubTenant = await _context.Tenant.FindAsync(tenantLinking.PublisherTenantId);
                            name = pubTenant.Name + ", Business " + tenant.Name;
                        }
                        else {
                            name = tenant.Name;
                        }
                        Tuple<string, bool> result = new Tuple<string, bool>(name, true);
                        return result;
                    }
                    else {
                        continue;
                    }
                }
            }
            else {
                Tuple<string, bool> result = new Tuple<string, bool>(string.Empty, false);
                return result;
            }
            Tuple<string, bool> result1 = new Tuple<string, bool>(string.Empty, false);
            return result1;
        }


        /// <inheritdoc />
        public async Task DeleteUserDependencyAsync(Guid tenantUserId, Guid tenantId, Guid appId, Guid? businessAppId) {
            string sql = string.Empty;
            if(businessAppId == null) {
                sql = string.Format("exec [prcQDeleteTenantUserDependency] '{0}' , '{1}' ,'{2}' , null ", tenantUserId, tenantId, appId);
            }
            else {
                sql = string.Format("exec [prcQDeleteTenantUserDependency] '{0}' , '{1}' ,'{2}' , '{3}' ", tenantUserId, tenantId, appId, businessAppId);
            }
            await _context.Database.ExecuteSqlCommandAsync(sql);
        }

        #endregion Public Methods
    }
}
