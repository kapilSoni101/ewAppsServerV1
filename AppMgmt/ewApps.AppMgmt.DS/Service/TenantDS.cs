/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: 
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
using ewApps.Core.UniqueIdentityGeneratorService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Class will rsgiter the business tenant with all child related entities.
    /// Business tenant may subscribe to muliple application in portal.
    /// 1) Add/update the tenant.
    /// 2) Add application.
    /// 3) Add application admin user.
    /// 4) Add application subscribe information.
    /// 5) send email to invited application user.    
    /// 6) Validating Tenant and its child entity.
    /// 7) Validating tenant entity.
    /// </summary>
    public class TenantDS:BaseDS<Tenant>, ITenantDS {

        #region Local Members

        ITenantRepository _tenantRepository;
       // IUniqueIdentityGeneratorDS _identityDS;

        #endregion local Members

        #region Constructor       

        /// <summary>
        /// Constructor with all required services and repo classes.
        /// </summary>
        public TenantDS(ITenantRepository tenantRepository) : base(tenantRepository) {
            _tenantRepository = tenantRepository;
           // _identityDS = identityDS;
        }

        #endregion Constructor 

        #region Public Methods

        /// <summary>
        /// return true if domain name already exist.
        /// </summary>
        /// <param name="subdomain">Domain name for tenant.</param>
        /// <param name="tenantId">ID of tenant.</param>
        /// <returns></returns>
        public async Task<bool> IsSubdomainExistAsync(string subdomain, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRepository.IsSubdomainExistAsync(subdomain, tenantId, token);
        }

        ///<inheritdoc/>
        public async Task<Tenant> AddTenantAsync(Tenant tenant) {
            // UpdateSystemFieldsByOpType(tenant, OperationType.Add);
            // tenant.IdentityNumber = GetNextMaxNo(tenant.ID, (int)CoreEntityTypeEnum.Tenant);
            await AddAsync(tenant);
            return tenant;
        }

        ///<inheritdoc/>
        public async Task<TenantInfoDTO> GetTenantInfoByTenantIdAsync(Guid tenantId, int uType, CancellationToken token = default(CancellationToken)) {
            return await _tenantRepository.GetTenantInfoByTenantIdAsync(tenantId, uType, token);
        }

        ///<inheritdoc/>
        public async Task<TenantInfoDTO> GetTenantInfoBySubdomainAsync(string subdomain , int uType, CancellationToken token = default(CancellationToken)) {
            return await _tenantRepository.GetTenantInfoBySubdomainAsync(subdomain, uType, token);
        }

        #endregion Public Methods

        #region Get

        /// <summary>
        /// Get tenant model by tenant id.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UpdateTenantModelDQ> GetBusinessTenantDetailModelDTOAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRepository.GetBusinessTenantDetailModelDTOAsync(tenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            List<string> businessList = await _tenantRepository.GetBusinessNameByAppIdAsync(appId, publisherTenantId, token);
            return businessList;
        }

        ///<inheritdoc/>
        public async Task<List<string>> GetBusinessNameByAppIdPlatAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<string> businessList = await _tenantRepository.GetBusinessNameByAppIdPlatAsync(appId,  token);
            return businessList;
        }

        #endregion Get

        ///<inheritdoc/>
        public string GetNextMaxNo(Guid tenantId, int entityType) {
            //return null;
            //int identityNumber = _identityDS.GetIdentityNo(Guid.Empty, entityType, "TNT", 100001);
            //return "TNT" + Convert.ToString(identityNumber);
            return "";
        }

        ///<inheritdoc/>
        public async Task<TenantInfoDTO> GetTenantByIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            Tenant tenant = await _tenantRepository.GetAsync(tenantId);
            TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();
            tenantInfoDTO.TenantName = tenant.Name;
            tenantInfoDTO.SubDomainName = tenant.SubDomainName;
            tenantInfoDTO.Active = tenant.Active;
            tenantInfoDTO.Deleted = tenant.Deleted;
            return  tenantInfoDTO;
        }

    }
}
