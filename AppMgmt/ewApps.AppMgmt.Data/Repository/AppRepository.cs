/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This class implements standard database logic and operations for app entity.
    /// </summary>
    public class AppRepository:BaseRepository<App, AppMgmtDbContext>, IAppRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public AppRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor 

        #region Get

        ///<inheritdoc/>
        public async Task<List<AppDetailDTO>> GetAppDetailsAsync() {
            string query = @"select a.ID,a.CreatedBy,a.ID as AppID,a.CreatedOn,a.UpdatedBy, a.UpdatedOn,a.Deleted,Name,a.Active,a.ThemeId, 
         a.InactiveComment, (select count(1) from AppService where AppId= a.id) servicecount ,(select count(1) from PublisherAppSetting pas 
        INNER JOIN publisher pub on pub.TenantId = pas.TenantId  where pas.deleted = 0 and pas.AppId = a.ID ) publishercount,
        (select  count(1) from TenantSubscription ts
        INNER JOIN Tenant t on t.ID=ts.TenantId where AppId= a.id and ts.Deleted=0 and t.Deleted = 0 and t.TenantType = @tenantType) PurchaseCount ,
        th.ThemeKey,u.FullName as CreaterName,t.ID as ThumbnailId,t.FileName ,ThumbnailUrl=null,a.IdentityNumber , a.appkey from App a 
        LEFT JOIN TenantUser u on a.CreatedBy=u.ID 
        LEFT JOIN Thumbnail t on a.ID=t.OwnerEntityId
        LEFT JOIN Theme th on th.ID =a.ThemeId where a.deleted=0 and a.appscope = 2";


     
      SqlParameter paramTenantType = new SqlParameter("@tenantType", TenantType.Buisness);
       List < AppDetailDTO > appDetailDTOs = await GetQueryEntityListAsync <AppDetailDTO>(query, new SqlParameter[] {  paramTenantType });

      
            return appDetailDTOs;
        }


        /// <summary>
        ///  Get application from appkey.
        /// </summary>
        public async Task<App> GetAppByAppKeyAsync(string appKey, CancellationToken token = default(CancellationToken)) {
            return await _context.App.FirstOrDefaultAsync(app => app.AppKey == appKey, token);
        }

        /// <summary>
        ///  Get tenant settings by subdomain name and tenanttype.
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<AppSettingDQ>> GetAppSettingByTenantTypeAndSubdomainAsync(string subdomain, int tenantType, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT app.ID as AppId, app.Name, appSetting.Currency, appSetting.DateTimeFormat, appSetting.Language, appSetting.TimeZone, appSetting.ThemeId " +
                        "FROM TenantAppSetting appSetting " +
                        "INNER JOIN App app ON app.ID = appSetting.AppId And app.Deleted = 0 And app.Active = 1 ";
            sql += "AND appSetting.TenantId IN (" + GetTenantIDQuerySubdomainAndTenantType(subdomain, tenantType) + " )";

            SqlParameter paramSubdomain = new SqlParameter("@subdomain", subdomain);
            SqlParameter paramTenantType = new SqlParameter("@tenantType", tenantType);
            return await GetQueryEntityListAsync<AppSettingDQ>(sql, new SqlParameter[] { paramSubdomain, paramTenantType }, token);
        }

        /// <summary>
        ///  Get tenant id using query by subdomain name and tenanttype.
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="tenantType"></param>    
        /// <returns></returns>
        private string GetTenantIDQuerySubdomainAndTenantType(string subdomain, int tenantType) {
            string sql = "";
            if(tenantType == (int)TenantType.Publisher) {
                sql = "SELECT TOP 1 tl.PublisherTenantId FROM TenantLinking tl INNER JOIN Tenant t ON tl.PublisherTenantId = t.ID And t.SubDomainName = @subdomain And  t.TenantType =@tenantType ";
            }
            else if(tenantType == (int)TenantType.Buisness || tenantType == (int)TenantType.BuisnessPartner) {
                sql = "SELECT TOP 1 tl.BusinessTenantId FROM TenantLinking tl INNER JOIN Tenant t ON tl.BusinessTenantId = t.ID And t.SubDomainName = @subdomain And  t.TenantType =@tenantType ";
            }
            else {
                sql = "SELECT TOP 1 tl.PlatformTenantId FROM TenantLinking tl INNER JOIN Tenant t ON tl.PlatformTenantId = t.ID And t.SubDomainName = @subdomain And  t.TenantType =@tenantType ";
            }

            return sql;
        }

        #endregion Get

    }
}
