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
using System.Threading;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
namespace ewApps.AppMgmt.Data {
    public interface IAppServiceRepository:IBaseRepository<AppService> {

        #region Get

        /// <summary>
        /// Get appservices by application ID.
        /// </summary>
        /// <param name="appId">Application id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<AppService>> GetAppServicesDetailsAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application service list by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="onlyActive">if set to <c>true</c> [only active].</param>
        /// <param name="includeDeleted">if set to <c>true</c> [include deleted].</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceDTO>> GetAppServiceListByAppIdAsync(Guid appId, bool onlyActive, bool includeDeleted, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application service asynchronous.
        /// </summary>
        /// <param name="pubBusinessSubsPlanAppServiceDTO">The pub business subs plan application service dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppService>> GetAppServiceAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken));

        #endregion Get

    }
}
