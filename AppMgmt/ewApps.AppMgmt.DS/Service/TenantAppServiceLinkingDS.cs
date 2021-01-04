
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
using ewApps.Core.UserSessionService;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Data;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    ///  This class implements standard business logic and operations for class TenantAppServiceLinking entity.
    /// </summary>
    public class TenantAppServiceLinkingDS:BaseDS<TenantAppServiceLinking>, ITenantAppServiceLinkingDS {

        #region Local Member 

        //IMapper _mapper;
        //IUnitOfWork _unitOfWork;
        ITenantAppServiceLinkingRepository _tenantAppServiceLinkingRepository;
        //IUserSessionManager _sessionmanager;
        //IServiceAccountDetailDS _appSrviceAccountDtl;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="tenantAppServiceLinkingRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="cacheService"></param>
        public TenantAppServiceLinkingDS(ITenantAppServiceLinkingRepository tenantAppServiceLinkingRepository, IUnitOfWork unitOfWork, IUserSessionManager sessionmanager) : base(tenantAppServiceLinkingRepository) {
            //_mapper = mapper;
            //_unitOfWork = unitOfWork;
            _tenantAppServiceLinkingRepository = tenantAppServiceLinkingRepository;
            //_sessionmanager = sessionmanager;
            //_appSrviceAccountDtl = appSrviceAccountDtl;
        }

        #endregion

        #region Get

        /// <inheritdoc></inheritdoc>
        //public async Task<List<TenantAppServiceDQ>> GetAppServiceByTenantIdAsync(Guid businessId, CancellationToken token = default(CancellationToken)) {
        //    return await _tenantAppServiceLinkingRepository.GetAppServiceByTenantIdAsync(businessId, token);
        //}


        /// <inheritdoc></inheritdoc>
        public async Task<List<TenantAppServiceLinking>> GetAppServiceByTenantIdAndAppIdAsync(Guid businessId, Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _tenantAppServiceLinkingRepository.GetAppServiceByTenantIdAndAppIdAsync(businessId, appId, token);
        }

        /// <summary>
        /// Get application sub services subscribed by a Tenant applications.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="entityTypeEnum">Entity type</param>
        /// <param name="token"></param>
        /// <returns>return Guid of application sub services and Application id.</returns>
        public async Task<List<TenantAppServiceDQ>> GetAppServiceByBusinessIdAsync(Guid businessId, int entityTypeEnum, CancellationToken token = default(CancellationToken)) {
            return await _tenantAppServiceLinkingRepository.GetAppServiceByBusinessIdAsync(businessId, entityTypeEnum, token);
        }

        public async Task<List<TenantAppServiceLinking>> GetEntityByAppAndTenantId(Guid appId, Guid tenantId, bool includeDeleted) {
            return await _tenantAppServiceLinkingRepository.GetEntityByAppAndTenantId(appId, tenantId, includeDeleted);
        }
        #endregion Get

        #region Delete

        /// <summary>
        /// Delete appservice linking and attribute.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appServiceId"></param>
        /// <param name="attrbuteId"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        //public async Task DeleteAppServiceLinkingByAppServiceAndAttributeAsync(Guid appId, Guid appServiceId, Guid attrbuteId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
        //    await _tenantAppServiceLinkingRepository.DeleteAppServiceLinkingByAppServiceAndAttributeAsync(appId, appServiceId, attrbuteId, tenantId, token);
        //}

        /// <summary>
        /// Delete appservice linking and attribute.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appServiceId"></param>        
        /// <param name="tenantId"></param>
        /// <returns></returns>
        //public async Task DeleteAppServiceLinkingByAppServiceAndAttributeAsync(Guid appId, Guid appServiceId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
        //    await _tenantAppServiceLinkingRepository.DeleteAppServiceLinkingByAppServiceAndAttributeAsync(appId, appServiceId, tenantId, token);
        //}

        #endregion Delete

    }
}
