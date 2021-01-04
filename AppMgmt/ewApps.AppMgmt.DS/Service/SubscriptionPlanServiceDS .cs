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
    public class SubscriptionPlanServiceDS:BaseDS<SubscriptionPlanService>, ISubscriptionPlanServiceDS {

        #region Constructor

        IUnitOfWork _unitOfWork;
        ISubscriptionPlanServiceRepository _subscriptionPlanServRepository;
        IUserSessionManager _userSessionManager;
        ISubscriptionPlanServiceAttributeDS _subscriptionPlanServiceAttributeDS;

        /// <summary>
        /// Constructor initialing all the mapper, unitofwork, and repositry methods.
        /// </summary>
        public SubscriptionPlanServiceDS(ISubscriptionPlanServiceRepository subscriptionPlanServRepository, IUserSessionManager userSessionManager,
          IUnitOfWork unitOfWork, ISubscriptionPlanServiceAttributeDS subscriptionPlanServiceAttributeDS) : base(subscriptionPlanServRepository) {

            _unitOfWork = unitOfWork;
            _subscriptionPlanServRepository = subscriptionPlanServRepository;
            _userSessionManager = userSessionManager;
            _subscriptionPlanServiceAttributeDS = subscriptionPlanServiceAttributeDS;
        }

        #endregion Constructor    

        #region Add/Update/Delete

        /// <inheritdoc/>
        public async Task AddSubscriptionPlanServiceAndAttribute(List<AppServiceDTO> appServList, Guid subPlanId, CancellationToken token = new CancellationToken()) {

            foreach(AppServiceDTO serviceDTO in appServList) {

                // Add only checked services
                if(serviceDTO.Active) {
                    SubscriptionPlanService entity = new SubscriptionPlanService();
                    SubscriptionPlanService newlyGeneratedEntity = new SubscriptionPlanService();

                    // map to entity.
                    entity = AppServiceDTO.MapToSubscriptionPlanService(serviceDTO, entity, subPlanId);

                    // Update sys fields.
                    UpdateSystemFieldsByOpType(entity, OperationType.Add);

                    // Add in DB
                    newlyGeneratedEntity = await _subscriptionPlanServRepository.AddAsync(entity);

                    // Now add attributes for the same service.
                    await _subscriptionPlanServiceAttributeDS.AddSubscriptionPlanAttribute(serviceDTO.AppServiceAttributeList, subPlanId, newlyGeneratedEntity.ID, token);
                }
            }
        }

        /// <inheritdoc/>
        public async Task UpdateAndAddSubscribedServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken()) {
            // first get all the existing services and attributes, and delete them.
            List<SubsPlanServiceInfoDTO> planServiceAndAttributes = await GetPlanServiceAndAttributeListByPlanIdsAsync(new List<Guid>() { addUdpateDTO.ID }, token);

            SubscriptionPlanServiceAttribute attEntity = null;
            SubscriptionPlanService serviceEntity = null;

            try {

                for(int i = 0; i < planServiceAndAttributes.Count; i++) {

                    for(int j = 0; j < planServiceAndAttributes[i].ServiceAttributeList.Count; j++) {

                        attEntity = await _subscriptionPlanServiceAttributeDS.GetAsync(planServiceAndAttributes[i].ServiceAttributeList[j].SubsPlanServiceAttributeId);

                        // update system fields value
                        _subscriptionPlanServiceAttributeDS.UpdateSystemFieldsByOpType(attEntity, OperationType.Update);
                        attEntity.Deleted = true;

                        await _subscriptionPlanServiceAttributeDS.UpdateAsync(attEntity, attEntity.ID, token);
                    }

                    // Get entity for service now.
                    serviceEntity = await _subscriptionPlanServRepository.GetAsync(planServiceAndAttributes[i].SubscriptionPlanServiceId);

                    // update sys fields.
                    UpdateSystemFieldsByOpType(serviceEntity, OperationType.Update);
                    serviceEntity.Deleted = true;

                    // update entity now
                    await _subscriptionPlanServRepository.UpdateAsync(serviceEntity, serviceEntity.ID, token);

                }
                // Now add new services and attributes.
                await AddSubscriptionPlanServiceAndAttribute(addUdpateDTO.Services, addUdpateDTO.ID, token);
            }
            catch(Exception ex) {

            }

        }

        #endregion Add/Update/Delete

        #region GetMethods

        /// <inheritdoc/>
        public async Task<List<SubsPlanServiceInfoDTO>> GetPlanServiceAndAttributeListByPlanIdsAsync(List<Guid> planIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            List<SubsPlanServiceInfoDTO> subsPlanServiceInfoDTOs = new List<SubsPlanServiceInfoDTO>();
            List<SubsPlanServiceAttributeInfoDTO> subsPlanServiceAttributeInfoDTOs = new List<SubsPlanServiceAttributeInfoDTO>();

            subsPlanServiceInfoDTOs = await _subscriptionPlanServRepository.GetPlanServiceListByPlanIdsAsync(planIdList, cancellationToken);

            subsPlanServiceAttributeInfoDTOs = await _subscriptionPlanServiceAttributeDS.GetPlanServiceAttributeListByPlanIdsAsync(planIdList, cancellationToken);
            //try {
                foreach(SubsPlanServiceInfoDTO subsPlanServiceInfo in subsPlanServiceInfoDTOs) {
                    subsPlanServiceInfo.ServiceAttributeList = subsPlanServiceAttributeInfoDTOs.FindAll(i => i.SubscriptionPlanServiceId == subsPlanServiceInfo.SubscriptionPlanServiceId);
                }
            //}

            //catch(Exception ee) {

            //}
            return subsPlanServiceInfoDTOs;

        }

        /// <inheritdoc/>
        public async Task<List<SubsPlanServiceInfoDTO>> GetPlanServiceAndAttributeListByPlanIdAsync(Guid planId, CancellationToken cancellationToken = default(CancellationToken)) {
            List<Guid> planIdList = new List<Guid>() { planId };
            List<SubsPlanServiceInfoDTO> subscriptionPlanList = await GetPlanServiceAndAttributeListByPlanIdsAsync(planIdList, cancellationToken);
            return subscriptionPlanList;
        }

        #endregion GetMethods

    }
}
