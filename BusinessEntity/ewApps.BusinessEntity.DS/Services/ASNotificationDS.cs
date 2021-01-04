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

  public class ASNotificationDS : BaseDS<ASNotification>, IASNotificationDS
  {

    private IASNotificationRepository _aSNotificationRespository;
    IUserSessionManager _userSessionManager;
    IUnitOfWork _unitOfWork;

    public ASNotificationDS(IASNotificationRepository aSNotificationRespository, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork) : base(aSNotificationRespository)
    {
      _aSNotificationRespository = aSNotificationRespository;
      _userSessionManager = userSessionManager;
      _unitOfWork = unitOfWork;
    }


    public async Task<List<ASNotificationDTO>> GetASNotificationList(Guid AppId, int fromCount, int toCount, CancellationToken token = default(CancellationToken))
    {
      UserSession userSession = _userSessionManager.GetSession();     
      return await _aSNotificationRespository.GetASNotificationList(AppId, userSession.TenantId, userSession.TenantUserId, fromCount, toCount, token);
    }

    public async Task<List<ASNotificationDTO>> GetUnreadASNotificationList(Guid AppId, CancellationToken token = default(CancellationToken)) {
      UserSession userSession = _userSessionManager.GetSession();
      return await _aSNotificationRespository.GetUnreadASNotificationList(AppId, userSession.TenantId, userSession.TenantUserId, token);
    }

    #region Update

    public async Task<ResponseModelDTO> ReadASNotification(Guid Id, CancellationToken token = default(CancellationToken))
    {
      ResponseModelDTO responseModelDTO = new ResponseModelDTO();
      // Get entity if exists
      ASNotification aSNotification = await FindAsync(a => a.ID == Id);
      if (aSNotification == null)
      {
        responseModelDTO.IsSuccess = false;
      }
      else
      {
        aSNotification.ReadState = true;
        Update(aSNotification, Id);

        // Save Data
        _unitOfWork.SaveAll();
        responseModelDTO.IsSuccess = true;
      }
      return responseModelDTO;
    }
   
    #endregion Update


  }
}
