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

using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Shipment.DTO;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {

    public interface ITenantUserAppPreferenceDS : IBaseDS<TenantUserAppPreference> {

        Task AddTenantUserAppPreferncesAsync(TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO);
    }
}
