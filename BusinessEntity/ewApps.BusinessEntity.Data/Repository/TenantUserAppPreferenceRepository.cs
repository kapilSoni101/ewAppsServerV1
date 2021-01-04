/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Gaurav Katiyar <gkatiyar@eworkplaceapps.com>
 * Date: 18 December 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 18 December 2019
 */
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.Data
{
    /// <summary>
    /// This class implements standard database logic and operations for TenantUserAppPreference entity.
    /// </summary>
    public class TenantUserAppPreferenceRepository : BaseRepository<TenantUserAppPreference, BusinessEntityDbContext>, ITenantUserAppPreferenceRepository
    {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public TenantUserAppPreferenceRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager)
        {
        }

        #endregion
    }
}
