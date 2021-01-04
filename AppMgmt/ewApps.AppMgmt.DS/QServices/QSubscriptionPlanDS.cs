

using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.QData;
using ewApps.Core.UserSessionService;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.AppMgmt.DS {

    public class QSubscriptionPlanDS : IQSubscriptionPlanDS {

    IQSubscriptionPlanRepository _iQSubPlaRepo;
    IUserSessionManager _userSessionManager;

    public QSubscriptionPlanDS(IQSubscriptionPlanRepository iQSubPlaRepo, IUserSessionManager userSessionManager) {
      _iQSubPlaRepo = iQSubPlaRepo;
      _userSessionManager = userSessionManager;
    }

    #region Get

    ///<inheritdoc/>
    public async Task<SubscriptionPlanInfoDTO> GetSubscriptionPlanReferece(Guid id, CancellationToken cancellationToken = default(CancellationToken)) {
      return await _iQSubPlaRepo.GetSubscriptionPlanReferece(id, cancellationToken);
    }

    /// <inheritdoc/>       
    public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken)) {
      UserSession session = _userSessionManager.GetSession();

      return await _iQSubPlaRepo.GetSubscriptionPlanListByPubTenantIdAsync(session.TenantId, planState, cancellationToken);
    }

    #endregion

  }
  }