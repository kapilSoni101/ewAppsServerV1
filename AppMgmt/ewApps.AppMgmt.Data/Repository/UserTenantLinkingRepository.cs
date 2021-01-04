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
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {

    public class UserTenantLinkingRepository:BaseRepository<UserTenantLinking, AppMgmtDbContext>, IUserTenantLinkingRepository {

        #region Constructor

        public UserTenantLinkingRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get

        /////<inheritdoc/>
        //public async Task<UserTenantLinking> GetPrimaryUserOfCustomerAsync(Guid businesPartnerTenantId, CancellationToken token = default(CancellationToken)) {
        //    string sql = @"SELECT tul.[ID], tul.[BusinessPartnerTenantId], tul.[CreatedBy], tul.[CreatedOn], tul.[Deleted], tul.[IsPrimary], 
        //             tul.[PartnerType], tul.[TenantId], tul.[TenantUserId], tul.[UpdatedBy], tul.[UpdatedOn], tul.[UserType]
        //             FROM TenantUser AS au
        //             INNER JOIN UserTenantLinking AS tul ON au.id = tul.TenantUserId AND tul.BusinessPartnerTenantId = @businesPartnerTenantId ";

        //    SqlParameter partnerUserIdParam = new SqlParameter("@businesPartnerTenantId", businesPartnerTenantId);
        //    return await _context.UserTenantLinking.FromSql(sql, new object[] { partnerUserIdParam }).FirstOrDefaultAsync(token);
        //}

        #endregion Get

    }
}
