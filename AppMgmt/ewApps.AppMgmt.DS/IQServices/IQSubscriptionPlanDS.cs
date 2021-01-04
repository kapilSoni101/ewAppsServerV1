using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS {

    public interface IQSubscriptionPlanDS {

    Task<SubscriptionPlanInfoDTO> GetSubscriptionPlanReferece(Guid id, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken));
  }
}
