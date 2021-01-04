/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {

    public interface IERPConnectorConfigRepository:IBaseRepository<ERPConnectorConfig> {

        #region Get

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="token"></param>
        /// <returns>return list of application.</returns>
        Task<List<ERPConnectorConfigDQ>> GetBusinessAppConnectorConfigByBusinessIdAsync(Guid businessId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business detail by tenantId And AppId.
        /// </summary>
        /// <param name="tenantId">Id of Business Tenant</param>
        /// <param name="appId">Id of Business Application</param>
        /// <param name="token"></param>
        Task<ERPConnectorConfig> GetConnectorConfigByTenantIdAndAppIdAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));        

        #endregion Get

    }
}
