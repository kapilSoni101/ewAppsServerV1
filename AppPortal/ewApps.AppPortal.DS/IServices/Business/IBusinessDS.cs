/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 08 Aug -2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// Provide functionality to write bussiness logic on business entity by creating an object to this class.
    /// </summary>
    public interface IBusinessDS:IBaseDS<Business> {

        /// <summary>
        /// Get business by tenantid.
        /// </summary>
        /// <returns></returns>
        Task<Business> GetBusinessByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business model by tenant id.
        /// </summary>
        /// <param name="tenantId">Unique tenant id.</param>
        /// <param name="token"></param>
        /// <returns>return detail business tenant model.</returns>
        Task<UpdateBusinessTenantModelDQ> GetBusinessTenantDetailModelDTOAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get publisher branding details by Tenantid and AppId.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BusinessBrandingDQ> GetBusinessBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Theme name theme key 
        /// </summary>
        /// <returns></returns>
        Task<List<ThemeResponseDTO>> GetThemeNameAndThemeKey();

        /// <summary>
        /// Update Business branding
        /// </summary>
        /// <param name="businessBrandingDQ"></param>
        Task UpdateBusinessBranding(BusinessBrandingDQ businessBrandingDQ);

        Task UpdateBusinessStatus(BusinessStatusDTO businessStatusDTO);

        /// <summary>
        /// Update Business From SAP
        /// </summary>
        /// <param name="businessSyncDTO"></param>
        Task SyncERPBusiness(BusinessSyncDTO businessSyncDTO);

    }
}
