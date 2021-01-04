using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers.Publisher {
    [Route("api/[controller]")]
    [ApiController]
    public class PubAppSubsController:ControllerBase {
        IPubBusinessSubsPlanDS _pubBusinessSubsPlanDS;
        IQPubBusinessSubsPlanAppServiceDS _qPubBusinessSubsPlanAppServiceDS;

        /// <summary>
        /// Initializes a new instance of the <see cref="PubAppSubsController"/> class.
        /// </summary>
        /// <param name="pubBusinessSubsPlanDS">The pub business subs plan ds.</param>
        /// <param name="qPubBusinessSubsPlanAppServiceDS">The pub business subs plan application service ds.</param>
        public PubAppSubsController(IPubBusinessSubsPlanDS pubBusinessSubsPlanDS, IQPubBusinessSubsPlanAppServiceDS qPubBusinessSubsPlanAppServiceDS) {
            _pubBusinessSubsPlanDS = pubBusinessSubsPlanDS;
            _qPubBusinessSubsPlanAppServiceDS = qPubBusinessSubsPlanAppServiceDS;
        }

        // Note: This is for Business Add.
        /// <summary>
        /// Gets the subscription plan service and attribute by publisher's business app subscription plan id.
        /// </summary>
        /// <param name="pubBusSubsPlanId">The publisher's business app subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns service and attribute list that matches given publisher's business app subscription plan id.</returns>
        [HttpGet]
        [Route("{pubBusSubsPlanId}/services")]
        public async Task<List<SubsPlanServiceInfoDTO>> GetSubscriptionPlanServiceAndAttributeByPlanIdAsync([FromRoute] Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPubBusinessSubsPlanAppServiceDS.GetSubscriptionPlanServiceAndAttributeByPlanIdAsync(pubBusSubsPlanId, cancellationToken);
        }

        /// <summary>
        /// Get subscribed services by PlanId.
        /// </summary>
        /// <param name="pubBusSubsPlanId">PubBusinessSubsPlanId</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("subscribedservices/{pubBusSubsPlanId}")]
        public async Task<List<ServiceInfoDTO>> GetPlanServicesByPlanIdAsync(Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPubBusinessSubsPlanAppServiceDS.GetPlanServicesByPlanIdAsync(pubBusSubsPlanId, cancellationToken);
        }
    }
}