/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 10 February 2019
 * 
 * Contributor/s: Hari Dudani.
* Last Updated On: 10 February 2019
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.AppDeeplinkService {
    /// <summary>
    /// Contains all AppDeeplink service manager class method.
    /// </summary>
    public interface IAppDeeplinkManager {

        /// <summary>
        /// Generate the deeplink with all necessary payload information.
        /// Url query endparam contains all necessary information(payload) to access the page and required data.
        /// </summary>
        /// <param name="appDeeplinkPayloadDTO"></param>
        /// <param name="useShortUrl"></param>
        /// <returns></returns>
        Task<string> GeneratingDeeplink(AppDeeplinkPayloadDTO appDeeplinkPayloadDTO, bool useShortUrl, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get deep link detail from short url.
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        Task<AppDeeplinkPayloadDTO> GetDeeplinkAccessAsync(string shortUrl, string accessMachineIpAddress, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Method will expire the deeplink.
        /// </summary>
        /// <param name="shortUrl">Unique short url key</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ExpireDeepLink(string shortUrl, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Method will expire the deeplink.
        /// </summary>
        /// <param name="tenantId">Id of tenant.</param>
        /// <param name="entityId">Action data enitytid.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ExpireDeepLink(Guid tenantId, Guid entityId, CancellationToken token = default(CancellationToken));
    }
}
