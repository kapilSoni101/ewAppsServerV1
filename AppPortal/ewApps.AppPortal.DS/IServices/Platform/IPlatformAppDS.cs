/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on app entity by creating an object to this class.
    /// </summary>
    public interface IPlatformAppDS {

        /// <summary>
        /// Method return all the apps with its service count
        /// </summary>
        /// <returns></returns>
        Task<List<AppDetailDTO>> GetAppDetailsAsync();

        /// <summary>
        /// Updates the application asynchronous.
        /// </summary>
        /// <param name="appAndServiceDTO">The application and service dto.</param>
        /// <returns></returns>
        Task UpdateAppAsync(AppAndServiceDTO appAndServiceDTO);

        /// <summary>
        /// Gets the application details by application identifier asynchronous.
        /// </summary>
        /// <param name="AppId">The application identifier.</param>
        /// <returns></returns>
        Task<AppDetailDTO> GetAppDetailsByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application details with services asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<AppAndServiceDTO> GetAppDetailsWithServicesAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the publisher list by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IEnumerable<StringDTO>> GetPublisherListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application services details asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceDTO>> GetAppServiceNameAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the business name by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));


    }
}
