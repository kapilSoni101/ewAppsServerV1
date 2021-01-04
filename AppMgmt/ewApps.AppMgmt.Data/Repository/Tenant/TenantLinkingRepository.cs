/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 January 2019
 * 
 * Contributor/s: Ishwar Rathore
 * Last Updated On: 30 January 2019
 */

using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// It will Add/Update/Delete the subservices of a Application for a Tenant.
    /// </summary>
    public class TenantLinkingRepository:BaseRepository<TenantLinking, AppMgmtDbContext>, ITenantLinkingRepository {

        #region Constructor
        public TenantLinkingRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion

        #region Get

        /// <summary>
        ///  Get tenant linking by subdomain name and tenanttype.
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TenantLinking> GetTenantLinkingBySubdomainAndTenantTypeAsync(string subdomain, TenantType tenantType, CancellationToken token = default(CancellationToken)) {
            string sql = "";
            if(tenantType == TenantType.Publisher) {
                sql = "SELECT tl.* FROM am.TenantLinking tl INNER JOIN am.Tenant t ON tl.PublisherTenantId = t.ID And t.SubDomainName = @subdomain And  t.TenantType =@tenantType ";
            }
            else if(tenantType == TenantType.Buisness) {
                sql = "SELECT tl.* FROM am.TenantLinking tl INNER JOIN am.Tenant t ON tl.BusinessTenantId = t.ID And t.SubDomainName = @subdomain And  t.TenantType =@tenantType ";
            }
            else {
                sql = "SELECT tl.* FROM am.TenantLinking tl INNER JOIN am.Tenant t ON tl.PlatformTenantId = t.ID And t.SubDomainName = @subdomain And  t.TenantType =@tenantType ";
            }
            SqlParameter paramSubdomain = new SqlParameter("@subdomain", subdomain);
            SqlParameter paramTenantType = new SqlParameter("@tenantType", (int)tenantType);
            return await GetEntityAsync(sql, new SqlParameter[] { paramSubdomain, paramTenantType }, token);
        }


        /// <summary>
        ///  Get tenant linking by tenantid and tenanttype.
        /// </summary>    
        /// <param name="tenantId"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TenantLinking> GetTenantLinkingByTenantIdAsync(Guid tenantId, TenantType tenantType, CancellationToken token = default(CancellationToken)) {


            string sql = "";
            if(tenantType == TenantType.Publisher) {
                sql = "SELECT tl.* FROM am.TenantLinking tl INNER JOIN am.Tenant t ON tl.PublisherTenantId = t.ID " +
                                    " AND t.ID = @tenantId And  t.TenantType = @tenantType ";
            }
            else if(tenantType == TenantType.Buisness) {
                sql = "SELECT tl.* FROM am.TenantLinking tl INNER JOIN am.Tenant t ON tl.BusinessTenantId = t.ID " +
                            " AND t.ID = @tenantId And  t.TenantType = @tenantType ";
            }
            else if(tenantType == TenantType.BuisnessPartner) {
                sql = "SELECT tl.* FROM am.TenantLinking tl INNER JOIN am.Tenant t ON tl.BusinessPartnerTenantId = t.ID " +
                            " AND t.ID = @tenantId And  t.TenantType = @tenantType ";
            }
            else {
                sql = "SELECT tl.* FROM am.TenantLinking tl INNER JOIN am.Tenant t ON tl.PlatformTenantId = t.ID " +
                                    " AND t.ID = @tenantId And  t.TenantType = @tenantType ";
            }
            SqlParameter paramtenantId = new SqlParameter("@tenantId", tenantId);
            SqlParameter paramTenantType = new SqlParameter("@tenantType", (int)tenantType);
            return await GetEntityAsync(sql, new SqlParameter[] { paramtenantId, paramTenantType }, token);
        }

        #endregion Get

    }
}
