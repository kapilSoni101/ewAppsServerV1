using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.Core.BaseService;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers {

    /// <summary>
    /// Publisher class contains all add/update/delete/get methods for publisher.
    /// </summary>
    [Route("api/[Controller]")]
    [ApiController]
    public class PublisherController:ControllerBase {

        #region Local member
        IPublisherDS _publisherDS;
        IPublisherSignUpDS _publisherSignUpDS;
        IQPublisherAndUserDS _qPublisherDS;
        IPublisherExtDS _publisherExtDS;
        IPublisherUpdateDS _publisherUpdateDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Publisher entity Add/Update/Delete
        /// </summary>
        public PublisherController(IPublisherUpdateDS publisherUpdateDS, IPublisherSignUpDS signupPublisherAndUserDS, IPublisherDS publisherDS, IQPublisherAndUserDS qPublisherDS, IPublisherExtDS publisherExtDS) {
            _publisherDS = publisherDS;
            _publisherSignUpDS = signupPublisherAndUserDS;
            _qPublisherDS = qPublisherDS;
            _publisherExtDS = publisherExtDS;
            _publisherUpdateDS = publisherUpdateDS;
        }

        #endregion

        #region GET branding
        ///<summary>
        /// Get Branding Setting Detail
        /// <paramref name="appid">App Id </paramref>
        /// <paramref name="tenantid">tenant Id</paramref>
        ///</summary>
        [HttpGet]
        [Route("getbranding/{tenantid}/{appid}")]
        public async Task<PublisherBrandingDQ> GetPublisherBrandingAsync([FromRoute] Guid tenantid, [FromRoute] Guid appid) {
            return await _publisherDS.GetPublisherBrandingAsync(tenantid, appid);
        }

        #endregion

        #region Get ThemeDetail
        ///<summary>
        /// Get ThemeList
        ///</summary>
        [HttpGet]
        [Route("getthemenameandthemekey")]
        public async Task<IEnumerable<ThemeResponseDTO>> GetThemeNameAndThemeKey() {
            return await _publisherDS.GetThemeNameAndThemeKey();
        }
        #endregion

        #region UPDATE Branding

        /// <summary>
        /// Update publisher Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="publisherBrandingDQ">platform branding model</param>
        [Route("updatebranding")]
        [HttpPut]
        public async Task<ResponseModelDTO> UpdatePublisherBranding(PublisherBrandingDQ publisherBrandingDQ) {
            await _publisherDS.UpdatePublisherBranding(publisherBrandingDQ);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        #endregion

        #region Sign Up Method

        /// <summary>
        /// Signs up publisher tenant and send invites.
        /// </summary>
        /// <param name="publisherSignUpRequestDTO">The publisher and primary user information to be signup.</param>
        /// <returns>Returns signed-up publisher tenant and user information.</returns>
        [HttpPost]
        [Route("signup")]
        public async Task<TenantSignUpResponseDTO> PublisherSignUpAsync(PublisherSignUpRequestDTO publisherSignUpRequestDTO) {
            return await _publisherSignUpDS.PublisherSignUpAsync(publisherSignUpRequestDTO);
        }

        [HttpPut]
        [Route("reinvite")]
        public async Task<ResponseModelDTO> ReInviteUser([FromBody] TenantUserIdentificationDTO tenantUserIdentificationDTO) {
            await _publisherSignUpDS.ReInviteTenantUserAsync( tenantUserIdentificationDTO);
            return new ResponseModelDTO() {
                Id = tenantUserIdentificationDTO.TenantUserId,
                IsSuccess = true,
                Message = "Publisher admin User reinvited sucessfully."
            };
        }

        #endregion

        #region Update Method

        [HttpPut]
        [Route("update")]
        public async Task UpdatePublisherAsync(PublisherUpdateReqDTO publisherUpdateReqDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            await _publisherUpdateDS.UpdatePublisherAsync(publisherUpdateReqDTO, cancellationToken);
        }

        #endregion

        #region Subscription Method

        /// <summary>
        /// Gets the subscription plan list by application identifier and plan state.
        /// </summary>
        /// <param name="appId">The application identifier to get application specific subscription.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        [HttpGet]
        [Route("subscription/list/{appId}/{planState}")]
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid appId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _publisherSignUpDS.GetSubscriptionPlanListByAppIdAsync(appId, planState, cancellationToken);
        }

        // Note: This api is use to get serivce and attribute list by selected plan id on Add/Update publisher.
        /// <summary>
        /// Gets the plan service and attribute list of provided plan id.
        /// </summary>
        /// <param name="planId">The plan identifier to get corresponding service and attributes.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns Service and Attributes list corresponding to provided plan id.</returns>
        [HttpGet]
        [Route("subscription/{planId}/services")]
        public async Task<List<SubsPlanServiceInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid planId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _publisherSignUpDS.GetServiceAndAttributeDetailByPlanId(planId, cancellationToken);
        }

        #endregion

        #region Service And Attribute Methods

        /// <summary>
        /// Gets the applications with service and attribute that matches the given publisher tenant id..
        /// </summary>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="cancellationToken">A token to cancel async operation.</param>
        /// <returns>Returns list of application with corresponding services and attributes.</returns>
        [HttpGet]
        [Route("{publisherTenantId}/apps/services/attributes")]
        public async Task<List<AppDetailInfoDTO>> GetAppsWithServiceAndAttributeAsync([FromRoute]Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherDS.GetAppsWithServiceAndAttributeAsync(publisherTenantId, cancellationToken);
        }

        #endregion

        #region Get/Update Configuration details

        ///<summary>
        /// Get Configuration Detail
        ///</summary>
        [HttpGet]
        [Route("getconfiguration")]
        public async Task<ConfigurationDTO> GetConfigurationDetailAsync() {
            return await _publisherDS.GetConfigurationDetailAsync();
        }


        /// <summary>
        /// Update configuration detail
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("updateconfiguration")]
        public async Task<ResponseModelDTO> UpdateConfigurationDetailAsync([FromBody] ConfigurationDTO configurationDQ) {
            await _publisherDS.UpdateConfigurationDetailAsync(configurationDQ);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }

        /// <summary>
        /// Gets the subscription plan list by application identifier and plan state.
        /// </summary>
        /// <param name="appId">The application identifier to get application specific subscription.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="pubTenantId">The publisher's tenant id to filter publisher specific subscription.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        [HttpGet]
        [Route("subscription/list/{appId}/{pubTenantId}/{planState}")]
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppAndPubTenantIdAsync(Guid appId, Guid pubTenantId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherDS.GetSubscriptionPlanListByAppAndPubTenantIdAsync(appId, pubTenantId, planState, cancellationToken);
        }

        #endregion

        #region Publisher List Methods

        /// <summary>
        /// Gets all publishers.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of publishers detail.</returns>
        [HttpGet]
        [Route("list")]
        public async Task<List<PublisherDetailsDTO>> GetAllPublishersAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherDS.GetAllPublishersAsync(cancellationToken);
        }

        #endregion

        #region Publisher View Method

        /// <summary>
        /// Gets the publisher detail by publisher identifier.
        /// </summary>
        /// <param name="publisherId">The publisher identifier to find publisher detail.</param>
        /// <param name="cancellationToken">The cancellation token to cnacel async operation.</param>
        /// <returns>Returns publisher details that matches provided publisher id.</returns>
        [HttpGet]
        [Route("{publisherId}")]
        public async Task<PublisherViewDTO> GetPublisherViewDetail([FromRoute]Guid publisherId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _publisherExtDS.GetPublisherDetailByPublisherIdAsync(publisherId, cancellationToken);
        }

        #endregion

        #region GET Connector

        /// <summary>
        /// get publisher Connector List
        /// </summary>
        [HttpGet]
        [Route("getpulisherconnectorlist")]
        public async Task<List<ConnectorDQ>> GetPublisherConnectorListAsync() {
            return await _publisherDS.GetPublisherConnectorListAsync();
        }

        #endregion


        #region GET Sync time-log details

        /// <summary>
        ///  Get sync time-log data from SAPB1 connector.
        /// </summary>
        /// <returns></returns>
        [HttpGet("synctimelog/{tenantid}")]
        public async Task<List<BusBASyncTimeLogDTO>> SyncTimeLogAsync([FromRoute] Guid tenantid, CancellationToken token = default(CancellationToken)) {
            return await _publisherDS.SyncTimeLogAsync(tenantid);
        }

    #endregion

    #region  Reinvite Publisher Primary User
    [HttpPut]
    [Route("reinviteprimaryuser/{tenantuserid:Guid}/{pubTenantId:Guid}/{sdomain}")]
    public async Task<ResponseModelDTO> ReInvitePrimaryUser([FromRoute] Guid tenantUserId, [FromRoute] Guid pubTenantId,  [FromRoute] string sdomain)
    {
      await _publisherSignUpDS.ReInvitePrimaryPublisherUserAsync(tenantUserId, pubTenantId,  sdomain);
      return new ResponseModelDTO()
      {
        Id = tenantUserId,
        IsSuccess = true,
        Message = "Tenant User reinvited sucessfully"
      };
    }
    #endregion  Reinvite Publisher Primary User

  }
}
