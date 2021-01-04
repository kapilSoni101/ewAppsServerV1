/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atmit mundra 
 * Date: 31 January 2019
 * 
 * Contributor/s: Amit Mundra
 * Last Updated On: 30 January 2019
 */


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// Class contains tenant/app service support methods.
    /// </summary>
    public interface IPubBusinessSubsPlanAppServiceRepository:IBaseRepository<PubBusinessSubsPlanAppService> {

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<PubBusinessSubsPlanAppService>> GetListPubBusinessSubsPlanAppService(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken));

    }
}
