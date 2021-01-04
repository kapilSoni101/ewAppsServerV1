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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This is the repository responsible for filtering data realted to Applciation and services related to it
    /// </summary>
    public interface IAppRepository:IBaseRepository<App> {

        /// <summary>
        ///  Get application from appkey.
        /// </summary>
        Task<App> GetAppByAppKeyAsync(string appKey, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///  Get tenant settings by subdomain name and tenanttype.
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<AppSettingDQ>> GetAppSettingByTenantTypeAndSubdomainAsync(string subdomain, int tenantType, CancellationToken token = default(CancellationToken));

        Task<List<AppDetailDTO>> GetAppDetailsAsync();

    }
}
