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
using ewApps.Payment.Entity;

namespace ewApps.Payment.Data {
    public interface ITenantUserAppPreferenceRepository : IBaseRepository<TenantUserAppPreference > {
        List<TenantUserAppPreference> GetPreferenceListByAppAndTenantAndUserId(Guid appId, Guid tenantId, Guid userId);
    }
}
