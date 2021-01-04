/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 31 January 2019
 * 
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {

    public class PublisherAppSettingRepository:BaseRepository<PublisherAppSetting, AppPortalDbContext>, IPublisherAppSettingRepository {


        #region Constructor 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        /// <param name="connectionManager"></param>
        public PublisherAppSettingRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Consturctor

        #region Get

        //       /// <summary>
        //       /// Get appdetail bu publisher tenant and appid.
        //       /// </summary>
        //       /// <param name="appId"></param>
        //       /// <param name="pubTenantId"></param>
        //       /// <returns></returns>
        //       public async Task<AppDetailDQ> GetAppDetailsAsyncFromPubAppSettingAppAndTenantID(Guid appId, Guid pubTenantId, CancellationToken token = default(CancellationToken)) {
        //           string query = @"select pas.ID,app.ID as AppID,pas.Deleted,pas.Name,pas.Active,pas.ThemeId,'' as Appdata,pas.CreatedOn, pas.CreatedBy, pas.UpdatedOn, pas.UpdatedBy, app.IdentityNumber,
        // pas.InactiveComment, 0 as servicecount ,0 as publishercount, 0 as PurchaseCount ,th.ThemeKey,u.FullName as CreaterName,
        //  t.ID as ThumbnailId,t.FileName ,  ThumbnailUrl=null, app.AppKey from PublisherAppSetting pas INNER JOIN app on 
        //  app.id = pas.appid AND app.ID = @appId AND pas.TenantId = @pubTenantId  left join TenantUser u on pas.CreatedBy=u.ID left join Thumbnail t on pas.ThumbnailId=t.ID
        //left join Theme th on th.ID =pas.ThemeId where pas.deleted=0 ";
        //           SqlParameter appIdParam = new SqlParameter("@appId", appId);
        //           SqlParameter publisherTenantIdParam = new SqlParameter("@pubTenantId", pubTenantId);
        //           AppDetailDQ appDetailDTOs = await GetQueryEntityAsync<AppDetailDQ>(query, new object[] { appIdParam, publisherTenantIdParam }, token);
        //           return appDetailDTOs;
        //       }

        /// <inheritdoc/>
        public async Task<PublisherAppSetting> GetByAppIdAndPublisherTenantIdAsync(Guid appId, Guid pubTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _context.PublisherAppSetting.Where(i => i.AppId == appId && i.TenantId == pubTenantId && i.Deleted == false).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetAppNameListByPublisherId(Guid publisherId) {
            string sql = @"SELECT DISTINCT pas.Name FROM ap.PublisherAppSetting AS pas
                            INNER JOIN ap.Publisher as p on p.TenantId=pas.TenantId
                            WHERE p.ID=@PublisherId ";
            SqlParameter pubTenantId = new SqlParameter("PublisherId", publisherId);
            return _context.PublisherAppSetting.FromSql(sql, new object[] { pubTenantId }).Select(i => i.Name);
        }

        #endregion Get

    }
}
