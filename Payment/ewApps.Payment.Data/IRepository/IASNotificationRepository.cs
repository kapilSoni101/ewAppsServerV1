using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;

namespace ewApps.Payment.Data {

    public interface IASNotificationRepository:IBaseRepository<ASNotification> {

        Task<List<ASNotificationDTO>> GetASNotificationList(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, CancellationToken token = default(CancellationToken));

        Task<List<ASNotificationDTO>> GetASNotificationListAsync(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, int entityType, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="tenantId"></param>
        /// <param name="TenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<ASNotificationDTO>> GetUnreadASNotificationList(Guid appId, Guid tenantId, Guid TenantUserId, CancellationToken token = default(CancellationToken));
    }
}
