/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 31 August 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Business class contains all add/update/delete/get methods for Business.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PubAppController:ControllerBase {

        #region Local variables

        IPublisherAppDS _publisherAppDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize local variables. 
        /// </summary>
        /// <param name="publisherAppDS"></param>
        public PubAppController(IPublisherAppDS publisherAppDS) {
            _publisherAppDS = publisherAppDS;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get all the apps with service count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{publisherTenantId}")]
        public async Task<IEnumerable<AppDetailDTO>> GetAppDetailsPublisherAsync([FromRoute] Guid publisherTenantId) {
            return await _publisherAppDS.GetAppDetailsPublisherAsync(publisherTenantId);
        }

        /// <summary>
        /// Get app details and services for the app by AppId and PublisherAppID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("appdetails/{publisherAppSettingID}")]
        public async Task<AppAndServiceDTO> GetAppDetailsWithServicesPublisherAsync([FromRoute] Guid publisherAppSettingID) {
            return await _publisherAppDS.GetAppDetailsWithServicesPublisherAsync(publisherAppSettingID);
        }

        /// <summary>
        /// Get all the business for the app by AppId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("businessname/{appId}/{publisherTenantId}")]
        public async Task<List<string>> GetBusinessNameByAppIdAsync([FromRoute] Guid appId, [FromRoute] Guid publisherTenantId) {
            return await _publisherAppDS.GetBusinessNameByAppIdAsync(appId, publisherTenantId);
        }

        /// <summary>
        /// Get all the services for the app by AppId.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("services/{appId}/{publisherTenantId}")]
        public async Task<IEnumerable<AppServiceDTO>> GetAppServicesNameByAppIdAsync([FromRoute] Guid appId, [FromRoute] Guid publisherTenantId) {
            return await _publisherAppDS.GetAppServicesNameByAppIdAsync(appId, publisherTenantId);
        }

        /// <summary>
        /// Get all the apps services and subscription related to it.
        /// </summary>
        /// <param name="sourcesubdomain"></param>
        /// <param name="tenantType"></param>
        /// <param name="includeInactiveApp">Include inactive application.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("servicesandsubscription/{sourcesubdomain}/{tenantType}/{includeInactiveApp}")]
        public async Task<List<BusinessApplicationDTO>> GetAppServicesAndSubscriptionDetailsAsync([FromRoute] string sourcesubdomain, [FromRoute] int tenantType, [FromRoute]bool includeInactiveApp, CancellationToken token = default(CancellationToken)) {
            return await _publisherAppDS.GetPublisherAppServicesAndSubscriptionDetailsAsync(sourcesubdomain, includeInactiveApp, token);
        }

        /// <summary>
        /// Get all the apps services and subscription related to it.
        /// </summary>
        /// <param name="sourcesubdomain"></param>
        /// <param name="tenantType"></param>
        /// <param name="includeInactiveApp">Include inactive application.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("servicesandsubscription/{sourcesubdomain}/{tenantType}/{includeInactiveApp}/{publishertenantId}")]
        public async Task<List<BusinessApplicationDTO>> GetAppServicesAndSubscriptionDetailsOnPlatformAsync([FromRoute] string sourcesubdomain, [FromRoute] int tenantType, [FromRoute]bool includeInactiveApp,[FromRoute]Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            return await _publisherAppDS.GetPublisherAppServicesAndSubscriptionDetailsOnPlatformAsync(sourcesubdomain, includeInactiveApp, publisherTenantId,token);
        }

        

        /// <summary>
        /// Gets the application list that matches input parameters.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active] only active applications will fetch.</param>
        /// <param name="subscriptionMode">The subscription mode to filter applications.</param>
        /// <returns>Returns application list that matches given input parameters.</returns>
        [HttpGet]
        [Route("list/{active}/{subscriptionMode}")]
        public async Task<List<AppInfoDTO>> GetApplicationListAsync([FromRoute] bool active, [FromRoute] int subscriptionMode) {
            return await _publisherAppDS.GetApplicationListAsync(active, subscriptionMode);
        }

        /// <summary>
        /// Gets the business application name list by publisher identifier.
        /// </summary>
        /// <param name="publisherId">The publisher identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns business application name list that matches the provided publisher id.</returns>
        [HttpGet]
        [Route("names/{publisherId}")]
        public async Task<IEnumerable<string>> GetAppNameListByPublisheIdAsync([FromRoute] Guid publisherId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _publisherAppDS.GetAppNameListByPublisheIdAsync(publisherId, cancellationToken);
        }

        #endregion Get

        #region Update

        [HttpPut]
        [Route("update")]
        public async Task<ResponseModelDTO> UpdateAppAsync([FromBody] AppAndServiceDTO appAndServiceDTO) {
            await _publisherAppDS.UpdateAppAsync(appAndServiceDTO);
            return new ResponseModelDTO() {
                Id = appAndServiceDTO.AppDetailDTO.ID,
                IsSuccess = true,
                Message = "App updated sucessfully"
            };
        }

        #endregion Update

    }
}
