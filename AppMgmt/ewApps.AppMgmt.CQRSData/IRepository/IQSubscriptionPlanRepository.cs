using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.QData {

    public interface IQSubscriptionPlanRepository {

    #region Get

    /// <summary>
    /// Get the refernces of plan in various tables.
    /// </summary>
    /// <param name="id">Unique plan id.</param>
    /// <param name="token">token</param>
    /// <returns>plan info dto idf references exists</returns>
    Task<SubscriptionPlanInfoDTO> GetSubscriptionPlanReferece(Guid id, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(Guid tenantId, bool planState, CancellationToken cancellationToken = default(CancellationToken));

    #endregion Get   

  }
}
