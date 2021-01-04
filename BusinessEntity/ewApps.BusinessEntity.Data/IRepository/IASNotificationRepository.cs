using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {
    public interface IASNotificationRepository:IBaseRepository<ASNotification> {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="appId"></param>
    /// <param name="TenantUserId"></param>
    /// <param name="fromCount"></param>
    /// <param name="toCount"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<ASNotificationDTO>> GetASNotificationList(Guid appId, Guid tenantId, Guid TenantUserId, int fromCount, int toCount, CancellationToken token = default(CancellationToken));
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
