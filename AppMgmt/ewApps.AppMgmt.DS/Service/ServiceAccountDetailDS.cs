/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 02 January 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 January 2019
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
    ///  This class implements standard business logic and operations for ServiceAttribute entity.
    /// </summary>
    public class ServiceAccountDetailDS:BaseDS<AppServiceAccountDetail>, IServiceAccountDetailDS {

        #region Local Member 
        IUnitOfWork _unitOfWork;
        IAppServiceAccountDetailRepository _serviceAccountDetailRepository;
        IUserSessionManager _sessionmanager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="serviceAccountDetailRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="cacheService"></param>
        public ServiceAccountDetailDS(IAppServiceAccountDetailRepository serviceAccountDetailRepository, IUnitOfWork unitOfWork, IUserSessionManager sessionmanager) : base(serviceAccountDetailRepository) {
          
            _unitOfWork = unitOfWork;
            _serviceAccountDetailRepository = serviceAccountDetailRepository;
            _sessionmanager = sessionmanager;
        }

        #endregion

        #region Add/Update/Delete

        public async Task UpdateAccountDetailJsonAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid entityId, string accountJson, CancellationToken token = default(CancellationToken)) {
            await _serviceAccountDetailRepository.UpdateAccountDetailJsonAsync(appId, appServiceId, attrbuteId, entityId, accountJson, token);

        }


        public async Task DeleteAppServiceAccountDetailAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid entityId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            await _serviceAccountDetailRepository.DeleteAppServiceAccountDetailAsync(appId, appServiceId, attrbuteId, entityId, tenantId, token);
        }

        public async Task<List<AppServiceAccDetailIdDTO>> DeleteByEntityTypeAndEntityAndServiceId(int entityType, Guid entityId, Guid appServiceId, Guid appServiceAttributeId, OperationType parentOpType) {
            List<AppServiceAccDetailIdDTO> appServiceAccDetailIdList = new List<AppServiceAccDetailIdDTO>();
            List<AppServiceAccountDetail> accountDetailList = await GetEntityByEntityTypeAndEntityAndServiceAndAttributeId(entityType, entityId, appServiceId, appServiceAttributeId);
            foreach(AppServiceAccountDetail accountDetail in accountDetailList) {
                AppServiceAccDetailIdDTO appServiceAccDetailId = new AppServiceAccDetailIdDTO();
                accountDetail.Deleted = true;
                UpdateSystemFieldsByOpType(accountDetail, OperationType.Update);
                Update(accountDetail, accountDetail.ID);
                //create model 
                appServiceAccDetailId.Id = accountDetail.ID;
                appServiceAccDetailId.SourceEntityId = entityId;
                appServiceAccDetailId.operationType = OperationType.Delete;
                appServiceAccDetailId.AccountJson = accountDetail.AccountJson;
                appServiceAccDetailId.ServiceId = appServiceId;
                appServiceAccDetailIdList.Add(appServiceAccDetailId);

            }
            if(parentOpType == OperationType.None) {
                _unitOfWork.Save();
            }
            return appServiceAccDetailIdList;
        }



        #endregion Add/Update/Delete

        public async Task<List<AppServiceAccountDetail>> GetEntityByEntityTypeAndEntityAndServiceAndAttributeId(int entityType, Guid entityId, Guid appServiceId, Guid appServiceAttributeId) {
            return await _serviceAccountDetailRepository.GetEntityByEntityTypeAndEntityAndServiceAndAttributeId(entityType, entityId, appServiceId, appServiceAttributeId, false);
        }
       

    }
}
