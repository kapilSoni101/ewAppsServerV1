using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.DS
{

  public interface IASNotificationDS : IBaseDS<ASNotification>
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="AppId"></param>
    /// <param name="fromCount"></param>
    /// <param name="toCount"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<ASNotificationDTO>> GetASNotificationList(Guid AppId, int fromCount, int toCount, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="token"></param>
    /// <returns></returns>
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
