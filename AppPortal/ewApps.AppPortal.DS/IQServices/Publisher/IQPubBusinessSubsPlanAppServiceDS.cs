using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IQPubBusinessSubsPlanAppServiceDS {
        /// <summary>
        /// Gets the subscription plan list service and attribute by publisher's business app subscription plan id.
        /// </summary>
        /// <param name="pubBusSubsPlanId">The publisher's business app subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns service and attribute list that matches given publisher's business app subscription plan id.</returns>
        Task<List<SubsPlanServiceInfoDTO>> GetSubscriptionPlanServiceAndAttributeByPlanIdAsync(Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get subscribed services by PlanId.
        /// </summary>
        /// <param name="pubBusSubsPlanId">PubBusinessSubsPlanId</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<ServiceInfoDTO>> GetPlanServicesByPlanIdAsync(Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
