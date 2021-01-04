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
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// Business class contains all add/update/delete/get methods for Business.    
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController:ControllerBase {

        #region Local variables

        IBusinessDS _businessDS;
        IBusinessExtDS _businessExtDS;
        IBusinessSignUpDS _businessSignUpDS;
        IQBusinessAndUserDS _qBusinessAndUserDS;
        IBusinessUpdateDS _businessUpdateDS;
        IUserSessionManager _userSessionManager;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize constructor for Business Tenant.
        /// </summary>
        /// <param name="businessDS">Business data service.</param>
        /// <param name="businessUpdateDS">Business update wrapper class.</param>
        /// <param name="businessExtDS"></param>
        /// <param name="businessUserDS"></param>
        /// <param name="qBusinessAndUserDS"></param>
        public BusinessController(IBusinessDS businessDS, IBusinessUpdateDS businessUpdateDS, IBusinessExtDS businessExtDS, IQBusinessAndUserDS qBusinessAndUserDS, IBusinessSignUpDS businessUserDS, IUserSessionManager userSessionManager) {
            _businessDS = businessDS;
            _businessUpdateDS = businessUpdateDS;
            _businessExtDS = businessExtDS;
            _qBusinessAndUserDS = qBusinessAndUserDS;
            _businessSignUpDS = businessUserDS;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get filter business list by login tenant.
        /// </summary>
        /// <returns>return login tenant business list.</returns>
        [Route("list")]
        [HttpPut]
        public async Task<List<BusinessViewModelDQ>> GetBusinessListAsync([FromBody]ListDateFilterDTO filterDto, CancellationToken token = default(CancellationToken)) {
            return await _qBusinessAndUserDS.GetBusinessListAsync(filterDto, token);
        }

        /// <summary>
        /// Get business detail model.
        /// </summary>
        /// <param name="id">Unique businessTenant id.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<UpdateBusinessTenantModelDQ> GetBusinessDetailAsync(Guid id, CancellationToken token = default(CancellationToken)) {
            return await _businessExtDS.GetBusinessUpdateModelAsync(id, token);
        }

        /// <summary>
        /// Get vendor list for publisher.
        /// </summary>
        /// <returns></returns>
        [Route("applications/{id}/{isdeleted}")]
        [HttpGet]
        public async Task<List<PubBusinessAppSubscriptionInfoDTO>> GetBusinessApplications(Guid id, bool isdeleted) {
            Guid pubTenantId = _userSessionManager.GetSession().TenantId;
            return await _qBusinessAndUserDS.GetBusinessAppSubscriptionInfoDTOAsync(id, pubTenantId, isdeleted);
        }

        #endregion Get

        #region POST

        /// <summary>
        /// Method is used to add business with all child entities.
        /// </summary>
        /// <param name="dto">Business registration model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("signupbusiness")]
        [HttpPost]
        public async Task<ResponseModelDTO> BusinessSignUpAsync(BusinessSignUpRequestDTO dto, CancellationToken token = default(CancellationToken)) {
            return await _businessSignUpDS.BusinessSignUpAsync(dto, token);
        }

        /// <summary>
        /// Get vendor list for publisher.
        /// </summary>
        /// <returns></returns>
        [Route("update")]
        [HttpPost]
        //ToDo: nitin- Input model class name.
        public async Task<BusinessResponseModelDTO> UpdateBusinessAsync([FromBody]UpdateBusinessTenantModelDQ dto, CancellationToken token = default(CancellationToken)) {
            return await _businessUpdateDS.UpdateBusinessAsync(dto, token);
        }

        #endregion POST

        #region ERP Mgmt

        /// <summary>
        /// Test app connection with SAPB1
        /// </summary>
        /// <returns></returns>
        [HttpPost("testconnection")]
        public async Task<IActionResult> TestConnectionAsync([FromBody] object request, CancellationToken token = default(CancellationToken)) {
            bool result = await _businessExtDS.TestConnectionAsync(request);
            return Ok(result);
        }

        /// <summary>
        ///  Get sync time-log data from SAPB1 connector.
        /// </summary>
        /// <returns></returns>
        [HttpGet("synctimelog/{tenantid}")]
        public async Task<List<BusBASyncTimeLogDTO>> SyncTimeLogAsync([FromRoute] Guid tenantid, CancellationToken token = default(CancellationToken)) {
            return await _businessExtDS.SyncTimeLogAsync(tenantid);
        }

        /// <summary>
        /// Test app connection with SAPB1
        /// </summary>
        /// <returns></returns>
        //ToDo: nitin- url is not correct. Parameter is in-correct it will not work never.
        //ToDo: nitin- Pls review return type.
        [HttpPost("manageconnection/{tenantid}")]
        public async Task<bool> ManageConnectorConfigsAsync([FromBody] List<ConnectorConfigDTO> connectorConfigDTO, [FromRoute] Guid tenantId, CancellationToken token) {
            await _businessExtDS.UpdateBusinessConnectorConfigsAsync(tenantId, connectorConfigDTO, token);
            return true;
        }


        ///// <summary>
        ///// Sync entity data from SAPB1 connector.
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("synctimelogs/{tenantid}")]
        //public async Task<IEnumerable<BASyncTimeLogDTO>> InitDataAsync([FromRoute] Guid tenantid, CancellationToken token = default(CancellationToken)) {
        //  Stopwatch s = new Stopwatch();
        //  s.Start();
        //  IEnumerable<BASyncTimeLogDTO> logs = null;//  await _syncService.InitDataAsync(tenantid);
        //  Log.Debug(string.Format("BE-Sync-InitDB: Total time taken: {0} in ms", s.ElapsedMilliseconds));
        //  s.Stop();

        //  return null;
        //}
        
        /// <summary>
        /// Sync entity data from SAPB1 connector.
        /// </summary>
        /// <returns></returns>
        [HttpPut("pullerpdata")]
        public async Task<bool> PullERPDataAsync([FromBody] PullERPDataReqDTO PullERPDataAsync, CancellationToken token = default(CancellationToken)) {

            return await _businessExtDS.PullERPDataAsync(PullERPDataAsync);
        }

        // <summary>
        /// Sync entity data from SAPB1 connector.
        /// </summary>
        /// <returns></returns>
        [HttpPost("pushsalesorderdata/{tenantid}")]
        public async Task<bool> PushSalesOrderDataInERPAsync([FromRoute] Guid tenantid, [FromBody] BusBASalesOrderDTO request, CancellationToken token = default(CancellationToken)) {

            return await _businessExtDS.PushSalesOrderDataInERPAsync(tenantid, request, token);
        }

        /// <summary>
        /// Sync entity data from SAPB1 connector.
        /// </summary>
        /// <returns></returns>
        [HttpPut("erpattachmentdata")]
        public async Task<AttachmentResDTO> GetAttachmentFromERP([FromBody] AttachmentReqDTO request, CancellationToken token = default(CancellationToken)) {

            return await _businessExtDS.GetAttachmentFromERP(request);
        }

        #endregion ERP Mgmt

        #region Get/Update Configuration details

        ///<summary>
        /// Get Configuration Detail
        ///</summary>
        [HttpGet]
        [Route("getconfiguration")]
        public async Task<BusConfigurationDTO> GetConfigurationDetailAsync(CancellationToken token = default(CancellationToken)) {
            return await _businessExtDS.GetBusinessConfigurationDetailAsync(token);
        }


        /// <summary>
        /// Update configuration detail
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("updateconfiguration")]
        public async Task<ResponseModelDTO> UpdateConfigurationDetailAsync([FromBody] BusConfigurationDTO configurationDQ, CancellationToken token = default(CancellationToken)) {
            await _businessExtDS.UpdateBusinessConfigurationDetailAsync(configurationDQ, token);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }



        #endregion

        #region Get Branding

        ///<summary>
        /// Get Branding Setting Detail
        /// <paramref name="appid">App Id </paramref>
        /// <paramref name="tenantid">tenant Id</paramref>
        ///</summary>
        [HttpGet]
        [Route("getbranding/{tenantid}/{appid}")]
        public async Task<BusinessBrandingDQ> GetBusinessBrandingAsync([FromRoute] Guid tenantid, [FromRoute] Guid appid) {
            return await _businessDS.GetBusinessBrandingAsync(tenantid, appid);
        }
        #endregion

        #region Get ThemeDetail
        ///<summary>
        /// Get ThemeList
        ///</summary>
        [HttpGet]
        [Route("getthemenameandthemekey")]
        public async Task<IEnumerable<ThemeResponseDTO>> GetThemeNameAndThemeKey() {
            return await _businessDS.GetThemeNameAndThemeKey();
        }
        #endregion

        #region Update Branding

        /// <summary>
        /// Update business Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="businessBrandingDQ">business branding model</param>
        [Route("updatebranding")]
        [HttpPut]
        public async Task<ResponseModelDTO> UpdateBusinessBranding(BusinessBrandingDQ businessBrandingDQ) {
            await _businessDS.UpdateBusinessBranding(businessBrandingDQ);
            return new ResponseModelDTO() {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }


        /// <summary>
        /// Delete busines.
        /// </summary>
        /// <param name="tenantid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("delete/{tenantid}")]
        [HttpPut]
        public async Task DeleteBusinessAsync([FromRoute]Guid tenantid, CancellationToken token = default(CancellationToken)) {
            await _businessExtDS.DeleteBusinessAsync(tenantid, token);
        }

        #endregion

        #region Status

        [HttpPut]
        [Route("updatestatus")]
        public async Task<ResponseModelDTO> UpdateBusinessSatatus(BusinessStatusDTO businessStatusDTO) {
            await _businessDS.UpdateBusinessStatus(businessStatusDTO);
            return new ResponseModelDTO {
                Id = businessStatusDTO.TenantId,
                IsSuccess = true,
                Message = "business status updated succesfully"
            };
        }

        #endregion Status

        #region Update  business From ERP

        /// <summary>
        /// Update  business From ERP
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="businessSyncDTO">business sync model</param>
        [Route("syncerp")]
        [HttpPut]
        public async Task<bool> SyncERPBusiness([FromBody]BusinessSyncDTO businessSyncDTO) {
            await _businessDS.SyncERPBusiness(businessSyncDTO);
            return true;
        }

    #endregion

    #region  Reinvite Business Primary User
    [HttpPut]
    [Route("reinviteprimaryuser/{tenantuserid:Guid}/{biztenantid:Guid}/{sdomain}")]
    public async Task<ResponseModelDTO> ReInvitePrimaryUser([FromRoute] Guid tenantUserId, [FromRoute] Guid biztenantid,  [FromRoute] string sdomain)
    {
      await _businessSignUpDS.ReInvitePrimaryBusinessUserAsync(tenantUserId, biztenantid,  sdomain);
      return new ResponseModelDTO()
      {
        Id = tenantUserId,
        IsSuccess = true,
        Message = "Tenant User reinvited sucessfully"
      };
    }
    #endregion  Reinvite Business Primary User
  }
}
