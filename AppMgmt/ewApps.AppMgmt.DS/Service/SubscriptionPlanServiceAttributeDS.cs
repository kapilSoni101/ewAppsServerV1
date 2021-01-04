/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {
    /// <summary>
    /// This class provides all the datservice methods for the subscription plans.
    /// </summary>
    public class SubscriptionPlanServiceAttributeDS:BaseDS<SubscriptionPlanServiceAttribute>, ISubscriptionPlanServiceAttributeDS {

        #region Constructor

        IUnitOfWork _unitOfWork;
        ISubscriptionPlanServiceAttributeRepository _subscriptionPlanservAttRepository;
        IUserSessionManager _userSessionManager;

        /// <summary>
        /// Constructor initialing all the mapper, unitofwork, and repositry methods.
        public SubscriptionPlanServiceAttributeDS(ISubscriptionPlanServiceAttributeRepository subscriptionPlanservAttRepository,
    IUserSessionManager userSessionManager,
          IUnitOfWork unitOfWork) : base(subscriptionPlanservAttRepository) {

            _unitOfWork = unitOfWork;
            _subscriptionPlanservAttRepository = subscriptionPlanservAttRepository;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor    

        #region Add/Update/Delete Methods

    /// <inheritdoc/>
    public async Task AddSubscriptionPlanAttribute(List<AppServiceAttributeDTO> attrListDTO, Guid subPlanId, Guid subPlanServiceId, CancellationToken token = new CancellationToken()) {

      foreach (AppServiceAttributeDTO attributeDTO in attrListDTO) {

        // Map only checked attribute 
        if (attributeDTO.Active) {
          SubscriptionPlanServiceAttribute entity = new SubscriptionPlanServiceAttribute();
          SubscriptionPlanServiceAttribute newlyGeneratedEntity = new SubscriptionPlanServiceAttribute();

          // map to entity.
          entity = AppServiceAttributeDTO.MapToSubscriptionPlanServiceAttribute(attributeDTO, entity, subPlanId, subPlanServiceId);

          // Update sys fields.
          UpdateSystemFieldsByOpType(entity, OperationType.Add);

          // Add in DB
          newlyGeneratedEntity = await _subscriptionPlanservAttRepository.AddAsync(entity);

        }
      }
    }

        #endregion Add/Update/Delete Methods

        public async Task<List<SubsPlanServiceAttributeInfoDTO>> GetPlanServiceAttributeListByPlanIdsAsync(List<Guid> planIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _subscriptionPlanservAttRepository.GetPlanServiceAttributeListByPlanIdsAsync(planIdList, cancellationToken);
        }


    }
}
