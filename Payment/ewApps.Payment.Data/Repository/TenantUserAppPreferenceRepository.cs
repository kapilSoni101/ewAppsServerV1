/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster.com>
 * Date: 5 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 5 September 2019
 */

using System;
using System.Collections.Generic;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ewApps.Payment.Data {
    public class TenantUserAppPreferenceRepository:BaseRepository<TenantUserAppPreference, PaymentDbContext>, ITenantUserAppPreferenceRepository {

        public TenantUserAppPreferenceRepository(PaymentDbContext paymentDbContext, IUserSessionManager userSessionManager) : base(paymentDbContext, userSessionManager) {
        }

        public List<TenantUserAppPreference> GetPreferenceListByAppAndTenantAndUserId(Guid appId, Guid tenantId, Guid userId) {
            return _context.TenantUserAppPreference.Where(i => i.AppId == appId && i.TenantId == tenantId && i.TenantUserId == userId && i.Deleted == false).ToList();
        }
    }
}
