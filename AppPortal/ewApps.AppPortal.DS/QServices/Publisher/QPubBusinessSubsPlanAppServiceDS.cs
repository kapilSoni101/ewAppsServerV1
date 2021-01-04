using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public class QPubBusinessSubsPlanAppServiceDS:IQPubBusinessSubsPlanAppServiceDS {
        private IQPubBusinessSubsPlanAppServiceRepository _qPubBusinessSubsPlanAppServiceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="QPubBusinessSubsPlanAppServiceDS"/> class.
        /// </summary>
        /// <param name="qPubBusinessSubsPlanAppServiceDS">The q pub business subs plan application service ds.</param>
        public QPubBusinessSubsPlanAppServiceDS(IQPubBusinessSubsPlanAppServiceRepository qPubBusinessSubsPlanAppServiceRepository) {
            _qPubBusinessSubsPlanAppServiceRepository = qPubBusinessSubsPlanAppServiceRepository;
        }

        #region Get Methods

        /// <summary>
        /// Get subscribed services by PlanId.
        /// </summary>
        /// <param name="pubBusSubsPlanId">PubBusinessSubsPlanId</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<ServiceInfoDTO>> GetPlanServicesByPlanIdAsync(Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPubBusinessSubsPlanAppServiceRepository.GetPlanServicesByPlanIdAsync(pubBusSubsPlanId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<List<SubsPlanServiceInfoDTO>> GetSubscriptionPlanServiceAndAttributeByPlanIdAsync(Guid pubBusSubsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            List<SubsPlanServiceInfoDTO> subsPlanServiceInfoDTOs = await _qPubBusinessSubsPlanAppServiceRepository.GetSubscriptionPlanServiceByPlanIdAsync(pubBusSubsPlanId, cancellationToken);
            List<SubsPlanServiceAttributeInfoDTO> subsPlanServiceAttributeInfoDTOs = await _qPubBusinessSubsPlanAppServiceRepository.GetSubscriptionPlanAttributeByPlanIdAsync(pubBusSubsPlanId, cancellationToken);

            foreach(SubsPlanServiceInfoDTO serviceInfoDTO in subsPlanServiceInfoDTOs) {
                serviceInfoDTO.ServiceAttributeList = subsPlanServiceAttributeInfoDTOs.FindAll(i => i.SubscriptionPlanServiceId == serviceInfoDTO.AppServiceId);
            }

            return subsPlanServiceInfoDTOs;
        }

        #endregion

    }
}
