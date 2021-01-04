/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 06 September 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 06 September 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// This class implements standard database logic and operations for PubBusinessSubsPlan entity.
    /// </summary>
    public class PubBusinessSubsPlanRepository:BaseRepository<PubBusinessSubsPlan, AppPortalDbContext>, IPubBusinessSubsPlanRepository {
        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public PubBusinessSubsPlanRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion

        #region Get Methods

        /// <inheritdoc/>   
        public async Task<PubBusinessSubsPlan> GetByPublisherTenantAndAppAndPlanIdAsync(Guid publisherTenantId, Guid appId, Guid subscriptionPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _context.PubBusinessSubsPlan.Where(i => i.TenantId == publisherTenantId && i.AppId == appId && i.SubscriptionPlanId == subscriptionPlanId && i.Deleted == false).FirstOrDefaultAsync(cancellationToken);
        }

        #endregion Get Methods


    }
}