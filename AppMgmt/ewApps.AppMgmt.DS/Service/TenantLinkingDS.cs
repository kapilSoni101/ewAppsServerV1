/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 January 2019
 * 
 * Contributor/s: Ishwar Rathore
 * Last Updated On: 30 January 2019
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {


    /// <summary>
    /// Class provide supportinv method for tenantlinking.
    /// TenantLinking contains the relationship between Platform,Publisher, Business tenants.
    /// </summary>
    public class TenantLinkingDS:BaseDS<TenantLinking>, ITenantLinkingDS {

        #region Local Member 

        //IMapper _mapper;
        IUnitOfWork _unitOfWork;
        ITenantLinkingRepository _tenantLinkingRepository;
        IUserSessionManager _sessionmanager;
        ITenantDS _tenantDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="tenantLinking"></param>
        /// <param name="unitOfWork"></param>   
        /// <param name="cacheService"></param>
        public TenantLinkingDS(ITenantLinkingRepository tenantLinking, IUnitOfWork unitOfWork, IUserSessionManager sessionmanager, ITenantDS tenantDS) : base(tenantLinking) {

            _unitOfWork = unitOfWork;
            _tenantLinkingRepository = tenantLinking;
            _sessionmanager = sessionmanager;
            _tenantDS = tenantDS;
        }

        #endregion

        #region Get

        /// <summary>
        /// Get tenant linking by tenanttype and subdomain.
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TenantLinking> GetTenantLinkingBySubdomainAndTenantTypeAsync(string subdomain, TenantType tenantType, CancellationToken token = default(CancellationToken)) {
            return await _tenantLinkingRepository.GetTenantLinkingBySubdomainAndTenantTypeAsync(subdomain, tenantType, token);
        }

        /// <summary>
        ///  Get tenant linking by tenantid and tenanttype.
        /// </summary>    
        /// <param name="tenantId"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TenantLinking> GetTenantLinkingByTenantIdAndTypeAsync(Guid tenantId, TenantType tenantType, CancellationToken token = default(CancellationToken)) {
            return await _tenantLinkingRepository.GetTenantLinkingByTenantIdAsync(tenantId,tenantType, token);
        }

        /// <summary>
        ///  Get tenant linking by tenantid and tenanttype.
        /// </summary>    
        /// <param name="tenantId"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TenantInfoDTO> GetTenantByTenantIdAndTypeAsync(Guid tenantId, TenantType tenantType, CancellationToken token = default(CancellationToken)) {           
            TenantLinking tenantlinking =  await GetTenantLinkingByTenantIdAndTypeAsync(tenantId, tenantType, token);
            Tenant tenant = await _tenantDS.GetAsync(tenantId);
            TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();
            tenantInfoDTO.TenantName = tenant.Name;
            tenantInfoDTO.PlatformTenantId = tenantlinking.PlatformTenantId;
            tenantInfoDTO.BusinessTenantId = tenantlinking.BusinessTenantId;
            tenantInfoDTO.PublisherTenantId = tenantlinking.PublisherTenantId;
            tenantInfoDTO.BusinessPartnerTenantId = tenantlinking.BusinessPartnerTenantId;
            tenantInfoDTO.SubDomainName = tenant.SubDomainName;

            return tenantInfoDTO;
        }

        #endregion Get
    }
}
