/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 14 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 August 2019
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.QData {


    /// <summary>
    /// Dependency injection for reposity base classes only.
    /// </summary>
    public class QPlatBusinessRepository:BaseRepository<BaseEntity, QAppPortalDbContext>, IQPlatBusinessRepository {

        #region Constructor
        public QPlatBusinessRepository(QAppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion

        ///<inheritdoc/>
        public async Task<List<PlatBusinessDTO>> GetTenantListOnPlatformAsync(ListDateFilterDTO filter, Guid homeAppId, CancellationToken token = default(CancellationToken)) {

            SqlParameter start = new SqlParameter("@startDate", filter.FromDate);
            SqlParameter end = new SqlParameter("@endDate", filter.ToDate);
            SqlParameter deletedParam = new SqlParameter("@deleted", filter.Deleted);
            SqlParameter homeAppIdParam = new SqlParameter("@homeAppId", homeAppId);
            //SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            return this.GetQueryEntityList<PlatBusinessDTO>("prcQPlatGetFilterVendorViewModelListForPlatform @startDate, @endDate, @homeAppId,@deleted", new SqlParameter[] { start, end, homeAppIdParam, deletedParam });


        }
    }
}
