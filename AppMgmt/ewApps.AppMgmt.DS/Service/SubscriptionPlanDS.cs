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
using ewApps.Core.ExceptionService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// This class provides all the datservice methods for the subscription plans.
    /// </summary>
    public class SubscriptionPlanDS:BaseDS<SubscriptionPlan>, ISubscriptionPlanDS {

        #region Constructor

        IUniqueIdentityGeneratorDS _uniqueIdentityGeneratorDS;
        IUnitOfWork _unitOfWork;
        ISubscriptionPlanRepository _subscriptionPlanRepository;
        IUserSessionManager _userSessionManager;
        ISubscriptionPlanServiceDS _subscriptionPlanServiceDS;
        IAppServiceDS _appServiceDS;
        ISubscriptionPlanServiceAttributeDS _subscriptionPlanServiceAttributeDS;
        IQSubscriptionPlanDS _qSubPlanDS;

        /// <summary>
        /// Constructor initialing all the mapper, unitofwork, and repositry methods.
        /// </summary>
        /// <param name="subscriptionPlanRep">sub plan ref </param>
        /// <param name="appServiceDS">appservice plan DS reference</param>
        /// <param name="subscriptionPlanServiceAttributeDS">plan attribute class reference</param>
        /// <param name="userSessionManager">Session manager class reference</param>
        /// <param name="unitOfWork">unit of work class reference</param>
        /// <param name="uniqueIdentityGeneratorDS">identity class generator  class reference</param>
        /// <param name="subscriptionPlanServiceDS">plan service  class reference</param>
        public SubscriptionPlanDS(ISubscriptionPlanRepository subscriptionPlanRep, IAppServiceDS appServiceDS, IQSubscriptionPlanDS qSubPlanDS,
            ISubscriptionPlanServiceAttributeDS subscriptionPlanServiceAttributeDS, IUserSessionManager userSessionManager,
            IUnitOfWork unitOfWork, IUniqueIdentityGeneratorDS uniqueIdentityGeneratorDS, ISubscriptionPlanServiceDS subscriptionPlanServiceDS) : base(subscriptionPlanRep) {
            _unitOfWork = unitOfWork;
            _subscriptionPlanRepository = subscriptionPlanRep;
            _userSessionManager = userSessionManager;
            _uniqueIdentityGeneratorDS = uniqueIdentityGeneratorDS;
            _subscriptionPlanServiceDS = subscriptionPlanServiceDS;
            _appServiceDS = appServiceDS;
            _subscriptionPlanServiceAttributeDS = subscriptionPlanServiceAttributeDS;
            _qSubPlanDS = qSubPlanDS;
        }

        #endregion Constructor    

        #region Get

        /// <inheritdoc/>    
        public async Task<SubscriptionPlan> GetSubscriptionPlansByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _subscriptionPlanRepository.GetSubscriptionPlansByAppIdAsync(appId, token);
        }

        /// <inheritdoc/>    
        public async Task<List<TenantApplicationSubscriptionDTO>> GetAppSubscriptionAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            List<SubscriptionPlan> listSub = await _subscriptionPlanRepository.GetAppSubscriptionAsync(appId, publisherTenantId, token);
            List<TenantApplicationSubscriptionDTO> listSubDTO = new List<TenantApplicationSubscriptionDTO>();
            TenantApplicationSubscriptionDTO dto;
            for(int i = 0; i < listSub.Count; i++) {
                dto = TenantApplicationSubscriptionDTO.MapEntityToTenantApplicationSubscriptionDTO(listSub[i]);
                listSubDTO.Add(dto);
            }

            return listSubDTO;
        }

        /// <inheritdoc/>       
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid appId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _subscriptionPlanRepository.GetSubscriptionPlanListByAppIdAsync(appId, planState, cancellationToken);
        }

        /// <inheritdoc/>       
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPlanIdListAsync(List<Guid> planIdList, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            List<SubscriptionPlanInfoDTO> subscriptionPlanInfoDTOs = new List<SubscriptionPlanInfoDTO>();
            bool planStateValue = Boolean.Parse(planState.ToString());
            foreach(Guid planId in planIdList) {
                SubscriptionPlan subscriptionPlan = await _subscriptionPlanRepository.FindAsync(i => i.ID == planId && i.Active == planStateValue && i.Deleted == false, cancellationToken);
                subscriptionPlanInfoDTOs.Add(SubscriptionPlanInfoDTO.MapFromSuscriptionPlan(subscriptionPlan));
            }
            return subscriptionPlanInfoDTOs;
        }

        /// <inheritdoc/>       
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();

            return await _subscriptionPlanRepository.GetSubscriptionPlanListByTenantIdAsync(session.TenantId, planState, cancellationToken);
        }

        /// <inheritdoc/>       
        public async Task<SubscriptionPlanInfoDTO> GetSubscriptionPlaninfoByIdAsync(Guid planId, CancellationToken cancellationToken = default(CancellationToken)) {
            // get plan info first.
            SubscriptionPlanInfoDTO infoDTO = await _subscriptionPlanRepository.GetSubscriptionPlaninfoByIdAsync(planId, cancellationToken);

            // Now get services and attribute related to plan.
            infoDTO.ServiceInfoDTO = await _subscriptionPlanServiceDS.GetPlanServiceAndAttributeListByPlanIdsAsync(new List<Guid>(new List<Guid>() { planId }), cancellationToken);

            return infoDTO;
        }


        /// <inheritdoc/>       
        /// <inheritdoc/>       
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken)) {
            //UserSession session = _userSessionManager.GetSession();
            return await _qSubPlanDS.GetSubscriptionPlanListByPubTenantIdAsync(planState, cancellationToken);
        }

        public async Task<IEnumerable<string>> GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(Guid subsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            return   _subscriptionPlanRepository.GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(subsPlanId, cancellationToken);
        }

        #endregion Get

        #region Add/Update/Delete

        /// <inheritdoc/>
        public async Task<Guid> AddSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken()) {
            UserSession session = _userSessionManager.GetSession();

            SubscriptionPlan entity = new SubscriptionPlan();

            // Map to plan entity;
            entity = SubscriptionAddUpdateDTO.MapToEntity(addUdpateDTO, entity);

            // update system fiels
            UpdateSystemFieldsByOpType(entity, OperationType.Add);
            SubscriptionPlan newlyGeneratedEntity = null;

            try {
                // Generate identity number.
                entity.IdentityNumber = _uniqueIdentityGeneratorDS.GetIdentityNo(session.TenantId, 1, "Subs", 7000).ToString();

                // Add in DB.            
                newlyGeneratedEntity = await _subscriptionPlanRepository.AddAsync(entity, token);

                // add service and attribute.
                if(addUdpateDTO.Services != null)
                    await _subscriptionPlanServiceDS.AddSubscriptionPlanServiceAndAttribute(addUdpateDTO.Services, newlyGeneratedEntity.ID, token);

                // SaveAsync();
                _unitOfWork.SaveAll();
            }

            catch(Exception ex) {

            }

            return newlyGeneratedEntity.ID;
        }

        /// <inheritdoc/>
        public async Task UpdateSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken()) {
            UserSession session = _userSessionManager.GetSession();

            SubscriptionPlan entity = await _subscriptionPlanRepository.GetAsync(addUdpateDTO.ID, token);

            // Map to plan entity;
            entity = SubscriptionAddUpdateDTO.MapToEntity(addUdpateDTO, entity);

            // update system fiels
            UpdateSystemFieldsByOpType(entity, OperationType.Update);

            try {
                // update plan in DB.            
                await _subscriptionPlanRepository.UpdateAsync(entity, entity.ID, token);

                // add service and attribute.
                //await _subscriptionPlanServiceDS.UpdateAndAddSubscribedServiceAttributeAsync(addUdpateDTO, token);

                // SaveAsync();
                _unitOfWork.SaveAll();
            }

            catch(Exception ex) {
                throw;
            }

        }

        /// <inheritdoc/>
        public async Task DeletePlanByPlanId(Guid planId) {
            try {

                // First check the referece of this plan.
                if(await _qSubPlanDS.GetSubscriptionPlanReferece(planId) != null) {
                    //throw new EwpValidationException("Plan references exists");

                    EwpError error = new EwpError();
                    error.ErrorType = ErrorType.InvalidFieldReference;
                    EwpErrorData errorData = new EwpErrorData();
                    errorData.ErrorSubType = (int)InvalidFieldReferenceErrorSubType.ReferenceExists;
                    errorData.Message = "Subscription plan reference exist";
                    error.EwpErrorDataList.Add(errorData);
                    EwpInvalidFieldReferenceException exc = new EwpInvalidFieldReferenceException(errorData.Message);
                    throw exc;
                }

                // Get plan entity first.
                SubscriptionPlan entity = await GetAsync(planId);

                if(entity != null) {
                    entity.Deleted = true;
                    UpdateSystemFieldsByOpType(entity, OperationType.Update);
                    await _subscriptionPlanRepository.UpdateAsync(entity, planId);

                    // first get all the existing services and attributes, and delete them.
                    List<SubsPlanServiceInfoDTO> planServiceAndAttributes = await _subscriptionPlanServiceDS.GetPlanServiceAndAttributeListByPlanIdsAsync(new List<Guid>() { planId });

                    SubscriptionPlanServiceAttribute attEntity = null;
                    SubscriptionPlanService serviceEntity = null;

                    for(int i = 0; i < planServiceAndAttributes.Count; i++) {

                        for(int j = 0; j < planServiceAndAttributes[i].ServiceAttributeList.Count; j++) {

                            attEntity = await _subscriptionPlanServiceAttributeDS.GetAsync(planServiceAndAttributes[i].ServiceAttributeList[j].SubsPlanServiceAttributeId);

                            // update system fields value
                            _subscriptionPlanServiceAttributeDS.UpdateSystemFieldsByOpType(attEntity, OperationType.Update);
                            attEntity.Deleted = true;

                            await _subscriptionPlanServiceAttributeDS.UpdateAsync(attEntity, attEntity.ID);
                        }

                        // Get entity for service now.
                        serviceEntity = await _subscriptionPlanServiceDS.GetAsync(planServiceAndAttributes[i].SubscriptionPlanServiceId);

                        // update sys fields.
                        _subscriptionPlanServiceDS.UpdateSystemFieldsByOpType(serviceEntity, OperationType.Update);
                        serviceEntity.Deleted = true;

                        // update entity now
                        await _subscriptionPlanServiceDS.UpdateAsync(serviceEntity, serviceEntity.ID);
                    }

                    _unitOfWork.SaveAll();

                }
            }

            catch(Exception ex) {
                throw;
            }

        }

        #endregion Add/Update/Delete

    }


}

