/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 06 Nov 2019

 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Data {

    public interface IASNotificationRepository:IBaseRepository<ASNotification> {

        Task<List<ASNotificationDTO>> GetASNotificationList(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, CancellationToken token = default(CancellationToken));

        Task<List<ASNotificationDTO>> GetASNotificationListByAppAndTenantAndUserAndEntityTypeAsync(Guid appId, Guid tenantId, Guid tenantUserId, int entityType, int fromCount, int toCount, CancellationToken token = default(CancellationToken));

        Task<List<ASNotificationDTO>> GetASNotificationListAsync(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, int entityType, CancellationToken token = default(CancellationToken));

        Task<List<ASNotificationDTO>> GetUnreadASNotificationFromAppPortal(Guid appId, Guid tenantId, Guid TenantUserId, CancellationToken token = default(CancellationToken));
    }
}
