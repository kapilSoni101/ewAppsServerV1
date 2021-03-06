﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster.com>
 * Date: 5 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 5 September 2019
 */

using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.Data {
    public class TenantUserAppPreferenceRepository:BaseRepository<TenantUserAppPreference, ShipmentDbContext> , ITenantUserAppPreferenceRepository{

        public TenantUserAppPreferenceRepository(ShipmentDbContext shipmentDbContext, IUserSessionManager userSessionManager) : base(shipmentDbContext, userSessionManager) {

        }
    }
}
