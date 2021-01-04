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

using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data
{
    /// <summary>
    /// Manages the Preferences related CRUD methods
    /// </summary>
    public interface ITenantUserAppPreferenceRepository : IBaseRepository<TenantUserAppPreference> {
    }
}
