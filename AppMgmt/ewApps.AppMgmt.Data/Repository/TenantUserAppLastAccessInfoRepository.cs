/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 14 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 Aug 2019
 */

using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This class implements standard database logic and operations for  AppLastAccessInfo entity.
    /// </summary>
    public class TenantUserAppLastAccessInfoRepository:BaseRepository<TenantUserAppLastAccessInfo, AppMgmtDbContext>, ITenantUserAppLastAccessInfoRepository {

        /// <summary>
        /// Constructor initializing the base variables.
        /// </summary>
        public TenantUserAppLastAccessInfoRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
    }

}