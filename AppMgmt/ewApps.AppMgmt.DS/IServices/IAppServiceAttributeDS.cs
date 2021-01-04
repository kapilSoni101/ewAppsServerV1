/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on app entity by creating an object to this class.
    /// </summary>
    public interface IAppServiceAttributeDS:IBaseDS<AppServiceAttribute> {

        #region Get

        /// <summary>
        /// Get the Appservice attribute list by service id.
        /// </summary>
        /// <param name="appServiceId">ServiceId</param>
        /// <param name="token"></param>
        Task<List<AppServiceAttributeDTO>> GetAppServiceAttributeByAppServiceAsync(Guid appServiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application service attribute list by service identifier asynchronous.
        /// </summary>
        /// <param name="appServiceId">The application service identifier.</param>
        /// <param name="skipDeleted">if set to <c>true</c> [skip deleted].</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceAttribute>> GetAppServiceAttributeListByServiceIdAsync(Guid appServiceId, bool skipDeleted, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application service attribute list by service identifier.
        /// </summary>
        /// <param name="appServiceId">The application service identifier.</param>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <param name="skipDeleted">if set to <c>true</c> [skip deleted].</param>
        /// <returns></returns>
        List<AppServiceAttributeDTO> GetAppServiceAttributeListByServiceId(Guid appServiceId, bool onlyActive, bool skipDeleted, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application service attribute list asynchronous.
        /// </summary>
        /// <param name="pubBusinessSubsPlanAppServiceDTO">The pub business subs plan application service dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceAttribute>> GetAppServiceAttributeListAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken));

        #endregion Get

    }
}
