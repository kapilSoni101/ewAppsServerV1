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
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Shipment.Common;
using ewApps.Shipment.Data;
using ewApps.Shipment.DTO;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {
    public class TenantUserAppPreferenceDS:BaseDS<TenantUserAppPreference>, ITenantUserAppPreferenceDS {

        public TenantUserAppPreferenceDS(ITenantUserAppPreferenceRepository tenantUserAppPreferenceRepository) : base(tenantUserAppPreferenceRepository) {

        }

        public async Task AddTenantUserAppPreferncesAsync(TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO) {

            // Add tenant user preference entry.
            TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
            tenantUserAppPreference.ID = Guid.NewGuid();
            tenantUserAppPreference.AppId = roleLinkingAndPreferneceDTO.AppId;
            tenantUserAppPreference.CreatedBy = roleLinkingAndPreferneceDTO.CreatedBy;
            tenantUserAppPreference.CreatedOn = DateTime.UtcNow;
            tenantUserAppPreference.Deleted = false;
            tenantUserAppPreference.EmailPreference = (long)ShipmentBusinessEmailPreferenceEnum.All;
            tenantUserAppPreference.SMSPreference = (long)ShipmentBusinessSMSPreferenceEnum.All;
            tenantUserAppPreference.TenantId = roleLinkingAndPreferneceDTO.TenantId;
            tenantUserAppPreference.TenantUserId = roleLinkingAndPreferneceDTO.TenantUserId;
            tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
            tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
            await AddAsync(tenantUserAppPreference);
        }
    }
}
