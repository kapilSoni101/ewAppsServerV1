using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;

namespace ewApps.Payment.DS
{
  public interface IASNotificationDS : IBaseDS<ASNotification>
  {

  
    Task<List<ASNotificationDTO>> GetASNotificationList(Guid AppId, int fromCount, int toCount, CancellationToken token = default(CancellationToken));

        Task<List<ASNotificationDTO>> GetASNotificationListAsync(Guid appId, int fromCount, int toCount, int entityType, CancellationToken token = default(CancellationToken));


    Task<ResponseModelDTO> ReadASNotification(Guid Id, CancellationToken token = default(CancellationToken));

/// <summary>
/// 
/// </summary>
/// <param name="AppId"></param>
/// <param name="token"></param>
/// <returns></returns>
    Task<List<ASNotificationDTO>> GetUnreadASNotificationList(Guid AppId, CancellationToken token = default(CancellationToken));
  }
}
