
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 2 January 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 2 January 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This is the repository responsible to perform CRUD operations ons ervice entity.
    /// </summary>
    public interface IAppServiceAttributeRepository:IBaseRepository<AppServiceAttribute> {

        /// <summary>
        /// Get the Appservice attribute list by service id.
        /// </summary>
        /// <param name="appServiceId">ServiceId</param>
        /// <param name="token"></param>
        Task<List<AppServiceAttribute>> GetAppServiceAttributeByAppServiceAsync(Guid appServiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application service attribute list by service identifier asynchronous.
        /// </summary>
        /// <param name="appServiceId">The application service identifier.</param>
        /// <param name="skipDeleted">if set to <c>true</c> [skip deleted].</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceAttribute>> GetAppServiceAttributeListByServiceIdAsync(Guid appServiceId, bool skipDeleted, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application service attribute list by service identifier asynchronous.
        /// </summary>
        /// <param name="appServiceId">The application service identifier.</param>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceAttributeDTO>> GetAppServiceAttributeListByServiceIdAsync(Guid appServiceId, bool onlyActive, bool includeDeleted, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Gets the application service attribute asynchronous.
        /// </summary>
        /// <param name="pubBusinessSubsPlanAppServiceDTO">The pub business subs plan application service dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceAttribute>> GetAppServiceAttributeListAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken));

  }
}
