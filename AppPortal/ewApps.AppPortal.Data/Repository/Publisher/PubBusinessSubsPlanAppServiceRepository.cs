/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {
    /// <summary>
    /// It will Add/Update/Delete the subservices of a Application for a Tenant.
    /// </summary>
    public class PubBusinessSubsPlanAppServiceRepository:BaseRepository<PubBusinessSubsPlanAppService, AppPortalDbContext>, IPubBusinessSubsPlanAppServiceRepository {

        #region Constructor
        public PubBusinessSubsPlanAppServiceRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion

        #region Get

        public async Task<List<PubBusinessSubsPlanAppService>> GetListPubBusinessSubsPlanAppService(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            List<PubBusinessSubsPlanAppService> pubBusinessSubsPlanAppService;
            pubBusinessSubsPlanAppService = await _context.PubBusinessSubsPlanAppService.Where(t => t.AppId == appId && t.TenantId == tenantId).ToListAsync(token);
            return pubBusinessSubsPlanAppService;

        }
        #endregion Get

    }
}
