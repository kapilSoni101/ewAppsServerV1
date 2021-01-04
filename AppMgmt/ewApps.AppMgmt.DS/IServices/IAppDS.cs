/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 08 Aug -2019
 * 
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on app entity by creating an object to this class.
    /// </summary>
    public interface IAppDS:IBaseDS<App> {

        Task<List<AppDetailDTO>> GetAppDetailsAsync();

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

        /// <summary>
        /// Method returns the app info list for the padssed appId list.
        /// </summary>
        /// <param name="appIdList">appid list</param>
        /// <returns></returns>
        Task<List<AppInfoDTO>> GetAppInfoListByAppIdListAsync(List<Guid> appIdList);

        /// <summary>
        /// Gets the application information list by application key list asynchronous.
        /// </summary>
        /// <param name="appKeyList">The application key list.</param>
        /// <returns></returns>
        Task<List<AppInfoDTO>> GetAppInfoListByAppKeyListAsync(List<string> appKeyList);


        /// <summary>
        /// Updates the application and service asynchronous.
        /// </summary>
        /// <param name="appAndServiceDTO">The application and service dto.</param>
        /// <returns></returns>
        Task UpdateAppAndServiceAsync(AppAndServiceDTO appAndServiceDTO);

        /// <summary>
        /// Gets the application and service details by application identifier.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <returns></returns>
        Task<AppAndServiceDTO> GetAppAndServiceDetailsByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application list that matches input parameters.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active] only active applications will fetch.</param>
        /// <param name="subscriptionMode">The subscription mode to filter applications.</param>
        /// <returns>Returns application list that matches given input parameters.</returns>
        Task<List<AppInfoDTO>> GetApplicationListAsync(  bool active, AppSubscriptionModeEnum subscriptionMode);

        /// <summary>
        /// Gets the application service by application identifier publisher asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceDTO>> GetAppServiceAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Gets the business name by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the service name by application identifier asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceDTO>> GetServiceNameByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the business name by application identifier plat asynchronous.
        /// </summary>
        /// <param name="appId">The application identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<string>> GetBusinessNameByAppIdPlatAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the application service attribute list asynchronous.
        /// </summary>
        /// <param name="pubBusinessSubsPlanAppServiceDTO">The pub business subs plan application service dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppServiceDTO>> GetAppServiceAttributeListAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken));

    }
}
